using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.DataAccess;
using System.Linq;

namespace Xamarin.Forms_EFCore.Helpers
{
    public class Measurement
    {
        public void loadValues()
        {

            var assembly = typeof(Measurement).GetTypeInfo().Assembly;

            /*Definovanie cesty suboru*/
            Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.initializationValues.txt");
            DatabaseContext databaseContext = new DatabaseContext();
            List<Json> objects = new List<Json>();

            /*nahranie dat a sparsovanie*/
            int i = 0;
            int index = 1;
            if (!databaseContext.Pulse.Any())
            {
                index = 1;
            }
            else
            {
                Tep tmp = databaseContext.Pulse.FirstOrDefault(t => t.TepId == databaseContext.Pulse.Max(x => x.TepId));
                index = tmp.TepId;
            }

            index++;
            
            using (StreamReader sr = new StreamReader(stream))
            {
                while (sr.Peek() >= 0)
                {
                    Json obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Json>(sr.ReadLine());
                    objects.Add(obj);
                    //System.Diagnostics.Debug.WriteLine(i++ +" --- " + obj.body.heart_rate.value + " -- " + Convert.ToInt32(obj.body.heart_rate.value));

                    String create_date = obj.header.creation_date_time;
                    float value = obj.body.heart_rate.value;

                    Tep tep = new Tep
                    {
                        TepId = index++,
                        TimeStamp = create_date,
                        Hodnota = value


                    };
                    databaseContext.Pulse.Add(tep);

                }

            }
            try
            {
                databaseContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

            var all = databaseContext.Pulse.ToList();
            foreach (var a in all)
            {
                System.Diagnostics.Debug.WriteLine("******************************* " + a.TepId + " " + a.TimeStamp + " " + a.Hodnota);


            }

        }

        public List<int> getValuesForList()
        {
            var assembly = typeof(Measurement).GetTypeInfo().Assembly;

            /*Definovanie cesty suboru*/
            Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.initializationValues.txt");
            DatabaseContext databaseContext = new DatabaseContext();
            List<Json> objects = new List<Json>();
            List<int> occurs = new List<int>();
            using (StreamReader sr = new StreamReader(stream))
            {
                while (sr.Peek() >= 0)
                {
                    Json obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Json>(sr.ReadLine());
                    objects.Add(obj);
                    //System.Diagnostics.Debug.WriteLine(i++ +" --- " + obj.body.heart_rate.value + " -- " + Convert.ToInt32(obj.body.heart_rate.value));
                    
                    occurs.Add(Convert.ToInt32(obj.body.heart_rate.value));

                }

            }

            return occurs;
        }


        private IEnumerable<IGrouping<int, int>> createOccurTable()
        {
            DatabaseContext context = new DatabaseContext();
            List<int> occurs = new List<int>();


            var all = context.Pulse.ToList();
            foreach (var a in all)
            {
                
                occurs.Add(Convert.ToInt32(a.Hodnota));

            }
            occurs.Sort();

            IEnumerable<IGrouping<int, int>> occurrDict = occurs.GroupBy(k => k);

            return occurrDict;
        }

        public void createLimits()
        {
           
            List<IGrouping<int, int>> occurrDicta = createOccurTable().ToList();

            int slabe_min = 0, slabe_max, stredne, vysoke;
            int maxOccKey = 0, maxOccValue = 0;

            /*Najdi hodnotu s najcastejsim vyskytom*/
            foreach (var grp in occurrDicta)
            {
                if (grp.Count() > maxOccValue)
                {
                    maxOccValue = grp.Count();
                    maxOccKey = grp.Key;
                }

            }

            /*Skontroluj ci na zaciatku nie je outlier
             ak ano = zmaz ho
             Po odstraneni uloz najnizsiu ako hodnotu pre slabe_min
             */
            string pom;
            for (int i = 0; i < occurrDicta.Count; i++)
            {
                if ((occurrDicta.ElementAt(i).Count() == 1) && ((occurrDicta.ElementAt(i).Key * 1.1) < occurrDicta.ElementAt(i + 1).Key))
                {
                    // pom = "true";
                    // Console.WriteLine("seruuuusssssssssssss {0} {1} {2}", occurrDicta.ElementAt(i).Key, occurrDicta.ElementAt(i).Count(), pom);

                    occurrDicta.RemoveAt(i);
                    i--;

                }
                else
                {
                    //pom = "false";
                    //Console.WriteLine("seruuuusssssssssssss {0} {1} {2}", occurrDicta.ElementAt(i).Key, occurrDicta.ElementAt(i).Count(), pom);
                    slabe_min = occurrDicta.ElementAt(i).Key;
                    break;
                }
            }

            /*
             K najcastejsej pripocitaj rozdiel so slabe_min
             */

            slabe_max = maxOccKey + (maxOccKey - slabe_min);

            foreach (var grp in occurrDicta)
            {
                if ((grp.Key > slabe_max) && (grp.Count() > 1))
                {
                    slabe_max = grp.Key;
                }

            }

            /*
             Vytvor ostatne hranice
            */
            stredne = Convert.ToInt32(slabe_max * 1.1);
            vysoke = Convert.ToInt32(slabe_max * 1.2);
            Console.WriteLine("hranice {0} {1} {2} {3} ", slabe_min, slabe_max, stredne, vysoke);


            /*Vytvor zaznam pre hranice tepu v databaze*/
            DatabaseContext context = context = new DatabaseContext();

            int index = 1;
            if (!context.PulseLimit.Any())
            {
                index = 1;
            }
            else
            {
                Hranice_Tep tmp = context.PulseLimit.FirstOrDefault(t => t.Hranica_TepId == context.PulseLimit.Max(x => x.Hranica_TepId));
                index = tmp.Hranica_TepId;
            }

            Hranice_Tep ht = new Hranice_Tep
            {
                Hranica_TepId = index,
                Hranica_Slabe_Min = slabe_min,
                Hranica_Slabe_Max = slabe_max,
                Hranica_Stredne = stredne,
                Hranica_Vysoke = vysoke,
                TimeStamp = DateTime.Now.ToString("h:mm:ss tt")
            };

            context.PulseLimit.Add(ht);

            /*Vytvor zaznam pre hranice teploty v databaze*/
            index = 1;
            if (!context.TemperatureLimit.Any())
            {
                index = 1;
            }
            else
            {
                Hranice_Teplota tmp = context.TemperatureLimit.FirstOrDefault(t => t.Hranice_TeplotaId == context.TemperatureLimit.Max(x => x.Hranice_TeplotaId));
                index = tmp.Hranice_TeplotaId;
            }

            Hranice_Teplota htemp = new Hranice_Teplota
            {
                Hranice_TeplotaId = index,
                TimeStamp = DateTime.Now.ToString("h:mm:ss tt"),
                Hranica_Slabe_Min = 35.5F,
                Hranica_Slabe_Max = 37.0F,
                Hranica_Stredne = 38.2F,
                Hranica_Vysoke = 39.0F

            };
            context.TemperatureLimit.Add(htemp);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

            /*Kontrolny vypis*/
            var all = context.TemperatureLimit.ToList();
            foreach (var a in all)
            {
                System.Diagnostics.Debug.WriteLine("teplota hranice " + a.Hranice_TeplotaId + " " + a.TimeStamp + " " + a.Hranica_Slabe_Min + " " + a.Hranica_Slabe_Max + " " + a.Hranica_Stredne + " " + a.Hranica_Vysoke);
            }

            var all1 = context.PulseLimit.ToList();
            foreach (var a in all1)
            {
                System.Diagnostics.Debug.WriteLine("tep hranice " + a.Hranica_TepId + " " + a.TimeStamp + " " + a.Hranica_Slabe_Min + " " + a.Hranica_Slabe_Max + " " + a.Hranica_Stredne + " " + a.Hranica_Vysoke);
            }

        }

    }
}

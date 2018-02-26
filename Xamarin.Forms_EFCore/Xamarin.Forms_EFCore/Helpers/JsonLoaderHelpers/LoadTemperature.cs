using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers
{
    public class LoadTemperature
    {

        public void LoadTemperatureJson(DatabaseContext context)
        {
            var assembly = typeof(LoadTemperature).GetTypeInfo().Assembly;

            /*Definovanie cesty suboru*/
            try
            {
                Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.temperatureWeek.txt");
                List<Json> objects = new List<Json>();
                /*nahranie dat a sparsovanie*/
                int i = 0;
                int index = 1;
                if (!context.Temperature.Any())
                {
                    index = 1;
                }
                else
                {
                    Teplota tmp = context.Temperature.FirstOrDefault(t => t.TeplotaId == context.Temperature.Max(x => x.TeplotaId));
                    index = tmp.TeplotaId;
                }
                using (StreamReader sr = new StreamReader(stream))
                {

                    while (sr.Peek() >= 0)
                    {
                        Json obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Json>(sr.ReadLine());
                        //objects.Add(obj);
                       // System.Diagnostics.Debug.WriteLine(i++ + " pppppp+ " + obj.header.creation_date_time.ToString() + " + " + obj.body.body_temperature.value);
                        

                        Teplota teplota = new Teplota
                        {   
                            TeplotaId = index++,
                            TimeStamp = obj.header.creation_date_time.ToString(),
                            Hodnota = obj.body.body_temperature.value


                        };
                        context.Temperature.Add(teplota);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }


            /*Vypis tabulky z DB*/

            /*   var all = _context.Temperature.ToList();
               foreach (var a in all)
               {
                   System.Diagnostics.Debug.WriteLine("" + a.TeplotaId + " " + a.TimeStamp + " " + a.Hodnota);


               }*/


        }
    }
}

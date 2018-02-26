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
    public class LoadPulse
    {
        public void LoadPulseJson(DatabaseContext context)
        {
            var assembly = typeof(LoadPulse).GetTypeInfo().Assembly;

            /*Definovanie cesty suboru*/
            Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.pulseWeek.txt");
            List<Json> objects = new List<Json>();

            /*nahranie dat a sparsovanie*/
             int i = 0;

            int index = 1;
            if (!context.Pulse.Any())
            {
                index = 1;
            }
            else
            {
                Tep tmp = context.Pulse.FirstOrDefault(t => t.TepId == context.Pulse.Max(x => x.TepId));
                index = tmp.TepId;
            }


            using (StreamReader sr = new StreamReader(stream))
             {
                 while (sr.Peek() >= 0)
                 {
                     Json obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Json>(sr.ReadLine());
                     objects.Add(obj);
                     //System.Diagnostics.Debug.WriteLine(i++ +" + " + obj.header.creation_date_time.ToString() + " + " + obj.body.heart_rate.value);

                     Tep tep = new Tep
                     {  
                         TepId = index++,
                         TimeStamp = obj.header.creation_date_time.ToString(),
                         Hodnota = obj.body.heart_rate.value


                     };
                     context.Pulse.Add(tep);
                                        
                 }

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

            /*  var all = context.Pulse.ToList();
              foreach (var a in all)
              {
                  System.Diagnostics.Debug.WriteLine(a.TepId + " " + a.TimeStamp + " " + a.Hodnota);


              }
              */
        }

}
}

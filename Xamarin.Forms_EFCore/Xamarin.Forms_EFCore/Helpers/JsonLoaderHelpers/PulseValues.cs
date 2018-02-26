using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers
{
    public class PulseValues
    {
        public IEnumerable<IGrouping<int, int>> CreatePulseFile()
        {
            var assembly = typeof(LoadPulse).GetTypeInfo().Assembly;

            /*Definovanie cesty suboru*/
            Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.initializationValues.txt");
            List<Json> objects = new List<Json>();

            /*nahranie dat a sparsovanie*/
            int i = 0;

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
            occurs.Sort();

            IEnumerable<IGrouping<int, int>> occurrDict = occurs.GroupBy(k => k);
            

            /*foreach (var grp in occurrDict)
            {
                Console.WriteLine("{0} {1}", grp.Key, grp.Count());
            }*/
            
            return occurrDict;
        }

    }
}

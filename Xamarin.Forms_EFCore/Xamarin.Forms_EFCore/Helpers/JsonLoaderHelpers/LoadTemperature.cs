using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;
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
                //Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.temperatureWeek.txt");
                Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.temperature100.txt");
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

                index++;
                using (StreamReader sr = new StreamReader(stream))
                {

                    while (sr.Peek() >= 0)
                    {
                        Json obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Json>(sr.ReadLine());
                        //objects.Add(obj);
                        //System.Diagnostics.Debug.WriteLine(i++ + " pppppp+ " + obj.header.creation_date_time.ToString() + " + " + obj.body.body_temperature.value);
                        

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

        public async void createFileFromFile()
        {
            try { 
            var assembly = typeof(LoadTemperature).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.temperature100.txt");
            IFileSystem fileSystem = FileSystem.Current;
            IFolder rootFolder = fileSystem.LocalStorage;
            IFile tempFile = await rootFolder.CreateFileAsync("temperatureData.txt", CreationCollisionOption.ReplaceExisting);

            String text;
            using (StreamReader sr = new StreamReader(stream))
            {
                text = sr.ReadToEnd();
            
            }
            tempFile.WriteAllTextAsync(text);

            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Zapis teplotneho suboru do zariadenia: " + e.ToString());

            }
        }


        public async void readFileByLines()
        {
            try {
                DatabaseContext context = new DatabaseContext();
            IFileSystem fileSystem = FileSystem.Current;
            IFolder rootFolder = fileSystem.LocalStorage;

            IFile tempFile = await rootFolder.GetFileAsync("temperatureData.txt");

            string newFileText; string line;
            string fileText = await tempFile.ReadAllTextAsync();
            using (System.IO.StringReader reader = new System.IO.StringReader(fileText))
            {
                line = reader.ReadLine();
                System.Diagnostics.Debug.WriteLine("temperature "+line);
                newFileText = reader.ReadToEnd();
            }

            tempFile.WriteAllTextAsync(newFileText);

            Json obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Json>(line);

            int index = 1;
            if (!context.Temperature.Any())
            {
                index = 1;
            }
            else
            {
                Teplota tmp = context.Temperature.FirstOrDefault(t => t.TeplotaId == context.Temperature.Max(x => x.TeplotaId));
                index = tmp.TeplotaId;
                index++;

                }

            
            Teplota teplota = new Teplota
            {
                TeplotaId = index++,
                //TimeStamp = obj.header.creation_date_time.ToString(),
                TimeStamp = DateTime.Now.ToString(),
                Hodnota = obj.body.body_temperature.value


            };
            context.Temperature.Add(teplota);

            try
            {
                context.SaveChanges();
                teplota = null;
            }
            catch (Exception e)
            {
                throw e;
            }
                /*SequenceCreator sequenceCreator = new SequenceCreator();
                sequenceCreator.TemperatureSequencer(context);*/
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("citanie teplotneho suboru zo zariadenia: " + e.ToString());

            }
        }
    }
}

using PCLStorage;
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
            //Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.pulseWeek.txt");

            Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.pulse100.txt");

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
                index++;
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


        public async void createFileFromFile()
        {
            try { 
                var assembly = typeof(LoadTemperature).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.pulse100.txt");
                IFileSystem fileSystem = FileSystem.Current;
                IFolder rootFolder = fileSystem.LocalStorage;
                IFile pulseFile = await rootFolder.CreateFileAsync("pulseData.txt", CreationCollisionOption.ReplaceExisting);

                String text;
                using (StreamReader sr = new StreamReader(stream))
                {
                    text = sr.ReadToEnd();

                }
                pulseFile.WriteAllTextAsync(text);
            }catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Zapis pulz suboru do zariadenia: " + e.ToString());

                }
}


        public async void readFileByLines()
        {
            try
            {
                DatabaseContext context = new DatabaseContext();

                IFileSystem fileSystem = FileSystem.Current;
                IFolder rootFolder = fileSystem.LocalStorage;

                IFile tempFile = await rootFolder.GetFileAsync("pulseData.txt");

                string newFileText; string line;
                string fileText = await tempFile.ReadAllTextAsync();
                using (System.IO.StringReader reader = new System.IO.StringReader(fileText))
                {
                    line = reader.ReadLine();
                    System.Diagnostics.Debug.WriteLine("pulse " + line);
                    newFileText = reader.ReadToEnd();
                }

                tempFile.WriteAllTextAsync(newFileText);

                Json obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Json>(line);

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
                index++;

                Tep tep = new Tep
                {
                    TepId = index,
                    //TimeStamp = obj.header.creation_date_time.ToString(),
                    TimeStamp = DateTime.Now.ToString(),
                    Hodnota = obj.body.heart_rate.value


                };
                context.Pulse.Add(tep);

                context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Citanie pulz suboru zo zariadenia: " + nameof(LoadPulse) + e.ToString());

            }


        }
        //-------------------------

        public async void createDatasetFile()
        {
            try
            {
                var assembly = typeof(LoadTemperature).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.pulseDataset.txt");
                IFileSystem fileSystem = FileSystem.Current;
                IFolder rootFolder = fileSystem.LocalStorage;
                IFile pulseFile = await rootFolder.CreateFileAsync("pulseDataset.txt", CreationCollisionOption.ReplaceExisting);

                String text;
                using (StreamReader sr = new StreamReader(stream))
                {
                    text = sr.ReadToEnd();

                }
                pulseFile.WriteAllTextAsync(text);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Zapis pulz suboru do zariadenia: " + e.ToString());

            }
        }


        public async void readLineFromDatasetFile()
        {
            try
            {
                DatabaseContext context = new DatabaseContext();

                IFileSystem fileSystem = FileSystem.Current;
                IFolder rootFolder = fileSystem.LocalStorage;

                IFile tempFile = await rootFolder.GetFileAsync("pulseDataset.txt");

                string newFileText; string line;
                string fileText = await tempFile.ReadAllTextAsync();
                using (System.IO.StringReader reader = new System.IO.StringReader(fileText))
                {
                    line = reader.ReadLine();
                    //System.Diagnostics.Debug.WriteLine("READ from file pulse " + line);
                    newFileText = reader.ReadToEnd();
                }

                tempFile.WriteAllTextAsync(newFileText);

                DatasetJson obj = Newtonsoft.Json.JsonConvert.DeserializeObject<DatasetJson>(line);

                int index = 1;
                if (!context.Pulse.Any())
                {
                    index = 1;
                }
                else
                {
                    Tep tmp = context.Pulse.FirstOrDefault(t => t.TepId == context.Pulse.Max(x => x.TepId));
                    index = tmp.TepId;
                    index++;
                }
                

                Tep tep = new Tep
                {
                    TepId = index,
                    //TimeStamp = obj.header.creation_date_time.ToString(),
                    TimeStamp = obj.timestamp.ToString(),
                    Hodnota = obj.value


                };
                context.Pulse.Add(tep);
                //System.Diagnostics.Debug.WriteLine("****** VLOZENIE TEPU DO DB: " + tep.TepId + " " + tep.Hodnota + " " + tep.TimeStamp);


                context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Citanie pulz suboru zo zariadenia: " + nameof(LoadPulse) + e.ToString());

            }


        }

        public void LoadPulseDataset(DatabaseContext context)
        {
            var assembly = typeof(LoadPulse).GetTypeInfo().Assembly;

            /*Definovanie cesty suboru*/
            //Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.pulseWeek.txt");

            Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.pulseDataset.txt");

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
                index++;
            }


            using (StreamReader sr = new StreamReader(stream))
            {
                while (sr.Peek() >= 0)
                {
                    DatasetJson obj = Newtonsoft.Json.JsonConvert.DeserializeObject<DatasetJson>(sr.ReadLine());
                    //objects.Add(obj);
                    //System.Diagnostics.Debug.WriteLine(i++ +" + " + obj.header.creation_date_time.ToString() + " + " + obj.body.heart_rate.value);

                    Tep tep = new Tep
                    {
                        TepId = index++,
                        TimeStamp = obj.timestamp.ToString(),
                        Hodnota = obj.value


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

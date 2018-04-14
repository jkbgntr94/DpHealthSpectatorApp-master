using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Models.HelpModels;

namespace Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers
{
    public class LoadMovement
    {
        public void LoadMovementJson(DatabaseContext context)
        {
            var assembly = typeof(LoadMovement).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.movementSample.txt");

            List<Xamarin.Forms_EFCore.Models.HelpModels.Json> objects = new List<Xamarin.Forms_EFCore.Models.HelpModels.Json>();

            int index = 1;

            if (!context.Movement.Any())
            {

                index = 1;
            }
            else
            {
                Pohyb poh = context.Movement.FirstOrDefault(p => p.PohybId == context.Movement.Max(t => t.PohybId));
                index = poh.PohybId;
                index++;
            }
            int i = 0;
            using (StreamReader sr = new StreamReader(stream))
            {
                while(sr.Peek() >= 0)
                {
                    Models.HelpModels.Json obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.HelpModels.Json>(sr.ReadLine());
                    objects.Add(obj);
                    System.Diagnostics.Debug.WriteLine("************** + " + obj.x + " " + obj.y);
                    
                    Pohyb pohyb = new Pohyb
                    {
                        PohybId = index++,
                        Xhodnota = obj.x,
                        Yhodnota = obj.y,
                        TimeStamp = DateTime.Now.ToShortTimeString()
                        //TimeStamp = DateTime.Now.AddMinutes(i++).ToShortTimeString()

                    };
                    context.Movement.Add(pohyb);


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

              var all = context.Movement.ToList();
              foreach (var a in all)
              {
                  System.Diagnostics.Debug.WriteLine(a.Xhodnota + " " + a.TimeStamp + " " + a.Yhodnota);


              }
              

        }

        public void GenerateMovementData(DatabaseContext context)
        {
            int index = 1;

            if (!context.Movement.Any())
            {

                index = 1;
            }
            else
            {
                insertNearPoints(context);
                Pohyb poh = context.Movement.FirstOrDefault(p => p.PohybId == context.Movement.Max(t => t.PohybId));
                index = poh.PohybId;
                index++;
            }

            

            for (int i = 0; i <= 100; i++)
            {
                Random rnd = new Random();

               
                Pohyb pohyb = new Pohyb
                {
                    PohybId = index++,
                    Xhodnota = rnd.Next(1, 150),
                    Yhodnota = rnd.Next(1, 150),
                    TimeStamp = DateTime.Now.AddMinutes(i).ToShortTimeString()

                };
                context.Movement.Add(pohyb);

            }

            Pohyb pohyba = new Pohyb
            {
                PohybId = index++,
                Xhodnota = 160,
                Yhodnota = 15,
                TimeStamp = DateTime.Now.AddMinutes(101).ToShortTimeString()

            };
            context.Movement.Add(pohyba);


            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

            insertNearPoints(context);

            /*Vypis tabulky z DB*/

            var all = context.Movement.ToList();
            foreach (var a in all)
            {
                System.Diagnostics.Debug.WriteLine(a.Xhodnota + " " + a.TimeStamp + " " + a.Yhodnota);


            }


        }

        public void insertNearPoints(DatabaseContext context)
        {
            int index = 1;

            if (!context.Movement.Any())
            {

                index = 1;
            }
            else
            {
                Pohyb poh = context.Movement.FirstOrDefault(p => p.PohybId == context.Movement.Max(t => t.PohybId));
                index = poh.PohybId;
                index++;
            }

            Pohyb pohyb = new Pohyb
            {
                PohybId = index++,
                Xhodnota = 10,
                Yhodnota = 10,
                TimeStamp = DateTime.Now.AddMinutes(1).ToShortTimeString()

            };
            context.Movement.Add(pohyb);

            pohyb = new Pohyb
            {
                PohybId = index++,
                Xhodnota = 20,
                Yhodnota = 20,
                TimeStamp = DateTime.Now.AddMinutes(2).ToShortTimeString()

            };
            context.Movement.Add(pohyb);

            pohyb = new Pohyb
            {
                PohybId = index++,
                Xhodnota = 10,
                Yhodnota = 20,
                TimeStamp = DateTime.Now.AddMinutes(2).ToShortTimeString()

            };
            context.Movement.Add(pohyb);

            pohyb = new Pohyb
            {
                PohybId = index++,
                Xhodnota = 0,
                Yhodnota = 30,
                TimeStamp = DateTime.Now.AddMinutes(2).ToShortTimeString()

            };
            context.Movement.Add(pohyb);

            pohyb = new Pohyb
            {
                PohybId = index++,
                Xhodnota = 15,
                Yhodnota = 25,
                TimeStamp = DateTime.Now.AddMinutes(2).ToShortTimeString()

            };
            context.Movement.Add(pohyb);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GenerateMovementOneSample()
        {
            DatabaseContext context = new DatabaseContext();
            int index = 1;

            if (!context.Movement.Any())
            {

                index = 1;
            }
            else
            {
                Pohyb poh = context.Movement.FirstOrDefault(p => p.PohybId == context.Movement.Max(t => t.PohybId));
                index = poh.PohybId;
                index++;
            }



              Random rnd = new Random();


                Pohyb pohyb = new Pohyb
                {
                    PohybId = index++,
                    Xhodnota = rnd.Next(0, 160),
                    Yhodnota = rnd.Next(0, 160),
                    TimeStamp = DateTime.Now.ToShortTimeString()

                };
                context.Movement.Add(pohyb);



            System.Diagnostics.Debug.WriteLine("pohyb " + pohyb.PohybId);


            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
        //--------------------------

        public async void createDatasetFile()
        {
            try
            {
                var assembly = typeof(LoadMovement).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.movementDataset.txt");
                IFileSystem fileSystem = FileSystem.Current;
                IFolder rootFolder = fileSystem.LocalStorage;
                IFile movementFile = await rootFolder.CreateFileAsync("movementDataset.txt", CreationCollisionOption.ReplaceExisting);

                String text;
                using (StreamReader sr = new StreamReader(stream))
                {
                    text = sr.ReadToEnd();

                }
                movementFile.WriteAllTextAsync(text);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Zapis movement suboru do zariadenia: " + e.ToString());

            }
        }

        public async void readLineFromDatasetFile()
        {
            try
            {
                DatabaseContext context = new DatabaseContext();

                IFileSystem fileSystem = FileSystem.Current;
                IFolder rootFolder = fileSystem.LocalStorage;

                IFile movFile = await rootFolder.GetFileAsync("movementDataset.txt");

                string newFileText; string line;
                string fileText = await movFile.ReadAllTextAsync();
                using (System.IO.StringReader reader = new System.IO.StringReader(fileText))
                {
                    line = reader.ReadLine();
                    //System.Diagnostics.Debug.WriteLine("READ from file pulse " + line);
                    newFileText = reader.ReadToEnd();
                }

                movFile.WriteAllTextAsync(newFileText);

                MovementJson obj = Newtonsoft.Json.JsonConvert.DeserializeObject<MovementJson>(line);

                int index = 1;
                if (!context.Movement.Any())
                {
                    index = 1;
                }
                else
                {
                    Pohyb tmp = context.Movement.FirstOrDefault(t => t.PohybId == context.Movement.Max(x => x.PohybId));
                    index = tmp.PohybId;
                    index++;
                }

                Pohyb POH = new Pohyb
                {
                    PohybId = index,
                    TimeStamp = obj.timestamp.ToString(),
                    Xhodnota = obj.x*10,
                    Yhodnota = obj.y*10
                };


             
                context.Movement.Add(POH);
                System.Diagnostics.Debug.WriteLine("****** VLOZENIE MOVEMENT DO DB: " + POH.PohybId + " " + POH.Xhodnota + " "+ POH.Yhodnota + " " + POH.TimeStamp);


                context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Citanie movement suboru zo zariadenia: " + nameof(LoadPulse) + e.ToString());

            }


        }


    }
}

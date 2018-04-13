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

    }
}

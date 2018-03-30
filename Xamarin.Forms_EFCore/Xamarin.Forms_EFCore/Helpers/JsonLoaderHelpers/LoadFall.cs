using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers
{
    public class LoadFall
    {
        public void GenerateFallData(DatabaseContext context)
        {
            int index = 1;

            if (!context.Akcelerometers.Any())
            {

                index = 1;
            }
            else
            {
                
                Akcelerometer pad = context.Akcelerometers.FirstOrDefault(p => p.AkcelerometerID == context.Akcelerometers.Max(t => t.AkcelerometerID));
                index = pad.AkcelerometerID;
                index++;
            }



            for (int i = 0; i <= 100; i++)
            {
                Random rnd = new Random();


                Akcelerometer pad = new Akcelerometer
                {
                    AkcelerometerID = index++,
                    Xhodnota = rnd.Next(1, 150),
                    Yhodnota = rnd.Next(1, 150),
                    Zhodnota = rnd.Next(1, 150),
                    TimeStamp = DateTime.Now.AddMinutes(i).ToShortTimeString()
                    

                };
                context.Akcelerometers.Add(pad);

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

            var all = context.Akcelerometers.ToList();
            foreach (var a in all)
            {
                System.Diagnostics.Debug.WriteLine(a.Xhodnota + " " + a.TimeStamp + " " + a.Yhodnota);


            }


        }



        public void GenerateFallOneSample()
        {

            DatabaseContext context = new DatabaseContext();

            int index = 1;

            if (!context.Akcelerometers.Any())
            {

                index = 1;
            }
            else
            {

                Akcelerometer pada = context.Akcelerometers.FirstOrDefault(p => p.AkcelerometerID == context.Akcelerometers.Max(t => t.AkcelerometerID));
                index = pada.AkcelerometerID;
                index++;
            }


                Random rnd = new Random();


                Akcelerometer pad = new Akcelerometer
                {
                    AkcelerometerID = index++,
                    Xhodnota = rnd.Next(1, 150),
                    Yhodnota = rnd.Next(1, 150),
                    Zhodnota = rnd.Next(1, 150),
                    TimeStamp = DateTime.Now.ToShortTimeString()
                    
                };
                context.Akcelerometers.Add(pad);

            System.Diagnostics.Debug.WriteLine("pad " + pad.Xhodnota + " " + pad.TimeStamp + " " + pad.Yhodnota);

            Izby izba = new RoomsDetection().findRoomByCoord(pad.Xhodnota, pad.Yhodnota);
            string izbameno = "NA";
            try
            {
                izbameno = izba.Nazov;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }

            new NotificationGenerator().GenerateNotificationFall(izbameno,pad.TimeStamp, pad.AkcelerometerID);


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

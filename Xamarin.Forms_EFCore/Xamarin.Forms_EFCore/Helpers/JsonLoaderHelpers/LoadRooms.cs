using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers
{
    public class LoadRooms
    {
        public void LoadRoomsData(DatabaseContext context)
        {
            int index = 1;

            if (!context.Rooms.Any())
            {

                index = 1;
            }
            else
            {
                Izby poh = context.Rooms.FirstOrDefault(p => p.IzbaID == context.Rooms.Max(t => t.IzbaID));
                
                index = poh.IzbaID;
                index++;
            }


            Izby iz = new Izby
            {
                LavaXhodnota = 0,
                LavaYhodnota=0,
                PravaXhodnota = 2,
                PravaYhodnota = 4,
                IzbaID = index++,
                Nazov = "Chodba"

            };

            context.Rooms.Add(iz);
            iz = null;

            iz = new Izby
            {
                LavaXhodnota = 2,
                LavaYhodnota = 0,
                PravaXhodnota = 15,
                PravaYhodnota = 4,
                IzbaID = index++,
                Nazov = "Kuchyna"

            };

            context.Rooms.Add(iz);
            iz = null;

            iz = new Izby
            {
                LavaXhodnota = 0,
                LavaYhodnota = 4,
                PravaXhodnota = 7,
                PravaYhodnota = 15,
                IzbaID = index++,
                Nazov = "Obyvacka"

            };

            context.Rooms.Add(iz);
            iz = null;

            iz = new Izby
            {
                LavaXhodnota = 4,
                LavaYhodnota = 4,
                PravaXhodnota = 15,
                PravaYhodnota = 15,
                IzbaID = index++,
                Nazov = "Spalna"

            };

            context.Rooms.Add(iz);
            iz = null;


            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

            /*Vypis tabulky z DB*/

            var all = context.Rooms.ToList();
            foreach (var a in all)
            {
                System.Diagnostics.Debug.WriteLine(a.LavaXhodnota + " " + a.LavaYhodnota + " " + a.PravaXhodnota + " " +  a.PravaYhodnota + " " + a.Nazov);


            }

        }

    }
}

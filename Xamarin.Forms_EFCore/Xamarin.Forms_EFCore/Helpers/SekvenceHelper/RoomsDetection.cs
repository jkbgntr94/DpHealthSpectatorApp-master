using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers.SekvenceHelper
{
    class RoomsDetection
    {

        public Izby findRoom(Pohyb pohyb)
        {
            float x = pohyb.Xhodnota;
            float y = pohyb.Yhodnota;

            DatabaseContext context = new DatabaseContext();

            var izby = context.Rooms.ToList();

            foreach(var izba in izby)
            {
                if ((x >= izba.LavaXhodnota*10) && (x <= izba.PravaXhodnota * 10) && (y >= izba.LavaYhodnota * 10) && (y <= izba.PravaYhodnota * 10)) {

                    return izba;

                }
                
            }

            return null;
        }
    }
}

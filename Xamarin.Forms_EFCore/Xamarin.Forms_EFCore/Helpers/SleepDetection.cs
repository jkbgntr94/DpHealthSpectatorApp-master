using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers
{
    public class SleepDetection
    {

        public Boolean DetectSleep(DatabaseContext context, String roomName, String start, String close)
        {
            if (roomName != "Spálňa" || roomName != "Spalna") return false;

            string sleepTime = SettingsController.SleepTime;
            if (sleepTime == string.Empty) return false;

            if(DateTime.Parse(DateTime.Parse(start).ToShortTimeString()) <= DateTime.Parse(DateTime.Parse(sleepTime).ToShortTimeString()))
            {
                return false;
            }
            
            List<Tep_Sekvencia> tep = new List<Tep_Sekvencia>();
            try
            {//ako zobrat z timestampu iba cas
               tep = context.PulseSekv.Where(t =>  DateTime.Parse(t.TimeStart)>= DateTime.Parse(start) && DateTime.Parse(t.TimeStart) < DateTime.Parse(close) || DateTime.Parse(t.TimeStart) <= DateTime.Parse(start) && DateTime.Parse(t.TimeClose) > DateTime.Parse(start) || DateTime.Parse(t.TimeClose) < DateTime.Parse(close) && DateTime.Parse(t.TimeClose) > DateTime.Parse(close)).ToList();
                foreach(var a in tep)
                {
                    if(a.Upozornenie > 0)
                    {
                        return false;
                    }

                }


            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("sleep detection" + e.ToString());

            }

            return true;
        }

    }
}

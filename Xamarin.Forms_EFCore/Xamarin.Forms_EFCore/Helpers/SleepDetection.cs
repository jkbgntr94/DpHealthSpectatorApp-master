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

        public Boolean DetectSleep(DatabaseContext context, String roomName, DateTime start, DateTime close)
        {
          
            if (!roomName.Equals("Spálňa") && !roomName.Equals("Spalna")) return false;
            /*  SettingsController.SleepTime = new TimeSpan(0, 20, 0, 0).ToString();
              string wakeTime = new TimeSpan(0, 7, 0, 0).ToString();*/
              //TODO: SWAP BACK
            SettingsController.SleepTime = new TimeSpan(0, 8, 0, 0).ToString();
            string wakeTime = new TimeSpan(0, 20, 0, 0).ToString();
            string sleepTime = SettingsController.SleepTime;
            if (sleepTime == string.Empty) return false;

            if(start.TimeOfDay <= TimeSpan.Parse(sleepTime) && start.TimeOfDay > TimeSpan.Parse(wakeTime))
            {
                return false;
            }
            
            List<Tep_Sekvencia> tep = new List<Tep_Sekvencia>();
            try
            {
               tep = context.PulseSekv.Where(p => DateTime.Parse(p.TimeStart) >= start && DateTime.Parse(p.TimeStart) <= close || DateTime.Parse(p.TimeStart) <= start && DateTime.Parse(p.TimeClose) > start || DateTime.Parse(p.TimeStart) < close && DateTime.Parse(p.TimeClose) >= close).ToList();
                foreach (var a in tep)
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

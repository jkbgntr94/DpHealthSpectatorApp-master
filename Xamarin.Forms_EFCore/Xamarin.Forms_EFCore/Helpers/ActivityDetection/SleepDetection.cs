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

        public Boolean DetectSleep(DatabaseContext context, String roomName, DateTime start, DateTime close, int sekvCount, List<Tep_Sekvencia> tep)
        {
          
            if (!roomName.Equals("Spálňa") && !roomName.Equals("Spalna")) return false;
            if (sekvCount > 3) return false;
              SettingsController.SleepTime = new TimeSpan(0, 20, 0, 0).ToString();
              string wakeTime = new TimeSpan(0, 7, 0, 0).ToString();
            
            /*SettingsController.SleepTime = new TimeSpan(0, 8, 0, 0).ToString();
            string wakeTime = new TimeSpan(0, 20, 0, 0).ToString();*/
            string sleepTime = SettingsController.SleepTime;
            if (sleepTime == string.Empty) return false;

            if(start.TimeOfDay <= TimeSpan.Parse(sleepTime) && start.TimeOfDay > TimeSpan.Parse(wakeTime))
            {
                return false;
            }
            
            
           
            //tep = context.PulseSekv.Where(p => DateTime.Parse(p.TimeStart) >= start && DateTime.Parse(p.TimeStart) <= close || DateTime.Parse(p.TimeStart) <= start && DateTime.Parse(p.TimeClose) > start || DateTime.Parse(p.TimeStart) < close && DateTime.Parse(p.TimeClose) >= close).ToList();
            if (tep.Count == 0) return false;
            double sumPulse = 0;
            foreach (var a in tep)
            {
                sumPulse += a.Sekvencia;

                if (a.Upozornenie > 0)
                {
                    return false;
                }
            }

            try
            {
                int id = tep.Last().TepSekvId - 1;
                Tep_Sekvencia before = context.PulseSekv.Where(a => a.TepSekvId == id).First();
                sumPulse = sumPulse / tep.Count;
                if (before.Sekvencia <= sumPulse) return false;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("sleeep detection " + e.ToString());

            }

            return true;
        }

        public Boolean DetectSleepSeq(DatabaseContext context, String roomName, DateTime start, DateTime close, int sekvCount)
        {

            if (!roomName.Equals("Spálňa") && !roomName.Equals("Spalna")) return false;
            if (sekvCount > 3) return false;
            SettingsController.SleepTime = new TimeSpan(0, 20, 0, 0).ToString();
            string wakeTime = new TimeSpan(0, 7, 0, 0).ToString();

            /*SettingsController.SleepTime = new TimeSpan(0, 8, 0, 0).ToString();
            string wakeTime = new TimeSpan(0, 20, 0, 0).ToString();*/
            string sleepTime = SettingsController.SleepTime;
            if (sleepTime == string.Empty) return false;

            if (start.TimeOfDay <= TimeSpan.Parse(sleepTime) && start.TimeOfDay > TimeSpan.Parse(wakeTime))
            {
                return false;
            }

            List<Tep_Sekvencia> tep = new List<Tep_Sekvencia>();
            try
            {
                tep = context.PulseSekv.Where(p => DateTime.Parse(p.TimeStart) >= start && DateTime.Parse(p.TimeStart) <= close || DateTime.Parse(p.TimeStart) <= start && DateTime.Parse(p.TimeClose) > start || DateTime.Parse(p.TimeStart) < close && DateTime.Parse(p.TimeClose) >= close).ToList();
                if (tep.Count == 0) return false;
                foreach (var a in tep)
                {
                    if (a.Upozornenie > 0)
                    {
                        return false;
                    }

                }


            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("sleep detection" + e.ToString());

            }

            return true;
        }

    }
}

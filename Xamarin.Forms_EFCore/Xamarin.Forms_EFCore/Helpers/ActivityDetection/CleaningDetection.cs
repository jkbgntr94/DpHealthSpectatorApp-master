using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers.ActivityDetection
{
    public class CleaningDetection
    {
        public Boolean DetectCleaning(DatabaseContext context, DateTime start, DateTime close, int sekvCount, List<Tep_Sekvencia> tep, List<Teplota_Sekvencia> teplota)
        {
            if (sekvCount < 5) return false;

           
           
                //tep = context.PulseSekv.Where(p => DateTime.Parse(p.TimeStart) >= start && DateTime.Parse(p.TimeStart) <= close || DateTime.Parse(p.TimeStart) <= start && DateTime.Parse(p.TimeClose) > start || DateTime.Parse(p.TimeStart) < close && DateTime.Parse(p.TimeClose) >= close).ToList();
                double sumPulse = 0;
                foreach (var a in tep)
                {
                    sumPulse += a.Sekvencia;
                    if (a.Upozornenie > 1)
                    {
                        return false;
                    }

                }
                int id = tep.First().TepSekvId - 1;

            try
            {
                Tep_Sekvencia before = context.PulseSekv.Where(a => a.TepSekvId == id).First();
                sumPulse = sumPulse / tep.Count;
                if (before.Sekvencia > sumPulse) return false;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("cleaning detection pulse" + e.ToString());
                return false;

            }

           
            
                //teplota = context.TemperatureSekv.Where(p => DateTime.Parse(p.TimeStart) >= start && DateTime.Parse(p.TimeStart) <= close || DateTime.Parse(p.TimeStart) <= start && DateTime.Parse(p.TimeClose) > start || DateTime.Parse(p.TimeStart) < close && DateTime.Parse(p.TimeClose) >= close).ToList();
                double sumTemp = 0;
                foreach (var a in teplota)
                {
                    sumTemp += a.Sekvencia;

                }

                id = teplota.Last().TeplSekvId - 1;

            try
            {
                Teplota_Sekvencia before = context.TemperatureSekv.Where(a => a.TeplSekvId == id).First();
                sumTemp = sumTemp / teplota.Count;
                if (before.Sekvencia > sumTemp) return false;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("cleaning detection temperature" + e.ToString());

            }

            return true;
        }
    }
}

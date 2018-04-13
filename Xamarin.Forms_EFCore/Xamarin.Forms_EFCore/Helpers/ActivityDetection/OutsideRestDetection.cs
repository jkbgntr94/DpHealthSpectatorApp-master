using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers.ActivityDetection
{
    class OutsideRestDetection
    {

        public Boolean DetectOutsideRest(DatabaseContext context, String roomName, DateTime start, DateTime close, int sekvCount)
        {
            if (!roomName.Equals("Vonku")) return false;
            if (sekvCount > 2) return false;

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
                //int id = tep.First().TepSekvId + 1;
                // Tep_Sekvencia before = context.PulseSekv.Where(a => a.TepSekvId == id).First();


            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("outside rest detection" + e.ToString());

            }

            List<Teplota_Sekvencia> teplota = new List<Teplota_Sekvencia>();
            try
            {
                teplota = context.TemperatureSekv.Where(p => DateTime.Parse(p.TimeStart) >= start && DateTime.Parse(p.TimeStart) <= close || DateTime.Parse(p.TimeStart) <= start && DateTime.Parse(p.TimeClose) > start || DateTime.Parse(p.TimeStart) < close && DateTime.Parse(p.TimeClose) >= close).ToList();
                if (teplota.Count == 0) return false;

                double sumTemp = 0;
                foreach (var a in teplota)
                {
                    sumTemp += a.Sekvencia;

                }

                int id = teplota.First().TeplSekvId - 1;
                Teplota_Sekvencia before = context.TemperatureSekv.Where(a => a.TeplSekvId == id).First();
                sumTemp = sumTemp / teplota.Count;
                if (before.Sekvencia <= sumTemp) return false;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("outside rest detection" + e.ToString());

            }

            return true;
        }
    }
}

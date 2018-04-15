﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers
{
    public class CookDetection
    {
        public Boolean DetectCooking(DatabaseContext context, String roomName, DateTime start, DateTime close, int sekvCount, List<Tep_Sekvencia> tep, List<Teplota_Sekvencia> teplota)
        {
            if (!roomName.Equals("Kuchyňa") && !roomName.Equals("Kuchyna")) return false;
            if (sekvCount < 2) return false;

            //List<Tep_Sekvencia> tep = new List<Tep_Sekvencia>();
            try
            {
                
                //tep = context.PulseSekv.Where(p => DateTime.Parse(p.TimeStart) >= start && DateTime.Parse(p.TimeStart) <= close || DateTime.Parse(p.TimeStart) <= start && DateTime.Parse(p.TimeClose) > start || DateTime.Parse(p.TimeStart) < close && DateTime.Parse(p.TimeClose) >= close).ToList();
                if (tep.Count == 0) return false;

                foreach (var a in tep)
                {
                    if (a.Upozornenie > 1)
                    {
                        return false;
                    }

                }
                //int id = tep.First().TepSekvId + 1;
               // Tep_Sekvencia before = context.PulseSekv.Where(a => a.TepSekvId == id).First();


            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("cook detection" + e.ToString());
                return false;

            }

            
          
                //teplota = context.TemperatureSekv.Where(p => DateTime.Parse(p.TimeStart) >= start && DateTime.Parse(p.TimeStart) <= close || DateTime.Parse(p.TimeStart) <= start && DateTime.Parse(p.TimeClose) > start || DateTime.Parse(p.TimeStart) < close && DateTime.Parse(p.TimeClose) >= close).ToList();
                if (teplota.Count == 0) return false;

                double sumTemp = 0;
                foreach (var a in teplota)
                {
                    sumTemp += a.Sekvencia;

                }
            int id = 0;
            if (teplota.Last().TimeClose.Equals(""))
            {
                if(teplota[teplota.Count - 1].TeplSekvId - teplota[teplota.Count - 2].TeplSekvId == 1)
                {
                    id = teplota.Last().TeplSekvId - 1;
                }
                else
                {
                    id = teplota[teplota.Count - 2].TeplSekvId - 1;

                }

            }
            else
            {
                 id = teplota.Last().TeplSekvId - 1;
            }


            try
            {
                Teplota_Sekvencia before = context.TemperatureSekv.Where(a => a.TeplSekvId == id).First();
                sumTemp = sumTemp / teplota.Count;
                if (before.Sekvencia > sumTemp) return false;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("cook detection" + e.ToString());
            }

            return true;
        }
    }
}

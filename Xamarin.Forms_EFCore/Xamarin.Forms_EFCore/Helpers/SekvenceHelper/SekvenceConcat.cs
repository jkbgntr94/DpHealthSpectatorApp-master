using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers.SekvenceHelper
{
    class SekvenceConcat
    {

        private int LENGTH_BORDER = 2;

        public void concatPulseSequences(int sekvId)
        {
            DatabaseContext context = new DatabaseContext();

            Tep_Sekvencia prevPulse = new Tep_Sekvencia();
            int prevSekvid = sekvId - 1;
            try
            {
                prevPulse = context.PulseSekv.Where(p => p.TepSekvId == prevSekvid).First();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(SekvenceConcat) + " " + e.ToString());

            }

            bool isLong = true;
            try
            {
                DateTime startTime = DateTime.Parse(prevPulse.TimeStart);
                DateTime endTime = DateTime.Parse(prevPulse.TimeClose);
                TimeSpan finalTime = endTime - startTime;
                if(finalTime.Minutes <= LENGTH_BORDER)
                {
                    isLong = false;
                }
                else
                {
                    return;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(SequenceCreator) + " " + e.ToString());

            }
            if (isLong == true) return;


            Tep_Sekvencia actPulse = new Tep_Sekvencia();
            try
            {
                actPulse = context.PulseSekv.Where(p => p.TepSekvId == sekvId).First();
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(SekvenceConcat) + " " + e.ToString());

            }

            int hodnota = (prevPulse.Sekvencia + actPulse.Sekvencia) / 2;

            Tep_Sekvencia newPulse = new Tep_Sekvencia
            {
                TepSekvId = prevSekvid,
                Sekvencia = hodnota,
                TimeStart = prevPulse.TimeStart,
                TimeClose = actPulse.TimeClose,
                Hranica_TepFK = actPulse.Hranica_TepFK,
                Upozornenie = findMax(actPulse.Upozornenie, prevPulse.Upozornenie)
                
            };

          
            context.PulseSekv.Remove(prevPulse);
            context.PulseSekv.Remove(actPulse);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(SekvenceConcat) + " " + e.ToString());
            }

            context.PulseSekv.Add(newPulse);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(SequenceCreator) + " " + e.ToString());
            }

        }


        public void concatTemperatureSequences(int sekvId)
        {
            DatabaseContext context = new DatabaseContext();

            Teplota_Sekvencia prevTemp = new Teplota_Sekvencia();
            int prevSekvid = sekvId - 1;
            try
            {
                prevTemp = context.TemperatureSekv.Where(p => p.TeplSekvId == prevSekvid).First();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(SekvenceConcat) + " " + e.ToString());

            }

            bool isLong = true;
            try
            {
                DateTime startTime = DateTime.Parse(prevTemp.TimeStart);
                DateTime endTime = DateTime.Parse(prevTemp.TimeClose);
                TimeSpan finalTime = endTime - startTime;
                if (finalTime.Minutes <= LENGTH_BORDER)
                {
                    isLong = false;
                }
                else
                {
                    return;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(SequenceCreator) + " " + e.ToString());

            }
            if (isLong == true) return;


            Teplota_Sekvencia actTemp = new Teplota_Sekvencia();
            try
            {
                actTemp = context.TemperatureSekv.Where(p => p.TeplSekvId == sekvId).First();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(SekvenceConcat) + " " + e.ToString());

            }

            float hodnota = (prevTemp.Sekvencia + actTemp.Sekvencia) / 2;

            Teplota_Sekvencia newTemp = new Teplota_Sekvencia
            {
                TeplSekvId = prevSekvid,
                Sekvencia = hodnota,
                TimeStart = prevTemp.TimeStart,
                TimeClose = actTemp.TimeClose,
                Hranice_TeplotaFk = actTemp.Hranice_TeplotaFk,
                Upozornenie = findMax(actTemp.Upozornenie, prevTemp.Upozornenie)

            };


            context.TemperatureSekv.Remove(prevTemp);
            context.TemperatureSekv.Remove(actTemp);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(SekvenceConcat) + " " + e.ToString());
            }

            context.TemperatureSekv.Add(newTemp);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(SequenceCreator) + " " + e.ToString());
            }

        }



        private int findMax(int a,int b)
        {
            if (a > b) return a;
            else return b;
        }

    }

   
}

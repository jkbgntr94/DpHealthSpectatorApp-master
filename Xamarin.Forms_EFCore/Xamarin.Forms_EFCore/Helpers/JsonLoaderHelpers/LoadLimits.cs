using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers
{
    public class LoadLimits
    {
        public void LoadPulseLimits(DatabaseContext context)
        {
            HelpMethods helpm = new HelpMethods();
                      
                        Hranice_Tep tep_limit = new Hranice_Tep
                        {
                            Hranica_Slabe_Min = 50,
                            Hranica_Slabe_Max = 90,
                            Hranica_Stredne = 100,
                            Hranica_Vysoke = 120,
                            TimeStamp = DateTime.Now.ToShortTimeString()

                        };
            context.PulseLimit.Add(tep_limit);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void LoadTemperatureLimits(DatabaseContext context)
        {
            HelpMethods helpm = new HelpMethods();

            Hranice_Teplota hrantep = new Hranice_Teplota
            {
                Hranica_Slabe_Min = 35.5f,
                Hranica_Slabe_Max = 37.0f,
                Hranica_Stredne = 38.2f,
                Hranica_Vysoke = 39.0f,
                TimeStamp = DateTime.Now.ToShortTimeString()

            };
            context.TemperatureLimit.Add(hrantep);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void LoadMovementLimits(DatabaseContext context)
        {
            HelpMethods helpm = new HelpMethods();

            Hranice_Pohyb hranpoh = new Hranice_Pohyb
            {
                Xhranica = 150,
                Yhranica = 150,
                OkruhHranica = 30,
                LimitCas = "120",
                TimeStamp = DateTime.Now.ToShortTimeString()

            };
            context.MovementLimit.Add(hranpoh);
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

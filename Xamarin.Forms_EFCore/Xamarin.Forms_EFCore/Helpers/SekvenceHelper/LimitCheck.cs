using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.Helpers.SekvenceHelper
{
    public class LimitCheck
    {

        public int CheckPulseLimits(DatabaseContext context, int hodnota) {

            Hranice_Tep hranice = context.PulseLimit.FirstOrDefault(t => t.Hranica_TepId == context.PulseLimit.Max(x => x.Hranica_TepId));

            if((hodnota >= hranice.Hranica_Slabe_Min) && (hodnota <= hranice.Hranica_Slabe_Max))
            {
                return 0;//ok
            }else if(hodnota < hranice.Hranica_Slabe_Min)
            {
                return -1;//low
            }else if((hodnota > hranice.Hranica_Slabe_Max) && (hodnota <= hranice.Hranica_Stredne))
            {
                return 1;//slabe


            }else if((hodnota > hranice.Hranica_Stredne) && (hodnota <= hranice.Hranica_Vysoke))
            {
                return 2;//stredne
            }else if(hodnota > hranice.Hranica_Vysoke)
            {
                return 3;//vysoke
            }else
            {
                return 404;//error
            }


            
        }

        public int CheckTemperatureLimits(DatabaseContext context, float hodnota)
        {
            Hranice_Teplota hranice = context.TemperatureLimit.FirstOrDefault(t => t.Hranice_TeplotaId == context.TemperatureLimit.Max(x => x.Hranice_TeplotaId));
            
            if ((hodnota >= hranice.Hranica_Slabe_Min) && (hodnota <= hranice.Hranica_Slabe_Max))
            {
                return 0;
            }
            else if (hodnota < hranice.Hranica_Slabe_Min)
            {
                return -1;
            }
            else if ((hodnota > hranice.Hranica_Slabe_Max) && (hodnota <= hranice.Hranica_Stredne))
            {
                return 1;


            }
            else if ((hodnota > hranice.Hranica_Stredne) && (hodnota <= hranice.Hranica_Vysoke))
            {
                return 2;
            }
            else if (hodnota > hranice.Hranica_Vysoke)
            {
                return 3;
            }
            else
            {
                return 404;
            }
        }

        public string getStringValuePulseAndTempLimit(int value)
        {
            String alert;

            if (value == 0) alert = "OK";
            else if (value == -1) alert = "Slabé Nízke";
            else if (value == 1) alert = "Slabé";
            else if (value == 2) alert = "Stredné";
            else if (value == 3) alert = "Vysoké";
            else alert = "NA";


            return alert;
        }


        public bool checkMovementValue(DatabaseContext context, Pohyb p, Pohyb_Sekvencia pohS)
        {
            Hranice_Pohyb hpoh = context.MovementLimit.FirstOrDefault(t => t.HranicePohybId == context.MovementLimit.Max(x => x.HranicePohybId));
            float hranica = hpoh.OkruhHranica / 100;

            if(((pohS.Xhodnota - hranica) <= p.Xhodnota) && ((pohS.Xhodnota + hranica) >= p.Xhodnota) && ((pohS.Yhodnota - hranica) <= p.Yhodnota) && ((pohS.Yhodnota + hranica) >= p.Yhodnota))
            {//neprekracuje limit vo vzdialenosti

                //skontroluj ci patri do rovnakej izby
                if(pohS.IzbyFK == new RoomsDetection().findRoom(p).IzbaID)
                {
                    return true;

                }
                else
                {

                    return false;
                }

                
            }
            else
            {

                return false;
            }
            
        }

        public int checkTimeLimitMovement(DatabaseContext context, Pohyb_Sekvencia pohS)
        {
            Hranice_Pohyb hpoh = context.MovementLimit.FirstOrDefault(t => t.HranicePohybId == context.MovementLimit.Max(x => x.HranicePohybId));

            int zot = 0;
            Int32.TryParse(pohS.Cas_Zotrvania, out zot);

            int hran = 0;
            Int32.TryParse(hpoh.LimitCas, out hran);

            if(zot > hran)
            {
                return 1;

            }
            else
            {
                return 0;
            }
            
        }
        public int checkIfOutside(DatabaseContext context, Pohyb pohyb)
        {

            Hranice_Pohyb hpoh = context.MovementLimit.FirstOrDefault(t => t.HranicePohybId == context.MovementLimit.Max(x => x.HranicePohybId));
            
            if ((pohyb.Xhodnota < 0) || (pohyb.Yhodnota < 0) || (pohyb.Xhodnota > hpoh.Xhranica) || (pohyb.Yhodnota > hpoh.Yhranica))
            {
                return 1;


            }
            else
            {

                return 0;
            }
            
        }
       
    }
}

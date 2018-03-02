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

       
    }
}

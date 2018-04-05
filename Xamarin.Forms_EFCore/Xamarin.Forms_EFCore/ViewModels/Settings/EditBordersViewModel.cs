using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models;

namespace Xamarin.Forms_EFCore.ViewModels.Settings
{
    public class EditBordersViewModel : BaseViewModel
    {
        private int lowDown;
        public int LowDown
        {
            get { return lowDown; }
            set
            {
                lowDown = value;
            }
        }

        private int lowUp;
        public int LowUp
        {
            get { return lowUp; }
            set
            {
                lowUp = value;
            }
        }

        private int middle;
        public int Middle
        {
            get { return middle; }
            set
            {
                middle = value;
            }
        }

        private int high;
        public int High
        {
            get { return high; }
            set
            {
                high = value;
            }
        }

        private float lowDownTemp;
        public float LowDownTemp
        {
            get { return lowDownTemp; }
            set
            {
                lowDownTemp = value;
            }
        }

        private float lowUpTemp;
        public float LowUpTemp
        {
            get { return lowUpTemp; }
            set
            {
                lowUpTemp = value;
            }
        }

        private float middleTemp;
        public float MiddleTemp
        {
            get { return middleTemp; }
            set
            {
                middleTemp = value;
            }
        }

        private float highTemp;
        public float HighTemp
        {
            get { return highTemp; }
            set
            {
                highTemp = value;
            }
        }

        private string timeLimit;
        public string TimeLimit
        {
            get { return timeLimit; }
            set
            {
                timeLimit = value;
            }
        }

        private float okruh;
        public float Okruh
        {
            get { return okruh; }
            set
            {
                okruh = value;

            }
        }

    public ICommand Save { get; private set; }

        DatabaseContext context;
        public EditBordersViewModel()
        {
            context = new DatabaseContext();
            Save = new Command(save);

            fillValues();
        }

        private void fillValues()
        {
            var lastBordersPulse = context.PulseLimit.FirstOrDefault(e => e.Hranica_TepId == context.PulseLimit.Max(o => o.Hranica_TepId));

            LowDown = lastBordersPulse.Hranica_Slabe_Min;
            LowUp = lastBordersPulse.Hranica_Slabe_Max;
            Middle = lastBordersPulse.Hranica_Stredne;
            High = lastBordersPulse.Hranica_Vysoke;

            var lastBordersTemp = context.TemperatureLimit.FirstOrDefault(a => a.Hranice_TeplotaId == context.TemperatureLimit.Max(p => p.Hranice_TeplotaId));

            LowDownTemp = lastBordersTemp.Hranica_Slabe_Min;
            LowUpTemp = lastBordersTemp.Hranica_Slabe_Max;
            MiddleTemp = lastBordersTemp.Hranica_Stredne;
            HighTemp = lastBordersTemp.Hranica_Vysoke;

            Hranice_Pohyb limit = context.MovementLimit.FirstOrDefault(t => t.HranicePohybId == context.MovementLimit.Max(x => x.HranicePohybId));

            TimeLimit = limit.LimitCas;
            Okruh = limit.OkruhHranica;

        }

        private void save()
        {
            var lastBordersPulse = context.PulseLimit.FirstOrDefault(e => e.Hranica_TepId == context.PulseLimit.Max(o => o.Hranica_TepId));

            Hranice_Tep ht = new Hranice_Tep
            {
                Hranica_TepId = lastBordersPulse.Hranica_TepId + 1,
                TimeStamp = DateTime.Now.ToString(),
                Hranica_Slabe_Min = LowDown,
                Hranica_Slabe_Max = LowUp,
                Hranica_Stredne = Middle,
                Hranica_Vysoke = High

            };

            context.PulseLimit.Add(ht);

            var lastBordersTemp = context.TemperatureLimit.FirstOrDefault(a => a.Hranice_TeplotaId == context.TemperatureLimit.Max(p => p.Hranice_TeplotaId));

            Hranice_Teplota htemp = new Hranice_Teplota
            {
                Hranice_TeplotaId = lastBordersTemp.Hranice_TeplotaId + 1,
                TimeStamp = DateTime.Now.ToString(),
                Hranica_Slabe_Min = LowDownTemp,
                Hranica_Slabe_Max = LowUpTemp,
                Hranica_Stredne = MiddleTemp,
                Hranica_Vysoke = HighTemp

            };

            context.TemperatureLimit.Add(htemp);

            Hranice_Pohyb limit = context.MovementLimit.FirstOrDefault(t => t.HranicePohybId == context.MovementLimit.Max(x => x.HranicePohybId));

            int index = limit.HranicePohybId;
            index++;

            Hranice_Pohyb hp = new Hranice_Pohyb()
            {
                HranicePohybId = index,
                LimitCas = TimeLimit.ToString(),
                OkruhHranica = Okruh,
                Xhranica = SettingsController.MaxX,
                Yhranica = SettingsController.MaxY,
                TimeStamp = DateTime.Now.ToString()

            };

            context.MovementLimit.Add(hp);



            try
            {
                context.SaveChanges();

            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + nameof(EditBordersViewModel) + " " + e.ToString());

            }

            Application.Current.MainPage.Navigation.PopAsync();


        }
    }
}

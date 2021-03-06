﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models.ObjectsForList;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    class PulseVisualViewModel : BaseViewModel
    {
        DatabaseContext _context;

        private PulseObj selectedSequence;
        public PulseObj SelectedSequence
        {
            get
            {
                return selectedSequence;
            }
            set
            {
                if (selectedSequence != value)
                {
                    selectedSequence = value;
                    HandleSelectedItem();

                }

            }
        }

        private void HandleSelectedItem()
        {
            fillPageWithSequence(SelectedSequence);

        }

        private ObservableCollection<PulseObj> sequenceList = new ObservableCollection<PulseObj>();
        public ObservableCollection<PulseObj> SequenceList
        {
            get
            {
                return sequenceList;
            }
            set
            {
                sequenceList = value;

            }
        }

        private string pulseAlert;
        public string PulseAlert
        {
            get
            {
                return pulseAlert;
            }
            set
            {
                pulseAlert = value;
                this.OnPropertyChanged("PulseAlert");
            }
        }

        private string pulseTime;
        public string PulseTime
        {
            get
            {
                return pulseTime;
            }
            set
            {
                pulseTime = value;
                this.OnPropertyChanged("PulseTime");
            }
        }

        private string pulseTimeEnd;
        public string PulseTimeEnd
        {
            get
            {
                return pulseTimeEnd;
            }
            set
            {
                pulseTimeEnd = value;
                this.OnPropertyChanged("PulseTimeEnd");
            }
        }

        private string pulseValue;
        public string PulseValue
        {
            get
            {
                return pulseValue;
            }
            set
            {
                pulseValue = value;
                this.OnPropertyChanged("PulseValue");
            }
        }

        private string pulseDuration;
        public string PulseDuration
        {
            get
            {
                return pulseDuration;
            }
            set
            {
                pulseDuration = value;
                this.OnPropertyChanged("PulseDuration");
            }
        }

        private string heightTwelve;
        public string HeightTwelve
        {
            get
            {
                return heightTwelve;
            }
            set
            {
                heightTwelve = value;

            }
        }

        private string middleTwelve;
        public string MiddleTwelve
        {
            get
            {
                return middleTwelve;
            }
            set
            {
                middleTwelve = value;

            }
        }

        private string lowTwelve;
        public string LowTwelve
        {
            get
            {
                return lowTwelve;
            }
            set
            {
                lowTwelve = value;

            }
        }

        private string okTwelve;
        public string OkTwelve
        {
            get
            {
                return okTwelve;
            }
            set
            {
                okTwelve = value;

            }
        }

        private string heightDay;
        public string HeightDay
        {
            get
            {
                return heightDay;
            }
            set
            {
                heightDay = value;

            }
        }

        private string middleDay;
        public string MiddleDay
        {
            get
            {
                return middleDay;
            }
            set
            {
                middleDay = value;

            }
        }

        private string lowDay;
        public string LowDay
        {
            get
            {
                return lowDay;
            }
            set
            {
                lowDay = value;

            }
        }

        private string okDay;
        public string OkDay
        {
            get
            {
                return okDay;
            }
            set
            {
                okDay = value;

            }
        }

        private ImageSource pulseImageSource;
        public ImageSource PulseImageSource
        {
            get
            {
                return pulseImageSource;
            }
            set
            {
                pulseImageSource = value;
                this.OnPropertyChanged("PulseImageSource");

            }
        }

        public ICommand TempVisualCommand { get; private set; }
        public ICommand PulseVisualCommand { get; private set; }
        public ICommand DashboardCommand { get; private set; }
        public ICommand MovementVisualCommand { get; private set; }
        public ICommand FallVisualCommand { get; private set; }

        public PulseVisualViewModel()
        {
            _context = new DatabaseContext();
            TempVisualCommand = new Command(tempVisualCommand);
            PulseVisualCommand = new Command(pulseVisualCommand);
            DashboardCommand = new Command(dashboardCommand);
            MovementVisualCommand = new Command(movementVisualCommand);
            FallVisualCommand = new Command(fallVisualCommand);


            fillList();


        }


        private void fillList()
        {
            int low = 0; int height = 0; int middle = 0; int ok = 0;
            int lowDay = 0; int heightDay = 0; int middleDay = 0; int okDay = 0;
            double lowMin = 0; double heightMin = 0; double middleMin = 0; double okMin = 0;
            double lowMinDay = 0; double heightMinDay = 0; double middleMinDay = 0; double okMinDay = 0;
            if (_context.PulseSekv.Any())
            {
                var listTemp = _context.PulseSekv.ToList();
                Helpers.SekvenceHelper.LimitCheck loader = new Helpers.SekvenceHelper.LimitCheck();
                DateTime actualForSummary = DateTime.Now;
                //DateTime actualForSummary = DateTime.Parse("2017-01-01T12:04:19Z");
                foreach (var t in listTemp)
                {
                    DateTime convertedDate = DateTime.Parse(t.TimeStart);
                    String durationTime = null;
                    String endTimeString = "NA"; String endDateString = "";
                    double time = 0;
                    try
                    {
                        DateTime endtime = DateTime.Parse(t.TimeClose);
                        //durationTime = (endtime - convertedDate).TotalMinutes.ToString();
                        time = (endtime - convertedDate).TotalMinutes;
                        var x = time - Math.Truncate(time);
                        durationTime = Math.Truncate(time).ToString() + " min " + Math.Round(x * 60).ToString() + " sec";
                        endDateString = endtime.ToLongDateString();
                        endTimeString = endtime.ToLongTimeString();

                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception parse date " + e.ToString());
                        durationTime = "NA";
                    }

                    if (actualForSummary <= convertedDate.AddHours(12))
                    {
                        switch (t.Upozornenie)
                        {
                            case 0:
                                ok++;
                                okMin += time;
                                break;
                            case 1:
                                low++;
                                lowMin += time;

                                break;
                            case -1:
                                low++;
                                lowMin += time;

                                break;
                            case 2:
                                middle++;
                                middleMin += time;

                                break;
                            case 3:
                                height++;
                                heightMin += time;

                                break;

                        }

                    }

                    if (actualForSummary <= convertedDate.AddHours(24))
                    {
                        switch (t.Upozornenie)
                        {
                            case 0:
                                okDay++;
                                okMinDay += time;

                                break;
                            case 1:
                                lowDay++;
                                lowMinDay += time;

                                break;
                            case -1:
                                lowDay++;
                                lowMinDay += time;

                                break;
                            case 2:
                                middleDay++;
                                middleMinDay += time;

                                break;
                            case 3:
                                heightDay++;
                                heightMinDay += time;

                                break;

                        }

                    }

                    PulseObj tem = new PulseObj()
                    {
                        PulseId = t.TepSekvId,
                        Value = "~" + t.Sekvencia + " BPM",
                        LongDate = convertedDate.ToLongDateString(),
                        Date = convertedDate.ToShortDateString(),
                        Time = convertedDate.ToLongTimeString(),
                        DateEnd = endDateString,
                        TimeEnd = endTimeString,
                        Duration = durationTime,
                        Upozornenie = t.Upozornenie,
                        Alert = loader.getStringValuePulseAndTempLimit(t.Upozornenie)
                    };

                    SequenceList.Add(tem);

                }
                fillPageWithSequence(SequenceList.Last());

            }
            else
            {
                PulseAlert = "Neexistuje žiadna sekvencia";

            }

            HeightTwelve = "Vysoké: ~" + Math.Truncate(heightMin).ToString() + " min";
            MiddleTwelve = "Stredné: ~" + Math.Truncate(middleMin).ToString() + " min";
            LowTwelve = "Slabé: ~" + Math.Truncate(lowMin).ToString() + " min";
            OkTwelve = "OK: ~" + Math.Truncate(okMin).ToString() + " min";

            HeightDay = HeightTwelve = "Vysoké: ~" + Math.Truncate(heightMinDay).ToString() + " min";
            MiddleDay = "Stredné: ~" + Math.Truncate(middleMinDay).ToString() + " min";
            LowDay = "Slabé: ~" + Math.Truncate(lowMinDay).ToString() + " min";
            OkDay = "OK: ~" + Math.Truncate(okMinDay).ToString() + " min";

            SequenceList = new ObservableCollection<PulseObj>(SequenceList.Reverse());

        }

        private void fillPageWithSequence(PulseObj to)
        {
            PulseAlert = to.Alert;
            PulseTime = to.LongDate + " " + to.Time;
            PulseTimeEnd = to.DateEnd + " " + to.TimeEnd;
            PulseValue = to.Value;
            PulseDuration = to.Duration;

            int upoz = to.Upozornenie;

            switch (upoz)
            {
                case 0:
                    PulseImageSource = ImageSource.FromResource("Xamarin.Forms_EFCore.pulseOk.png"); break;
                case 1:
                    PulseImageSource = ImageSource.FromResource("Xamarin.Forms_EFCore.pulseLow.png"); break;
                case 2:
                    PulseImageSource = ImageSource.FromResource("Xamarin.Forms_EFCore.pulseMiddle.png"); break;
                case 3:
                    PulseImageSource = ImageSource.FromResource("Xamarin.Forms_EFCore.pulseHigh.png"); break;
                default:
                    PulseImageSource = ImageSource.FromResource("Xamarin.Forms_EFCore.pulse.png");
                    break;

            }

        }



        async void tempVisualCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new TemperatureVisualPage());

        }

        async void pulseVisualCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new PulseVisualPage());

        }

        async void dashboardCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new DashboardPage());

        }

        async void movementVisualCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new MovementVisualPage());

        }

        async void fallVisualCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new FallVisualPage());

        }
    }
}

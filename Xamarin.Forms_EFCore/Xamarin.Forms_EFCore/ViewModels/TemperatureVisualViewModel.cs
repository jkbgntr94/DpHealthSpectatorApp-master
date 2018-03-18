using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Views;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Models.ObjectsForList;

namespace Xamarin.Forms_EFCore.ViewModels
{
    class TemperatureVisualViewModel : BaseViewModel
    {
        DatabaseContext _context;
        
        private TemperatureObj selectedSequence;
        public TemperatureObj SelectedSequence
        {
            get
            {
                return selectedSequence;
            }
            set
            {
                if(selectedSequence != value)
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



        private ObservableCollection<TemperatureObj> sequenceList = new ObservableCollection<TemperatureObj>();
        public ObservableCollection<TemperatureObj> SequenceList
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

        private string tempAlert;
        public string TempAlert
        {
            get
            {
                return tempAlert;
            }
            set
            {
                tempAlert = value;
                this.OnPropertyChanged("TempAlert");
            }
        }

        private string tempTime;
        public string TempTime
        {
            get
            {
                return tempTime;
            }
            set
            {
                tempTime = value;
                this.OnPropertyChanged("TempTime");
            }
        }

        private string tempValue;
        public string TempValue
        {
            get
            {
                return tempValue;
            }
            set
            {
                tempValue = value;
                this.OnPropertyChanged("TempValue");
            }
        }

        private string tempDuration;
        public string TempDuration
        {
            get
            {
                return tempDuration;
            }
            set
            {
                tempDuration = value;
                this.OnPropertyChanged("TempDuration");
            }
        }

        private string alert;
        public string Alert
        {
            get
            {
                return alert;
            }
            set
            {
                alert = value;

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

        private ImageSource temperatureImageSource;
        public ImageSource TemperatureImageSource
        {
            get
            {
                return temperatureImageSource;
            }
            set
            {
                temperatureImageSource = value;
                this.OnPropertyChanged("TemperatureImageSource");

            }
        }




        public ICommand TempVisualCommand { get; private set; }
        public ICommand PulseVisualCommand { get; private set; }
        public ICommand DashboardCommand { get; private set; }
        public ICommand MovementVisualCommand { get; private set; }
        public ICommand FallVisualCommand { get; private set; }

        

        public TemperatureVisualViewModel()
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
            if (_context.TemperatureSekv.Any())
            { 
                var listTemp = _context.TemperatureSekv.ToList();
                Helpers.SekvenceHelper.LimitCheck loader = new Helpers.SekvenceHelper.LimitCheck();
                DateTime actualForSummary = DateTime.Parse("2017-01-01T12:04:19Z");
                foreach (var t in listTemp)
                {
                    DateTime convertedDate = DateTime.Parse(t.TimeStart);
                    String durationTime = null;
                    try
                    {
                        DateTime endtime = DateTime.Parse(t.TimeClose);
                        double time = (endtime - convertedDate).TotalMinutes;
                        var x = time - Math.Truncate(time);
                        durationTime = Math.Truncate(time).ToString() + " min " + Math.Round(x * 60).ToString() + " sec"; 
                    }
                    catch(Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception parse date " + e.ToString());
                        durationTime = "NA";
                    }
                    
                    if(actualForSummary <= convertedDate.AddHours(12))
                    {
                        switch (t.Upozornenie)
                        {
                            case 0: ok++;
                                break;
                            case 1: low++;
                                break;
                            case -1: low++;
                                break;
                            case 2: middle++;
                                break;
                            case 3: height++;
                                break;

                        }

                    }

                    if (actualForSummary <= convertedDate.AddHours(24))
                    {
                        switch (t.Upozornenie)
                        {
                            case 0:
                                okDay++;
                                break;
                            case 1:
                                lowDay++;
                                break;
                            case -1:
                                lowDay++;
                                break;
                            case 2:
                                middleDay++;
                                break;
                            case 3:
                                heightDay++;
                                break;

                        }

                    }




                    TemperatureObj tem = new TemperatureObj()
                    {
                        TempId = t.TeplSekvId,
                        Value = t.Sekvencia.ToString("n2") + " °C",
                        Date = convertedDate.ToShortDateString(),
                        Time = convertedDate.ToLongTimeString(),
                        Duration = durationTime,
                        Alert = loader.getStringValuePulseAndTempLimit(t.Upozornenie)
                    };
                    
                        SequenceList.Add(tem);

                }
                fillPageWithSequence(SequenceList.First());
                
            }
            else
            {
                TempAlert = "Neexistuje žiadna sekvencia";

            }

            HeightTwelve ="Vysoké: " + height.ToString() + "x";
            MiddleTwelve ="Stredné: " +  middle.ToString() + "x";
            LowTwelve ="Slabé: " +  low.ToString() + "x";
            OkTwelve ="OK: " + ok.ToString() + "x";

            HeightDay = "Vysoké: " + heightDay.ToString() + "x";
            MiddleDay = "Stredné: " + middleDay.ToString() + "x";
            LowDay = "Slabé: " + lowDay.ToString() + "x";
            OkDay = "OK: " + okDay.ToString() + "x";

            //SequenceList.Reverse();

        }

        private void fillPageWithSequence(TemperatureObj to)
        {
            TempAlert = to.Alert;
            TempTime = to.Date + " " + to.Time;
            TempValue = to.Value;
            TempDuration = to.Duration;
            TemperatureImageSource = ImageSource.FromResource("Xamarin.Forms_EFCore.temperature_high.png");

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

            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new MovementVisualPage());

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }


        }

        async void fallVisualCommand()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new FallVisualPage());

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }
            

        }
    }
}

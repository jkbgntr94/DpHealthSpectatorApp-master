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
            if (_context.TemperatureSekv.Any())
            {
                var listTemp = _context.TemperatureSekv.ToList();
                Helpers.SekvenceHelper.LimitCheck loader = new Helpers.SekvenceHelper.LimitCheck();
                foreach (var t in listTemp)
                {
                    DateTime convertedDate = DateTime.Parse(t.TimeStart);
                    String durationTime = null;
                    try
                    {
                        DateTime endtime = DateTime.Parse(t.TimeClose);
                        durationTime = (endtime - convertedDate).TotalMinutes.ToString();
                    }
                    catch(Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception parse date " + e.ToString());
                        durationTime = "NA";
                    }

                    TemperatureObj tem = new TemperatureObj()
                    {
                        TempId = t.TeplSekvId,
                        Value = t.Sekvencia + "C",
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
            
            //SequenceList.Reverse();

        }

        private void fillPageWithSequence(TemperatureObj to)
        {
            TempAlert = to.Alert;
            TempTime = to.Date + " " + to.Time;
            TempValue = to.Value;
            TempDuration = to.Duration;

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

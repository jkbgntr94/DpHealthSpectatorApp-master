using System;
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
            if (_context.PulseSekv.Any())
            {
                var listTemp = _context.PulseSekv.ToList();
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
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception parse date " + e.ToString());
                        durationTime = "NA";
                    }

                    PulseObj tem = new PulseObj()
                    {
                        PulseId = t.TepSekvId,
                        Value = t.Sekvencia + "BPM",
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
                PulseAlert = "Neexistuje žiadna sekvencia";

            }

            //SequenceList.Reverse();

        }

        private void fillPageWithSequence(PulseObj to)
        {
            PulseAlert = to.Alert;
            PulseTime = to.Date + " " + to.Time;
            PulseValue = to.Value;
            PulseDuration = to.Duration;

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

            await Application.Current.MainPage.Navigation.PushAsync(new HomeScreenPage());

        }
    }
}

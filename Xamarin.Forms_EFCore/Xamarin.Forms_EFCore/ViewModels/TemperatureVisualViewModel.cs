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

namespace Xamarin.Forms_EFCore.ViewModels
{
    class TemperatureVisualViewModel : BaseViewModel
    {
        DatabaseContext _context;

        private ObservableCollection<string> sequenceList = new ObservableCollection<string>();
        public ObservableCollection<string> SequenceList
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
                    String sekv = t.TeplSekvId + " " + t.Sekvencia + "C " + t.TimeStart + " " + loader.getStringValuePulseAndTempLimit(t.Upozornenie);
                    SequenceList.Add(sekv);

                }


            }
            else
            {
                SequenceList.Add("Neexistuje žiadna sekvencia");

            }

            //SequenceList.Reverse();

        }

        private void fillPageWithSequence(int position)
        {


        }

        async void tempVisualCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new TemperatureVisualPage());

        }

        async void pulseVisualCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new HomeScreenPage());

        }

        async void dashboardCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new DashboardPage());

        }

        async void movementVisualCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new HomeScreenPage());

        }

        async void fallVisualCommand()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new HomeScreenPage());

        }
    }
}

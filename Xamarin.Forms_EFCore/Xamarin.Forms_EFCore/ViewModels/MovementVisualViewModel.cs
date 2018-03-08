using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Models.ObjectsForList;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    class MovementVisualViewModel : BaseViewModel
    {


        private string movAlert;
        public string MovAlert
        {
            get
            {
                return movAlert;
            }
            set
            {
                movAlert = value;
                this.OnPropertyChanged("MovAlert");
            }
        }

        private string movTime;
        public string MovTime
        {
            get
            {
                return movTime;
            }
            set
            {
                movTime = value;
                this.OnPropertyChanged("MovTime");
            }
        }

        private string roomValue;
        public string RoomValue
        {
            get
            {
                return roomValue;
            }
            set
            {
                roomValue = value;
                this.OnPropertyChanged("RoomValue");
            }
        }

        private string movDuration;
        public string MovDuration
        {
            get
            {
                return movDuration;
            }
            set
            {
                movDuration = value;
                this.OnPropertyChanged("MovDuration");
            }
        }

        private ObservableCollection<MovementObj> sequenceList = new ObservableCollection<MovementObj>();
        public ObservableCollection<MovementObj> SequenceList
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


        private MovementObj selectedSequence;
        public MovementObj SelectedSequence
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


        DatabaseContext _context;
        public ICommand TempVisualCommand { get; private set; }
        public ICommand PulseVisualCommand { get; private set; }
        public ICommand DashboardCommand { get; private set; }
        public ICommand MovementVisualCommand { get; private set; }
        public ICommand FallVisualCommand { get; private set; }

        public MovementVisualViewModel()
        {


            _context = new DatabaseContext();
            TempVisualCommand = new Command(tempVisualCommand);
            PulseVisualCommand = new Command(pulseVisualCommand);
            DashboardCommand = new Command(dashboardCommand);
            MovementVisualCommand = new Command(movementVisualCommand);
            FallVisualCommand = new Command(fallVisualCommand);


        }

        private void fillList()
        {
            RoomsDetection roomsDetection = new RoomsDetection();

            if (_context.MovementSekv.Any())
            {
                var movSekvList = _context.MovementSekv.ToList();

                foreach(var movSekv in movSekvList)
                {
                    DateTime convertedDate = DateTime.Parse(movSekv.TimeStamp);
                    Izby izba = _context.Rooms.Where(t => t.IzbaID == movSekv.IzbyFK).First();

                    string alert = "NA";

                    if(movSekv.Upozornenie_Cas != 0)
                    {
                        alert = "Čas";

                    }else if (movSekv.Upozornenie_Hranica != 0)
                    {
                        alert = "Hranica";

                    }


                    MovementObj movObj = new MovementObj
                    {
                        PohId = movSekv.PohSekvId,
                        RoomName = izba.Nazov,
                        Alert = alert,
                        Date = convertedDate.ToShortDateString(),
                        Time = convertedDate.ToLongTimeString(),
                        Duration = movSekv.Cas_Zotrvania
                    };

                    SequenceList.Add(movObj);

                }
                fillPageWithSequence(SequenceList.First());

            }
            else {

                MovAlert = "Neexistuje žiadna sekvencia";
            }


        }

        private void fillPageWithSequence(MovementObj mo)
        {
            MovAlert = mo.Alert;
            RoomValue = mo.RoomName;
            MovTime = mo.Date + " " + mo.Time;
            MovDuration = mo.Duration;

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

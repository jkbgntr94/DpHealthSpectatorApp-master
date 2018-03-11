using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Helpers.JsonLoaderHelpers;
using Xamarin.Forms_EFCore.Helpers.SekvenceHelper;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Models.ObjectsForList;
using Xamarin.Forms_EFCore.Views;

namespace Xamarin.Forms_EFCore.ViewModels
{
    public class FallVisualViewModel : BaseViewModel
    {

        private string fallTime;
        public string FallTime
        {
            get
            {
                return fallTime;
            }
            set
            {
                fallTime = value;
                this.OnPropertyChanged("FallTime");
            }
        }
        private ObservableCollection<FallObj> fallList = new ObservableCollection<FallObj>();
        public ObservableCollection<FallObj> FallList
        {
            get
            {
                return fallList;
            }
            set
            {
                fallList = value;

            }
        }


        private FallObj selectedFall;
        public FallObj SelectedFall
        {
            get
            {
                return selectedFall;
            }
            set
            {
                if (selectedFall != value)
                {
                    selectedFall = value;
                    HandleSelectedItem();

                }

            }
        }

        private void HandleSelectedItem()
        {
            fillPageWithFall(SelectedFall);

        }

        public delegate void MyEventAction(float x, float y);
        public event MyEventAction MyEventFall;

        public delegate void MovePageAction();
        public event MovePageAction movePage;


        DatabaseContext _context;
        public ICommand TempVisualCommand { get; private set; }
        public ICommand PulseVisualCommand { get; private set; }
        public ICommand DashboardCommand { get; private set; }
        public ICommand MovementVisualCommand { get; private set; }
        public ICommand FallVisualCommand { get; private set; }
        public FallVisualViewModel()
        {

            _context = new DatabaseContext();
           /* LoadFall loadFall = new LoadFall();
            loadFall.GenerateFallData(_context);
            */
            
            TempVisualCommand = new Command(tempVisualCommand);
            PulseVisualCommand = new Command(pulseVisualCommand);
            DashboardCommand = new Command(dashboardCommand);
            MovementVisualCommand = new Command(movementVisualCommand);
            FallVisualCommand = new Command(fallVisualCommand);
            fillList();
        }


        private void fillList()
        {
            RoomsDetection roomsDetection = new RoomsDetection();

            if (_context.Akcelerometers.Any())
            {
                var FallValueList = _context.Akcelerometers.ToList();

                foreach (var fallValue in FallValueList)
                {
                    DateTime convertedDate = DateTime.Parse(fallValue.TimeStamp);

                    Izby izba = null;
                    String izbaName = "";
                    try
                    {
                        izba = roomsDetection.findRoomByCoord(fallValue.Xhodnota,fallValue.Yhodnota);
                        izbaName = izba.Nazov;
                    }
                    catch (Exception e)
                    {
                        izbaName = "Vonku";

                    }

                    FallObj fallObj = new FallObj
                    {
                        FallId = fallValue.AkcelerometerID,
                        RoomName = izbaName,
                        xValue = fallValue.Xhodnota,
                        yValue = fallValue.Yhodnota,
                        Date = convertedDate.ToShortDateString(),
                        Time = convertedDate.ToLongTimeString()

                    };

                    FallList.Add(fallObj);

                }
                fillPageWithFall(FallList.First());

            }
            else
            {

                FallTime = "Neexistuje žiadna sekvencia";
            }


        }

        private void fillPageWithFall(FallObj fo)
        {
            FallTime = fo.Date + " " + fo.Time;
            

            MyEventFall?.Invoke(fo.xValue, fo.yValue);

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

        public void movementVisualCommand()
        {
            movePage?.Invoke();

            /* try
             {
                 await Application.Current.MainPage.Navigation.PushAsync(new MovementVisualPage());

             }
             catch (Exception e)
             {
                 System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

             }*/


        }

        async void fallVisualCommand()
        {

          /*  try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new FallVisualPage());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

            }

          */

        }


    }
}

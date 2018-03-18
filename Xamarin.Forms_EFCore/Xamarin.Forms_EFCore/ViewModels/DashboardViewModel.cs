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
    class DashboardViewModel : BaseViewModel
    {


        private AlertSequenceObj selectedAlerts;
        public AlertSequenceObj SelectedAlerts
        {
            get
            {
                return selectedAlerts;
            }
            set
            {
                if (selectedAlerts != value)
                {
                    selectedAlerts = value;
                    HandleSelectedItem();

                }

            }
        }

        private void HandleSelectedItem()
        {
            movePageSequence(SelectedAlerts);

        }

        private ObservableCollection<AlertSequenceObj> alerts = new ObservableCollection<AlertSequenceObj>();
        public ObservableCollection<AlertSequenceObj> Alerts
        {
            get
            {
                return alerts;
            }
            set
            {
                alerts = value;

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

        private string motionValue;
        public string MotionValue
        {
            get
            {
                return motionValue;
            }
            set
            {
                motionValue = value;

            }
        }

        private string fallValue;
        public string FallValue
        {
            get
            {
                return fallValue;
            }
            set
            {
                fallValue = value;

            }
        }

        DatabaseContext _context;
        public ICommand TempVisualCommand { get; private set; }
        public ICommand PulseVisualCommand { get; private set; }
        public ICommand DashboardCommand { get; private set; }
        public ICommand MovementVisualCommand { get; private set; }
        public ICommand FallVisualCommand { get; private set; }

        public DashboardViewModel()
        {
            
            //new DatabaseContext(999);

            _context = new DatabaseContext();
            TempVisualCommand = new Command(tempVisualCommand);
            PulseVisualCommand = new Command(pulseVisualCommand);
            DashboardCommand = new Command(dashboardCommand);
            MovementVisualCommand = new Command(movementVisualCommand);
            FallVisualCommand = new Command(fallVisualCommand);
            /* PulseValue = 10.ToString();
             TempValue = 15.ToString();
             MotionValue = "Spalna";
             FallValue = "NA";*/

            //new TestDataDbFiller();

            /*Pohyb pohyba = new Pohyb
            {
                PohybId = 999, 
                Xhodnota = 160,
                Yhodnota = 15,
                TimeStamp = DateTime.Now.AddMinutes(101).ToShortTimeString()

            };
            _context.Movement.Add(pohyba);


            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            */

            SettingsController.MaxX = 150;
            SettingsController.MaxY = 150;

            setValues();

        }

        public void setValues()
        {
            setPulseValue();
            setTemperatureValue();
            setMovementValue();
            setFallValue();
            setAlertListValue();

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

        public void setPulseValue()
        {
            if (_context.Pulse.Any())
            {

                Tep tep = _context.Pulse.FirstOrDefault(t => t.TepId == _context.Pulse.Max(x => x.TepId));

                PulseValue = tep.Hodnota.ToString();

            }
            else
            {
                PulseValue = "NA";

            }

        }

        public void setTemperatureValue()
        {
            if (_context.Temperature.Any())
            {

                Teplota teplota = _context.Temperature.FirstOrDefault(t => t.TeplotaId == _context.Temperature.Max(x => x.TeplotaId));

                TempValue = teplota.Hodnota.ToString();

            }
            else
            {
                TempValue = "NA";

            }
        }

        public void setMovementValue()
        {
            if (_context.Movement.Any())
            {

                Pohyb pohyb = _context.Movement.FirstOrDefault(t => t.PohybId == _context.Movement.Max(x => x.PohybId));

                RoomsDetection roomsDetection = new RoomsDetection();
                Izby izba = roomsDetection.findRoom(pohyb);
                if(izba == null)
                {
                    MotionValue = "Vonku";

                }
                else
                {
                    MotionValue = roomsDetection.findRoom(pohyb).Nazov;

                }

                
            }
            else
            {
                MotionValue = "NA";

            }


        }

        public void setFallValue()
        {
            if (_context.Akcelerometers.Any())
            {

                Akcelerometer akcelerometer = _context.Akcelerometers.FirstOrDefault(t => t.AkcelerometerID == _context.Akcelerometers.Max(x => x.AkcelerometerID));

                FallValue = akcelerometer.TimeStamp.ToString();
                
            }
            else
            {
                FallValue = "NA";

            }


        }

        public void setAlertListValue()
        {
            
            if (_context.PulseSekv.Any())
            {

                var all = _context.PulseSekv.OrderBy(x => x.TimeStart).ToList();
                all.Reverse();
                Tep_Sekvencia ts = null;
                foreach (var a in all)
                {
                    ts = a;
                    if (a.Upozornenie != 0) break;

                }
                
               // Tep_Sekvencia ts = _context.PulseSekv.FirstOrDefault(t => t.TepSekvId == _context.PulseSekv.Max(x => x.TepSekvId));

                Helpers.SekvenceHelper.LimitCheck loader = new Helpers.SekvenceHelper.LimitCheck();
                if(ts != null)
                {
                    DateTime convertedDate = DateTime.Parse(ts.TimeStart);

                    AlertSequenceObj myObj = new AlertSequenceObj
                    {
                        Name = "Tep",
                        Value = ts.Sekvencia + " BPM",
                        Date = convertedDate.ToShortDateString(),
                        Time = convertedDate.ToLongTimeString(),
                        Alert = loader.getStringValuePulseAndTempLimit(ts.Upozornenie)

                    };
                    Alerts.Add(myObj);

                }
                else
                {
                    AlertSequenceObj myObj = new AlertSequenceObj
                    {
                        Name = "Tep",
                        Alert = "Neexistuje upozornenie"

                    };
                    Alerts.Add(myObj);

                }


                //String tepSekvString = "Tep " + ts.Sekvencia + " " + ts.TimeStart + " " + loader.getStringValuePulseAndTempLimit(ts.Upozornenie);


            }
            else
            {
                AlertSequenceObj myObj = new AlertSequenceObj
                {
                    Name = "Tep",
                    Alert = "Neexistuje sekvencia"

                };
                Alerts.Add(myObj);

            }



            if (_context.TemperatureSekv.Any())
            {

                var all = _context.TemperatureSekv.OrderBy(x => x.TimeStart).ToList();
                all.Reverse();
                Teplota_Sekvencia ts = null;
                foreach (var a in all)
                {
                    ts = a;
                    if (a.Upozornenie != 0) break;

                }

                Helpers.SekvenceHelper.LimitCheck loader = new Helpers.SekvenceHelper.LimitCheck();
                if (ts != null)
                {
                    DateTime convertedDate = DateTime.Parse(ts.TimeStart);

                    AlertSequenceObj myObj = new AlertSequenceObj
                    {
                        Name = "Teplota",
                        Value = ts.Sekvencia.ToString("n2") + " °C",
                        Date = convertedDate.ToShortDateString(),
                        Time = convertedDate.ToLongTimeString(),
                        Alert = loader.getStringValuePulseAndTempLimit(ts.Upozornenie)

                    };
                    Alerts.Add(myObj);

                }
                else
                {
                    AlertSequenceObj myObj = new AlertSequenceObj
                    {
                        Name = "Teplota",
                        Alert = "Neexistuje upozornenie"

                    };
                    Alerts.Add(myObj);

                }



            }
            else
            {
                AlertSequenceObj myObj = new AlertSequenceObj
                {
                    Name = "Teplota",
                    Alert = "Neexistuje sekvencia"

                };
                Alerts.Add(myObj);

            }




            if (_context.MovementSekv.Any())
            {
               
                var all = _context.MovementSekv.OrderBy(x => x.TimeStamp).ToList();
                all.Reverse();

                Pohyb_Sekvencia ps = null;
                foreach (var a in all)
                {
                    ps = a;
                    if (a.Upozornenie_Cas != 0) break;
                    if (a.Upozornenie_Hranica != 0) break;

                }

                if (ps != null)
                {
                    DateTime convertedDate = DateTime.Parse(ps.TimeStamp);

                    if(ps.Upozornenie_Cas != 0)
                    {
                        AlertSequenceObj myObj = new AlertSequenceObj
                        {
                            Name = "Pohyb",
                            Date = convertedDate.ToShortDateString(),
                            Time = convertedDate.ToLongTimeString(),
                            Alert = "Čas"

                        };
                        Alerts.Add(myObj);



                    }else if (ps.Upozornenie_Hranica != 0)
                    {
                        AlertSequenceObj myObj = new AlertSequenceObj
                        {
                            Name = "Pohyb",
                            Date = convertedDate.ToShortDateString(),
                            Time = convertedDate.ToLongTimeString(),
                            Alert = "Hranica"

                        };
                        Alerts.Add(myObj);



                    }
                    else
                    {
                        AlertSequenceObj myObj = new AlertSequenceObj
                        {
                            Name = "Pohyb",
                            Alert = "Neexistuje upozornenie"

                        };
                        Alerts.Add(myObj);

                    }
                    
                }
                else
                {
                    AlertSequenceObj myObj = new AlertSequenceObj
                    {
                        Name = "Pohyb",
                        Alert = "Neexistuje upozornenie"

                    };
                    Alerts.Add(myObj);

                }



            }
            else
            {
                AlertSequenceObj myObj = new AlertSequenceObj
                {
                    Name = "Pohyb",
                    Alert = "Neexistuje sekvencia"

                };
                Alerts.Add(myObj);

            }



            if (_context.Akcelerometers.Any())
            {
                var pad = _context.Akcelerometers.FirstOrDefault(t => t.AkcelerometerID == _context.Akcelerometers.Max(x => x.AkcelerometerID));

                DateTime convertedDate = DateTime.Parse(pad.TimeStamp);
                RoomsDetection roomsDetection = new RoomsDetection();

               
                Izby izba = roomsDetection.findRoomByCoord(pad.Xhodnota, pad.Yhodnota);
                string izbameno = "Vonku";
                try
                {
                    izbameno = izba.Nazov;
                }catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("EXCEPTION " + e.ToString());

                }
                

                AlertSequenceObj myObj = new AlertSequenceObj
                {
                    Name = "Pád",
                    Date = convertedDate.ToShortDateString(),
                    Time = convertedDate.ToLongTimeString(),
                    Value = izbameno,
                    Alert = "Nastal pád"

                };
                Alerts.Add(myObj);


            }
            else
            {
                AlertSequenceObj myObj = new AlertSequenceObj
                {
                    Name = "Pád",
                    Alert = "Pád nenastal"

                };
                Alerts.Add(myObj);

            }


            //TODO: ZOBRAZENIE POHYBU
        }

       public void movePageSequence(AlertSequenceObj alertSequenceObj)
        {
            if (alertSequenceObj.Name.Equals("Teplota"))
            {
                

            }


        }

    }
}

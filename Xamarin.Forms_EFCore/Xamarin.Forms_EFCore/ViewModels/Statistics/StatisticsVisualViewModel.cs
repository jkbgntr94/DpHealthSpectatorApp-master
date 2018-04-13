using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Models.ObjectsForList;
using Xamarin.Forms_EFCore.Views;
using Xamarin.Forms_EFCore.Views.Statistics;

namespace Xamarin.Forms_EFCore.ViewModels.Statistics
{
    public class StatisticsVisualViewModel : BaseViewModel
    {
        private string roomNameLabel;
        public string RoomNameLabel
        {
            get { return roomNameLabel; }
            set
            {
                roomNameLabel = value;
            }
        }

        private string isFall;
        public string IsFall
        {
            get { return isFall; }
            set
            {
                isFall = value;
            }
        }

        private string startTime;
        public string StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
            }
        }

        private string stopTime;
        public string StopTime
        {
            get { return stopTime; }
            set
            {
                stopTime = value;
            }
        }

        private string activityName;
        public string ActivityName
        {
            get { return activityName; }
            set
            {
                activityName = value;
            }
        }

        private ObservableCollection<StatisticsObj> statsList = new ObservableCollection<StatisticsObj>();
        public ObservableCollection<StatisticsObj> StatsList
        {
            get
            {
                return statsList;
            }
            set
            {
                statsList = value;

            }
        }


        public ICommand ToBack { get; private set; }

        private List<Akcelerometer> fallList;
        private DateTime StartdateTime;
        private DateTime StopdateTime;

        private RoomStatisticsObj _roomStatistics;
        private DatabaseContext _context;

        public StatisticsVisualViewModel(RoomStatisticsObj roomStats)
        {
            _roomStatistics = roomStats;
            _context = new DatabaseContext();
            StartdateTime = DateTime.Parse(_roomStatistics.StartTime);
            StopdateTime = DateTime.Parse(_roomStatistics.EndTime);
            StartTime = _roomStatistics.StartTime;
            StopTime = _roomStatistics.EndTime;

            ToBack = new Command(toBack);

            FillValues();
            CreateList();
            FindActivity();
        }

        private void FindActivity()
        {
            SleepDetection sleepDetection = new SleepDetection();
            Boolean sleep = sleepDetection.DetectSleep(_context, _roomStatistics.RoomName, StartdateTime, StopdateTime);
            if (sleep)
            {
                ActivityName = "Spánok";
            }
           
            System.Diagnostics.Debug.WriteLine("************ ACTIVITY: " + sleep.ToString());
        }


        private void FillValues()
        {
            //System.Diagnostics.Debug.WriteLine("************ TIME BORDERS: " + StartdateTime + " " + StopdateTime);

            RoomNameLabel = _roomStatistics.RoomName;
            FindFall();
            if(fallList.Count > 0)
            {
                IsFall = "Áno";
            }
            else
            {
                IsFall = "Nie";

            }
        }

        private void CreateList()
        {
            List<Tep_Sekvencia> tepList = null;
            try { 
            tepList = _context.PulseSekv.Where(p => DateTime.Parse(p.TimeStart) >= StartdateTime && DateTime.Parse(p.TimeStart) <= StopdateTime || DateTime.Parse(p.TimeStart) <= StartdateTime && DateTime.Parse(p.TimeClose) >= StartdateTime || DateTime.Parse(p.TimeStart) <= StopdateTime && DateTime.Parse(p.TimeClose) >= StopdateTime).ToList();
            }
            catch(Exception e) {
                System.Diagnostics.Debug.WriteLine("Cant find list pulse for selected time" + e.ToString());

            }
           /* foreach (var t in tepList) {
                System.Diagnostics.Debug.WriteLine("************ Pulse: " + t.TimeStart + " " + t.TimeClose + " " + t.Sekvencia);

            }*/
            List<Teplota_Sekvencia> tempList = null;
            try
            {
                tempList = _context.TemperatureSekv.Where(p => DateTime.Parse(p.TimeStart) >= StartdateTime && DateTime.Parse(p.TimeStart) <= StopdateTime || DateTime.Parse(p.TimeStart) <= StartdateTime && DateTime.Parse(p.TimeClose) >= StartdateTime || DateTime.Parse(p.TimeStart) <= StopdateTime && DateTime.Parse(p.TimeClose) >= StopdateTime).ToList();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Cant find list temperature for selected time" + e.ToString());

            }
          /*  foreach (var t in tempList)
            {
                System.Diagnostics.Debug.WriteLine("************ Temperature: " + t.TimeStart + " " + t.TimeClose + " " + t.Sekvencia);

            }*/

            DateTime myTime = StartdateTime;
            Helpers.SekvenceHelper.LimitCheck loader = new Helpers.SekvenceHelper.LimitCheck();

            while (myTime <= StopdateTime)
            {
                string tempVal = "NA";
                string tempAlert = "";
                try
                {
                    var temp = tempList.Where(p => DateTime.Parse(p.TimeStart) <= myTime && DateTime.Parse(p.TimeClose) >= myTime).First();

                    tempVal = temp.Sekvencia.ToString("n2") + " °C";
                    tempAlert = loader.getStringValuePulseAndTempLimit(temp.Upozornenie);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Cant find value for selected time" + e.ToString());

                }

                string pulseVal = "NA";
                string pulseAlert = "";

                try
                {
                    var pulse = tepList.Where(p => DateTime.Parse(p.TimeStart) <= myTime && DateTime.Parse(p.TimeClose) >= myTime).First();
                    pulseVal = pulse.Sekvencia.ToString() + " BPM";
                    pulseAlert = loader.getStringValuePulseAndTempLimit(pulse.Upozornenie);

                } catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Cant find value for selected time" + e.ToString());

                }

                string fallValue = "";
                try
                {
                    var fall = fallList.Where(p => DateTime.Parse(p.TimeStamp) >= myTime && DateTime.Parse(p.TimeStamp) <= myTime.AddMinutes(1)).First();
                    if(fall != null)
                    {
                        fallValue = "Áno";
                    }
                    //System.Diagnostics.Debug.WriteLine("find value for selected time fall " + fall.TimeStamp);

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Cant find value for selected time fall " + e.ToString());

                }

                StatisticsObj statisticsObj = new StatisticsObj
                {
                    Time = myTime.ToShortTimeString(),
                    PulseValue = pulseVal,
                    PulseAlert = pulseAlert,
                    TemperatureValue = tempVal,
                    TemperatureAlert = tempAlert,
                    isFall = fallValue
                    
                };

                StatsList.Add(statisticsObj);

              //  System.Diagnostics.Debug.WriteLine("--- LIST ---" + myTime + " " + pulseVal + " " + pulseAlert + " " + tempVal + " " + tempAlert + " " + fallValue);

                myTime = myTime.AddMinutes(1);
            }
            //StatsList = new ObservableCollection<StatisticsObj>(StatsList.Reverse());

        }

        private void FindFall()
        {
           

            fallList = _context.Akcelerometers.Where(t => DateTime.Parse(t.TimeStamp) >= StartdateTime && DateTime.Parse(t.TimeStamp) <= StopdateTime).ToList();

         /*   foreach (var f in fallList)
            {
                System.Diagnostics.Debug.WriteLine("************ FALL: " + f.TimeStamp);


            }*/


        }

        private async void toBack()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new StatisticsMainPage());

        }
    }
}

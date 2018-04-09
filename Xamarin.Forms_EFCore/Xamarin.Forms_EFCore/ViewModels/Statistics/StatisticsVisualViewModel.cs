using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models;
using Xamarin.Forms_EFCore.Models.ObjectsForList;
using Xamarin.Forms_EFCore.Views;

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
            ToBack = new Command(toBack);

            FillValues();
            CreateList();
        }

        private void FillValues()
        {
            System.Diagnostics.Debug.WriteLine("************ TIME BORDERS: " + StartdateTime + " " + StopdateTime);

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
            var tepList = _context.PulseSekv.Where(p => DateTime.Parse(p.TimeStart) >= StartdateTime && DateTime.Parse(p.TimeStart) <= StopdateTime || DateTime.Parse(p.TimeStart) <= StartdateTime && DateTime.Parse(p.TimeClose) >= StartdateTime || DateTime.Parse(p.TimeStart) <= StopdateTime && DateTime.Parse(p.TimeClose) >= StopdateTime).ToList();

            foreach (var t in tepList) {
                System.Diagnostics.Debug.WriteLine("************ Pulse: " + t.TimeStart + " " + t.TimeClose + " " + t.Sekvencia);

            }

            var tempList = _context.TemperatureSekv.Where(p => DateTime.Parse(p.TimeStart) >= StartdateTime && DateTime.Parse(p.TimeStart) <= StopdateTime || DateTime.Parse(p.TimeStart) <= StartdateTime && DateTime.Parse(p.TimeClose) >= StartdateTime || DateTime.Parse(p.TimeStart) <= StopdateTime && DateTime.Parse(p.TimeClose) >= StopdateTime).ToList();

            foreach (var t in tempList)
            {
                System.Diagnostics.Debug.WriteLine("************ Temperature: " + t.TimeStart + " " + t.TimeClose + " " + t.Sekvencia);

            }

            DateTime myTime = StartdateTime;

            while(myTime <= StopdateTime)
            {
               
                try
                {
                    var temp = tempList.Where(p => DateTime.Parse(p.TimeStart) <= myTime && DateTime.Parse(p.TimeClose) >= myTime).First();
                    var pulse = tepList.Where(p => DateTime.Parse(p.TimeStart) <= myTime && DateTime.Parse(p.TimeClose) >= myTime).First();
                    System.Diagnostics.Debug.WriteLine(" ---- LIST ---- " + myTime + " " + temp.Sekvencia + " C " + temp.TimeStart + " " + pulse.Sekvencia + " BPM " + pulse.TimeStart);
                }catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Cant find value for selected time" + e.ToString());

                }
                myTime = myTime.AddMinutes(1);
            }

        }

        private void FindFall()
        {
           

            fallList = _context.Akcelerometers.Where(t => DateTime.Parse(t.TimeStamp) >= StartdateTime && DateTime.Parse(t.TimeStamp) <= StopdateTime).ToList();

            foreach (var f in fallList)
            {
                System.Diagnostics.Debug.WriteLine("************ FALL: " + f.TimeStamp);


            }


        }

        private async void toBack()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new DashboardPage());

        }
    }
}

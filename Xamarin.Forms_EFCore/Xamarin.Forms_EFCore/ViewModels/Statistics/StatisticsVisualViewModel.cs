using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms_EFCore.DataAccess;
using Xamarin.Forms_EFCore.Helpers;
using Xamarin.Forms_EFCore.Models.ObjectsForList;

namespace Xamarin.Forms_EFCore.ViewModels.Statistics
{
    public class StatisticsVisualViewModel : BaseViewModel
    {
        private string roomName;
        public string RoomName
        {
            get { return roomName; }
            set
            {
                roomName = value;
                OnPropertyChanged("RoomName");
            }
        }

        private RoomStatisticsObj _roomStatistics;
        private DatabaseContext _context;
        public StatisticsVisualViewModel()
        {

        }

        public StatisticsVisualViewModel(RoomStatisticsObj roomStats)
        {
            _roomStatistics = roomStats;
            _context = new DatabaseContext();

            FillValues();
        }

        private void FillValues()
        {
            RoomName = _roomStatistics.RoomName;
            FindFall();
        }

        private void FindFall()
        {
            DateTime StartdateTime = DateTime.Parse(_roomStatistics.StartTime);
            DateTime StopdateTime = DateTime.Parse(_roomStatistics.EndTime);

            var fall = _context.Akcelerometers.Where(t => DateTime.Parse(t.TimeStamp) >= StartdateTime && DateTime.Parse(t.TimeStamp) <= StopdateTime).ToList();

            foreach (var f in fall)
            {
                System.Diagnostics.Debug.WriteLine("************FALL: " + f.TimeStamp);


            }


        }
    }
}

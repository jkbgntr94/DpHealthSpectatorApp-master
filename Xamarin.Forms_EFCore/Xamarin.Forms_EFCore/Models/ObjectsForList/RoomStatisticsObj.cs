using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms_EFCore.Models.ObjectsForList
{
    public class RoomStatisticsObj
    {
        public string RoomName { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
        public List<Pohyb_Sekvencia> Sekvencie { get; set; }
    }
}

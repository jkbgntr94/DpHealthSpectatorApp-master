using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms_EFCore.Models.ObjectsForList
{
    public class StatisticsObj
    {
        public string Time { get; set; }
        public string PulseValue { get; set; }
        public string PulseAlert { get; set; }
        public string TemperatureValue { get; set; }
        public string TemperatureAlert{ get; set; }
        public string isFall { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms_EFCore.Models.ObjectsForList
{
    class MovementObj
    {
        public int PohId { get; set; }

        public string RoomName { get; set; }
        public string LongDate { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Duration { get; set; }
        public string Alert { get; set; }
        public float xValue { get; set; }
        public float yValue { get; set; }
    }
}

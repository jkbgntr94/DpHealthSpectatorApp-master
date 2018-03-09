using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms_EFCore.Models.ObjectsForList
{
    public class FallObj
    {
        public int FallId { get; set; }

        public string RoomName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
       
        public float xValue { get; set; }
        public float yValue { get; set; }
        public float zValue { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms_EFCore.Models.ObjectsForList
{
    public class TemperatureObj
    {
        public int TempId { get; set; }
        public string Value { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Duration { get; set; }
        public string Alert { get; set; }
    }
}

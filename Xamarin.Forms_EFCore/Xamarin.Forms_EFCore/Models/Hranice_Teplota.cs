using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin.Forms_EFCore.Models
{
    public class Hranice_Teplota
    {
        [Key]
        public int Hranice_TeplotaId { get; set; }

        public float Hranica_Slabe_Min { get; set; }
        public float Hranica_Slabe_Max { get; set; }
        public float Hranica_Stredne { get; set; }
        public float Hranica_Vysoke { get; set; }
        public string TimeStamp { get; set; }


    }
}

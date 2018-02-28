using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin.Forms_EFCore.Models
{
    public class Teplota_Sekvencia
    {
        [Key]
        public int TeplSekvId { get; set; }
        public float Sekvencia { get; set; }
        public string TimeStart { get; set; }
        public string TimeClose { get; set; }
        public int Upozornenie { get; set; }

        public virtual Profile Profile { get; set; }
        [ForeignKey("Profile")]
        public int? ProfileFK { get; set; }


        public virtual Hranice_Teplota Hranice_Teplota { get; set; }
        [ForeignKey("Hranice_Teplota")]
        public int? Hranice_TeplotaFk { get; set; }
    }
}

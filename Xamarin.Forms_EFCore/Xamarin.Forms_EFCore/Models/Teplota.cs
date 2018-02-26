using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin.Forms_EFCore.Models
{
    public class Teplota
    {
        [Key]
        public int TeplotaId { get; set; }
        public string TimeStamp { get; set; }
        public float Hodnota { get; set; }

        public virtual Teplota_Sekvencia Teplota_Sekvencia { get; set; }
        [ForeignKey("Teplota_Sekvencia")]
        public int? TeplSekvFk { get; set; }
    }
}

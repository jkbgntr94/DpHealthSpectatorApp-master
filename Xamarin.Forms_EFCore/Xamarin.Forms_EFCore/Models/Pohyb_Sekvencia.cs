using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Xamarin.Forms_EFCore.Models
{
    public class Pohyb_Sekvencia
    {
        [Key]
        public int PohSekvId { get; set; }
        public string TimeStamp { get; set; }
        public float Xhodnota { get; set; }
        public float Yhodnota { get; set; }
        public string Cas_Zotrvania { get; set; }
        public int Upozornenie_Cas { get; set; }
        public int Upozornenie_Hranica { get; set; }

        public virtual Izby Izby { get; set; }
        [ForeignKey("Izby")]
        public int? IzbyFK { get; set; }

        public virtual Profile Profile { get; set; }
        [ForeignKey("Profile")]
        public int? ProfileFK { get; set; }
    }
}

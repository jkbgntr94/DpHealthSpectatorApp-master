using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin.Forms_EFCore.Models
{
    public class Pohyb
    {
        [Key]
        public int PohybId { get; set; }

        public float Xhodnota { get; set; }
        public float Yhodnota { get; set; }
        public string TimeStamp { get; set; }

        public virtual Pohyb_Sekvencia Pohyb_Sekvencia { get; set; }
        [ForeignKey("Pohyb_Sekvencia")]
        public int? PohSekvFK { get; set; }

    }
}

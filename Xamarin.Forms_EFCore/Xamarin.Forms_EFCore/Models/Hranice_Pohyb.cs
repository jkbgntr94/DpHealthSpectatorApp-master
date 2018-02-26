using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin.Forms_EFCore.Models
{
    public class Hranice_Pohyb
    {
        [Key]
        public int HranicePohybId { get; set; }
        public float Xhranica { get; set; }
        public float Yhranica { get; set; }
        public float OkruhHranica { get; set; }
        public string TimeStamp { get; set; }

        public virtual Pohyb_Sekvencia Pohyb_Sekvencia { get; set; }
        [ForeignKey("Pohyb_Sekvencia")]
        public int? PohSekvFk { get; set; }
    }
}

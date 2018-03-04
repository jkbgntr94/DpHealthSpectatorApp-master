using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin.Forms_EFCore.Models
{
    public class Izby
    {
        [Key]
        public int IzbaID { get; set; }

        public string Nazov { get; set; }

        public float LavaXhodnota { get; set; }

        public float LavaYhodnota { get; set; }

        public float PravaXhodnota { get; set; }

        public float PravaYhodnota { get; set; }

        public int Vonku { get; set; }

       
        public virtual Profile Profile { get; set; }
        [ForeignKey("Profile")]
        public int? ProfileFK { get; set; }
    }
}

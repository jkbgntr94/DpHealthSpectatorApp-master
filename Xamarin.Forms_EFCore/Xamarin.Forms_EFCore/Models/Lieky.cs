using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Xamarin.Forms_EFCore.Models
{
    public class Lieky
    {
        [Key]
        public int LiekId { get; set; }

        public string Nazov { get; set; }
        public int Pocet { get; set; }
        public int Dlhodobo { get; set; }
        public string Poznamka { get; set; }


        public virtual Profile Profile { get; set; }

        [ForeignKey("Profile")]
        public int? ProfileFK { get; set; }
    }
}

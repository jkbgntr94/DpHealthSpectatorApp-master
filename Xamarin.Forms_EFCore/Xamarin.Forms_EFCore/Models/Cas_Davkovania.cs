using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Xamarin.Forms_EFCore.Models
{
    public class Cas_Davkovania
    {
        [Key]
        public int CasDavkovaniaId { get; set; }
        public string Cas { get; set;}

        public virtual Lieky Lieky { get; set; }

        [ForeignKey("Lieky")]
        public int? LiekyFK { get; set; }
    }
}

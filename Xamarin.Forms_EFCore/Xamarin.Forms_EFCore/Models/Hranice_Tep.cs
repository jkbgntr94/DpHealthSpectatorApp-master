using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin.Forms_EFCore.Models
{
    public class Hranice_Tep
    {
        [Key]
        public int Hranica_TepId { get; set; }

        public int Hranica_Slabe_Min { get; set; }
        public int Hranica_Slabe_Max { get; set; }
        public int Hranica_Stredne { get; set; }
        public int Hranica_Vysoke { get; set; }
        public string TimeStamp { get; set; }

        public virtual Tep_Sekvencia Tep_Sekvencia { get; set; }
        [ForeignKey("Tep_Sekvencia")]
        public int? TepSekvFK { get; set; }
    }
}

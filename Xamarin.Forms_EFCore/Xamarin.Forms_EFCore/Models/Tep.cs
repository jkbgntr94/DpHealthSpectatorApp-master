using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin.Forms_EFCore.Models
{
    public class Tep
    {
        [Key]
        public int TepId { get; set; }
        public string TimeStamp { get; set; }
        public float Hodnota { get; set; }

        public virtual Tep_Sekvencia Tep_Sekvencia { get; set; }
        [ForeignKey("Tep_Sekvencia")]
        public int? TepSekvId { get; set; }
                
    }
}

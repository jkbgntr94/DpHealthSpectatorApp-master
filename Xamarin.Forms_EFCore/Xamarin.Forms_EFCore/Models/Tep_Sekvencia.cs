using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin.Forms_EFCore.Models
{
    public class Tep_Sekvencia
    {
        [Key]
        public int TepSekvId { get; set; }

        public int Sekvencia { get; set; }

        public string TimeStart { get; set; }
        public string TimeClose { get; set; }
        public int Upozornenie { get; set; }

        public virtual Profile Profile { get; set; }
        [ForeignKey("Profile")]
        public int? ProfileFK { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Xamarin.Forms_EFCore.Models
{
    public class Akcelerometer
    {
        [Key]
        public int  AkcelerometerID { get; set; }

        public string TimeStamp { get; set; }

        public float Xhodnota { get; set; }
        public float Yhodnota { get; set; }
        public float Zhodnota { get; set; }

        public int Upozornenie { get; set; }

        
        public virtual Profile Profile { get; set; }
        [ForeignKey("Profile")]
        public int? ProfileFK { get; set; }

    }
}

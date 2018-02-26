using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin.Forms_EFCore.Models
{
    public class Hranice_Akcelerometer
    {
        [Key]
        public int Hran_Akce_Id { get; set; }

        public float HranicaX { get; set; }
        public float HranicaY { get; set; }
        public float HranicaZ { get; set; }

        public string TimeStamp { get; set;}

        
        public virtual Akcelerometer Akcelerometer { get; set; }
        [ForeignKey("Akcelerometer")]
        public int? AkcelerometerFK { get; set; }
    }
}

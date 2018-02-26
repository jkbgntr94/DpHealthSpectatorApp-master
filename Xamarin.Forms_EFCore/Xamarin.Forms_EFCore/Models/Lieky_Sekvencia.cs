using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xamarin.Forms_EFCore.Models
{
    public class Lieky_Sekvencia
    {
        [Key]
        public int Lieky_SekvenciaId { get; set; }



        public virtual Tep_Sekvencia Tep_Sekvencia { get; set; }
        [ForeignKey("Tep_Sekvencia")]
        public int? TepSekvId { get; set; }

        public virtual Teplota_Sekvencia Teplota_Sekvencia { get; set; }
        [ForeignKey("Teplota_Sekvencia")]
        public int? TeplSekvFk { get; set; }


         public virtual Lieky Lieky { get; set; }

         [ForeignKey("Lieky")]
         public int? LiekyFK
         {
             get; set;

         }
    }
}

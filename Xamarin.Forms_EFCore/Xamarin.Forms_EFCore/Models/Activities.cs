using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Xamarin.Forms_EFCore.Models
{
    public class Activities
    {

        [Key]
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public Boolean ActSwitchIsChecked { get; set; }

        public Boolean StressIsChecked { get; set; }
    }
}

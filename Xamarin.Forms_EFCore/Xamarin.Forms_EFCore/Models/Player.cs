﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Xamarin.Forms_EFCore.Models {

    public class Player {

        [Key]
        public int PlayerId { get; set; }

        public string Name { get; set; }
        public int JerseyNumber { get; set; }
        public string Position { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

    }
}

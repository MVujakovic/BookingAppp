﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class AccommodationType
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(3), MaxLength(20)]
        public String Name { get; set; }

        public IList<Accomodation> Accomodations { get; set; }

        public AccommodationType()
        {
            this.Accomodations = new List<Accomodation>();
        }
    }
}
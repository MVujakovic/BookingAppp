﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Place
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        public Region Region { get; set; }

        [ForeignKey("Region")]
        public int RegionId { get; set; }

        public IList<Accomodation> Accomodations { get; set; }

        public Place()
        {
            this.Accomodations = new List<Accomodation>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Comment
    {
        public int Id { get; set; } 

        [Required]
        public double Grade { get; set; }

        [MinLength(10), MaxLength(200)]
        public string Text { get; set; }

        public Accomodation Accomodation { get; set; }

        [ForeignKey("Accomodation")]
        public int AccomodationId { get; set; }

        public AppUser AppUser { get; set; }

        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
    }
}
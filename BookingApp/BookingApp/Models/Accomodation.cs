using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Accomodation
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public String Name { get; set; }

        [MaxLength(200)]
        public String Description { get; set; }

        public String Address { get; set; }

        public double AverageGrade { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public String ImageUrl { get; set; }

        public bool Approved { get; set; }

        public Place Place { get; set; }

        [ForeignKey("Place")]
        public int PlaceId { get; set; }

        public IList<Comment> Comments { get; set; }

        public AppUser Owner { get; set; }
  
        [ForeignKey("Owner")] // ??? pitati
        public int OwnerId { get; set; }

        public IList<Room> Rooms { get; set; }

        //[Required]
        public AccommodationType AccomodationType { get; set; }

        [ForeignKey("AccomodationType")]
        public int AccomodationTypeId { get; set; }

        public Accomodation()
        {
            this.Comments = new List<Comment>();
            this.Rooms = new List<Room>();
        }

    }
}
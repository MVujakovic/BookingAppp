using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }

        public IList<Comment> Comments { get; set; }
        public IList<Accomodation> Accomodations { get; set; }
        public IList<RoomReservations> RoomReservations { get; set; }

        public AppUser()
        {
            this.Comments = new List<Comment>();
            this.Accomodations = new List<Accomodation>();
            this.RoomReservations = new List<RoomReservations>();
        }
        public AppUser(string fName) : this() { FullName = fName; }
    }
}
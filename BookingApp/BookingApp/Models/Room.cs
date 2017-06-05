using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int BedCount { get; set; }

        public string Description { get; set; }

        public double PricePerNight { get; set; }

        public IList<RoomReservations> RoomReservations { get; set; }

        public Accomodation Accomodation { get; set; }

        [ForeignKey("Accomodation")]
        public int AccomodationId { get; set; }

    }
}
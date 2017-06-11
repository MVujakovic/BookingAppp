using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class RoomReservations
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime Timestamp { get; set; }

        public AppUser AppUser { get; set; }

        // napraviti kljuc kombianciju, to valjda ako nemas Id za kljuc,
        // tako nesto je spominjao i red kolona, nisam sigurna kako se to radi
        [ForeignKey("AppUser")]
        //[Key]
        //[Column(Order =1)]
        public int AppUserId { get; set; }

        public Room Room { get; set; }

        [ForeignKey("Room")]
        //[Key]
        //[Column(Order = 2)]
        public int RoomId { get; set; }       
    }
}
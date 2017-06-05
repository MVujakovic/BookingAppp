using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class AccommodationType
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public IList<Accomodation> Accomodations { get; set; }
    }
}
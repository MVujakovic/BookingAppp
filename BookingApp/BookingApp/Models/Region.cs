using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }

        
        public Country Country { get; set; }


        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public IList<Place> Places { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        //[Required]
        //[Column(TypeName = "VARCHAR")]
        //[StringLength(10)]
        [Index(IsUnique = true)]
        [MinLength(3), MaxLength(20)]
        public string Code { get; set; }

        public IList<Region> Regions { get; set; }

        public Country()
        {
            this.Regions = new List<Region>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Region
    {
        public int Id { get; set; }

        [Required]
        // [Key]
        //[Column(Order = 2)]
        [Index(IsUnique = true)]
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        // videti gde unique da dodas
        // pitanje ogranicenja donjih kardinaliteta, ono nullable??
        // [Index(IsUnique = true)]

        // da li ovde treba required?
        // i sta ce nam uopste ovo polje,
        // ako mi preko Id pristupamo tom objektu u bazi
        public Country Country { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public IList<Place> Places { get; set; }

        public Region()
        {
            this.Places = new List<Place>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookingApp.Models
{
    public class BAContext : IdentityDbContext<BAIdentityUser>
    {

        public virtual DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AccommodationType> AccomodationTypes { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Place> Places { get; set; }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomReservations> RoomReservations { get; set; }

        public BAContext() : base("name=DCDB")
        {
        }

        public static BAContext Create()
        {
            // proveriti kako ovo radi
            // teoretski bi trebalo da se izbrise baza, i napravi nova, ako su menjane model klase
            Database.SetInitializer<BAContext>(new DropCreateDatabaseIfModelChanges<BAContext>());

            return new BAContext();
        }
    }
}
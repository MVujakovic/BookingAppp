using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingApp.Controllers
{
    [RoutePrefix("api")]
    public class CountryController : ApiController
    {
        private BAContext db = new BAContext();

        [HttpGet]
        [Route("Countries")]
        public IQueryable<Country> m1()
        {
            List<Country> con = new List<Country>();
            Country c = new Country();
            c.Id = 1;
            c.Name = "Srbija";
            c.Regions = new List<Region>();
            Country c2 = new Country();
            c2.Id = 2;
            c2.Name = "Crna Gora";
            c2.Regions = new List<Region>();
            con.Add(c);
            con.Add(c2);
            return con.AsQueryable();

            //return db.Countries;
        }


    }
}

using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

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

        [HttpGet]
        [Route("Country/{id}")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult m2(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpPut]
        [Route("CountryMod/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult m3(int id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.Id)
            {
                return BadRequest();
            }

            db.Entry(country).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("CountryPost")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult m4(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Countries.Add(country);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = country.Id }, country);
        }

        [HttpDelete]
        [Route("CountryDelete/{id}")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult m5(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            db.Countries.Remove(country);
            db.SaveChanges();

            return Ok(country);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryExists(int id)
        {
            return db.Countries.Count(e => e.Id == id) > 0;
        }

    }
}

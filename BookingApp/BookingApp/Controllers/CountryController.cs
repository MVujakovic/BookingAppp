using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;

namespace BookingApp.Controllers
{
    [RoutePrefix("api")]
    public class CountryController : ApiController
    {
        private BAContext db = new BAContext();

        [HttpGet]
        [Route("Countries", Name ="CountryController")]
        public IQueryable<Country> m1()
        {
            return db.Countries;
            //return db.Countries.Include("Regions");
        }

        [HttpGet]
        [EnableQuery]
        [Route("Country/{id}")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult m2(int id)
        {
            //Country country = db.Countries.Find(id);
            Country country = db.Countries.Where(c => c.Id == id).Include("Regions").SingleOrDefault();

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // GET: api/Countries(5)/Regions
        [EnableQuery]
        public IQueryable<Region> GetRegions([FromODataUri] int key)
        {
            return db.Countries.Where(m => m.Id == key).SelectMany(m => m.Regions);
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

            //try
            //{
            db.Countries.Add(country);
            db.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
            //    var fullErrorMessage = string.Join("; ", errorMessages);
            //    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: \n\t", fullErrorMessage);

            //    Debug.Print(exceptionMessage);

            //    //throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

            //}



            return CreatedAtRoute("CountryController", new { id = country.Id }, country);
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

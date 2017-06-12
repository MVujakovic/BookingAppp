using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookingApp.Models;

namespace BookingApp.Controllers
{
    [RoutePrefix("api")]
    public class PlacesController : ApiController
    {
        private BAContext db = new BAContext();

        // GET: api/Places
        [HttpGet] //Ne znam mogu li da include i region i accomodations istovremeno???
        [Route("Places", Name ="PlacesController")]
        public IQueryable<Place> GetPlaces()
        {
            return db.Places.Include("Region");
            //return db.Places.Include("Accomodations");
        }

        // GET: api/Places
        [HttpGet]
        [Route("Places2", Name = "PlacesController2")] //Ne znam moze li se zvati PlacesController2, mada mozda mi to ni ne treba
        public IQueryable<Place> GetPlaces2()
        {
            return db.Places.Include("Accomodations");
        }

        // GET: api/Places/5
        [HttpGet]
        [Route("Places/{id}")]
        [ResponseType(typeof(Place))]
        public IHttpActionResult GetPlace(int id)
        {
            Place place = db.Places.Include("Accomodations").FirstOrDefault(p=>p.Id==id);
                                   
            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }

        // PUT: api/Places/5
        [HttpPut]
        [Route("PlacesMod/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlace(int id, Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != place.Id)
            {
                return BadRequest();
            }

            db.Entry(place).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
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

        // POST: api/Places
        [HttpPost]
        [Route("PlacesPost")]
        [ResponseType(typeof(Place))]
        public IHttpActionResult PostPlace(Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Places.Add(place);
            db.SaveChanges();

            return CreatedAtRoute("PlacesController", new { id = place.Id }, place);
        }

        // DELETE: api/Places/5
        [HttpDelete]
        [Route("PlacesDelete/{id}")]
        [ResponseType(typeof(Place))]
        public IHttpActionResult DeletePlace(int id)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return NotFound();
            }

            db.Places.Remove(place);
            db.SaveChanges();

            return Ok(place);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlaceExists(int id)
        {
            return db.Places.Count(e => e.Id == id) > 0;
        }
    }
}
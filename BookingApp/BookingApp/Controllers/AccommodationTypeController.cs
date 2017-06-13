using BookingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
    [RoutePrefix("api")]
    public class AccommodationTypeController : ApiController
    {
        private BAContext db = new BAContext();

        [HttpGet]
        [Route("AccomodationTypes",Name = "AccommodationTypeController")]
        public IQueryable<AccommodationType> m1()
        {
            return db.AccomodationTypes;
        }

        [HttpGet]
        [Route("AccomodationType/{id}")]
        public IHttpActionResult m2(int id)
        {
            AccommodationType at = db.AccomodationTypes.Find(id);
            if (at == null)
            {
                return NotFound();
            }
            return Ok(at);
        }

        [HttpPut]
        [Route("AccomodationTypeMod/{id}")]
        public IHttpActionResult m2(int id, AccommodationType at)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != at.Id)
            {
                return BadRequest();
            }

            db.Entry(at).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!AccommTypeExists(id))
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
        [Route("AccomodationTypePost")]
        [ResponseType(typeof(AccommodationType))]
        public IHttpActionResult m4(AccommodationType at)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccomodationTypes.Add(at);
            db.SaveChanges();

            return CreatedAtRoute("AccommodationTypeController", new { id = at.Id }, at);
        }

        [HttpDelete]
        [Route("AccommodationTypeDelete/{id}")]
        [ResponseType(typeof(AccommodationType))]
        public IHttpActionResult m5(int id)
        {
            AccommodationType at = db.AccomodationTypes.Find(id);
            if (at == null)
            {
                return NotFound();
            }

            db.AccomodationTypes.Remove(at);
            db.SaveChanges();

            return Ok(at);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private bool AccommTypeExists(int id)
        {
            return db.AccomodationTypes.Count(e => e.Id == id) > 0;
        }

    }
}

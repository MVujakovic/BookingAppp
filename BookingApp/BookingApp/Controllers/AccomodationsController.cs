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
using System.Web.Http.OData;

namespace BookingApp.Controllers
{
    [RoutePrefix("api")]
    public class AccomodationsController : ApiController
    {
        private BAContext db = new BAContext();

        // GET: api/Accomodations
        [HttpGet]
        [Route("Accomodations",Name = "AccomodationsController")]
        public IQueryable<Accomodation> GetAccomodations()
        {
            return db.Accomodations;
        }

        // GET: api/Accomodations2
        [HttpGet]
        [Route("Accomodations2", Name = "AccomodationsController2")]
        public IQueryable<Accomodation> GetAccomodationsWithRooms()
        {
            return db.Accomodations.Include("Rooms");
        }

        [HttpGet]
        [EnableQuery]
        [Route("AccomodationsByOwnerId/{id}")]
        public IQueryable<Accomodation> GetAccomodationByOwnerId(int id)
        {
            return db.Accomodations.Where(a => a.OwnerId == id);
        }

        [HttpGet]
        [EnableQuery]
        [Route("AccomodationsByGrade/{id}")]
        public IQueryable<Accomodation> GetAccomodationByGrade(int id)
        {
            return db.Accomodations.Where(a => (a.AverageGrade <= id && a.AverageGrade >= (id - 1))).Include("Rooms");
        }

        // GET: api/Accomodations/5
        [HttpGet]
        [EnableQuery]
        [Route("Accomodation/{id}")]
        [ResponseType(typeof(Accomodation))]
        public IHttpActionResult GetAccomodation(int id)
        {
            Accomodation accomodation = db.Accomodations.Where(a => a.Id == id).Include("Rooms").SingleOrDefault();
            //Accomodation accomodation = db.Accomodations.Find(id);
            if (accomodation == null)
            {
                return NotFound();
            }

            return Ok(accomodation);
        }

        [HttpGet]
        [EnableQuery]
        [Route("AccomodationReservationsByUser/{id}/{id2}")]
        public IQueryable<RoomReservations> GetRoomReservations(int id,int id2)
        {
            Accomodation acc = db.Accomodations.Where(a => a.Id == id).Include("Rooms").SingleOrDefault();
            //List<Room> rooms = new List<Room>();
            List<int> roomRes = new List<int>();
            List<RoomReservations> reservations = new List<RoomReservations>();
            reservations = db.RoomReservations.Where(ro => ro.AppUserId == id2).ToList();

            foreach (Room r in acc.Rooms)
            {
                foreach(RoomReservations rr in reservations)
                {
                    if (rr.RoomId == r.Id)
                    {
                        roomRes.Add(rr.Id);
                    }
                }
            }

            

            return db.RoomReservations.Where(a => roomRes.Contains(a.Id));
        }


        // PUT: api/AccomodationsMod/5
        [HttpPut]
        [Route("AccomodationsMod/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccomodation(int id, Accomodation accomodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accomodation.Id)
            {
                return BadRequest();
            }

            db.Entry(accomodation).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccomodationExists(id))
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



        // POST: api/AccomodationsPost
        [HttpPost]
        [Route("AccomodationsPost")]
        [ResponseType(typeof(Accomodation))]
        public IHttpActionResult PostAccomodation(Accomodation accomodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accomodations.Add(accomodation);
            db.SaveChanges();

            return CreatedAtRoute("AccomodationsController", new { id = accomodation.Id }, accomodation);
        }

        // DELETE: api/AccomodationsDelete/5
        [HttpDelete]
        [Route("AccomodationDelete/{id}")]
        [ResponseType(typeof(Accomodation))]
        public IHttpActionResult DeleteAccomodation(int id)
        {
            Accomodation accomodation = db.Accomodations.Find(id);
            if (accomodation == null)
            {
                return NotFound();
            }

            db.Accomodations.Remove(accomodation);
            db.SaveChanges();

            return Ok(accomodation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccomodationExists(int id)
        {
            return db.Accomodations.Count(e => e.Id == id) > 0;
        }
    }
}
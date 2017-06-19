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
    public class RoomReservationsController : ApiController
    {
        private BAContext db = new BAContext();

        // GET: api/RoomReservations
        [HttpGet]
        [Route("RoomReservations",Name = "RoomReservationsController")]
        public IQueryable<RoomReservations> GetRoomReservations()
        {
            //return db.RoomReservations;
            return db.RoomReservations.Include("AppUser");
        }

        [HttpGet]
        [EnableQuery]
        [Route("AccRoomRes/{id}")]
        public IQueryable<Accomodation> GetAccomodations(int id)
        {
            List<RoomReservations> roomRes = db.RoomReservations.Where(r => r.AppUserId == id).Include("Room").ToList();
            List<Room> rooms = new List<Room>();
            List<Accomodation> accomodations = new List<Accomodation>();
            List<int> accIds = new List<int>();
            foreach (RoomReservations res in roomRes)
            {
                rooms.Add(db.Rooms.Where(r => r.Id == res.RoomId).Include("Accomodation").SingleOrDefault());
            }

            if (rooms.Count == 0)
            {
                IQueryable<Accomodation> ret = Enumerable.Empty<Accomodation>().AsQueryable();
                return ret;
            }
            else
            {
                foreach (Room ro in rooms)
                {
                    accomodations.Add(db.Accomodations.Where(a => a.Id == ro.AccomodationId).SingleOrDefault());
                }

                foreach (Accomodation a in accomodations)
                {
                    accIds.Add(a.Id);
                }

                return db.Accomodations.Where(a => accIds.Contains(a.Id));
            }
        }

        [HttpGet]
        [EnableQuery]
        [Route("RoomResByUserId/{id}")]
        public IQueryable<Accomodation> GetAccomodation(int id)
        {
            List<RoomReservations> roomRes = db.RoomReservations.Where(r => r.AppUserId == id).Include("Room").ToList();
            List<Room> rooms = new List<Room>();
            List<Accomodation> accomodations = new List<Accomodation>();
            List<int> accIds = new List<int>();
            foreach (RoomReservations res in roomRes)
            {
                if (res.EndDate < DateTime.Now)
                {
                    rooms.Add(db.Rooms.Where(r => r.Id == res.RoomId ).Include("Accomodation").SingleOrDefault());
                }             
            }

            if (rooms.Count == 0)
            {
                IQueryable<Accomodation> ret = Enumerable.Empty<Accomodation>().AsQueryable();
                return ret;
            }
            else
            {
                foreach (Room ro in rooms)
                {
                    accomodations.Add(db.Accomodations.Where(a => a.Id == ro.AccomodationId).SingleOrDefault());
                }

                foreach (Accomodation a in accomodations)
                {
                    accIds.Add(a.Id);
                }

                return db.Accomodations.Where(a => accIds.Contains(a.Id));
            }

            
        }

        // GET: api/RoomReservations/5
        [HttpGet]
        [Route("RoomReservation/{id}")]
        [ResponseType(typeof(RoomReservations))]
        public IHttpActionResult GetRoomReservations(int id)
        {
            RoomReservations roomReservations = db.RoomReservations.Find(id);
            if (roomReservations == null)
            {
                return NotFound();
            }

            return Ok(roomReservations);
        }

        // PUT: api/RoomReservations/5
        [HttpPut]
        [Route("RoomReservationMod/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoomReservations(int id, RoomReservations roomReservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomReservations.Id)
            {
                return BadRequest();
            }

            db.Entry(roomReservations).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomReservationsExists(id))
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

        // POST: api/RoomReservations
        [HttpPost]
        [Route("RoomReservationPost")]
        [ResponseType(typeof(RoomReservations))]
        public IHttpActionResult PostRoomReservations(RoomReservations roomReservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoomReservations.Add(roomReservations);
            db.SaveChanges();

            return CreatedAtRoute("RoomReservationsController", new { id = roomReservations.Id }, roomReservations);
        }

        // DELETE: api/RoomReservations/5
        [HttpDelete]
        [Route("RoomReservationDelete/{id}")]
        [ResponseType(typeof(RoomReservations))]
        public IHttpActionResult DeleteRoomReservations(int id)
        {
            RoomReservations roomReservations = db.RoomReservations.Find(id);
            if (roomReservations == null)
            {
                return NotFound();
            }

            db.RoomReservations.Remove(roomReservations);
            db.SaveChanges();

            return Ok(roomReservations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomReservationsExists(int id)
        {
            return db.RoomReservations.Count(e => e.Id == id) > 0;
        }
    }
}
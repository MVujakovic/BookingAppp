using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using BookingApp.Models;

namespace BookingApp.OData
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller.
    Merge these statements into the Register method of the WebApiConfig class as applicable. 
    Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using BookingApp.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Accomodation>("Accomodations");
    builder.EntitySet<AccommodationType>("AccomodationTypes"); 
    builder.EntitySet<Comment>("Comments"); 
    builder.EntitySet<AppUser>("AppUsers"); 
    builder.EntitySet<Place>("Places"); 
    builder.EntitySet<Room>("Rooms"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AccomodationsController : ODataController
    {
        private BAContext db = new BAContext();

        // GET: odata/Accomodations
        [EnableQuery]
        public IQueryable<Accomodation> GetAccomodations()
        {
            return db.Accomodations;
        }

        // GET: odata/Accomodations(5)
        [EnableQuery]
        public SingleResult<Accomodation> GetAccomodation([FromODataUri] int key)
        {
            return SingleResult.Create(db.Accomodations.Where(accomodation => accomodation.Id == key));
        }

        // PUT: odata/Accomodations(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Accomodation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Accomodation accomodation = db.Accomodations.Find(key);
            if (accomodation == null)
            {
                return NotFound();
            }

            patch.Put(accomodation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccomodationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accomodation);
        }

        // POST: odata/Accomodations
        public IHttpActionResult Post(Accomodation accomodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accomodations.Add(accomodation);
            db.SaveChanges();

            return Created(accomodation);
        }

        // PATCH: odata/Accomodations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Accomodation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Accomodation accomodation = db.Accomodations.Find(key);
            if (accomodation == null)
            {
                return NotFound();
            }

            patch.Patch(accomodation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccomodationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(accomodation);
        }

        // DELETE: odata/Accomodations(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Accomodation accomodation = db.Accomodations.Find(key);
            if (accomodation == null)
            {
                return NotFound();
            }

            db.Accomodations.Remove(accomodation);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Accomodations(5)/AccomodationType
        [EnableQuery]
        public SingleResult<AccommodationType> GetAccomodationType([FromODataUri] int key)
        {
            return SingleResult.Create(db.Accomodations.Where(m => m.Id == key).Select(m => m.AccomodationType));
        }

        // GET: odata/Accomodations(5)/Comments
        [EnableQuery]
        public IQueryable<Comment> GetComments([FromODataUri] int key)
        {
            return db.Accomodations.Where(m => m.Id == key).SelectMany(m => m.Comments);
        }

        // GET: odata/Accomodations(5)/Owner
        [EnableQuery]
        public SingleResult<AppUser> GetOwner([FromODataUri] int key)
        {
            return SingleResult.Create(db.Accomodations.Where(m => m.Id == key).Select(m => m.Owner));
        }

        // GET: odata/Accomodations(5)/Place
        [EnableQuery]
        public SingleResult<Place> GetPlace([FromODataUri] int key)
        {
            return SingleResult.Create(db.Accomodations.Where(m => m.Id == key).Select(m => m.Place));
        }

        // GET: odata/Accomodations(5)/Rooms
        [EnableQuery]
        public IQueryable<Room> GetRooms([FromODataUri] int key)
        {
            return db.Accomodations.Where(m => m.Id == key).SelectMany(m => m.Rooms);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccomodationExists(int key)
        {
            return db.Accomodations.Count(e => e.Id == key) > 0;
        }
    }
}

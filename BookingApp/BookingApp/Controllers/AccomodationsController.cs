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
            return db.Accomodations.Include("Place");
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


        // METODA UBICA!!! 

        //// srediti metodu, dodati try-catch  http://www.c-sharpcorner.com/article/uploading-image-to-server-using-web-api-2-0/
        //// POST: api/AccomodationsPost
        //[HttpPost]
        //[Route("AccomodationsPost")]
        //[ResponseType(typeof(Accomodation))]
        //public IHttpActionResult PostAccomodation(Accomodation accomodation)
        //{
        //    Accomodation accommodation = new Accomodation();

        //    // Check if the request contains multipart/form-data. // za debug mi ovo treba
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        //throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = db.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    //    var userRole = user.Roles.First().RoleId;
        //    //    var role = BAContext.Roles.FirstOrDefault(r => r.Id == userRole);
        //    //    AppUser appUser = BAContext.AppUsers.Where(au => au.Id == user.appUserId).FirstOrDefault();

        //    var httpRequest = HttpContext.Current.Request;
        //    try
        //    {
        //        accommodation = JsonConvert.DeserializeObject<Accomodation>(httpRequest.Form[0]);
        //    }
        //    catch (Exception e)
        //    {

        //        Console.Write(e.Message);
        //        return BadRequest(ModelState);
        //    }

        //    foreach (string file in httpRequest.Files)
        //    {
        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

        //        var postedFile = httpRequest.Files[file];
        //        if (postedFile != null && postedFile.ContentLength > 0)
        //        {

        //            IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png", ".jpeg" };
        //            var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
        //            var extension = ext.ToLower();
        //            if (!AllowedFileExtensions.Contains(extension))
        //            {
        //                return BadRequest();
        //            }
        //            else
        //            {
        //                var filePath = HttpContext.Current.Server.MapPath("~/Content/Images/" + postedFile.FileName);
        //                accommodation.ImageUrl = "Content/Images/" + postedFile.FileName;
        //                postedFile.SaveAs(filePath);
        //            }
        //        }
        //    }

        //    db.Accomodations.Add(accomodation);
        //    //ContextHelper.SaveChanges(db);        
        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbEntityValidationException)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    catch (DbUpdateException)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    //foreach (string file in httpRequest.Files)
        //    //{
        //    //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);


        //    //    var postedFile = httpRequest.Files[file];
        //    //    if (postedFile != null && postedFile.ContentLength > 0)
        //    //    {

        //    //        //int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB 

        //    //        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".png", ".jpeg" };
        //    //        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
        //    //        var extension = ext.ToLower();

        //    //        if (!AllowedFileExtensions.Contains(extension))
        //    //        {
        //    //            var message = string.Format("Please Upload image of type .jpg,.png,.jpeg");
        //    //            return BadRequest(message);

        //    //        }
        //    //        /*
        //    //        Object { message: "The request entity's media type 'multipart/form-data' 
        //    //        is not supported for this resource.", exceptionMessage: 
        //    //        "No MediaTypeFormatter is available to read an obje…om content with 
        //    //        media type 'multipart/form-data'.", exceptionType.....
        //    //       */
        //    //        //else if (postedFile.ContentLength > MaxContentLength)
        //    //        //{
        //    //        //    var message = string.Format("Please Upload a file upto 1 mb.");
        //    //        //    return BadRequest(message);
        //    //        //}
        //    //        else
        //    //        {
        //    //            var filePath = HttpContext.Current.Server.MapPath("~/Content/Images/" + postedFile.FileName);
        //    //            //var filePath = HttpContext.Current.Server.MapPath("~/Content/Images/" + postedFile.FileName+extension);
        //    //            accommodation.ImageUrl = "Content/Images/" + postedFile.FileName;
        //    //            //accommodation.ImageUrl = "Content/Images/" + postedFile.FileName+extension;
        //    //            postedFile.SaveAs(filePath);
        //    //        }
        //    //    }
        //    //}



        //    return CreatedAtRoute("AccomodationsController", new { id = accomodation.Id }, accomodation);
        //}


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
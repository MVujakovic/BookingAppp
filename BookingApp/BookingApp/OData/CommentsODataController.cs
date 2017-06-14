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
using System.Web.Http.Description;

namespace BookingApp.OData
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using BookingApp.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Comment>("Comments");
    builder.EntitySet<Accomodation>("Accomodations"); 
    builder.EntitySet<AppUser>("AppUsers"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    [RoutePrefix("api")]
    public class CommentsODataController : ODataController
    {
        private BAContext db = new BAContext();

        // GET: odata/Comments
        [HttpGet]
        [Route("CommentsOData", Name = "CommentsODataController")]
        [EnableQuery]
        public IQueryable<Comment> GetComments()
        {
            return db.Comments;
        }

        // GET: odata/Comments(5)
        [HttpGet]
        [Route("CommentsOData/{id}")]
        [ResponseType(typeof(Comment))]
        [EnableQuery]
        public SingleResult<Comment> GetComment([FromODataUri] int key)
        {
            return SingleResult.Create(db.Comments.Where(comment => comment.Id == key));
        }

        // PUT: odata/Comments(5)
        [HttpPut]
        [Route("CommentsODataMod/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put([FromODataUri] int key, Delta<Comment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Comment comment = db.Comments.Find(key);
            if (comment == null)
            {
                return NotFound();
            }

            patch.Put(comment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(comment);
        }

        // POST: odata/Comments
        [HttpPost]
        [Route("CommentODataPost")]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult Post(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(comment);
            db.SaveChanges();

            return Created(comment);
        }

        // PATCH: odata/Comments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Comment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Comment comment = db.Comments.Find(key);
            if (comment == null)
            {
                return NotFound();
            }

            patch.Patch(comment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(comment);
        }

        // DELETE: odata/Comments(5)
        [HttpDelete]
        [Route("CommentODataDelete/{id}")]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Comment comment = db.Comments.Find(key);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Comments(5)/Accomodation
        [EnableQuery]
        public SingleResult<Accomodation> GetAccomodation([FromODataUri] int key)
        {
            return SingleResult.Create(db.Comments.Where(m => m.Id == key).Select(m => m.Accomodation));
        }

        // GET: odata/Comments(5)/AppUser
        [EnableQuery]
        public SingleResult<AppUser> GetAppUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Comments.Where(m => m.Id == key).Select(m => m.AppUser));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int key)
        {
            return db.Comments.Count(e => e.Id == key) > 0;
        }
    }
}

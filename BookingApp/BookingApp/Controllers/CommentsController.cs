﻿using System;
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
using System.Data.Entity.Migrations;

namespace BookingApp.Controllers
{
    [RoutePrefix("api")]
    public class CommentsController : ApiController
    {
        private BAContext db = new BAContext();

        // GET: api/Comments
        [HttpGet]
        [Route("Comments", Name = "CommentsController")]
        public IQueryable<Comment> GetComments()
        {
            //return db.Comments;
            return db.Comments.Include("Accomodation"); //da se prikaze i accomodation kom priprada komentar
        }

        [HttpGet]
        [EnableQuery]
        [Route("CommentsByUserId/{id}")]
        public IQueryable<Comment> GetCommentsByUserId(int id)
        {
            return db.Comments.Where(c => c.AppUserId == id).Include("AppUser").Include("Accomodation");
        }

        // GET: api/Comments/5
        [HttpGet]
        [Route("Comments/{id}")]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Comments/5
        [HttpPut]
        [Route("CommentsMod/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }

            //db.Entry(comment).State = EntityState.Modified;

            db.Comments.AddOrUpdate(comment);
            ContextHelper.SaveChanges(db);
            //db.SaveChanges();

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        [HttpPost]
        [Route("CommentPost")]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PostComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(comment);
            db.SaveChanges();

            return CreatedAtRoute("CommentsController", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete]
        [Route("CommentDelete/{id}")]
        [ResponseType(typeof(Comment))]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.Id == id) > 0;
        }
    }
}
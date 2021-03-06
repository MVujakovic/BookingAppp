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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace BookingApp.Controllers
{
    [RoutePrefix("api")]
    public class AppUsersController : ApiController
    {
        private BAContext db = new BAContext();


       // User manager -> We will use it to check role if needed.
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set { _userManager = value; }
        }


        /*
         IdentityResult result = UserManager.Create(usr);
            await UserManager.AddToRoleAsync(usr.Id, "User");

             if (!result.Succeeded)
            {
                return BadRequest();
            }
        */

        // GET: api/AppUsers
        [HttpGet]
        [Route("AppUsers",Name ="AppUsersController")]
        [Authorize(Roles="Admin")] // provera da li je role admin
        public IQueryable<AppUser> GetAppUsers()
        {
            return db.AppUsers;
            //return db.AppUsers.Include("Comments");
        }

        // GET: api/AppUsers2
        [HttpGet]
        [Route("AppUsers2", Name = "AppUsersController2")]
        [Authorize(Roles = "Admin")] // provera da li je role admin
        public IQueryable<AppUser> GetAppUsers2()
        {
            //return db.AppUsers;
            return db.AppUsers.Include("Accomodations");
        }

        // GET: api/AppUsers3
        [HttpGet]
        [Route("AppUsers3", Name = "AppUsersController3")]
        [Authorize(Roles = "Admin")] // provera da li je role admin
        public IQueryable<AppUser> GetAppUsers3()
        {
            //return db.AppUsers;
            return db.AppUsers.Include("RoomReservations");
        }

        // GET: api/AppUsers/5
        [HttpGet]
        [Route("AppUsers/{id}")]
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult GetAppUser(int id)
        {

            // User.Identity.Name => Username (Identity User-a)! UserManager trazi po njegovom username-u, i onda poredi! 
            bool isAdmin = UserManager.IsInRole(User.Identity.Name, "Admin");

            // Vadimo iz Identity baze po username-u Identity User-a, koji u sebi sadrzi AppUser-a!
            // User je current principal associated with request
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            // Ako korisnik nije admin, i nije AppUser koji trazi podatke o sebi, nije autorizovan!
            if (isAdmin || (user != null && user.appUserId.Equals(id)))
            {
                AppUser appUser = db.AppUsers.Find(id);
                if (appUser == null)
                {
                    return NotFound();
                }
                return Ok(appUser);
            }

            return Unauthorized();         
        }

        // PUT: api/AppUsers/5
        [HttpPut]
        [Route("AppUsersMod/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppUser(int id, AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appUser.Id)
            {
                return BadRequest();
            }

            db.Entry(appUser).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
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

        // POST: api/AppUsers
        [HttpPost]
        [Route("AppUsersPost")]
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult PostAppUser(AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppUsers.Add(appUser);
            db.SaveChanges();

            return CreatedAtRoute("AppUsersController", new { id = appUser.Id }, appUser);
        }

        // DELETE: api/AppUsers/5
        [HttpDelete]
        [Route("AppUsersDelete/{id}")]
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult DeleteAppUser(int id)
        {
            AppUser appUser = db.AppUsers.Find(id);
            if (appUser == null)
            {
                return NotFound();
            }

            db.AppUsers.Remove(appUser);
            db.SaveChanges();

            return Ok(appUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppUserExists(int id)
        {
            return db.AppUsers.Count(e => e.Id == id) > 0;
        }
    }
}
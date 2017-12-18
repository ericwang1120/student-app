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
using student_api.Models;

namespace student_api.Controllers
{
    public class HobbiesController : ApiController
    {
        private StudentContext db = new StudentContext();

        // GET: api/Hobbies
        public IQueryable<Hobby> GetHobbies()
        {
            return db.Hobbies;
        }

        // GET: api/Hobbies/5
        [ResponseType(typeof(Hobby))]
        public IHttpActionResult GetHobby(int id)
        {
            Hobby hobby = db.Hobbies.Find(id);
            if (hobby == null)
            {
                return NotFound();
            }

            return Ok(hobby);
        }

        // PUT: api/Hobbies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHobby(int id, Hobby hobby)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hobby.Id)
            {
                return BadRequest();
            }

            db.Entry(hobby).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HobbyExists(id))
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

        // POST: api/Hobbies
        [ResponseType(typeof(Hobby))]
        public IHttpActionResult PostHobby(Hobby hobby)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hobbies.Add(hobby);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hobby.Id }, hobby);
        }

        // DELETE: api/Hobbies/5
        [ResponseType(typeof(Hobby))]
        public IHttpActionResult DeleteHobby(int id)
        {
            Hobby hobby = db.Hobbies.Find(id);
            if (hobby == null)
            {
                return NotFound();
            }

            db.Hobbies.Remove(hobby);
            db.SaveChanges();

            return Ok(hobby);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HobbyExists(int id)
        {
            return db.Hobbies.Count(e => e.Id == id) > 0;
        }
    }
}
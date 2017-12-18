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
	public class StudentsController : ApiController
	{
		private StudentContext db = new StudentContext();

		// GET: api/Students
		public IQueryable<Student> GetStudents()
		{
			return db.Students.AsQueryable();
		}

		// GET: api/Students/5
		[ResponseType(typeof(Student))]
		public IHttpActionResult GetStudent(int id)
		{
			Student student = db.Students.Find(id);
			if (student == null)
			{
				return NotFound();
			}

			return Ok(student);
		}

		// PUT: api/Students/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutStudent(int id, Student student)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != student.Id)
			{
				return BadRequest();
			}

			var currentState = db.Students.Include(x => x.Hobbies).Single(x => x.Id == student.Id);

			// remove all hobbies
			currentState.Hobbies = new List<Hobby>();
			// insert new hobbies
			foreach (var hobby in student.Hobbies)
			{
				var hobbyToTrack = db.Hobbies.FirstOrDefault(x => x.Id == hobby.Id);
				db.Hobbies.Attach(hobbyToTrack);
				currentState.Hobbies.Add(hobbyToTrack);
			}

			// update other properties
			currentState.Name = student.Name;
			currentState.Address = student.Address;
			currentState.Gender = student.Gender;


			//db.Entry(student).State = EntityState.Modified;


			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!StudentExists(id))
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

		// POST: api/Students
		[ResponseType(typeof(Student))]
		public IHttpActionResult PostStudent(Student student)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			foreach (var hobby in student.Hobbies.ToList())
			{
				var hobbyToTrack = db.Hobbies.FirstOrDefault(x => x.Id == hobby.Id);
				db.Hobbies.Attach(hobbyToTrack);
				student.Hobbies.Remove(hobby);
				student.Hobbies.Add(hobbyToTrack);
			}

			db.Students.Add(student);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = student.Id }, student);
		}

		// DELETE: api/Students/5
		[ResponseType(typeof(Student))]
		public IHttpActionResult DeleteStudent(int id)
		{
			Student student = db.Students.Find(id);
			if (student == null)
			{
				return NotFound();
			}

			db.Students.Remove(student);
			db.SaveChanges();

			return Ok(student);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool StudentExists(int id)
		{
			return db.Students.Count(e => e.Id == id) > 0;
		}
	}
}
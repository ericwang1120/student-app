using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace student_api.Models
{
	public class Hobby
	{
		public Hobby()
		{
			this.Students = new HashSet<Student>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<Student> Students { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace student_api.Models
{
	public class Student
	{
		public Student()
		{
			this.Hobbies = new HashSet<Hobby>();
		}
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public EnumGender Gender { get; set; }
		public string Address { get; set; }
		public virtual ICollection<Hobby> Hobbies { get; set; }

	}

	public enum EnumGender
	{
		male,
		female,
		others
	}
}
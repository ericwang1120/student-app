namespace student_api.Migrations
{
	using student_api.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<student_api.Models.StudentContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(student_api.Models.StudentContext context)
		{

			var hobby1 = new Hobby() { Id = 1, Name = "Music" };
			var hobby2 = new Hobby() { Id = 2, Name = "Computer" };
			var hobby3 = new Hobby() { Id = 3, Name = "Basketball" };

			context.Hobbies.AddOrUpdate(x => x.Id,
				hobby1,
				hobby2,
				hobby3
			);

			context.Students.AddOrUpdate(x => x.Id,
				new Student() { Id = 1, Name = "John", Gender = EnumGender.male, Hobbies = new List<Hobby>() { hobby1, hobby2 } },
				new Student() { Id = 2, Name = "micha", Gender = EnumGender.female },
				new Student() { Id = 3, Name = "John2", Gender = EnumGender.others },
				new Student() { Id = 4, Name = "Scoot", Gender = EnumGender.male },
				new Student() { Id = 5, Name = "MAT", Gender = EnumGender.male },
				new Student() { Id = 6, Name = "Tom", Gender = EnumGender.male },
				new Student() { Id = 7, Name = "Tim", Gender = EnumGender.male },
				new Student() { Id = 8, Name = "Alan", Gender = EnumGender.male }
			);
		}
	}
}

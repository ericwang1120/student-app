using System.Data.Entity;


namespace student_api.Models
{
	public class StudentContext : DbContext
	{
		public StudentContext() : base("name=DefaultConnection")
		{
		}
		public DbSet<Student> Students { get; set; }
		public DbSet<Hobby> Hobbies { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
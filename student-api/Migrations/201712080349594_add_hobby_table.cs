namespace student_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_hobby_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hobbies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentHobbies",
                c => new
                    {
                        Student_Id = c.Int(nullable: false),
                        Hobby_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Hobby_Id })
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.Hobbies", t => t.Hobby_Id, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Hobby_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentHobbies", "Hobby_Id", "dbo.Hobbies");
            DropForeignKey("dbo.StudentHobbies", "Student_Id", "dbo.Students");
            DropIndex("dbo.StudentHobbies", new[] { "Hobby_Id" });
            DropIndex("dbo.StudentHobbies", new[] { "Student_Id" });
            DropTable("dbo.StudentHobbies");
            DropTable("dbo.Hobbies");
        }
    }
}

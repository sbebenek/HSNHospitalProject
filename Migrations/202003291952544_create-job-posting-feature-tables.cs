namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createjobpostingfeaturetables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        departmentId = c.Int(nullable: false, identity: true),
                        departmentName = c.String(),
                    })
                .PrimaryKey(t => t.departmentId);
            
            CreateTable(
                "dbo.JobPosts",
                c => new
                    {
                        jobPostId = c.Int(nullable: false, identity: true),
                        jobPostType = c.String(),
                        jobPostName = c.String(),
                        departmentId = c.Int(nullable: false),
                        jobPostExperience = c.Int(nullable: false),
                        jobPostFilled = c.Boolean(nullable: false),
                        jobPostSalary = c.Single(nullable: false),
                        jobPostPostedDate = c.DateTime(nullable: false),
                        jobPostClosedDate = c.DateTime(nullable: false),
                        jobPostContent = c.String(),
                    })
                .PrimaryKey(t => t.jobPostId)
                .ForeignKey("dbo.Departments", t => t.departmentId, cascadeDelete: true)
                .Index(t => t.departmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobPosts", "departmentId", "dbo.Departments");
            DropIndex("dbo.JobPosts", new[] { "departmentId" });
            DropTable("dbo.JobPosts");
            DropTable("dbo.Departments");
        }
    }
}

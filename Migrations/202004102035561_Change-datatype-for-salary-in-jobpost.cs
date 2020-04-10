namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedatatypeforsalaryinjobpost : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobPosts", "jobPostSalary", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobPosts", "jobPostSalary", c => c.Single(nullable: false));
        }
    }
}

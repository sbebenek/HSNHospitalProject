namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnullablefortheclosedDateinjobPosttable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobPosts", "jobPostClosedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobPosts", "jobPostClosedDate", c => c.DateTime(nullable: false));
        }
    }
}

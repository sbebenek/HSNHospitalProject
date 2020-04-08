namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changerecorddatetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActivityRecords", "activityrecorddate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ActivityRecords", "activityrecorddate", c => c.String());
        }
    }
}

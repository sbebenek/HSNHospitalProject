namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class working : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "doctorWorkingDays", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "doctorWorkingDays");
        }
    }
}

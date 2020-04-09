namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "doctorPNumber", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Patients", "patientPNumber", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "patientPNumber", c => c.String());
            AlterColumn("dbo.Doctors", "doctorPNumber", c => c.String());
        }
    }
}

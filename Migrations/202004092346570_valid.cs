namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class valid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patients", "patientPNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "patientPNumber", c => c.String(nullable: false, maxLength: 30));
        }
    }
}

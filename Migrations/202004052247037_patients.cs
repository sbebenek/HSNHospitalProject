namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patients : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        appointmentId = c.Int(nullable: false, identity: true),
                        appointmentDate = c.DateTime(nullable: false),
                        appointmentReferenceNumebr = c.String(),
                        appointmentReason = c.String(),
                        doctorId = c.Int(nullable: false),
                        patientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.appointmentId)
                .ForeignKey("dbo.Doctors", t => t.doctorId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.patientId)
                .Index(t => t.doctorId)
                .Index(t => t.patientId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        doctorId = c.Int(nullable: false, identity: true),
                        doctorFName = c.String(),
                        doctorLName = c.String(),
                        doctorDOB = c.DateTime(nullable: false),
                        doctorPNumber = c.String(),
                        doctorEAddress = c.String(),
                        doctorJoinDate = c.DateTime(nullable: false),
                        departmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.doctorId)
                .ForeignKey("dbo.Departments", t => t.departmentId, cascadeDelete: true)
                .Index(t => t.departmentId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        patientId = c.String(nullable: false, maxLength: 128),
                        patientFName = c.String(),
                        patientLName = c.String(),
                        patientDOB = c.String(),
                        patientEAddress = c.String(),
                        patientPNumber = c.String(),
                        patientHAddress = c.String(),
                        patientHealthCard = c.String(),
                    })
                .PrimaryKey(t => t.patientId)
                .ForeignKey("dbo.AspNetUsers", t => t.patientId)
                .Index(t => t.patientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "patientId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "patientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Doctors", "departmentId", "dbo.Departments");
            DropForeignKey("dbo.Appointments", "doctorId", "dbo.Doctors");
            DropIndex("dbo.Patients", new[] { "patientId" });
            DropIndex("dbo.Doctors", new[] { "departmentId" });
            DropIndex("dbo.Appointments", new[] { "patientId" });
            DropIndex("dbo.Appointments", new[] { "doctorId" });
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
            DropTable("dbo.Appointments");
        }
    }
}

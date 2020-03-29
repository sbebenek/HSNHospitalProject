namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createsamstables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityRecords",
                c => new
                    {
                        activityrecordid = c.Int(nullable: false, identity: true),
                        activityrecorddate = c.String(nullable: false),
                        activityrecordrating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.activityrecordid);
            
            CreateTable(
                "dbo.GalleryImages",
                c => new
                    {
                        galleryimageid = c.Int(nullable: false, identity: true),
                        galleryimageref = c.String(),
                        galleryimagetitle = c.String(),
                        galleryimagealt = c.String(),
                        galleryimagedate = c.DateTime(nullable: false),
                        galleryimagedescription = c.String(),
                    })
                .PrimaryKey(t => t.galleryimageid);
            
            AddColumn("dbo.AspNetUsers", "is_admin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "is_admin");
            DropTable("dbo.GalleryImages");
            DropTable("dbo.ActivityRecords");
        }
    }
}

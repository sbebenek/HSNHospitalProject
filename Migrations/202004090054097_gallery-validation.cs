namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class galleryvalidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GalleryImages", "galleryimagetitle", c => c.String(nullable: false));
            AlterColumn("dbo.GalleryImages", "galleryimagealt", c => c.String(nullable: false));
            AlterColumn("dbo.GalleryImages", "galleryimagedescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GalleryImages", "galleryimagedescription", c => c.String());
            AlterColumn("dbo.GalleryImages", "galleryimagealt", c => c.String());
            AlterColumn("dbo.GalleryImages", "galleryimagetitle", c => c.String());
        }
    }
}

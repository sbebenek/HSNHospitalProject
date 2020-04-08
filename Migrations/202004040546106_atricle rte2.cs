namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atriclerte2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "articleBody", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "articleBody", c => c.String(nullable: false));
        }
    }
}

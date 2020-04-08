namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableeditdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "articleDateLastEdit", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "articleDateLastEdit", c => c.DateTime(nullable: false));
        }
    }
}

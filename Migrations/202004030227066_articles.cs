namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class articles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        articleId = c.Int(nullable: false, identity: true),
                        articleTitle = c.String(nullable: false),
                        articleBody = c.String(nullable: false),
                        articleDatePosted = c.DateTime(nullable: false),
                        articleDateLastEdit = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.articleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Articles");
        }
    }
}

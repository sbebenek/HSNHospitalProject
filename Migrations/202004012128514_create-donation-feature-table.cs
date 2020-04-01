namespace HSNHospitalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdonationfeaturetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        donationId = c.Int(nullable: false, identity: true),
                        donationName = c.String(),
                        donationAmount = c.Int(nullable: false),
                        donationEmail = c.String(),
                        donationAnonymous = c.Boolean(nullable: false),
                        donationReceiptId = c.String(),
                    })
                .PrimaryKey(t => t.donationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Donations");
        }
    }
}

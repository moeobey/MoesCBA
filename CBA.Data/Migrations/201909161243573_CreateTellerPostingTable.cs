namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTellerPostingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TellerPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerAccountId = c.Int(nullable: false),
                        PostingType = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Narration = c.String(),
                        PostedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerAccounts", t => t.CustomerAccountId, cascadeDelete: true)
                .Index(t => t.CustomerAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TellerPosts", "CustomerAccountId", "dbo.CustomerAccounts");
            DropIndex("dbo.TellerPosts", new[] { "CustomerAccountId" });
            DropTable("dbo.TellerPosts");
        }
    }
}

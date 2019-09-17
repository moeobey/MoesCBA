namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCOTPostsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.COTPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountToDebitId = c.Int(nullable: false),
                        AccountToCreditId = c.Int(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PostedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlAccounts", t => t.AccountToCreditId)
                .ForeignKey("dbo.CustomerAccounts", t => t.AccountToDebitId, cascadeDelete: true)
                .Index(t => t.AccountToDebitId)
                .Index(t => t.AccountToCreditId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.COTPosts", "AccountToDebitId", "dbo.CustomerAccounts");
            DropForeignKey("dbo.COTPosts", "AccountToCreditId", "dbo.GlAccounts");
            DropIndex("dbo.COTPosts", new[] { "AccountToCreditId" });
            DropIndex("dbo.COTPosts", new[] { "AccountToDebitId" });
            DropTable("dbo.COTPosts");
        }
    }
}

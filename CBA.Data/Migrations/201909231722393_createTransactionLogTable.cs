namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTransactionLogTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountToDebitId = c.Int(),
                        AccountToCreditId = c.Int(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Narration = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlAccounts", t => t.AccountToCreditId)
                .ForeignKey("dbo.CustomerAccounts", t => t.AccountToDebitId)
                .Index(t => t.AccountToDebitId)
                .Index(t => t.AccountToCreditId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionLogs", "AccountToDebitId", "dbo.CustomerAccounts");
            DropForeignKey("dbo.TransactionLogs", "AccountToCreditId", "dbo.GlAccounts");
            DropIndex("dbo.TransactionLogs", new[] { "AccountToCreditId" });
            DropIndex("dbo.TransactionLogs", new[] { "AccountToDebitId" });
            DropTable("dbo.TransactionLogs");
        }
    }
}

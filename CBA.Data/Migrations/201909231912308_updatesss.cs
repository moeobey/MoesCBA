namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesss : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionLogs", "AccountToCreditId", "dbo.GlAccounts");
            DropForeignKey("dbo.TransactionLogs", "AccountToDebitId", "dbo.CustomerAccounts");
            DropIndex("dbo.TransactionLogs", new[] { "AccountToDebitId" });
            DropIndex("dbo.TransactionLogs", new[] { "AccountToCreditId" });
            AddColumn("dbo.SavingsAccountConfigs", "InterestPayableGlId", c => c.Int());
            AddColumn("dbo.TransactionLogs", "AccountToDebitCategory", c => c.String());
            AddColumn("dbo.TransactionLogs", "AccountToCreditCategory", c => c.String());
            CreateIndex("dbo.SavingsAccountConfigs", "InterestPayableGlId");
            AddForeignKey("dbo.SavingsAccountConfigs", "InterestPayableGlId", "dbo.GlAccounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavingsAccountConfigs", "InterestPayableGlId", "dbo.GlAccounts");
            DropIndex("dbo.SavingsAccountConfigs", new[] { "InterestPayableGlId" });
            DropColumn("dbo.TransactionLogs", "AccountToCreditCategory");
            DropColumn("dbo.TransactionLogs", "AccountToDebitCategory");
            DropColumn("dbo.SavingsAccountConfigs", "InterestPayableGlId");
            CreateIndex("dbo.TransactionLogs", "AccountToCreditId");
            CreateIndex("dbo.TransactionLogs", "AccountToDebitId");
            AddForeignKey("dbo.TransactionLogs", "AccountToDebitId", "dbo.CustomerAccounts", "Id");
            AddForeignKey("dbo.TransactionLogs", "AccountToCreditId", "dbo.GlAccounts", "Id");
        }
    }
}

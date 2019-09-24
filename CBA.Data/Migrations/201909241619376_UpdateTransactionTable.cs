namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTransactionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionLogs", "Name", c => c.String());
            AddColumn("dbo.TransactionLogs", "MainAccountCategory", c => c.Int(nullable: false));
            AddColumn("dbo.TransactionLogs", "TransactionType", c => c.Int(nullable: false));
            DropColumn("dbo.TransactionLogs", "AccountToDebitCategory");
            DropColumn("dbo.TransactionLogs", "AccountToDebitId");
            DropColumn("dbo.TransactionLogs", "AccountToCreditCategory");
            DropColumn("dbo.TransactionLogs", "AccountToCreditId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionLogs", "AccountToCreditId", c => c.Int());
            AddColumn("dbo.TransactionLogs", "AccountToCreditCategory", c => c.String());
            AddColumn("dbo.TransactionLogs", "AccountToDebitId", c => c.Int());
            AddColumn("dbo.TransactionLogs", "AccountToDebitCategory", c => c.String());
            DropColumn("dbo.TransactionLogs", "TransactionType");
            DropColumn("dbo.TransactionLogs", "MainAccountCategory");
            DropColumn("dbo.TransactionLogs", "Name");
        }
    }
}

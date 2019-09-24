namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTransactionTables : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TransactionLogs", "Narration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransactionLogs", "Narration", c => c.String());
        }
    }
}

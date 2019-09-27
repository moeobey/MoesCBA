namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTransctions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionLogs", "AccountCode", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransactionLogs", "AccountCode");
        }
    }
}

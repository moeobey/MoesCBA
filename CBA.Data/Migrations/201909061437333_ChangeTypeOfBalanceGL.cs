namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfBalanceGL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlAccounts", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GlAccounts", "Balance");
        }
    }
}

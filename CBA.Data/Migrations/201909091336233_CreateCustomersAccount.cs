namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCustomersAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlAccounts", "AccountType", c => c.Int(nullable: false));
            AddColumn("dbo.GlAccounts", "IsOpen", c => c.Boolean(nullable: false));
            AddColumn("dbo.GlAccounts", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.GlAccounts", "Interest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GlAccounts", "Interest");
            DropColumn("dbo.GlAccounts", "CreatedAt");
            DropColumn("dbo.GlAccounts", "IsOpen");
            DropColumn("dbo.GlAccounts", "AccountType");
        }
    }
}

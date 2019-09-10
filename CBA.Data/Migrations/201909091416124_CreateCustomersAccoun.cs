namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCustomersAccoun : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.GlAccounts", "AccountType");
            DropColumn("dbo.GlAccounts", "IsOpen");
            DropColumn("dbo.GlAccounts", "CreatedAt");
            DropColumn("dbo.GlAccounts", "Interest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GlAccounts", "Interest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.GlAccounts", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.GlAccounts", "IsOpen", c => c.Boolean(nullable: false));
            AddColumn("dbo.GlAccounts", "AccountType", c => c.Int(nullable: false));
        }
    }
}

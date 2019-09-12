namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckIfGlsTill : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlAccounts", "IsTillAccount", c => c.Boolean(nullable: false));
            DropColumn("dbo.GlAccounts", "IsAssignableToTeller");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GlAccounts", "IsAssignableToTeller", c => c.Boolean(nullable: false));
            DropColumn("dbo.GlAccounts", "IsTillAccount");
        }
    }
}

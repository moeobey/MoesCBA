namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckIfGlIsAssignableToTeller : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlAccounts", "IsAssignableToTeller", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GlAccounts", "IsAssignableToTeller");
        }
    }
}

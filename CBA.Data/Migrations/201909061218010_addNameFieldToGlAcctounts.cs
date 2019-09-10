namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNameFieldToGlAcctounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlAccounts", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GlAccounts", "Name");
        }
    }
}

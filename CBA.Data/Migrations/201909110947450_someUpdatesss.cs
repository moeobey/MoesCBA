namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someUpdatesss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GlAccounts", "AccountCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GlAccounts", "AccountCode", c => c.String());
        }
    }
}

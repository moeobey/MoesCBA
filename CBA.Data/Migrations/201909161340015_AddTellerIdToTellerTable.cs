namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTellerIdToTellerTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TellerPosts", "TellerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TellerPosts", "TellerId");
        }
    }
}

namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTellerId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.COTPosts", "TellerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.COTPosts", "TellerId");
        }
    }
}

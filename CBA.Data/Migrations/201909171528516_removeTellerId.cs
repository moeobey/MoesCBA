namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeTellerId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.COTPosts", "TellerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.COTPosts", "TellerId", c => c.Int(nullable: false));
        }
    }
}

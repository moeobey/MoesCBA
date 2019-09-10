namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsAssignedToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "isAssigned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "isAssigned");
        }
    }
}

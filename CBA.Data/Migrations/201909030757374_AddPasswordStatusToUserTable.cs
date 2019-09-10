namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPasswordStatusToUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswordStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PasswordStatus");
        }
    }
}

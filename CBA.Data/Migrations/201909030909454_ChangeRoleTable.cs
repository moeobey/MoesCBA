namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRoleTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Role", c => c.String());
            DropColumn("dbo.Users", "RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Role");
        }
    }
}

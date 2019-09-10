namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRolesToUse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "UserRoleId" });
            AddColumn("dbo.Users", "Role", c => c.String());
            DropColumn("dbo.Users", "UserRoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserRoleId", c => c.Int());
            DropColumn("dbo.Users", "Role");
            CreateIndex("dbo.Users", "UserRoleId");
            AddForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles", "Id");
        }
    }
}

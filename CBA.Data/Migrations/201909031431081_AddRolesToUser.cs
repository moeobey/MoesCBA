namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRolesToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserRoleId", c => c.Int());
            CreateIndex("dbo.Users", "UserRoleId");
            AddForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "UserRoleId" });
            DropColumn("dbo.Users", "UserRoleId");
        }
    }
}

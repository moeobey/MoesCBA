namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBranchToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "BranchId", c => c.Int());
            CreateIndex("dbo.Users", "BranchId");
            AddForeignKey("dbo.Users", "BranchId", "dbo.Branches", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "BranchId", "dbo.Branches");
            DropIndex("dbo.Users", new[] { "BranchId" });
            DropColumn("dbo.Users", "BranchId");
        }
    }
}

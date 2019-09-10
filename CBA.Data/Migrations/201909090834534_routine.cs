namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class routine : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tellers", "UserId", "dbo.Users");
            DropIndex("dbo.Tellers", new[] { "UserId" });
            AlterColumn("dbo.Tellers", "UserId", c => c.Int());
            CreateIndex("dbo.Tellers", "UserId");
            AddForeignKey("dbo.Tellers", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tellers", "UserId", "dbo.Users");
            DropIndex("dbo.Tellers", new[] { "UserId" });
            AlterColumn("dbo.Tellers", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tellers", "UserId");
            AddForeignKey("dbo.Tellers", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}

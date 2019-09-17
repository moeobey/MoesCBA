namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropDuplicate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TillAccounts", "GlAccountId", "dbo.GlAccounts");
            DropForeignKey("dbo.TillAccounts", "UserId", "dbo.Users");
            DropIndex("dbo.TillAccounts", new[] { "UserId" });
            DropIndex("dbo.TillAccounts", new[] { "GlAccountId" });
            DropTable("dbo.TillAccounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TillAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        GlAccountId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TillAccounts", "GlAccountId");
            CreateIndex("dbo.TillAccounts", "UserId");
            AddForeignKey("dbo.TillAccounts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TillAccounts", "GlAccountId", "dbo.GlAccounts", "Id", cascadeDelete: true);
        }
    }
}

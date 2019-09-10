namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTillAccount : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlAccounts", t => t.GlAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GlAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TillAccounts", "UserId", "dbo.Users");
            DropForeignKey("dbo.TillAccounts", "GlAccountId", "dbo.GlAccounts");
            DropIndex("dbo.TillAccounts", new[] { "GlAccountId" });
            DropIndex("dbo.TillAccounts", new[] { "UserId" });
            DropTable("dbo.TillAccounts");
        }
    }
}

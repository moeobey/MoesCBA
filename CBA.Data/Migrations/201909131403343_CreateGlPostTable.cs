namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGlPostTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GlPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GlAccountToCreditId = c.Int(),
                        GlAccountToDebitId = c.Int(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Narration = c.String(),
                        UserId = c.Int(nullable: false),
                        PostedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlAccounts", t => t.GlAccountToCreditId)
                .ForeignKey("dbo.GlAccounts", t => t.GlAccountToDebitId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.GlAccountToCreditId)
                .Index(t => t.GlAccountToDebitId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlPosts", "UserId", "dbo.Users");
            DropForeignKey("dbo.GlPosts", "GlAccountToDebitId", "dbo.GlAccounts");
            DropForeignKey("dbo.GlPosts", "GlAccountToCreditId", "dbo.GlAccounts");
            DropIndex("dbo.GlPosts", new[] { "UserId" });
            DropIndex("dbo.GlPosts", new[] { "GlAccountToDebitId" });
            DropIndex("dbo.GlPosts", new[] { "GlAccountToCreditId" });
            DropTable("dbo.GlPosts");
        }
    }
}

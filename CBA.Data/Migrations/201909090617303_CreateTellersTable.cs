namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTellersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tellers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GlAccountId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GlAccounts", t => t.GlAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.GlAccountId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tellers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tellers", "GlAccountId", "dbo.GlAccounts");
            DropIndex("dbo.Tellers", new[] { "UserId" });
            DropIndex("dbo.Tellers", new[] { "GlAccountId" });
            DropTable("dbo.Tellers");
        }
    }
}

namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGlAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GlAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GlAccountCategoryId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        AccountCode = c.String(),
                        IsAssigned = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.GlAccountCategories", t => t.GlAccountCategoryId, cascadeDelete: true)
                .Index(t => t.GlAccountCategoryId)
                .Index(t => t.BranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlAccounts", "GlAccountCategoryId", "dbo.GlAccountCategories");
            DropForeignKey("dbo.GlAccounts", "BranchId", "dbo.Branches");
            DropIndex("dbo.GlAccounts", new[] { "BranchId" });
            DropIndex("dbo.GlAccounts", new[] { "GlAccountCategoryId" });
            DropTable("dbo.GlAccounts");
        }
    }
}

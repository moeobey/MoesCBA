namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoringGlCategory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GlAccountCategories", newName: "GlCategories");
            RenameColumn(table: "dbo.GlAccounts", name: "GlAccountCategoryId", newName: "GlCategoryId");
            RenameIndex(table: "dbo.GlAccounts", name: "IX_GlAccountCategoryId", newName: "IX_GlCategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.GlAccounts", name: "IX_GlCategoryId", newName: "IX_GlAccountCategoryId");
            RenameColumn(table: "dbo.GlAccounts", name: "GlCategoryId", newName: "GlAccountCategoryId");
            RenameTable(name: "dbo.GlCategories", newName: "GlAccountCategories");
        }
    }
}

namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfAccountCodes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.GlPosts", new[] { "GlAccountToCredit_Id" });
            DropIndex("dbo.GlPosts", new[] { "GlAccountToDebit_Id" });
            DropColumn("dbo.GlPosts", "GlAccountToCreditId");
            DropColumn("dbo.GlPosts", "GlAccountToDebitId");
            RenameColumn(table: "dbo.GlPosts", name: "GlAccountToCredit_Id", newName: "GlAccountToCreditId");
            RenameColumn(table: "dbo.GlPosts", name: "GlAccountToDebit_Id", newName: "GlAccountToDebitId");
            AlterColumn("dbo.GlPosts", "GlAccountToCreditId", c => c.Int());
            AlterColumn("dbo.GlPosts", "GlAccountToDebitId", c => c.Int());
            CreateIndex("dbo.GlPosts", "GlAccountToCreditId");
            CreateIndex("dbo.GlPosts", "GlAccountToDebitId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.GlPosts", new[] { "GlAccountToDebitId" });
            DropIndex("dbo.GlPosts", new[] { "GlAccountToCreditId" });
            AlterColumn("dbo.GlPosts", "GlAccountToDebitId", c => c.Long());
            AlterColumn("dbo.GlPosts", "GlAccountToCreditId", c => c.Long());
            RenameColumn(table: "dbo.GlPosts", name: "GlAccountToDebitId", newName: "GlAccountToDebit_Id");
            RenameColumn(table: "dbo.GlPosts", name: "GlAccountToCreditId", newName: "GlAccountToCredit_Id");
            AddColumn("dbo.GlPosts", "GlAccountToDebitId", c => c.Long());
            AddColumn("dbo.GlPosts", "GlAccountToCreditId", c => c.Long());
            CreateIndex("dbo.GlPosts", "GlAccountToDebit_Id");
            CreateIndex("dbo.GlPosts", "GlAccountToCredit_Id");
        }
    }
}

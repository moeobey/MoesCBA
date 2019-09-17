namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfAccountCodeid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GlPosts", "GlAccountToCreditId", "dbo.GlAccounts");
            DropForeignKey("dbo.GlPosts", "GlAccountToDebitId", "dbo.GlAccounts");
            DropIndex("dbo.GlPosts", new[] { "GlAccountToCreditId" });
            DropIndex("dbo.GlPosts", new[] { "GlAccountToDebitId" });
            AddColumn("dbo.GlPosts", "GlAccountToCredit_Id", c => c.Int());
            AddColumn("dbo.GlPosts", "GlAccountToDebit_Id", c => c.Int());
            AlterColumn("dbo.GlPosts", "GlAccountToCreditId", c => c.Long());
            AlterColumn("dbo.GlPosts", "GlAccountToDebitId", c => c.Long());
            CreateIndex("dbo.GlPosts", "GlAccountToCredit_Id");
            CreateIndex("dbo.GlPosts", "GlAccountToDebit_Id");
            AddForeignKey("dbo.GlPosts", "GlAccountToCredit_Id", "dbo.GlAccounts", "Id");
            AddForeignKey("dbo.GlPosts", "GlAccountToDebit_Id", "dbo.GlAccounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GlPosts", "GlAccountToDebit_Id", "dbo.GlAccounts");
            DropForeignKey("dbo.GlPosts", "GlAccountToCredit_Id", "dbo.GlAccounts");
            DropIndex("dbo.GlPosts", new[] { "GlAccountToDebit_Id" });
            DropIndex("dbo.GlPosts", new[] { "GlAccountToCredit_Id" });
            AlterColumn("dbo.GlPosts", "GlAccountToDebitId", c => c.Int());
            AlterColumn("dbo.GlPosts", "GlAccountToCreditId", c => c.Int());
            DropColumn("dbo.GlPosts", "GlAccountToDebit_Id");
            DropColumn("dbo.GlPosts", "GlAccountToCredit_Id");
            CreateIndex("dbo.GlPosts", "GlAccountToDebitId");
            CreateIndex("dbo.GlPosts", "GlAccountToCreditId");
            AddForeignKey("dbo.GlPosts", "GlAccountToDebitId", "dbo.GlAccounts", "Id");
            AddForeignKey("dbo.GlPosts", "GlAccountToCreditId", "dbo.GlAccounts", "Id");
        }
    }
}

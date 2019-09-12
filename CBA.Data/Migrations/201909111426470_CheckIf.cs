namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckIf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavingsAccountConfigs", "GlAccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.SavingsAccountConfigs", "GlAccountId");
            AddForeignKey("dbo.SavingsAccountConfigs", "GlAccountId", "dbo.GlAccounts", "Id", cascadeDelete: true);
            DropColumn("dbo.SavingsAccountConfigs", "InterestExpenseGlId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SavingsAccountConfigs", "InterestExpenseGlId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SavingsAccountConfigs", "GlAccountId", "dbo.GlAccounts");
            DropIndex("dbo.SavingsAccountConfigs", new[] { "GlAccountId" });
            DropColumn("dbo.SavingsAccountConfigs", "GlAccountId");
        }
    }
}

namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NamingConvention : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanAccountConfigs", "GlAccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.CurrentAccountConfigs", "InterestExpenseGlId");
            CreateIndex("dbo.LoanAccountConfigs", "GlAccountId");
            AddForeignKey("dbo.CurrentAccountConfigs", "InterestExpenseGlId", "dbo.GlAccounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoanAccountConfigs", "GlAccountId", "dbo.GlAccounts", "Id", cascadeDelete: true);
            DropColumn("dbo.LoanAccountConfigs", "InterestIncomeGlId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanAccountConfigs", "InterestIncomeGlId", c => c.Int(nullable: false));
            DropForeignKey("dbo.LoanAccountConfigs", "GlAccountId", "dbo.GlAccounts");
            DropForeignKey("dbo.CurrentAccountConfigs", "InterestExpenseGlId", "dbo.GlAccounts");
            DropIndex("dbo.LoanAccountConfigs", new[] { "GlAccountId" });
            DropIndex("dbo.CurrentAccountConfigs", new[] { "InterestExpenseGlId" });
            DropColumn("dbo.LoanAccountConfigs", "GlAccountId");
        }
    }
}

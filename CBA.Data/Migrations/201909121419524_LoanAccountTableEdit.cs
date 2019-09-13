namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoanAccountTableEdit : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.LoanAccountConfigs", name: "GlAccountId", newName: "InterestIncomeGlId");
            RenameIndex(table: "dbo.LoanAccountConfigs", name: "IX_GlAccountId", newName: "IX_InterestIncomeGlId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.LoanAccountConfigs", name: "IX_InterestIncomeGlId", newName: "IX_GlAccountId");
            RenameColumn(table: "dbo.LoanAccountConfigs", name: "InterestIncomeGlId", newName: "GlAccountId");
        }
    }
}

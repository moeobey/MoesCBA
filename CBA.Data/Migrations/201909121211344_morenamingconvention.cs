namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class morenamingconvention : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SavingsAccountConfigs", name: "GlAccountId", newName: "InterestExpenseGlId");
            RenameIndex(table: "dbo.SavingsAccountConfigs", name: "IX_GlAccountId", newName: "IX_InterestExpenseGlId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SavingsAccountConfigs", name: "IX_InterestExpenseGlId", newName: "IX_GlAccountId");
            RenameColumn(table: "dbo.SavingsAccountConfigs", name: "InterestExpenseGlId", newName: "GlAccountId");
        }
    }
}

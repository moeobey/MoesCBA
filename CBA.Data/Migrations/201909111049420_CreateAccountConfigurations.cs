namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAccountConfigurations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentAccountConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CInterestRate = c.Int(nullable: false),
                        MinBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InterestExpenseGlId = c.Int(nullable: false),
                        COT = c.Int(nullable: false),
                        COTIncomeGlId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoanAccountConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DInterestRate = c.Int(nullable: false),
                        InterestIncomeGlId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SavingsAccountConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CInterestRate = c.Int(nullable: false),
                        MinBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InterestExpenseGlId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SavingsAccountConfigs");
            DropTable("dbo.LoanAccountConfigs");
            DropTable("dbo.CurrentAccountConfigs");
        }
    }
}

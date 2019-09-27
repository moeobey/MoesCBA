namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDecimalPrecisionForAccountConfig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CurrentAccountConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 20, scale: 2));
            AlterColumn("dbo.LoanAccountConfigs", "DInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 2));
            AlterColumn("dbo.SavingsAccountConfigs", "CInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 2));
            AlterColumn("dbo.SavingsAccountConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 20, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SavingsAccountConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.SavingsAccountConfigs", "CInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.LoanAccountConfigs", "DInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CurrentAccountConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 20, scale: 10));
        }
    }
}

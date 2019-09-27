namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDecimalPrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.COTPosts", "Amount", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.GlAccounts", "Balance", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CustomerAccounts", "Balance", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CustomerAccounts", "Lien", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CustomerAccounts", "Interest", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CurrentAccountConfigs", "CInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CurrentAccountConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CurrentAccountConfigs", "COT", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.GlPosts", "Amount", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.LoanAccountConfigs", "DInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "LoanAmount", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "LoanAmountRemaining", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "LoanPayable", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "Interest", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "InterestRemaining", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.Loans", "InterestPayable", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.SavingsAccountConfigs", "CInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.SavingsAccountConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.TellerPosts", "Amount", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.TransactionLogs", "Amount", c => c.Decimal(nullable: false, precision: 20, scale: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransactionLogs", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TellerPosts", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SavingsAccountConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SavingsAccountConfigs", "CInterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "InterestPayable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "InterestRemaining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "Interest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "LoanPayable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "LoanAmountRemaining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Loans", "LoanAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.LoanAccountConfigs", "DInterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.GlPosts", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CurrentAccountConfigs", "COT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CurrentAccountConfigs", "MinBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CurrentAccountConfigs", "CInterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CustomerAccounts", "Interest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CustomerAccounts", "Lien", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CustomerAccounts", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.GlAccounts", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.COTPosts", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

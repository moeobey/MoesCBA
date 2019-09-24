namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateLoansTabel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "LoanAmountRemaining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Loans", "LoanRepayable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Loans", "Interest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Loans", "InterestRemaining", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Loans", "InterestRepayable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Loans", "LoanTerms", c => c.Int());
            AddColumn("dbo.Loans", "IsLoanPaymentComplete", c => c.Boolean(nullable: false));
            DropColumn("dbo.Loans", "InterestRate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Loans", "InterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Loans", "IsLoanPaymentComplete");
            DropColumn("dbo.Loans", "LoanTerms");
            DropColumn("dbo.Loans", "InterestRepayable");
            DropColumn("dbo.Loans", "InterestRemaining");
            DropColumn("dbo.Loans", "Interest");
            DropColumn("dbo.Loans", "LoanRepayable");
            DropColumn("dbo.Loans", "LoanAmountRemaining");
        }
    }
}

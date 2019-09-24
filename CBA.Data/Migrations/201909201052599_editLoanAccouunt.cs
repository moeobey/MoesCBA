namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editLoanAccouunt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "LoanPayable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Loans", "LoanRepayable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Loans", "LoanRepayable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Loans", "LoanPayable");
        }
    }
}

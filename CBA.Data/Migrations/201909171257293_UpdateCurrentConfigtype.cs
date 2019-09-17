namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCurrentConfigtype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CurrentAccountConfigs", "CInterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.LoanAccountConfigs", "DInterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoanAccountConfigs", "DInterestRate", c => c.Int(nullable: false));
            AlterColumn("dbo.CurrentAccountConfigs", "CInterestRate", c => c.Int(nullable: false));
        }
    }
}

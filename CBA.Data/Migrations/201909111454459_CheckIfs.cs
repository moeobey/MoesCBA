namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckIfs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SavingsAccountConfigs", "CInterestRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SavingsAccountConfigs", "CInterestRate", c => c.Int(nullable: false));
        }
    }
}

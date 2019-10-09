namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setMorePrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CurrentAccountConfigs", "CInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 2));
            AlterColumn("dbo.CurrentAccountConfigs", "COT", c => c.Decimal(nullable: false, precision: 20, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CurrentAccountConfigs", "COT", c => c.Decimal(nullable: false, precision: 20, scale: 10));
            AlterColumn("dbo.CurrentAccountConfigs", "CInterestRate", c => c.Decimal(nullable: false, precision: 20, scale: 10));
        }
    }
}

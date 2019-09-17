namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCurrentConfig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CurrentAccountConfigs", "COT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CurrentAccountConfigs", "COT", c => c.Int(nullable: false));
        }
    }
}

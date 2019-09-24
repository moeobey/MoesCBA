namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RollBackChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Loans", "DurationInMonths", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Loans", "DurationInMonths", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

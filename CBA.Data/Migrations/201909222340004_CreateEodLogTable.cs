namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEodLogTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "InterestPayable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Loans", "DayCount", c => c.Int(nullable: false));
            AddColumn("dbo.Loans", "AccountBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Loans", "InterestRepayable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Loans", "InterestRepayable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Loans", "AccountBalance");
            DropColumn("dbo.Loans", "DayCount");
            DropColumn("dbo.Loans", "InterestPayable");
        }
    }
}

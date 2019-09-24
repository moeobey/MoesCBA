namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditLoanTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Loans", "AccountBalance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Loans", "AccountBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

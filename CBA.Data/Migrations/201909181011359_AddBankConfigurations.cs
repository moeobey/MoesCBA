namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBankConfigurations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankConfigurations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FinancialDate = c.DateTime(nullable: false),
                        IsBusinessOpen = c.Boolean(nullable: false),
                        DayCount = c.Int(nullable: false),
                        MonthCount = c.Int(nullable: false),
                        YearCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BankConfigurations");
        }
    }
}

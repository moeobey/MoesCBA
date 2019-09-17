namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCOTPostsTab : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.COTPosts", "AccountToDebitId", "dbo.CustomerAccounts");
            DropIndex("dbo.COTPosts", new[] { "AccountToDebitId" });
            AlterColumn("dbo.COTPosts", "AccountToDebitId", c => c.Int());
            CreateIndex("dbo.COTPosts", "AccountToDebitId");
            AddForeignKey("dbo.COTPosts", "AccountToDebitId", "dbo.CustomerAccounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.COTPosts", "AccountToDebitId", "dbo.CustomerAccounts");
            DropIndex("dbo.COTPosts", new[] { "AccountToDebitId" });
            AlterColumn("dbo.COTPosts", "AccountToDebitId", c => c.Int(nullable: false));
            CreateIndex("dbo.COTPosts", "AccountToDebitId");
            AddForeignKey("dbo.COTPosts", "AccountToDebitId", "dbo.CustomerAccounts", "Id", cascadeDelete: true);
        }
    }
}

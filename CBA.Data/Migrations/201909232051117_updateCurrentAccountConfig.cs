namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCurrentAccountConfig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurrentAccountConfigs", "InterestPayableGlId", c => c.Int());
            CreateIndex("dbo.CurrentAccountConfigs", "InterestPayableGlId");
            AddForeignKey("dbo.CurrentAccountConfigs", "InterestPayableGlId", "dbo.GlAccounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurrentAccountConfigs", "InterestPayableGlId", "dbo.GlAccounts");
            DropIndex("dbo.CurrentAccountConfigs", new[] { "InterestPayableGlId" });
            DropColumn("dbo.CurrentAccountConfigs", "InterestPayableGlId");
        }
    }
}

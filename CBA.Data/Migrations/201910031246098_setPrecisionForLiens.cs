namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setPrecisionForLiens : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerAccounts", "Lien", c => c.Decimal(nullable: false, precision: 20, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerAccounts", "Lien", c => c.Decimal(nullable: false, precision: 20, scale: 10));
        }
    }
}

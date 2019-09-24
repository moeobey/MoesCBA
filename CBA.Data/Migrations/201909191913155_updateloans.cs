namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateloans : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "Name", c => c.String());
            AddColumn("dbo.Loans", "AccountNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "AccountNumber");
            DropColumn("dbo.Loans", "Name");
        }
    }
}

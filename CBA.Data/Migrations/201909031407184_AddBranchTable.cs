namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBranchTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "BranchId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "BranchId", c => c.String());
        }
    }
}

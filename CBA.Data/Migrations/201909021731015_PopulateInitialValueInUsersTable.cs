namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateInitialValueInUsersTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Date", c => c.DateTime(nullable: false));
        }
    }
}

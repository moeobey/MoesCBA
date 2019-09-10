namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakePasswordDefaultFalse : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "PasswordStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PasswordStatus", c => c.Boolean());
        }
    }
}

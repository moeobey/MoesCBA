namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeNullables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Date", c => c.DateTime());
            AlterColumn("dbo.Users", "PasswordStatus", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PasswordStatus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "Date", c => c.DateTime(nullable: false));
        }
    }
}

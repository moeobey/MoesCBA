namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class routineUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TellerPosts", "PostType", c => c.Int(nullable: false));
            DropColumn("dbo.TellerPosts", "PostingType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TellerPosts", "PostingType", c => c.Int(nullable: false));
            DropColumn("dbo.TellerPosts", "PostType");
        }
    }
}

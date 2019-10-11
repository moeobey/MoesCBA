namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO UserRoles(RoleName) VALUES('Admin')");
            Sql("INSERT INTO UserRoles(RoleName) VALUES('Teller')");

        }

        public override void Down()
        {
        }
    }
}

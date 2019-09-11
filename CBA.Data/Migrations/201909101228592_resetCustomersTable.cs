namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resetCustomersTable : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE Customers");
        }
        
        public override void Down()
        {
        }
    }
}

namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Populatebranchtable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Branches( Name) VALUES('Yaba')");
            Sql("INSERT INTO Branches( Name) VALUES('Lagos')");

        }

        public override void Down()
        {
        }
    }
}

namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSuperAdmin : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT into User(FullName, BranchId, Email,Username, PhoneNumber, Password, Date ), VALUES()");
        }
        
        public override void Down()
        {
        }
    }
}

using System.Data.Entity.ModelConfiguration.Configuration;

namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedAdmin : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Users(FullName, Email,Username,PhoneNumber,Password,PasswordStatus,BranchId,Role) VALUES('moyin obey','moyinobey@gmail.com','moe','08099277645','5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8','true', '1', 'Admin')");
        }
        
        public override void Down()
        {
        }
    }
}

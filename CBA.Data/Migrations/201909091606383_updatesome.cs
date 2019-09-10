namespace CBA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesome : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerAccounts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerAccounts", new[] { "CustomerId" });
            AddColumn("dbo.CustomerAccounts", "Customer_Id", c => c.Int());
            AlterColumn("dbo.CustomerAccounts", "CustomerId", c => c.String());
            CreateIndex("dbo.CustomerAccounts", "Customer_Id");
            AddForeignKey("dbo.CustomerAccounts", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAccounts", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.CustomerAccounts", new[] { "Customer_Id" });
            AlterColumn("dbo.CustomerAccounts", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.CustomerAccounts", "Customer_Id");
            CreateIndex("dbo.CustomerAccounts", "CustomerId");
            AddForeignKey("dbo.CustomerAccounts", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}

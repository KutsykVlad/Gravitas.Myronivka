namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerIdStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("ext.Stock", "CustomerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("ext.Stock", "CustomerId");
        }
    }
}

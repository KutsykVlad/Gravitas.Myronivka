namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductTitleToSingleWindowOpData : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.SingleWindowOpData", "ProductTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("opd.SingleWindowOpData", "ProductTitle");
        }
    }
}

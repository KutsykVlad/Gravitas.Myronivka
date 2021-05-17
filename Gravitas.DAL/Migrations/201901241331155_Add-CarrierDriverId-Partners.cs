namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarrierDriverIdPartners : DbMigration
    {
        public override void Up()
        {
            AddColumn("ext.Partner", "CarrierDriverId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("ext.Partner", "CarrierDriverId");
        }
    }
}

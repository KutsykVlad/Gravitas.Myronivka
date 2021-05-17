namespace Gravitas.PreRegistration.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Partnenamefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegisteredTrucks", "PartnerName", c => c.String());
            AddColumn("dbo.AspNetUsers", "PartnerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PartnerName");
            DropColumn("dbo.RegisteredTrucks", "PartnerName");
        }
    }
}

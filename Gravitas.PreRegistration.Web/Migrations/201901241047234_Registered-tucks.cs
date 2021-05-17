namespace Gravitas.PreRegistration.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Registeredtucks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegisteredTrucks",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PartnerId = c.String(),
                        TruckNo = c.String(),
                        TrailerNo = c.String(),
                        PhoneNo = c.String(),
                        RouteId = c.Long(nullable: false),
                        EntranceTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RegisteredTrucks");
        }
    }
}

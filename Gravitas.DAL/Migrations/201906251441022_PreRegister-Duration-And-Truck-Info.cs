namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreRegisterDurationAndTruckInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreRegisterProduct", "RouteTimeInMinutes", c => c.Int(nullable: false));
            AddColumn("dbo.PreRegisterQueue", "TruckNumber", c => c.String());
            AddColumn("dbo.PreRegisterQueue", "Notice", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreRegisterQueue", "Notice");
            DropColumn("dbo.PreRegisterQueue", "TruckNumber");
            DropColumn("dbo.PreRegisterProduct", "RouteTimeInMinutes");
        }
    }
}

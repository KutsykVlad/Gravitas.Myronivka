namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSecondaryRouteFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ticket", "SecondaryRouteTemplateId", c => c.Long());
            AddColumn("dbo.Ticket", "SecondaryRouteItemIndex", c => c.Int(nullable: false));
            AddColumn("dbo.Ticket", "RouteType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ticket", "RouteType");
            DropColumn("dbo.Ticket", "SecondaryRouteItemIndex");
            DropColumn("dbo.Ticket", "SecondaryRouteTemplateId");
        }
    }
}

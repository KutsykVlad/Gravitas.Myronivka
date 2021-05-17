namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsInQueueRouteTemplate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteTemplate", "IsInQueue", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RouteTemplate", "IsInQueue");
        }
    }
}

namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RouteEditorChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RouteTemplate", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.RouteTemplate", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.RouteTemplate", "OwnerId", c => c.String());
            AddColumn("dbo.RouteTemplate", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.RouteTemplate", "Tickets", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RouteTemplate", "Tickets");
            DropColumn("dbo.RouteTemplate", "Status");
            DropColumn("dbo.RouteTemplate", "OwnerId");
            DropColumn("dbo.RouteTemplate", "CreatedOn");
            DropColumn("dbo.RouteTemplate", "UpdatedAt");
        }
    }
}

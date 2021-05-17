namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRouteTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RouteMap",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NodeId = c.Long(nullable: false),
                        RouteTemplateId = c.Long(nullable: false),
                        StatusId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId, cascadeDelete: true)
                .ForeignKey("dbo.RouteTemplate", t => t.RouteTemplateId, cascadeDelete: true)
                .ForeignKey("dbo.RouteMapStatus", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.NodeId)
                .Index(t => t.RouteTemplateId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.RouteMapStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Ticket", "RouteTemplateId", c => c.Long());
            CreateIndex("dbo.Ticket", "RouteTemplateId");
            AddForeignKey("dbo.Ticket", "RouteTemplateId", "dbo.RouteTemplate", "Id");
            DropColumn("dbo.Ticket", "Route");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ticket", "Route", c => c.String());
            DropForeignKey("dbo.Ticket", "RouteTemplateId", "dbo.RouteTemplate");
            DropForeignKey("dbo.RouteMap", "StatusId", "dbo.RouteMapStatus");
            DropForeignKey("dbo.RouteMap", "RouteTemplateId", "dbo.RouteTemplate");
            DropForeignKey("dbo.RouteMap", "NodeId", "dbo.Node");
            DropIndex("dbo.RouteMap", new[] { "StatusId" });
            DropIndex("dbo.RouteMap", new[] { "RouteTemplateId" });
            DropIndex("dbo.RouteMap", new[] { "NodeId" });
            DropIndex("dbo.Ticket", new[] { "RouteTemplateId" });
            DropColumn("dbo.Ticket", "RouteTemplateId");
            DropTable("dbo.RouteMapStatus");
            DropTable("dbo.RouteMap");
        }
    }
}

namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUnusedFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RouteMap", "NodeId", "dbo.Node");
            DropForeignKey("dbo.RouteMap", "RouteTemplateId", "dbo.RouteTemplate");
            DropForeignKey("dbo.RouteMap", "StatusId", "dbo.RouteMapStatus");
            DropIndex("dbo.RouteMap", new[] { "NodeId" });
            DropIndex("dbo.RouteMap", new[] { "RouteTemplateId" });
            DropIndex("dbo.RouteMap", new[] { "StatusId" });
            AddColumn("dbo.RouteTemplate", "IsMain", c => c.Boolean(nullable: false));
            DropColumn("dbo.RouteTemplate", "Status");
            DropColumn("dbo.RouteTemplate", "Tickets");
            DropTable("dbo.RouteMap");
            DropTable("dbo.RouteMapStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RouteMapStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RouteMap",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NodeId = c.Long(nullable: false),
                        RouteTemplateId = c.Long(nullable: false),
                        StatusId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RouteTemplate", "Tickets", c => c.Int(nullable: false));
            AddColumn("dbo.RouteTemplate", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.RouteTemplate", "IsMain");
            CreateIndex("dbo.RouteMap", "StatusId");
            CreateIndex("dbo.RouteMap", "RouteTemplateId");
            CreateIndex("dbo.RouteMap", "NodeId");
            AddForeignKey("dbo.RouteMap", "StatusId", "dbo.RouteMapStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RouteMap", "RouteTemplateId", "dbo.RouteTemplate", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RouteMap", "NodeId", "dbo.Node", "Id", cascadeDelete: true);
        }
    }
}

namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrafficHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrafficHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TicketContainerId = c.Long(nullable: false),
                        NodeId = c.Long(nullable: false),
                        EntranceTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        DepartureTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId, cascadeDelete: true)
                .ForeignKey("dbo.TicketConteiner", t => t.TicketContainerId, cascadeDelete: true)
                .Index(t => t.TicketContainerId)
                .Index(t => t.NodeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrafficHistory", "TicketContainerId", "dbo.TicketConteiner");
            DropForeignKey("dbo.TrafficHistory", "NodeId", "dbo.Node");
            DropIndex("dbo.TrafficHistory", new[] { "NodeId" });
            DropIndex("dbo.TrafficHistory", new[] { "TicketContainerId" });
            DropTable("dbo.TrafficHistory");
        }
    }
}

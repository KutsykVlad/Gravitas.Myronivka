namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoadPiontsTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "opd.LoadGuideOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        LoadPointNodeId = c.Long(nullable: false),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        Node_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .ForeignKey("dbo.Node", t => t.Node_Id)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId)
                .Index(t => t.Node_Id);
            
            CreateTable(
                "opd.LoadPointOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            AddColumn("dbo.OpVisa", "LoadPointOpDataId", c => c.Guid());
            AddColumn("dbo.OpVisa", "LoadGuideOpDataId", c => c.Guid());
            AddColumn("dbo.OpCameraImage", "LoadGuideOpDataId", c => c.Guid());
            AddColumn("dbo.OpCameraImage", "LoadPointOpDataId", c => c.Guid());
            CreateIndex("dbo.OpVisa", "LoadPointOpDataId");
            CreateIndex("dbo.OpVisa", "LoadGuideOpDataId");
            CreateIndex("dbo.OpCameraImage", "LoadGuideOpDataId");
            CreateIndex("dbo.OpCameraImage", "LoadPointOpDataId");
            AddForeignKey("dbo.OpCameraImage", "LoadGuideOpDataId", "opd.LoadGuideOpData", "Id");
            AddForeignKey("dbo.OpCameraImage", "LoadPointOpDataId", "opd.LoadPointOpData", "Id");
            AddForeignKey("dbo.OpVisa", "LoadPointOpDataId", "opd.LoadPointOpData", "Id");
            AddForeignKey("dbo.OpVisa", "LoadGuideOpDataId", "opd.LoadGuideOpData", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("opd.LoadGuideOpData", "Node_Id", "dbo.Node");
            DropForeignKey("opd.LoadGuideOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "LoadGuideOpDataId", "opd.LoadGuideOpData");
            DropForeignKey("opd.LoadGuideOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.LoadPointOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "LoadPointOpDataId", "opd.LoadPointOpData");
            DropForeignKey("opd.LoadPointOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "LoadPointOpDataId", "opd.LoadPointOpData");
            DropForeignKey("opd.LoadPointOpData", "NodeId", "dbo.Node");
            DropForeignKey("dbo.OpCameraImage", "LoadGuideOpDataId", "opd.LoadGuideOpData");
            DropForeignKey("opd.LoadGuideOpData", "NodeId", "dbo.Node");
            DropIndex("opd.LoadPointOpData", new[] { "TicketId" });
            DropIndex("opd.LoadPointOpData", new[] { "NodeId" });
            DropIndex("opd.LoadPointOpData", new[] { "StateId" });
            DropIndex("opd.LoadGuideOpData", new[] { "Node_Id" });
            DropIndex("opd.LoadGuideOpData", new[] { "TicketId" });
            DropIndex("opd.LoadGuideOpData", new[] { "NodeId" });
            DropIndex("opd.LoadGuideOpData", new[] { "StateId" });
            DropIndex("dbo.OpCameraImage", new[] { "LoadPointOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "LoadGuideOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "LoadGuideOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "LoadPointOpDataId" });
            DropColumn("dbo.OpCameraImage", "LoadPointOpDataId");
            DropColumn("dbo.OpCameraImage", "LoadGuideOpDataId");
            DropColumn("dbo.OpVisa", "LoadGuideOpDataId");
            DropColumn("dbo.OpVisa", "LoadPointOpDataId");
            DropTable("opd.LoadPointOpData");
            DropTable("opd.LoadGuideOpData");
        }
    }
}

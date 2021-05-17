namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMixedFeedTables : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OpVisa", name: "CentralLabolatoryOpData", newName: "CentralLaboratoryOpData");
            RenameIndex(table: "dbo.OpVisa", name: "IX_CentralLabolatoryOpData", newName: "IX_CentralLaboratoryOpData");
            CreateTable(
                "opd.MixedFeedGuideOpData",
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "opd.MixedFeedLoadOpData",
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
            
            CreateTable(
                "dbo.MixedFeedSilo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        Drive = c.Int(nullable: false),
                        LoadQueue = c.Int(nullable: false),
                        SiloWeight = c.Long(nullable: false),
                        SiloEmpty = c.Single(nullable: false),
                        SiloFull = c.Single(nullable: false),
                        Specification = c.String(),
                        ProductId = c.String(maxLength: 250),
                        DeviceId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("ext.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.DeviceId);
            
            AddColumn("dbo.OpCameraImage", "MixedFeedLoadOpDataId", c => c.Guid());
            AddColumn("dbo.OpCameraImage", "MixedFeedGuideOpDataId", c => c.Guid());
            AddColumn("dbo.OpVisa", "MixedFeedSiloId", c => c.Long());
            AddColumn("dbo.OpVisa", "MixedFeedGuideOpDataId", c => c.Guid());
            AddColumn("dbo.OpVisa", "MixedFeedLoadOpDataId", c => c.Guid());
            CreateIndex("dbo.OpVisa", "MixedFeedSiloId");
            AddForeignKey("dbo.OpCameraImage", "LoadPointOpDataId", "opd.MixedFeedGuideOpData", "Id");
            AddForeignKey("dbo.OpVisa", "LoadGuideOpDataId", "opd.MixedFeedGuideOpData", "Id");
            AddForeignKey("dbo.OpCameraImage", "LoadPointOpDataId", "opd.MixedFeedLoadOpData", "Id");
            AddForeignKey("dbo.OpVisa", "LoadPointOpDataId", "opd.MixedFeedLoadOpData", "Id");
            AddForeignKey("dbo.OpVisa", "MixedFeedSiloId", "dbo.MixedFeedSilo", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OpVisa", "MixedFeedSiloId", "dbo.MixedFeedSilo");
            DropForeignKey("dbo.MixedFeedSilo", "ProductId", "ext.Product");
            DropForeignKey("dbo.MixedFeedSilo", "DeviceId", "dbo.Device");
            DropForeignKey("opd.MixedFeedLoadOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "LoadPointOpDataId", "opd.MixedFeedLoadOpData");
            DropForeignKey("opd.MixedFeedLoadOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "LoadPointOpDataId", "opd.MixedFeedLoadOpData");
            DropForeignKey("opd.MixedFeedLoadOpData", "NodeId", "dbo.Node");
            DropForeignKey("opd.MixedFeedGuideOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "LoadGuideOpDataId", "opd.MixedFeedGuideOpData");
            DropForeignKey("opd.MixedFeedGuideOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "LoadPointOpDataId", "opd.MixedFeedGuideOpData");
            DropForeignKey("opd.MixedFeedGuideOpData", "NodeId", "dbo.Node");
            DropIndex("dbo.MixedFeedSilo", new[] { "DeviceId" });
            DropIndex("dbo.MixedFeedSilo", new[] { "ProductId" });
            DropIndex("opd.MixedFeedLoadOpData", new[] { "TicketId" });
            DropIndex("opd.MixedFeedLoadOpData", new[] { "NodeId" });
            DropIndex("opd.MixedFeedLoadOpData", new[] { "StateId" });
            DropIndex("opd.MixedFeedGuideOpData", new[] { "TicketId" });
            DropIndex("opd.MixedFeedGuideOpData", new[] { "NodeId" });
            DropIndex("opd.MixedFeedGuideOpData", new[] { "StateId" });
            DropIndex("dbo.OpVisa", new[] { "MixedFeedSiloId" });
            DropColumn("dbo.OpVisa", "MixedFeedLoadOpDataId");
            DropColumn("dbo.OpVisa", "MixedFeedGuideOpDataId");
            DropColumn("dbo.OpVisa", "MixedFeedSiloId");
            DropColumn("dbo.OpCameraImage", "MixedFeedGuideOpDataId");
            DropColumn("dbo.OpCameraImage", "MixedFeedLoadOpDataId");
            DropTable("dbo.MixedFeedSilo");
            DropTable("opd.MixedFeedLoadOpData");
            DropTable("opd.MixedFeedGuideOpData");
            RenameIndex(table: "dbo.OpVisa", name: "IX_CentralLaboratoryOpData", newName: "IX_CentralLabolatoryOpData");
            RenameColumn(table: "dbo.OpVisa", name: "CentralLaboratoryOpData", newName: "CentralLabolatoryOpData");
        }
    }
}

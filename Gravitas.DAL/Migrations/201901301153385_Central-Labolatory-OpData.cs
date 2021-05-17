namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CentralLabolatoryOpData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "opd.CentralLabOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SampleCheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        SampleCheckOutTime = c.DateTime(precision: 7, storeType: "datetime2"),
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
            
            AddColumn("dbo.OpVisa", "CentralLabolatoryOpData", c => c.Guid());
            AddColumn("dbo.OpCameraImage", "CentralLabOpData_Id", c => c.Guid());
            CreateIndex("dbo.OpCameraImage", "CentralLabOpData_Id");
            CreateIndex("dbo.OpVisa", "CentralLabolatoryOpData");
            AddForeignKey("dbo.OpCameraImage", "CentralLabOpData_Id", "opd.CentralLabOpData", "Id");
            AddForeignKey("dbo.OpVisa", "CentralLabolatoryOpData", "opd.CentralLabOpData", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("opd.CentralLabOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "CentralLabolatoryOpData", "opd.CentralLabOpData");
            DropForeignKey("opd.CentralLabOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "CentralLabOpData_Id", "opd.CentralLabOpData");
            DropForeignKey("opd.CentralLabOpData", "NodeId", "dbo.Node");
            DropIndex("dbo.OpVisa", new[] { "CentralLabolatoryOpData" });
            DropIndex("dbo.OpCameraImage", new[] { "CentralLabOpData_Id" });
            DropIndex("opd.CentralLabOpData", new[] { "TicketId" });
            DropIndex("opd.CentralLabOpData", new[] { "NodeId" });
            DropIndex("opd.CentralLabOpData", new[] { "StateId" });
            DropColumn("dbo.OpCameraImage", "CentralLabOpData_Id");
            DropColumn("dbo.OpVisa", "CentralLabolatoryOpData");
            DropTable("opd.CentralLabOpData");
        }
    }
}

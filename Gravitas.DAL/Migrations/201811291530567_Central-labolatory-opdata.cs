namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Centrallabolatoryopdata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "opd.CentralLabOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SampleCheckInDateTime = c.DateTime(),
                        SampleCheckOutTime = c.DateTime(),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        OpDataState_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.OpDataState_Id)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId)
                .Index(t => t.OpDataState_Id);
            
            AddColumn("dbo.OpVisa", "CentralLabOpData_Id", c => c.Guid());
            AddColumn("dbo.OpCameraImage", "CentralLabOpData_Id", c => c.Guid());
            CreateIndex("dbo.OpVisa", "CentralLabOpData_Id");
            CreateIndex("dbo.OpCameraImage", "CentralLabOpData_Id");
            AddForeignKey("dbo.OpCameraImage", "CentralLabOpData_Id", "opd.CentralLabOpData", "Id");
            AddForeignKey("dbo.OpVisa", "CentralLabOpData_Id", "opd.CentralLabOpData", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("opd.CentralLabOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "CentralLabOpData_Id", "opd.CentralLabOpData");
            DropForeignKey("opd.CentralLabOpData", "OpDataState_Id", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "CentralLabOpData_Id", "opd.CentralLabOpData");
            DropForeignKey("opd.CentralLabOpData", "NodeId", "dbo.Node");
            DropIndex("opd.CentralLabOpData", new[] { "OpDataState_Id" });
            DropIndex("opd.CentralLabOpData", new[] { "TicketId" });
            DropIndex("opd.CentralLabOpData", new[] { "NodeId" });
            DropIndex("dbo.OpCameraImage", new[] { "CentralLabOpData_Id" });
            DropIndex("dbo.OpVisa", new[] { "CentralLabOpData_Id" });
            DropColumn("dbo.OpCameraImage", "CentralLabOpData_Id");
            DropColumn("dbo.OpVisa", "CentralLabOpData_Id");
            DropTable("opd.CentralLabOpData");
        }
    }
}

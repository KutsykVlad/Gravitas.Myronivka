namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLoadOpData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("opd.LoadOpData", "NodeId", "dbo.Node");
            DropForeignKey("dbo.OpCameraImage", "LoadOpDataId", "opd.LoadOpData");
            DropForeignKey("opd.LoadOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpVisa", "LoadOpDataId", "opd.LoadOpData");
            DropForeignKey("opd.LoadOpData", "TicketId", "dbo.Ticket");
            DropIndex("dbo.OpCameraImage", new[] { "LoadOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "LoadOpDataId" });
            DropIndex("opd.LoadOpData", new[] { "StateId" });
            DropIndex("opd.LoadOpData", new[] { "NodeId" });
            DropIndex("opd.LoadOpData", new[] { "TicketId" });
            DropTable("opd.LoadOpData");
        }
        
        public override void Down()
        {
            CreateTable(
                "opd.LoadOpData",
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("opd.LoadOpData", "TicketId");
            CreateIndex("opd.LoadOpData", "NodeId");
            CreateIndex("opd.LoadOpData", "StateId");
            CreateIndex("dbo.OpVisa", "LoadOpDataId");
            CreateIndex("dbo.OpCameraImage", "LoadOpDataId");
            AddForeignKey("opd.LoadOpData", "TicketId", "dbo.Ticket", "Id");
            AddForeignKey("dbo.OpVisa", "LoadOpDataId", "opd.LoadOpData", "Id");
            AddForeignKey("opd.LoadOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OpCameraImage", "LoadOpDataId", "opd.LoadOpData", "Id");
            AddForeignKey("opd.LoadOpData", "NodeId", "dbo.Node", "Id");
        }
    }
}

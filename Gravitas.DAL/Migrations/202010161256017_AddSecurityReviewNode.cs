namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSecurityReviewNode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "opd.SecurityCheckReviewOpData",
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
            
            AddColumn("dbo.OpCameraImage", "SecurityCheckReviewOpData_Id", c => c.Guid());
            AddColumn("dbo.OpVisa", "SecurityCheckReviewOpDataId", c => c.Guid());
            CreateIndex("dbo.OpCameraImage", "SecurityCheckReviewOpData_Id");
            CreateIndex("dbo.OpVisa", "SecurityCheckReviewOpDataId");
            AddForeignKey("dbo.OpCameraImage", "SecurityCheckReviewOpData_Id", "opd.SecurityCheckReviewOpData", "Id");
            AddForeignKey("dbo.OpVisa", "SecurityCheckReviewOpDataId", "opd.SecurityCheckReviewOpData", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("opd.SecurityCheckReviewOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "SecurityCheckReviewOpDataId", "opd.SecurityCheckReviewOpData");
            DropForeignKey("opd.SecurityCheckReviewOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "SecurityCheckReviewOpData_Id", "opd.SecurityCheckReviewOpData");
            DropForeignKey("opd.SecurityCheckReviewOpData", "NodeId", "dbo.Node");
            DropIndex("opd.SecurityCheckReviewOpData", new[] { "TicketId" });
            DropIndex("opd.SecurityCheckReviewOpData", new[] { "NodeId" });
            DropIndex("opd.SecurityCheckReviewOpData", new[] { "StateId" });
            DropIndex("dbo.OpVisa", new[] { "SecurityCheckReviewOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "SecurityCheckReviewOpData_Id" });
            DropColumn("dbo.OpVisa", "SecurityCheckReviewOpDataId");
            DropColumn("dbo.OpCameraImage", "SecurityCheckReviewOpData_Id");
            DropTable("opd.SecurityCheckReviewOpData");
        }
    }
}

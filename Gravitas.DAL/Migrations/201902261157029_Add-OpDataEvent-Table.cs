namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOpDataEventTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OpDataEvent",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TicketId = c.Long(nullable: false),
                        NodeId = c.Long(nullable: false),
                        EmployeeId = c.String(maxLength: 250),
                        OpDataEventType = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        TypeOfTransaction = c.String(),
                        Cause = c.String(),
                        Weight = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ext.Employee", t => t.EmployeeId)
                .ForeignKey("dbo.Node", t => t.NodeId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId)
                .Index(t => t.NodeId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OpDataEvent", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpDataEvent", "NodeId", "dbo.Node");
            DropForeignKey("dbo.OpDataEvent", "EmployeeId", "ext.Employee");
            DropIndex("dbo.OpDataEvent", new[] { "EmployeeId" });
            DropIndex("dbo.OpDataEvent", new[] { "NodeId" });
            DropIndex("dbo.OpDataEvent", new[] { "TicketId" });
            DropTable("dbo.OpDataEvent");
        }
    }
}

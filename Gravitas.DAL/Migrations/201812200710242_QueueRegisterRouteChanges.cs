namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QueueRegisterRouteChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QueuePreRegister", "Product_Id", "ext.Product");
            DropIndex("dbo.QueuePreRegister", new[] { "Product_Id" });
            CreateTable(
                "dbo.QueueRegister",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RegisterTime = c.DateTime(nullable: false),
                        IsAllowedToEnterTerritory = c.Boolean(nullable: false),
                        TicketContainerId = c.Long(),
                        PhoneNumber = c.String(),
                        TrailerPlate = c.String(),
                        TruckPlate = c.String(),
                        Product_Id = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ext.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.Ticket", "RouteItemIndex", c => c.Int(nullable: false));
            DropTable("dbo.QueuePreRegister");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QueuePreRegister",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false),
                        TrailerPlate = c.String(nullable: false),
                        TruckPlate = c.String(nullable: false),
                        Product_Id = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.QueueRegister", "Product_Id", "ext.Product");
            DropIndex("dbo.QueueRegister", new[] { "Product_Id" });
            DropColumn("dbo.Ticket", "RouteItemIndex");
            DropTable("dbo.QueueRegister");
            CreateIndex("dbo.QueuePreRegister", "Product_Id");
            AddForeignKey("dbo.QueuePreRegister", "Product_Id", "ext.Product", "Id");
        }
    }
}

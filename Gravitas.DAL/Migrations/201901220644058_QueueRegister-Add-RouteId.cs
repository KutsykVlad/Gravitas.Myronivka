namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QueueRegisterAddRouteId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QueueRegister", "Product_Id", "ext.Product");
            DropIndex("dbo.QueueRegister", new[] { "Product_Id" });
            AddColumn("dbo.QueueRegister", "RouteTemplateId", c => c.Long());
            CreateIndex("dbo.QueueRegister", "RouteTemplateId");
            AddForeignKey("dbo.QueueRegister", "RouteTemplateId", "dbo.RouteTemplate", "Id");
            DropColumn("dbo.QueueRegister", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QueueRegister", "Product_Id", c => c.String(maxLength: 250));
            DropForeignKey("dbo.QueueRegister", "RouteTemplateId", "dbo.RouteTemplate");
            DropIndex("dbo.QueueRegister", new[] { "RouteTemplateId" });
            DropColumn("dbo.QueueRegister", "RouteTemplateId");
            CreateIndex("dbo.QueueRegister", "Product_Id");
            AddForeignKey("dbo.QueueRegister", "Product_Id", "ext.Product", "Id");
        }
    }
}

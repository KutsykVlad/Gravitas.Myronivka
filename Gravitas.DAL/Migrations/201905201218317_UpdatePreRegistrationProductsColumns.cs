namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePreRegistrationProductsColumns : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PreRegisterProduct", "ProductId", "ext.Product");
            DropIndex("dbo.PreRegisterProduct", new[] { "RouteTemplateId" });
            DropIndex("dbo.PreRegisterProduct", new[] { "ProductId" });
            AddColumn("dbo.PreRegisterProduct", "Title", c => c.String());
            CreateIndex("dbo.PreRegisterProduct", "RouteTemplateId", unique: true);
            DropColumn("dbo.PreRegisterProduct", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PreRegisterProduct", "ProductId", c => c.String(maxLength: 250));
            DropIndex("dbo.PreRegisterProduct", new[] { "RouteTemplateId" });
            DropColumn("dbo.PreRegisterProduct", "Title");
            CreateIndex("dbo.PreRegisterProduct", "ProductId");
            CreateIndex("dbo.PreRegisterProduct", "RouteTemplateId");
            AddForeignKey("dbo.PreRegisterProduct", "ProductId", "ext.Product", "Id");
        }
    }
}

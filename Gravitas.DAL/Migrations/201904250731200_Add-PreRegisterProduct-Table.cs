namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPreRegisterProductTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreRegisterProduct",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RouteTemplateId = c.Long(nullable: false),
                        ProductId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ext.Product", t => t.ProductId)
                .ForeignKey("dbo.RouteTemplate", t => t.RouteTemplateId, cascadeDelete: true)
                .Index(t => t.RouteTemplateId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PreRegisterProduct", "RouteTemplateId", "dbo.RouteTemplate");
            DropForeignKey("dbo.PreRegisterProduct", "ProductId", "ext.Product");
            DropIndex("dbo.PreRegisterProduct", new[] { "ProductId" });
            DropIndex("dbo.PreRegisterProduct", new[] { "RouteTemplateId" });
            DropTable("dbo.PreRegisterProduct");
        }
    }
}

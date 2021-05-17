namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PredictionTime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoadPointSetting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        Node_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.Node_Id)
                .Index(t => t.Node_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("ext.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.LoadPointSettingProducts",
                c => new
                    {
                        LoadPointSetting_Id = c.Long(nullable: false),
                        Product_Id = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => new { t.LoadPointSetting_Id, t.Product_Id })
                .ForeignKey("dbo.LoadPointSetting", t => t.LoadPointSetting_Id, cascadeDelete: true)
                .ForeignKey("ext.Product", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.LoadPointSetting_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QueuePreRegister", "Product_Id", "ext.Product");
            DropForeignKey("dbo.LoadPointSettingProducts", "Product_Id", "ext.Product");
            DropForeignKey("dbo.LoadPointSettingProducts", "LoadPointSetting_Id", "dbo.LoadPointSetting");
            DropForeignKey("dbo.LoadPointSetting", "Node_Id", "dbo.Node");
            DropIndex("dbo.LoadPointSettingProducts", new[] { "Product_Id" });
            DropIndex("dbo.LoadPointSettingProducts", new[] { "LoadPointSetting_Id" });
            DropIndex("dbo.QueuePreRegister", new[] { "Product_Id" });
            DropIndex("dbo.LoadPointSetting", new[] { "Node_Id" });
            DropTable("dbo.LoadPointSettingProducts");
            DropTable("dbo.QueuePreRegister");
            DropTable("dbo.LoadPointSetting");
        }
    }
}

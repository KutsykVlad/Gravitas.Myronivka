namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMixedFeedTablesRelations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OpCameraImage", "LoadPointOpDataId", "opd.MixedFeedGuideOpData");
            DropForeignKey("dbo.OpVisa", "LoadGuideOpDataId", "opd.MixedFeedGuideOpData");
            DropForeignKey("dbo.OpCameraImage", "LoadPointOpDataId", "opd.MixedFeedLoadOpData");
            DropForeignKey("dbo.OpVisa", "LoadPointOpDataId", "opd.MixedFeedLoadOpData");
            AddColumn("dbo.OpCameraImage", "MixedFeedGuideOpData_Id", c => c.Guid());
            AddColumn("dbo.OpCameraImage", "MixedFeedLoadOpData_Id", c => c.Guid());
            AddColumn("dbo.OpVisa", "MixedFeedGuideOpData_Id", c => c.Guid());
            AddColumn("dbo.OpVisa", "MixedFeedLoadOpData_Id", c => c.Guid());
            CreateIndex("dbo.OpCameraImage", "MixedFeedGuideOpData_Id");
            CreateIndex("dbo.OpCameraImage", "MixedFeedLoadOpData_Id");
            CreateIndex("dbo.OpVisa", "MixedFeedGuideOpData_Id");
            CreateIndex("dbo.OpVisa", "MixedFeedLoadOpData_Id");
            AddForeignKey("dbo.OpCameraImage", "MixedFeedGuideOpData_Id", "opd.MixedFeedGuideOpData", "Id");
            AddForeignKey("dbo.OpVisa", "MixedFeedGuideOpData_Id", "opd.MixedFeedGuideOpData", "Id");
            AddForeignKey("dbo.OpCameraImage", "MixedFeedLoadOpData_Id", "opd.MixedFeedLoadOpData", "Id");
            AddForeignKey("dbo.OpVisa", "MixedFeedLoadOpData_Id", "opd.MixedFeedLoadOpData", "Id");
            DropColumn("dbo.OpCameraImage", "MixedFeedLoadOpDataId");
            DropColumn("dbo.OpCameraImage", "MixedFeedGuideOpDataId");
            DropColumn("dbo.OpVisa", "MixedFeedGuideOpDataId");
            DropColumn("dbo.OpVisa", "MixedFeedLoadOpDataId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OpVisa", "MixedFeedLoadOpDataId", c => c.Guid());
            AddColumn("dbo.OpVisa", "MixedFeedGuideOpDataId", c => c.Guid());
            AddColumn("dbo.OpCameraImage", "MixedFeedGuideOpDataId", c => c.Guid());
            AddColumn("dbo.OpCameraImage", "MixedFeedLoadOpDataId", c => c.Guid());
            DropForeignKey("dbo.OpVisa", "MixedFeedLoadOpData_Id", "opd.MixedFeedLoadOpData");
            DropForeignKey("dbo.OpCameraImage", "MixedFeedLoadOpData_Id", "opd.MixedFeedLoadOpData");
            DropForeignKey("dbo.OpVisa", "MixedFeedGuideOpData_Id", "opd.MixedFeedGuideOpData");
            DropForeignKey("dbo.OpCameraImage", "MixedFeedGuideOpData_Id", "opd.MixedFeedGuideOpData");
            DropIndex("dbo.OpVisa", new[] { "MixedFeedLoadOpData_Id" });
            DropIndex("dbo.OpVisa", new[] { "MixedFeedGuideOpData_Id" });
            DropIndex("dbo.OpCameraImage", new[] { "MixedFeedLoadOpData_Id" });
            DropIndex("dbo.OpCameraImage", new[] { "MixedFeedGuideOpData_Id" });
            DropColumn("dbo.OpVisa", "MixedFeedLoadOpData_Id");
            DropColumn("dbo.OpVisa", "MixedFeedGuideOpData_Id");
            DropColumn("dbo.OpCameraImage", "MixedFeedLoadOpData_Id");
            DropColumn("dbo.OpCameraImage", "MixedFeedGuideOpData_Id");
            AddForeignKey("dbo.OpVisa", "LoadPointOpDataId", "opd.MixedFeedLoadOpData", "Id");
            AddForeignKey("dbo.OpCameraImage", "LoadPointOpDataId", "opd.MixedFeedLoadOpData", "Id");
            AddForeignKey("dbo.OpVisa", "LoadGuideOpDataId", "opd.MixedFeedGuideOpData", "Id");
            AddForeignKey("dbo.OpCameraImage", "LoadPointOpDataId", "opd.MixedFeedGuideOpData", "Id");
        }
    }
}

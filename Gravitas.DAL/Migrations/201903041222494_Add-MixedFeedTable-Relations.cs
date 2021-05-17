namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMixedFeedTableRelations : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OpCameraImage", name: "MixedFeedGuideOpData_Id", newName: "MixedFeedGuideOpDataId");
            RenameColumn(table: "dbo.OpVisa", name: "MixedFeedGuideOpData_Id", newName: "MixedFeedGuideOpDataId");
            RenameColumn(table: "dbo.OpCameraImage", name: "MixedFeedLoadOpData_Id", newName: "MixedFeedLoadOpDataId");
            RenameColumn(table: "dbo.OpVisa", name: "MixedFeedLoadOpData_Id", newName: "MixedFeedLoadOpDataId");
            RenameIndex(table: "dbo.OpCameraImage", name: "IX_MixedFeedLoadOpData_Id", newName: "IX_MixedFeedLoadOpDataId");
            RenameIndex(table: "dbo.OpCameraImage", name: "IX_MixedFeedGuideOpData_Id", newName: "IX_MixedFeedGuideOpDataId");
            RenameIndex(table: "dbo.OpVisa", name: "IX_MixedFeedGuideOpData_Id", newName: "IX_MixedFeedGuideOpDataId");
            RenameIndex(table: "dbo.OpVisa", name: "IX_MixedFeedLoadOpData_Id", newName: "IX_MixedFeedLoadOpDataId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.OpVisa", name: "IX_MixedFeedLoadOpDataId", newName: "IX_MixedFeedLoadOpData_Id");
            RenameIndex(table: "dbo.OpVisa", name: "IX_MixedFeedGuideOpDataId", newName: "IX_MixedFeedGuideOpData_Id");
            RenameIndex(table: "dbo.OpCameraImage", name: "IX_MixedFeedGuideOpDataId", newName: "IX_MixedFeedGuideOpData_Id");
            RenameIndex(table: "dbo.OpCameraImage", name: "IX_MixedFeedLoadOpDataId", newName: "IX_MixedFeedLoadOpData_Id");
            RenameColumn(table: "dbo.OpVisa", name: "MixedFeedLoadOpDataId", newName: "MixedFeedLoadOpData_Id");
            RenameColumn(table: "dbo.OpCameraImage", name: "MixedFeedLoadOpDataId", newName: "MixedFeedLoadOpData_Id");
            RenameColumn(table: "dbo.OpVisa", name: "MixedFeedGuideOpDataId", newName: "MixedFeedGuideOpData_Id");
            RenameColumn(table: "dbo.OpCameraImage", name: "MixedFeedGuideOpDataId", newName: "MixedFeedGuideOpData_Id");
        }
    }
}

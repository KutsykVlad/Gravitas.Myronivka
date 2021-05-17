namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpDataStateUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("opd.NonStandartOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.ScaleOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.SecurityCheckInOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.SecurityCheckOutOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.SingleWindowOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.UnloadGuideOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.UnloadPointOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.LoadOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.LabRegularOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.LabFacelessOpData", "StateId", "dbo.OpDataState");
            DropPrimaryKey("dbo.OpDataState");
            AlterColumn("dbo.OpDataState", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.OpDataState", "Id");
            AddForeignKey("opd.NonStandartOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.ScaleOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.SecurityCheckInOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.SecurityCheckOutOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.SingleWindowOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.UnloadGuideOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.UnloadPointOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.LoadOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.LabRegularOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.LabFacelessOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("opd.LabFacelessOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.LabRegularOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.LoadOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.UnloadPointOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.UnloadGuideOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.SingleWindowOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.SecurityCheckOutOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.SecurityCheckInOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.ScaleOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.NonStandartOpData", "StateId", "dbo.OpDataState");
            DropPrimaryKey("dbo.OpDataState");
            AlterColumn("dbo.OpDataState", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.OpDataState", "Id");
            AddForeignKey("opd.LabFacelessOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.LabRegularOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.LoadOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.UnloadPointOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.UnloadGuideOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.SingleWindowOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.SecurityCheckOutOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.SecurityCheckInOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.ScaleOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("opd.NonStandartOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
        }
    }
}

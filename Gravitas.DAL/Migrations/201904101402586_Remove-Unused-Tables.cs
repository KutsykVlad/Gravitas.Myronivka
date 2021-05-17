namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUnusedTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Connect", "ApiMessageGuid", "dbo.ConnectApiMessage");
            DropForeignKey("dbo.Connect", "DirrectionId", "dbo.ConnectDirrection");
            DropForeignKey("dbo.Connect", "StateId", "dbo.ConnectState");
            DropForeignKey("dbo.Connect", "TypeId", "dbo.ConnectType");
            DropForeignKey("dbo.Connect", "EmailMessageGuid", "dbo.ConnectEmailMessage");
            DropForeignKey("dbo.Connect", "SmsMessageGuid", "dbo.ConnectSmsMessage");
            DropForeignKey("dbo.Connect", "TicketContainerId", "dbo.TicketConteiner");
            DropForeignKey("opd.LabRegularOpData", "NodeId", "dbo.Node");
            DropForeignKey("dbo.OpCameraImage", "LabRegularOpDataId", "opd.LabRegularOpData");
            DropForeignKey("opd.LabRegularOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpVisa", "LabRegularOpDataId", "opd.LabRegularOpData");
            DropForeignKey("opd.LabRegularOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.LoadPointSetting", "Node_Id", "dbo.Node");
            DropForeignKey("dbo.LoadPointSettingProducts", "LoadPointSetting_Id", "dbo.LoadPointSetting");
            DropForeignKey("dbo.LoadPointSettingProducts", "Product_Id", "ext.Product");
            DropForeignKey("dbo.DeepLink", "StatusId", "dbo.DeepLinkStatus");
            DropForeignKey("dbo.DeepLink", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.User", "Cards_Id", "dbo.Card");
            DropForeignKey("dbo.OpVisa", "User_Id", "dbo.User");
            DropIndex("dbo.Connect", new[] { "TicketContainerId" });
            DropIndex("dbo.Connect", new[] { "TypeId" });
            DropIndex("dbo.Connect", new[] { "StateId" });
            DropIndex("dbo.Connect", new[] { "DirrectionId" });
            DropIndex("dbo.Connect", new[] { "ApiMessageGuid" });
            DropIndex("dbo.Connect", new[] { "SmsMessageGuid" });
            DropIndex("dbo.Connect", new[] { "EmailMessageGuid" });
            DropIndex("dbo.OpCameraImage", new[] { "LabRegularOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "LabRegularOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "User_Id" });
            DropIndex("opd.LabRegularOpData", new[] { "StateId" });
            DropIndex("opd.LabRegularOpData", new[] { "NodeId" });
            DropIndex("opd.LabRegularOpData", new[] { "TicketId" });
            DropIndex("dbo.LoadPointSetting", new[] { "Node_Id" });
            DropIndex("dbo.DeepLink", new[] { "StatusId" });
            DropIndex("dbo.DeepLink", new[] { "TicketId" });
            DropIndex("dbo.User", new[] { "Cards_Id" });
            DropIndex("dbo.LoadPointSettingProducts", new[] { "LoadPointSetting_Id" });
            DropIndex("dbo.LoadPointSettingProducts", new[] { "Product_Id" });
            DropColumn("dbo.OpVisa", "User_Id");
            DropTable("dbo.Connect");
            DropTable("dbo.ConnectApiMessage");
            DropTable("dbo.ConnectDirrection");
            DropTable("dbo.ConnectState");
            DropTable("dbo.ConnectType");
            DropTable("dbo.ConnectEmailMessage");
            DropTable("dbo.ConnectSmsMessage");
            DropTable("opd.LabRegularOpData");
            DropTable("dbo.LoadPointSetting");
            DropTable("dbo.DeepLink");
            DropTable("dbo.DeepLinkStatus");
            DropTable("dbo.User");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LoadPointSettingProducts",
                c => new
                    {
                        LoadPointSetting_Id = c.Long(nullable: false),
                        Product_Id = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => new { t.LoadPointSetting_Id, t.Product_Id });
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        CardRfid = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        Cards_Id = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeepLinkStatus",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeepLink",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ContractId = c.String(),
                        Text = c.String(),
                        Code = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdateAt = c.DateTime(),
                        ExpiresAt = c.DateTime(),
                        Expired = c.Boolean(nullable: false),
                        ManagerId = c.String(),
                        ManagerEmail = c.String(),
                        ManagerPhone = c.String(),
                        ManagerName = c.String(),
                        ManagerSmsStatus = c.Int(nullable: false),
                        ProviderId = c.String(),
                        ProviderEmail = c.String(),
                        ProviderPhone = c.String(),
                        ProviderSmsStatus = c.Int(nullable: false),
                        OwnerId = c.String(),
                        OwnerEmail = c.String(),
                        OwnerPhone = c.String(),
                        OwnerName = c.String(),
                        OwnerSmsStatus = c.Int(nullable: false),
                        StatusId = c.Long(nullable: false),
                        TicketId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoadPointSetting",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        Node_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "opd.LabRegularOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ImpurityRate = c.Single(),
                        HumidityRate = c.Single(),
                        InfectionRate = c.Boolean(),
                        EffectiveValue = c.Single(),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConnectSmsMessage",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        DeliveryStatusId = c.Long(nullable: false),
                        ReceiverPhoneNo = c.String(),
                        MessageText = c.String(),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.ConnectEmailMessage",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Receiver = c.String(),
                        MessageTitle = c.String(),
                        MessageText = c.String(),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.ConnectType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConnectState",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConnectDirrection",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConnectApiMessage",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        MethodTypeId = c.Int(nullable: false),
                        RequestUrl = c.String(),
                        RequestContent = c.String(),
                        ResponseContent = c.String(),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.Connect",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TicketContainerId = c.Long(),
                        TypeId = c.Long(nullable: false),
                        StateId = c.Long(nullable: false),
                        DirrectionId = c.Long(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        EditTime = c.DateTime(nullable: false),
                        ApiMessageGuid = c.Guid(),
                        SmsMessageGuid = c.Guid(),
                        EmailMessageGuid = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.OpVisa", "User_Id", c => c.Long());
            CreateIndex("dbo.LoadPointSettingProducts", "Product_Id");
            CreateIndex("dbo.LoadPointSettingProducts", "LoadPointSetting_Id");
            CreateIndex("dbo.User", "Cards_Id");
            CreateIndex("dbo.DeepLink", "TicketId");
            CreateIndex("dbo.DeepLink", "StatusId");
            CreateIndex("dbo.LoadPointSetting", "Node_Id");
            CreateIndex("opd.LabRegularOpData", "TicketId");
            CreateIndex("opd.LabRegularOpData", "NodeId");
            CreateIndex("opd.LabRegularOpData", "StateId");
            CreateIndex("dbo.OpVisa", "User_Id");
            CreateIndex("dbo.OpVisa", "LabRegularOpDataId");
            CreateIndex("dbo.OpCameraImage", "LabRegularOpDataId");
            CreateIndex("dbo.Connect", "EmailMessageGuid");
            CreateIndex("dbo.Connect", "SmsMessageGuid");
            CreateIndex("dbo.Connect", "ApiMessageGuid");
            CreateIndex("dbo.Connect", "DirrectionId");
            CreateIndex("dbo.Connect", "StateId");
            CreateIndex("dbo.Connect", "TypeId");
            CreateIndex("dbo.Connect", "TicketContainerId");
            AddForeignKey("dbo.OpVisa", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.User", "Cards_Id", "dbo.Card", "Id");
            AddForeignKey("dbo.DeepLink", "TicketId", "dbo.Ticket", "Id");
            AddForeignKey("dbo.DeepLink", "StatusId", "dbo.DeepLinkStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoadPointSettingProducts", "Product_Id", "ext.Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoadPointSettingProducts", "LoadPointSetting_Id", "dbo.LoadPointSetting", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LoadPointSetting", "Node_Id", "dbo.Node", "Id");
            AddForeignKey("opd.LabRegularOpData", "TicketId", "dbo.Ticket", "Id");
            AddForeignKey("dbo.OpVisa", "LabRegularOpDataId", "opd.LabRegularOpData", "Id");
            AddForeignKey("opd.LabRegularOpData", "StateId", "dbo.OpDataState", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OpCameraImage", "LabRegularOpDataId", "opd.LabRegularOpData", "Id");
            AddForeignKey("opd.LabRegularOpData", "NodeId", "dbo.Node", "Id");
            AddForeignKey("dbo.Connect", "TicketContainerId", "dbo.TicketConteiner", "Id");
            AddForeignKey("dbo.Connect", "SmsMessageGuid", "dbo.ConnectSmsMessage", "Guid");
            AddForeignKey("dbo.Connect", "EmailMessageGuid", "dbo.ConnectEmailMessage", "Guid");
            AddForeignKey("dbo.Connect", "TypeId", "dbo.ConnectType", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Connect", "StateId", "dbo.ConnectState", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Connect", "DirrectionId", "dbo.ConnectDirrection", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Connect", "ApiMessageGuid", "dbo.ConnectApiMessage", "Guid");
        }
    }
}

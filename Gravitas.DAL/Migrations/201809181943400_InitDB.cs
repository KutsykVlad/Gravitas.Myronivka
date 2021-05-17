namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganizationUnit",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ParentId = c.Long(),
                        UnitTypeId = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationUnit", t => t.ParentId)
                .ForeignKey("dbo.OrganizationUnitType", t => t.UnitTypeId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.UnitTypeId);
            
            CreateTable(
                "dbo.Node",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        OrganisationUnitId = c.Long(),
                        OpRoutineId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 50),
                        Code = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        IsStart = c.Boolean(nullable: false),
                        IsFinish = c.Boolean(nullable: false),
                        Quota = c.Int(nullable: false),
                        Config = c.String(),
                        Context = c.String(),
                        ProcessingMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OpRoutine", t => t.OpRoutineId)
                .ForeignKey("dbo.OrganizationUnit", t => t.OrganisationUnitId)
                .Index(t => t.OrganisationUnitId)
                .Index(t => t.OpRoutineId);
            
            CreateTable(
                "opd.LabFacelessOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ImpurityClassId = c.String(),
                        ImpurityValue = c.Single(),
                        HumidityClassId = c.String(),
                        HumidityValue = c.Single(),
                        InfectionedClassId = c.String(),
                        EffectiveValue = c.Single(),
                        Comment = c.String(),
                        DataSourceName = c.String(),
                        ImpurityValueSourceId = c.Long(),
                        HumidityValueSourceId = c.Long(),
                        InfectionedValueSourceId = c.Long(),
                        EffectiveValueSourceId = c.Long(),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "opd.LabFacelessOpDataComponent",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LabFacelessOpDataId = c.Guid(nullable: false),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(nullable: false),
                        AnalysisTrayRfid = c.String(),
                        AnalysisValueDescriptor = c.String(),
                        CheckInDateTime = c.DateTime(),
                        ImpurityClassId = c.String(),
                        ImpurityValue = c.Single(),
                        HumidityClassId = c.String(),
                        HumidityValue = c.Single(),
                        InfectionedClassId = c.String(),
                        EffectiveValue = c.Single(),
                        Comment = c.String(),
                        DataSourceName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("opd.LabFacelessOpData", t => t.LabFacelessOpDataId, cascadeDelete: true)
                .Index(t => t.LabFacelessOpDataId);
            
            CreateTable(
                "dbo.OpVisa",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        ExternalUserId = c.String(maxLength: 250),
                        Message = c.String(),
                        LabFacelessOpDataId = c.Guid(),
                        LabRegularOpDataId = c.Guid(),
                        LoadOpDataId = c.Guid(),
                        SingleWindowOpDataId = c.Guid(),
                        SecurityCheckInOpDataId = c.Guid(),
                        SecurityCheckOutOpDataId = c.Guid(),
                        ScaleTareOpDataId = c.Guid(),
                        ScaleGrossOpDataId = c.Guid(),
                        UnloadPointOpDataId = c.Guid(),
                        UnloadGuideOpDataId = c.Guid(),
                        LabFacelessOpDataComponentId = c.Long(),
                        NonStandartOpData_Id = c.Guid(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("opd.NonStandartOpData", t => t.NonStandartOpData_Id)
                .ForeignKey("opd.ScaleOpData", t => t.ScaleTareOpDataId)
                .ForeignKey("opd.SecurityCheckInOpData", t => t.SecurityCheckInOpDataId)
                .ForeignKey("opd.SecurityCheckOutOpData", t => t.SecurityCheckOutOpDataId)
                .ForeignKey("opd.SingleWindowOpData", t => t.SingleWindowOpDataId)
                .ForeignKey("opd.UnloadGuideOpData", t => t.UnloadGuideOpDataId)
                .ForeignKey("opd.UnloadPointOpData", t => t.UnloadPointOpDataId)
                .ForeignKey("opd.LoadOpData", t => t.LoadOpDataId)
                .ForeignKey("opd.LabRegularOpData", t => t.LabRegularOpDataId)
                .ForeignKey("ext.User", t => t.ExternalUserId)
                .ForeignKey("opd.LabFacelessOpDataComponent", t => t.LabFacelessOpDataComponentId)
                .ForeignKey("opd.LabFacelessOpData", t => t.LabFacelessOpDataId)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.ExternalUserId)
                .Index(t => t.LabFacelessOpDataId)
                .Index(t => t.LabRegularOpDataId)
                .Index(t => t.LoadOpDataId)
                .Index(t => t.SingleWindowOpDataId)
                .Index(t => t.SecurityCheckInOpDataId)
                .Index(t => t.SecurityCheckOutOpDataId)
                .Index(t => t.ScaleTareOpDataId)
                .Index(t => t.UnloadPointOpDataId)
                .Index(t => t.UnloadGuideOpDataId)
                .Index(t => t.LabFacelessOpDataComponentId)
                .Index(t => t.NonStandartOpData_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "ext.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        ShortName = c.String(maxLength: 250),
                        FullName = c.String(maxLength: 250),
                        EmployeeId = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Card",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 50),
                        TypeId = c.Long(nullable: false),
                        No = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ExternalUserId = c.String(maxLength: 250),
                        TicketContainerId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CardType", t => t.TypeId)
                .ForeignKey("dbo.TicketConteiner", t => t.TicketContainerId)
                .ForeignKey("ext.User", t => t.ExternalUserId)
                .Index(t => t.TypeId)
                .Index(t => t.ExternalUserId)
                .Index(t => t.TicketContainerId);
            
            CreateTable(
                "dbo.CardType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketConteiner",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StatusId = c.Long(nullable: false),
                        QueueStatusId = c.Long(nullable: false),
                        ProcessingMessage = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketContainerStatus", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConnectApiMessage", t => t.ApiMessageGuid)
                .ForeignKey("dbo.ConnectDirrection", t => t.DirrectionId, cascadeDelete: true)
                .ForeignKey("dbo.ConnectState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.ConnectType", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.ConnectEmailMessage", t => t.EmailMessageGuid)
                .ForeignKey("dbo.ConnectSmsMessage", t => t.SmsMessageGuid)
                .ForeignKey("dbo.TicketConteiner", t => t.TicketContainerId)
                .Index(t => t.TicketContainerId)
                .Index(t => t.TypeId)
                .Index(t => t.StateId)
                .Index(t => t.DirrectionId)
                .Index(t => t.ApiMessageGuid)
                .Index(t => t.SmsMessageGuid)
                .Index(t => t.EmailMessageGuid);
            
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
                "dbo.ConnectDirrection",
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
                "dbo.ConnectType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.TicketContainerStatus",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ContainerId = c.Long(nullable: false),
                        StatusId = c.Long(nullable: false),
                        OrderNo = c.Long(nullable: false),
                        Route = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TicketStatus", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.TicketConteiner", t => t.ContainerId, cascadeDelete: true)
                .Index(t => new { t.ContainerId, t.OrderNo }, unique: true)
                .Index(t => t.StatusId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeepLinkStatus", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StatusId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.DeepLinkStatus",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.OpCameraImage",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Source = c.String(),
                        ImagePath = c.String(),
                        DateTime = c.DateTime(),
                        SourceDeviceId = c.Long(nullable: false),
                        LabFacelessOpDataId = c.Guid(),
                        LabRegularOpDataId = c.Guid(),
                        LoadOpDataId = c.Guid(),
                        SingleWindowOpDataId = c.Guid(),
                        SecurityCheckInOpDataId = c.Guid(),
                        SecurityCheckOutOpDataId = c.Guid(),
                        ScaleOpDataId = c.Guid(),
                        UnloadGuideOpDataId = c.Guid(),
                        UnloadPointOpDataId = c.Guid(),
                        NonStandartOpDataId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Device", t => t.SourceDeviceId, cascadeDelete: true)
                .ForeignKey("opd.LoadOpData", t => t.LoadOpDataId)
                .ForeignKey("opd.NonStandartOpData", t => t.NonStandartOpDataId)
                .ForeignKey("opd.ScaleOpData", t => t.ScaleOpDataId)
                .ForeignKey("opd.SecurityCheckInOpData", t => t.SecurityCheckInOpDataId)
                .ForeignKey("opd.SecurityCheckOutOpData", t => t.SecurityCheckOutOpDataId)
                .ForeignKey("opd.SingleWindowOpData", t => t.SingleWindowOpDataId)
                .ForeignKey("opd.UnloadGuideOpData", t => t.UnloadGuideOpDataId)
                .ForeignKey("opd.UnloadPointOpData", t => t.UnloadPointOpDataId)
                .ForeignKey("opd.LabRegularOpData", t => t.LabRegularOpDataId)
                .ForeignKey("opd.LabFacelessOpData", t => t.LabFacelessOpDataId)
                .Index(t => t.SourceDeviceId)
                .Index(t => t.LabFacelessOpDataId)
                .Index(t => t.LabRegularOpDataId)
                .Index(t => t.LoadOpDataId)
                .Index(t => t.SingleWindowOpDataId)
                .Index(t => t.SecurityCheckInOpDataId)
                .Index(t => t.SecurityCheckOutOpDataId)
                .Index(t => t.ScaleOpDataId)
                .Index(t => t.UnloadGuideOpDataId)
                .Index(t => t.UnloadPointOpDataId)
                .Index(t => t.NonStandartOpDataId);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ParentDeviceId = c.Long(),
                        IsActive = c.Boolean(nullable: false),
                        TypeId = c.Long(nullable: false),
                        ParamId = c.Long(),
                        StateId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceParam", t => t.ParamId)
                .ForeignKey("dbo.DeviceState", t => t.StateId)
                .ForeignKey("dbo.DeviceType", t => t.TypeId)
                .ForeignKey("dbo.Device", t => t.ParentDeviceId)
                .Index(t => t.ParentDeviceId)
                .Index(t => t.TypeId)
                .Index(t => t.ParamId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.DeviceParam",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ParamJson = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceState",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        LastUpdate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ErrorCode = c.Int(nullable: false),
                        InData = c.String(),
                        OutData = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "opd.LoadOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.OpDataState",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "opd.NonStandartOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Message = c.String(),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "opd.ScaleOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        TypeId = c.Long(nullable: false),
                        TruckWeightDateTime = c.DateTime(),
                        TruckWeightValue = c.Double(),
                        TruckWeightIsAccepted = c.Boolean(),
                        TrailerWeightDateTime = c.DateTime(),
                        TrailerWeightValue = c.Double(),
                        TrailerWeightIsAccepted = c.Boolean(),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "opd.SecurityCheckInOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "opd.SecurityCheckOutOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        IsCameraImagesConfirmed = c.Boolean(),
                        SealCount = c.Int(),
                        SealList = c.String(),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "opd.SingleWindowOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ContactPhoneNo = c.String(),
                        LoadTarget = c.Double(nullable: false),
                        OrganizationId = c.String(maxLength: 250),
                        CreateOperatorId = c.String(maxLength: 250),
                        CreteDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EditOperatorId = c.String(maxLength: 250),
                        EditDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DocumentTypeId = c.String(maxLength: 250),
                        StockId = c.String(maxLength: 250),
                        ReceiverTypeId = c.String(maxLength: 250),
                        ReceiverId = c.String(maxLength: 250),
                        ReceiverAnaliticsId = c.String(maxLength: 250),
                        ProductId = c.String(maxLength: 250),
                        HarvestId = c.String(maxLength: 250),
                        GrossValue = c.Double(),
                        TareValue = c.Double(),
                        NetValue = c.Double(),
                        DriverOneId = c.String(maxLength: 250),
                        DriverTwoId = c.String(maxLength: 250),
                        TransportId = c.String(maxLength: 250),
                        HiredDriverCode = c.String(maxLength: 250),
                        HiredTansportNumber = c.String(maxLength: 250),
                        IncomeInvoiceSeries = c.String(maxLength: 250),
                        IncomeInvoiceNumber = c.String(maxLength: 250),
                        ReceiverDepotId = c.String(maxLength: 250),
                        IsThirdPartyCarrier = c.Boolean(nullable: false),
                        CarrierCode = c.String(maxLength: 250),
                        BuyBudgetId = c.String(maxLength: 250),
                        SellBudgetId = c.String(maxLength: 250),
                        PackingWeightValue = c.Double(),
                        KeeperOrganizationId = c.String(maxLength: 250),
                        OrderCode = c.String(maxLength: 250),
                        SupplyCode = c.String(maxLength: 250),
                        SupplyTypeId = c.String(maxLength: 250),
                        RegistrationDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        InTime = c.DateTime(),
                        OutTime = c.DateTime(),
                        FirstGrossTime = c.DateTime(),
                        FirstTareTime = c.DateTime(),
                        LastGrossTime = c.DateTime(),
                        LastTareTime = c.DateTime(),
                        CollectionPointId = c.String(),
                        LabHumidityName = c.String(),
                        LabImpurityName = c.String(),
                        LabIsInfectioned = c.Boolean(nullable: false),
                        LabHumidityValue = c.Double(),
                        LabImpurityValue = c.Double(),
                        DocHumidityValue = c.Double(),
                        DocImpurityValue = c.Double(),
                        DocNetValue = c.Double(),
                        DocNetDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        ReturnCauseId = c.String(),
                        TrailerId = c.String(maxLength: 250),
                        TrailerNumber = c.String(maxLength: 250),
                        TripTicketNumber = c.String(maxLength: 250),
                        TripTicketDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        WarrantSeries = c.String(maxLength: 250),
                        WarrantNumber = c.String(maxLength: 250),
                        WarrantDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        WarrantManagerName = c.String(maxLength: 250),
                        StampList = c.String(),
                        StatusType = c.String(),
                        RuleNumber = c.String(maxLength: 250),
                        TrailerGrossValue = c.Double(),
                        TrailerTareValue = c.Double(),
                        IncomeDocGrossValue = c.Double(),
                        IncomeDocTareValue = c.Double(),
                        IncomeDocDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        Comments = c.String(maxLength: 250),
                        WeightDeltaValue = c.Double(),
                        SupplyTransportTypeId = c.String(maxLength: 250),
                        LabolatoryOperatorId = c.String(),
                        GrossOperatorId = c.String(),
                        ScaleInNumber = c.Long(nullable: false),
                        ScaleOutNumber = c.Long(nullable: false),
                        BatchNumber = c.String(maxLength: 250),
                        TareOperatorId = c.String(),
                        LoadingOperatorId = c.String(),
                        LoadOutDateTime = c.DateTime(),
                        CarrierRouteId = c.String(maxLength: 250),
                        LabOilContentValue = c.Double(),
                        DeliveryBillId = c.String(maxLength: 250),
                        DeliveryBillCode = c.String(maxLength: 250),
                        InformationCarrier = c.String(maxLength: 250),
                        ProductContents = c.String(),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "opd.UnloadGuideOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        UnloadPointNodeId = c.Long(nullable: false),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "opd.UnloadPointOpData",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StateId = c.Long(nullable: false),
                        NodeId = c.Long(),
                        TicketId = c.Long(),
                        TicketContainerId = c.Long(),
                        CheckInDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        CheckOutDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .ForeignKey("dbo.OpDataState", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId)
                .Index(t => t.StateId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.TicketStatus",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dev.NodeConstraint",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NodeId = c.Long(nullable: false),
                        NomenclatureId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId)
                .Index(t => t.NodeId);
            
            CreateTable(
                "dbo.OpRoutine",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 50),
                        StateCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OpRoutineState",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        OpRoutineId = c.Long(nullable: false),
                        StateNo = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OpRoutine", t => t.OpRoutineId)
                .Index(t => t.OpRoutineId);
            
            CreateTable(
                "dbo.OpRoutineTransition",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        OpRoutineId = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        StartStateId = c.Long(),
                        StopStateId = c.Long(),
                        ProcessorId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OpRoutineStateProcessor", t => t.ProcessorId)
                .ForeignKey("dbo.OpRoutineState", t => t.StartStateId)
                .ForeignKey("dbo.OpRoutineState", t => t.StopStateId)
                .Index(t => t.StartStateId)
                .Index(t => t.StopStateId)
                .Index(t => t.ProcessorId);
            
            CreateTable(
                "dbo.OpRoutineStateProcessor",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrganizationUnitType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        CardRfid = c.String(maxLength: 50),
                        Name = c.String(maxLength: 50),
                        Cards_Id = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Card", t => t.Cards_Id)
                .Index(t => t.Cards_Id);
            
            CreateTable(
                "dbo.RouteTemplate",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        RouteConfig = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.AcceptancePoint",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        Name = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.Budget",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        Name = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.Crop",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        Name = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.Contract",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        Name = c.String(maxLength: 250),
                        StartDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        StopDateTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        ManagerId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.Employee",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        ShortName = c.String(maxLength: 250),
                        FullName = c.String(maxLength: 250),
                        Position = c.String(maxLength: 250),
                        Email = c.String(maxLength: 250),
                        PhoneNo = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.FixedAsset",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        Brand = c.String(maxLength: 250),
                        Model = c.String(maxLength: 250),
                        TypeCode = c.String(maxLength: 250),
                        RegistrationNo = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.Organization",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        ShortName = c.String(maxLength: 250),
                        FullName = c.String(maxLength: 250),
                        Address = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.Partner",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        ShortName = c.String(maxLength: 250),
                        FullName = c.String(maxLength: 250),
                        Address = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.Product",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        ShortName = c.String(maxLength: 250),
                        FullName = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.ReasonForRefund",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        Name = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.Route",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        Name = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.Stock",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        ShortName = c.String(maxLength: 250),
                        FullName = c.String(maxLength: 250),
                        Address = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.Subdivision",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        ShortName = c.String(maxLength: 250),
                        FullName = c.String(maxLength: 250),
                        Address = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.YearOfHarvest",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        Name = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.MeasureUnit",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Code = c.String(maxLength: 250),
                        ShortName = c.String(maxLength: 250),
                        FullName = c.String(maxLength: 250),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.DeliveryBillStatus",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.DeliveryBillType",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.LabDeviceResultType",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.LabHumidityСlassifier",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.LabImpurityСlassifier",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.LabInfectionedСlassifier",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.OrganisationType",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.SupplyType",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ext.SupplyTransportType",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 250),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OpVisa", "User_Id", "dbo.User");
            DropForeignKey("dbo.User", "Cards_Id", "dbo.Card");
            DropForeignKey("dbo.OrganizationUnit", "UnitTypeId", "dbo.OrganizationUnitType");
            DropForeignKey("dbo.OrganizationUnit", "ParentId", "dbo.OrganizationUnit");
            DropForeignKey("dbo.Node", "OrganisationUnitId", "dbo.OrganizationUnit");
            DropForeignKey("dbo.OpRoutineState", "OpRoutineId", "dbo.OpRoutine");
            DropForeignKey("dbo.OpRoutineTransition", "StopStateId", "dbo.OpRoutineState");
            DropForeignKey("dbo.OpRoutineTransition", "StartStateId", "dbo.OpRoutineState");
            DropForeignKey("dbo.OpRoutineTransition", "ProcessorId", "dbo.OpRoutineStateProcessor");
            DropForeignKey("dbo.Node", "OpRoutineId", "dbo.OpRoutine");
            DropForeignKey("dev.NodeConstraint", "NodeId", "dbo.Node");
            DropForeignKey("opd.LabFacelessOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "LabFacelessOpDataId", "opd.LabFacelessOpData");
            DropForeignKey("opd.LabFacelessOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "LabFacelessOpDataId", "opd.LabFacelessOpData");
            DropForeignKey("opd.LabFacelessOpData", "NodeId", "dbo.Node");
            DropForeignKey("opd.LabFacelessOpDataComponent", "LabFacelessOpDataId", "opd.LabFacelessOpData");
            DropForeignKey("dbo.OpVisa", "LabFacelessOpDataComponentId", "opd.LabFacelessOpDataComponent");
            DropForeignKey("dbo.OpVisa", "ExternalUserId", "ext.User");
            DropForeignKey("dbo.Card", "ExternalUserId", "ext.User");
            DropForeignKey("dbo.Ticket", "ContainerId", "dbo.TicketConteiner");
            DropForeignKey("dbo.Ticket", "StatusId", "dbo.TicketStatus");
            DropForeignKey("opd.LabRegularOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "LabRegularOpDataId", "opd.LabRegularOpData");
            DropForeignKey("opd.LabRegularOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "LabRegularOpDataId", "opd.LabRegularOpData");
            DropForeignKey("opd.LoadOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "LoadOpDataId", "opd.LoadOpData");
            DropForeignKey("opd.LoadOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("opd.UnloadPointOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "UnloadPointOpDataId", "opd.UnloadPointOpData");
            DropForeignKey("opd.UnloadPointOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "UnloadPointOpDataId", "opd.UnloadPointOpData");
            DropForeignKey("opd.UnloadPointOpData", "NodeId", "dbo.Node");
            DropForeignKey("opd.UnloadGuideOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "UnloadGuideOpDataId", "opd.UnloadGuideOpData");
            DropForeignKey("opd.UnloadGuideOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "UnloadGuideOpDataId", "opd.UnloadGuideOpData");
            DropForeignKey("opd.UnloadGuideOpData", "NodeId", "dbo.Node");
            DropForeignKey("opd.SingleWindowOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "SingleWindowOpDataId", "opd.SingleWindowOpData");
            DropForeignKey("opd.SingleWindowOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "SingleWindowOpDataId", "opd.SingleWindowOpData");
            DropForeignKey("opd.SingleWindowOpData", "NodeId", "dbo.Node");
            DropForeignKey("opd.SecurityCheckOutOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "SecurityCheckOutOpDataId", "opd.SecurityCheckOutOpData");
            DropForeignKey("opd.SecurityCheckOutOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "SecurityCheckOutOpDataId", "opd.SecurityCheckOutOpData");
            DropForeignKey("opd.SecurityCheckOutOpData", "NodeId", "dbo.Node");
            DropForeignKey("opd.SecurityCheckInOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "SecurityCheckInOpDataId", "opd.SecurityCheckInOpData");
            DropForeignKey("opd.SecurityCheckInOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "SecurityCheckInOpDataId", "opd.SecurityCheckInOpData");
            DropForeignKey("opd.SecurityCheckInOpData", "NodeId", "dbo.Node");
            DropForeignKey("opd.ScaleOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "ScaleTareOpDataId", "opd.ScaleOpData");
            DropForeignKey("opd.ScaleOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "ScaleOpDataId", "opd.ScaleOpData");
            DropForeignKey("opd.ScaleOpData", "NodeId", "dbo.Node");
            DropForeignKey("opd.NonStandartOpData", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.OpVisa", "NonStandartOpData_Id", "opd.NonStandartOpData");
            DropForeignKey("opd.NonStandartOpData", "StateId", "dbo.OpDataState");
            DropForeignKey("dbo.OpCameraImage", "NonStandartOpDataId", "opd.NonStandartOpData");
            DropForeignKey("opd.NonStandartOpData", "NodeId", "dbo.Node");
            DropForeignKey("dbo.OpCameraImage", "LoadOpDataId", "opd.LoadOpData");
            DropForeignKey("opd.LoadOpData", "NodeId", "dbo.Node");
            DropForeignKey("dbo.OpCameraImage", "SourceDeviceId", "dbo.Device");
            DropForeignKey("dbo.Device", "ParentDeviceId", "dbo.Device");
            DropForeignKey("dbo.Device", "TypeId", "dbo.DeviceType");
            DropForeignKey("dbo.Device", "StateId", "dbo.DeviceState");
            DropForeignKey("dbo.Device", "ParamId", "dbo.DeviceParam");
            DropForeignKey("opd.LabRegularOpData", "NodeId", "dbo.Node");
            DropForeignKey("dbo.DeepLink", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.DeepLink", "StatusId", "dbo.DeepLinkStatus");
            DropForeignKey("dbo.TicketConteiner", "StatusId", "dbo.TicketContainerStatus");
            DropForeignKey("dbo.Connect", "TicketContainerId", "dbo.TicketConteiner");
            DropForeignKey("dbo.Connect", "SmsMessageGuid", "dbo.ConnectSmsMessage");
            DropForeignKey("dbo.Connect", "EmailMessageGuid", "dbo.ConnectEmailMessage");
            DropForeignKey("dbo.Connect", "TypeId", "dbo.ConnectType");
            DropForeignKey("dbo.Connect", "StateId", "dbo.ConnectState");
            DropForeignKey("dbo.Connect", "DirrectionId", "dbo.ConnectDirrection");
            DropForeignKey("dbo.Connect", "ApiMessageGuid", "dbo.ConnectApiMessage");
            DropForeignKey("dbo.Card", "TicketContainerId", "dbo.TicketConteiner");
            DropForeignKey("dbo.Card", "TypeId", "dbo.CardType");
            DropIndex("dbo.User", new[] { "Cards_Id" });
            DropIndex("dbo.OpRoutineTransition", new[] { "ProcessorId" });
            DropIndex("dbo.OpRoutineTransition", new[] { "StopStateId" });
            DropIndex("dbo.OpRoutineTransition", new[] { "StartStateId" });
            DropIndex("dbo.OpRoutineState", new[] { "OpRoutineId" });
            DropIndex("dev.NodeConstraint", new[] { "NodeId" });
            DropIndex("opd.UnloadPointOpData", new[] { "TicketId" });
            DropIndex("opd.UnloadPointOpData", new[] { "NodeId" });
            DropIndex("opd.UnloadPointOpData", new[] { "StateId" });
            DropIndex("opd.UnloadGuideOpData", new[] { "TicketId" });
            DropIndex("opd.UnloadGuideOpData", new[] { "NodeId" });
            DropIndex("opd.UnloadGuideOpData", new[] { "StateId" });
            DropIndex("opd.SingleWindowOpData", new[] { "TicketId" });
            DropIndex("opd.SingleWindowOpData", new[] { "NodeId" });
            DropIndex("opd.SingleWindowOpData", new[] { "StateId" });
            DropIndex("opd.SecurityCheckOutOpData", new[] { "TicketId" });
            DropIndex("opd.SecurityCheckOutOpData", new[] { "NodeId" });
            DropIndex("opd.SecurityCheckOutOpData", new[] { "StateId" });
            DropIndex("opd.SecurityCheckInOpData", new[] { "TicketId" });
            DropIndex("opd.SecurityCheckInOpData", new[] { "NodeId" });
            DropIndex("opd.SecurityCheckInOpData", new[] { "StateId" });
            DropIndex("opd.ScaleOpData", new[] { "TicketId" });
            DropIndex("opd.ScaleOpData", new[] { "NodeId" });
            DropIndex("opd.ScaleOpData", new[] { "StateId" });
            DropIndex("opd.NonStandartOpData", new[] { "TicketId" });
            DropIndex("opd.NonStandartOpData", new[] { "NodeId" });
            DropIndex("opd.NonStandartOpData", new[] { "StateId" });
            DropIndex("opd.LoadOpData", new[] { "TicketId" });
            DropIndex("opd.LoadOpData", new[] { "NodeId" });
            DropIndex("opd.LoadOpData", new[] { "StateId" });
            DropIndex("dbo.Device", new[] { "StateId" });
            DropIndex("dbo.Device", new[] { "ParamId" });
            DropIndex("dbo.Device", new[] { "TypeId" });
            DropIndex("dbo.Device", new[] { "ParentDeviceId" });
            DropIndex("dbo.OpCameraImage", new[] { "NonStandartOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "UnloadPointOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "UnloadGuideOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "ScaleOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "SecurityCheckOutOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "SecurityCheckInOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "SingleWindowOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "LoadOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "LabRegularOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "LabFacelessOpDataId" });
            DropIndex("dbo.OpCameraImage", new[] { "SourceDeviceId" });
            DropIndex("opd.LabRegularOpData", new[] { "TicketId" });
            DropIndex("opd.LabRegularOpData", new[] { "NodeId" });
            DropIndex("opd.LabRegularOpData", new[] { "StateId" });
            DropIndex("dbo.DeepLink", new[] { "TicketId" });
            DropIndex("dbo.DeepLink", new[] { "StatusId" });
            DropIndex("dbo.Ticket", new[] { "StatusId" });
            DropIndex("dbo.Ticket", new[] { "ContainerId", "OrderNo" });
            DropIndex("dbo.Connect", new[] { "EmailMessageGuid" });
            DropIndex("dbo.Connect", new[] { "SmsMessageGuid" });
            DropIndex("dbo.Connect", new[] { "ApiMessageGuid" });
            DropIndex("dbo.Connect", new[] { "DirrectionId" });
            DropIndex("dbo.Connect", new[] { "StateId" });
            DropIndex("dbo.Connect", new[] { "TypeId" });
            DropIndex("dbo.Connect", new[] { "TicketContainerId" });
            DropIndex("dbo.TicketConteiner", new[] { "StatusId" });
            DropIndex("dbo.Card", new[] { "TicketContainerId" });
            DropIndex("dbo.Card", new[] { "ExternalUserId" });
            DropIndex("dbo.Card", new[] { "TypeId" });
            DropIndex("dbo.OpVisa", new[] { "User_Id" });
            DropIndex("dbo.OpVisa", new[] { "NonStandartOpData_Id" });
            DropIndex("dbo.OpVisa", new[] { "LabFacelessOpDataComponentId" });
            DropIndex("dbo.OpVisa", new[] { "UnloadGuideOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "UnloadPointOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "ScaleTareOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "SecurityCheckOutOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "SecurityCheckInOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "SingleWindowOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "LoadOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "LabRegularOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "LabFacelessOpDataId" });
            DropIndex("dbo.OpVisa", new[] { "ExternalUserId" });
            DropIndex("opd.LabFacelessOpDataComponent", new[] { "LabFacelessOpDataId" });
            DropIndex("opd.LabFacelessOpData", new[] { "TicketId" });
            DropIndex("opd.LabFacelessOpData", new[] { "NodeId" });
            DropIndex("opd.LabFacelessOpData", new[] { "StateId" });
            DropIndex("dbo.Node", new[] { "OpRoutineId" });
            DropIndex("dbo.Node", new[] { "OrganisationUnitId" });
            DropIndex("dbo.OrganizationUnit", new[] { "UnitTypeId" });
            DropIndex("dbo.OrganizationUnit", new[] { "ParentId" });
            DropTable("ext.SupplyTransportType");
            DropTable("ext.SupplyType");
            DropTable("ext.OrganisationType");
            DropTable("ext.LabInfectionedСlassifier");
            DropTable("ext.LabImpurityСlassifier");
            DropTable("ext.LabHumidityСlassifier");
            DropTable("ext.LabDeviceResultType");
            DropTable("ext.DeliveryBillType");
            DropTable("ext.DeliveryBillStatus");
            DropTable("ext.MeasureUnit");
            DropTable("ext.YearOfHarvest");
            DropTable("ext.Subdivision");
            DropTable("ext.Stock");
            DropTable("ext.Route");
            DropTable("ext.ReasonForRefund");
            DropTable("ext.Product");
            DropTable("ext.Partner");
            DropTable("ext.Organization");
            DropTable("ext.FixedAsset");
            DropTable("ext.Employee");
            DropTable("ext.Contract");
            DropTable("ext.Crop");
            DropTable("ext.Budget");
            DropTable("ext.AcceptancePoint");
            DropTable("dbo.RouteTemplate");
            DropTable("dbo.User");
            DropTable("dbo.OrganizationUnitType");
            DropTable("dbo.OpRoutineStateProcessor");
            DropTable("dbo.OpRoutineTransition");
            DropTable("dbo.OpRoutineState");
            DropTable("dbo.OpRoutine");
            DropTable("dev.NodeConstraint");
            DropTable("dbo.TicketStatus");
            DropTable("opd.UnloadPointOpData");
            DropTable("opd.UnloadGuideOpData");
            DropTable("opd.SingleWindowOpData");
            DropTable("opd.SecurityCheckOutOpData");
            DropTable("opd.SecurityCheckInOpData");
            DropTable("opd.ScaleOpData");
            DropTable("opd.NonStandartOpData");
            DropTable("dbo.OpDataState");
            DropTable("opd.LoadOpData");
            DropTable("dbo.DeviceType");
            DropTable("dbo.DeviceState");
            DropTable("dbo.DeviceParam");
            DropTable("dbo.Device");
            DropTable("dbo.OpCameraImage");
            DropTable("opd.LabRegularOpData");
            DropTable("dbo.DeepLinkStatus");
            DropTable("dbo.DeepLink");
            DropTable("dbo.Ticket");
            DropTable("dbo.TicketContainerStatus");
            DropTable("dbo.ConnectSmsMessage");
            DropTable("dbo.ConnectEmailMessage");
            DropTable("dbo.ConnectType");
            DropTable("dbo.ConnectState");
            DropTable("dbo.ConnectDirrection");
            DropTable("dbo.ConnectApiMessage");
            DropTable("dbo.Connect");
            DropTable("dbo.TicketConteiner");
            DropTable("dbo.CardType");
            DropTable("dbo.Card");
            DropTable("ext.User");
            DropTable("dbo.OpVisa");
            DropTable("opd.LabFacelessOpDataComponent");
            DropTable("opd.LabFacelessOpData");
            DropTable("dbo.Node");
            DropTable("dbo.OrganizationUnit");
        }
    }
}

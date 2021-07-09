namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcceptancePoints",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TypeId = c.Int(nullable: false),
                        No = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        EmployeeId = c.Guid(),
                        TicketContainerId = c.Int(),
                        ParentCardId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.TicketContainer", t => t.TicketContainerId)
                .ForeignKey("dbo.Cards", t => t.ParentCardId)
                .Index(t => t.EmployeeId)
                .Index(t => t.TicketContainerId)
                .Index(t => t.ParentCardId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        ShortName = c.String(),
                        FullName = c.String(),
                        Position = c.String(),
                        Email = c.String(),
                        PhoneNo = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Guid(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleAssignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        NodeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.NodeId);
            
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        OrganizationUnitId = c.Int(nullable: false),
                        OpRoutineId = c.Int(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsEmergency = c.Boolean(nullable: false),
                        Quota = c.Int(nullable: false),
                        Config = c.String(),
                        Context = c.String(),
                        MaximumProcessingTime = c.Int(nullable: false),
                        MaximumDepartureTime = c.Int(nullable: false),
                        NodeGroup = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CentralLabOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SampleCheckInDateTime = c.DateTime(),
                        SampleCheckOutTime = c.DateTime(),
                        LaboratoryComment = c.String(),
                        CollisionComment = c.String(),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.OpCameraImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Source = c.String(),
                        ImagePath = c.String(),
                        DateTime = c.DateTime(),
                        DeviceId = c.Int(nullable: false),
                        LabFacelessOpDataId = c.Guid(),
                        LabRegularOpDataId = c.Guid(),
                        LoadOpDataId = c.Guid(),
                        SingleWindowOpDataId = c.Guid(),
                        SecurityCheckInOpDataId = c.Guid(),
                        SecurityCheckOutOpDataId = c.Guid(),
                        ScaleOpDataId = c.Guid(),
                        UnloadGuideOpDataId = c.Guid(),
                        UnloadPointOpDataId = c.Guid(),
                        LoadGuideOpDataId = c.Guid(),
                        LoadPointOpDataId = c.Guid(),
                        NonStandartOpDataId = c.Guid(),
                        SecurityCheckReviewOpData_Id = c.Guid(),
                        CentralLabOpData_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.LoadGuideOpDatas", t => t.LoadGuideOpDataId)
                .ForeignKey("dbo.LoadPointOpDatas", t => t.LoadPointOpDataId)
                .ForeignKey("dbo.NonStandartOpDatas", t => t.NonStandartOpDataId)
                .ForeignKey("dbo.SingleWindowOpDatas", t => t.SingleWindowOpDataId)
                .ForeignKey("dbo.ScaleOpDatas", t => t.ScaleOpDataId)
                .ForeignKey("dbo.SecurityCheckInOpDatas", t => t.SecurityCheckInOpDataId)
                .ForeignKey("dbo.SecurityCheckOutOpDatas", t => t.SecurityCheckOutOpDataId)
                .ForeignKey("dbo.SecurityCheckReviewOpDatas", t => t.SecurityCheckReviewOpData_Id)
                .ForeignKey("dbo.UnloadGuideOpDatas", t => t.UnloadGuideOpDataId)
                .ForeignKey("dbo.UnloadPointOpDatas", t => t.UnloadPointOpDataId)
                .ForeignKey("dbo.LabFacelessOpDatas", t => t.LabFacelessOpDataId)
                .ForeignKey("dbo.CentralLabOpDatas", t => t.CentralLabOpData_Id)
                .Index(t => t.DeviceId)
                .Index(t => t.LabFacelessOpDataId)
                .Index(t => t.SingleWindowOpDataId)
                .Index(t => t.SecurityCheckInOpDataId)
                .Index(t => t.SecurityCheckOutOpDataId)
                .Index(t => t.ScaleOpDataId)
                .Index(t => t.UnloadGuideOpDataId)
                .Index(t => t.UnloadPointOpDataId)
                .Index(t => t.LoadGuideOpDataId)
                .Index(t => t.LoadPointOpDataId)
                .Index(t => t.NonStandartOpDataId)
                .Index(t => t.SecurityCheckReviewOpData_Id)
                .Index(t => t.CentralLabOpData_Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentDeviceId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        TypeId = c.Int(nullable: false),
                        DeviceParamId = c.Int(nullable: false),
                        DeviceStateId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.ParentDeviceId)
                .ForeignKey("dbo.DeviceParams", t => t.DeviceParamId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceStates", t => t.DeviceStateId, cascadeDelete: true)
                .Index(t => t.ParentDeviceId)
                .Index(t => t.DeviceParamId)
                .Index(t => t.DeviceStateId);
            
            CreateTable(
                "dbo.DeviceParams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParamJson = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastUpdate = c.DateTime(),
                        ErrorCode = c.Int(nullable: false),
                        InData = c.String(),
                        OutData = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LabFacelessOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImpurityClassId = c.String(),
                        ImpurityValue = c.Single(),
                        HumidityClassId = c.String(),
                        HumidityValue = c.Single(),
                        InfectionedClassId = c.String(),
                        EffectiveValue = c.Single(),
                        Comment = c.String(),
                        DataSourceName = c.String(),
                        ImpurityValueSourceId = c.Int(),
                        HumidityValueSourceId = c.Int(),
                        InfectionedValueSourceId = c.Int(),
                        EffectiveValueSourceId = c.Int(),
                        CollisionComment = c.String(),
                        LabEffectiveClassifier = c.String(),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.LabFacelessOpDataComponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LabFacelessOpDataId = c.Guid(nullable: false),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(nullable: false),
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
                .ForeignKey("dbo.LabFacelessOpDatas", t => t.LabFacelessOpDataId, cascadeDelete: true)
                .ForeignKey("dbo.Nodes", t => t.NodeId, cascadeDelete: true)
                .Index(t => t.LabFacelessOpDataId)
                .Index(t => t.NodeId);
            
            CreateTable(
                "dbo.OpVisas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(),
                        EmployeeId = c.Guid(),
                        Message = c.String(),
                        LabFacelessOpDataId = c.Guid(),
                        LabRegularOpDataId = c.Guid(),
                        SingleWindowOpDataId = c.Guid(),
                        SecurityCheckInOpDataId = c.Guid(),
                        SecurityCheckOutOpDataId = c.Guid(),
                        SecurityCheckReviewOpDataId = c.Guid(),
                        ScaleTareOpDataId = c.Guid(),
                        UnloadPointOpDataId = c.Guid(),
                        UnloadGuideOpDataId = c.Guid(),
                        LoadPointOpDataId = c.Guid(),
                        LoadGuideOpDataId = c.Guid(),
                        CentralLaboratoryOpData = c.Guid(),
                        MixedFeedSiloId = c.Int(),
                        OpRoutineStateId = c.Int(nullable: false),
                        LabFacelessOpDataComponentId = c.Int(),
                        CentralLabOpData_Id = c.Guid(),
                        NonStandartOpData_Id = c.Guid(),
                        ScaleOpData_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CentralLabOpDatas", t => t.CentralLabOpData_Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.LabFacelessOpDatas", t => t.LabFacelessOpDataId)
                .ForeignKey("dbo.LabFacelessOpDataComponents", t => t.LabFacelessOpDataComponentId)
                .ForeignKey("dbo.LoadGuideOpDatas", t => t.LoadGuideOpDataId)
                .ForeignKey("dbo.LoadPointOpDatas", t => t.LoadPointOpDataId)
                .ForeignKey("dbo.NonStandartOpDatas", t => t.NonStandartOpData_Id)
                .ForeignKey("dbo.SingleWindowOpDatas", t => t.SingleWindowOpDataId)
                .ForeignKey("dbo.ScaleOpDatas", t => t.ScaleOpData_Id)
                .ForeignKey("dbo.SecurityCheckInOpDatas", t => t.SecurityCheckInOpDataId)
                .ForeignKey("dbo.SecurityCheckOutOpDatas", t => t.SecurityCheckOutOpDataId)
                .ForeignKey("dbo.SecurityCheckReviewOpDatas", t => t.SecurityCheckReviewOpDataId)
                .ForeignKey("dbo.UnloadGuideOpDatas", t => t.UnloadGuideOpDataId)
                .ForeignKey("dbo.UnloadPointOpDatas", t => t.UnloadPointOpDataId)
                .ForeignKey("dbo.MixedFeedSiloes", t => t.MixedFeedSiloId)
                .Index(t => t.EmployeeId)
                .Index(t => t.LabFacelessOpDataId)
                .Index(t => t.SingleWindowOpDataId)
                .Index(t => t.SecurityCheckInOpDataId)
                .Index(t => t.SecurityCheckOutOpDataId)
                .Index(t => t.SecurityCheckReviewOpDataId)
                .Index(t => t.UnloadPointOpDataId)
                .Index(t => t.UnloadGuideOpDataId)
                .Index(t => t.LoadPointOpDataId)
                .Index(t => t.LoadGuideOpDataId)
                .Index(t => t.MixedFeedSiloId)
                .Index(t => t.LabFacelessOpDataComponentId)
                .Index(t => t.CentralLabOpData_Id)
                .Index(t => t.NonStandartOpData_Id)
                .Index(t => t.ScaleOpData_Id);
            
            CreateTable(
                "dbo.LoadGuideOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoadPointNodeId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                        Node_Id = c.Int(),
                        Node_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.LoadPointNodeId, cascadeDelete: true)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .ForeignKey("dbo.Nodes", t => t.Node_Id)
                .ForeignKey("dbo.Nodes", t => t.Node_Id1)
                .Index(t => t.LoadPointNodeId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId)
                .Index(t => t.Node_Id)
                .Index(t => t.Node_Id1);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketContainerId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        OrderNo = c.Int(nullable: false),
                        SecondaryRouteTemplateId = c.Int(),
                        SecondaryRouteItemIndex = c.Int(nullable: false),
                        RouteTemplateId = c.Int(),
                        RouteItemIndex = c.Int(nullable: false),
                        RouteType = c.Int(nullable: false),
                        NodeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.RouteTemplates", t => t.RouteTemplateId)
                .ForeignKey("dbo.TicketContainer", t => t.TicketContainerId, cascadeDelete: true)
                .Index(t => t.TicketContainerId)
                .Index(t => t.RouteTemplateId)
                .Index(t => t.NodeId);
            
            CreateTable(
                "dbo.LoadPointOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MixedFeedSiloId = c.Int(),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MixedFeedSiloes", t => t.MixedFeedSiloId)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .Index(t => t.MixedFeedSiloId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.MixedFeedSiloes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        Drive = c.Int(nullable: false),
                        LoadQueue = c.Int(nullable: false),
                        SiloWeight = c.Single(nullable: false),
                        SiloEmpty = c.Single(nullable: false),
                        SiloFull = c.Single(nullable: false),
                        Specification = c.String(),
                        ProductId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        ShortName = c.String(),
                        FullName = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NonStandartOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Message = c.String(),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.RouteTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RouteConfig = c.String(),
                        OwnerId = c.String(),
                        IsMain = c.Boolean(nullable: false),
                        IsInQueue = c.Boolean(nullable: false),
                        IsTechnological = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SingleWindowOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContactPhoneNo = c.String(),
                        LoadTarget = c.Double(nullable: false),
                        LoadTargetDeviationPlus = c.Int(nullable: false),
                        LoadTargetDeviationMinus = c.Int(nullable: false),
                        OrganizationId = c.Guid(),
                        OrganizationTitle = c.String(),
                        ProductTitle = c.String(),
                        CreateOperatorId = c.Guid(),
                        CreateDate = c.DateTime(),
                        EditOperatorId = c.Guid(),
                        EditDate = c.DateTime(),
                        DocumentTypeId = c.String(),
                        StockId = c.Guid(),
                        ReceiverTypeId = c.Guid(),
                        ReceiverId = c.Guid(),
                        ReceiverTitle = c.String(),
                        ReceiverAnaliticsId = c.Guid(),
                        ProductId = c.Guid(),
                        HarvestId = c.Guid(),
                        GrossValue = c.Double(),
                        TareValue = c.Double(),
                        NetValue = c.Double(),
                        DriverOneId = c.Guid(),
                        DriverTwoId = c.Guid(),
                        TransportId = c.Guid(),
                        HiredDriverCode = c.String(),
                        TransportNumber = c.String(),
                        IncomeInvoiceSeries = c.String(),
                        IncomeInvoiceNumber = c.String(),
                        ReceiverDepotId = c.Guid(),
                        IsThirdPartyCarrier = c.Boolean(nullable: false),
                        CarrierCode = c.String(),
                        BuyBudgetId = c.Guid(),
                        SellBudgetId = c.Guid(),
                        PackingWeightValue = c.Double(),
                        KeeperOrganizationId = c.Guid(),
                        OrderCode = c.String(),
                        SupplyCode = c.String(),
                        SupplyTypeId = c.Guid(),
                        RegistrationDateTime = c.DateTime(),
                        InTime = c.DateTime(),
                        OutTime = c.DateTime(),
                        FirstGrossTime = c.DateTime(),
                        FirstTareTime = c.DateTime(),
                        LastGrossTime = c.DateTime(),
                        LastTareTime = c.DateTime(),
                        CollectionPointId = c.Guid(),
                        LabHumidityName = c.String(),
                        LabImpurityName = c.String(),
                        LabIsInfectioned = c.Boolean(nullable: false),
                        LabHumidityValue = c.Double(),
                        LabImpurityValue = c.Double(),
                        DocHumidityValue = c.Double(),
                        DocImpurityValue = c.Double(),
                        DocNetValue = c.Double(),
                        DocNetDateTime = c.DateTime(),
                        ReturnCauseId = c.String(),
                        TrailerId = c.Guid(),
                        TripTicketNumber = c.String(),
                        TripTicketDateTime = c.DateTime(),
                        WarrantSeries = c.String(),
                        WarrantNumber = c.String(),
                        WarrantDateTime = c.DateTime(),
                        WarrantManagerName = c.String(),
                        StampList = c.String(),
                        StatusType = c.String(),
                        RuleNumber = c.String(),
                        TrailerGrossValue = c.Double(),
                        TrailerTareValue = c.Double(),
                        IncomeDocGrossValue = c.Double(),
                        IncomeDocTareValue = c.Double(),
                        IncomeDocDateTime = c.DateTime(),
                        Comments = c.String(),
                        WeightDeltaValue = c.Double(),
                        SupplyTransportTypeId = c.String(),
                        LabolatoryOperatorId = c.Guid(),
                        GrossOperatorId = c.Guid(),
                        ScaleInNumber = c.Int(nullable: false),
                        ScaleOutNumber = c.Int(nullable: false),
                        BatchNumber = c.String(),
                        TareOperatorId = c.Guid(),
                        LoadingOperatorId = c.Guid(),
                        LoadOutDateTime = c.DateTime(),
                        CarrierRouteId = c.String(),
                        LabOilContentValue = c.Double(),
                        DeliveryBillId = c.String(),
                        DeliveryBillCode = c.String(),
                        InformationCarrier = c.String(),
                        CustomPartnerName = c.String(),
                        CarrierId = c.Guid(),
                        ProductContents = c.String(),
                        BarCode = c.String(),
                        OriginalТТN = c.String(),
                        TrailerNumber = c.String(),
                        ContractCarrierId = c.Guid(),
                        SenderWeight = c.Double(),
                        DriverPhotoId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                        RouteTemplate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .ForeignKey("dbo.RouteTemplates", t => t.RouteTemplate_Id)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId)
                .Index(t => t.RouteTemplate_Id);
            
            CreateTable(
                "dbo.ScaleOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeId = c.Int(nullable: false),
                        TruckWeightDateTime = c.DateTime(),
                        TruckWeightValue = c.Double(),
                        TruckWeightIsAccepted = c.Boolean(),
                        TrailerIsAvailable = c.Boolean(nullable: false),
                        GuardianPresence = c.Boolean(nullable: false),
                        TrailerWeightDateTime = c.DateTime(),
                        TrailerWeightValue = c.Double(),
                        TrailerWeightIsAccepted = c.Boolean(),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.SecurityCheckInOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.SecurityCheckOutOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsCameraImagesConfirmed = c.Boolean(),
                        SealList = c.String(),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.SecurityCheckReviewOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.TicketContainer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TrafficHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketContainerId = c.Int(nullable: false),
                        NodeId = c.Int(nullable: false),
                        EntranceTime = c.DateTime(),
                        DepartureTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId, cascadeDelete: true)
                .ForeignKey("dbo.TicketContainer", t => t.TicketContainerId, cascadeDelete: true)
                .Index(t => t.TicketContainerId)
                .Index(t => t.NodeId);
            
            CreateTable(
                "dbo.TicketFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FilePath = c.String(),
                        DateTime = c.DateTime(),
                        TicketId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.UnloadGuideOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UnloadPointNodeId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                        Node_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .ForeignKey("dbo.Nodes", t => t.UnloadPointNodeId, cascadeDelete: true)
                .ForeignKey("dbo.Nodes", t => t.Node_Id)
                .Index(t => t.UnloadPointNodeId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId)
                .Index(t => t.Node_Id);
            
            CreateTable(
                "dbo.UnloadPointOpDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Valve = c.String(),
                        StateId = c.Int(nullable: false),
                        NodeId = c.Int(),
                        TicketId = c.Int(),
                        TicketContainerId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        CheckOutDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .Index(t => t.NodeId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        StartDateTime = c.DateTime(),
                        StopDateTime = c.DateTime(),
                        ManagerId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Crops",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeliveryBillStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeliveryBillTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DriverCheckInOpDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NodeId = c.Int(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        Driver = c.String(),
                        Truck = c.String(),
                        Trailer = c.String(),
                        DriverPhotoId = c.Int(),
                        CheckInDateTime = c.DateTime(),
                        TicketId = c.Int(),
                        IsInvited = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DriverPhotoes", t => t.DriverPhotoId)
                .ForeignKey("dbo.Nodes", t => t.NodeId, cascadeDelete: true)
                .ForeignKey("dbo.Tickets", t => t.TicketId)
                .Index(t => t.NodeId)
                .Index(t => t.DriverPhotoId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.DriverPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        PhoneNumber = c.String(),
                        DeviceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.DeviceId);
            
            CreateTable(
                "dbo.DriversBlackListRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExternalUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        ShortName = c.String(),
                        FullName = c.String(),
                        EmployeeId = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FixedAssets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Brand = c.String(),
                        Model = c.String(),
                        TypeCode = c.String(),
                        RegistrationNo = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LabHumidityСlassifier",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LabImpurityСlassifier",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LabInfectionedСlassifier",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MeasureUnits",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        ShortName = c.String(),
                        FullName = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardId = c.String(maxLength: 128),
                        TypeId = c.Int(nullable: false),
                        Text = c.String(),
                        Created = c.DateTime(nullable: false),
                        Receiver = c.String(),
                        AttachmentPath = c.String(),
                        DeliveryId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        RetryCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.CardId)
                .Index(t => t.CardId);
            
            CreateTable(
                "dbo.NodeProcessingMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NodeId = c.Int(nullable: false),
                        Message = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId, cascadeDelete: true)
                .Index(t => t.NodeId);
            
            CreateTable(
                "dbo.OpDataEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        NodeId = c.Int(nullable: false),
                        EmployeeId = c.String(),
                        OpDataEventType = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        TypeOfTransaction = c.String(),
                        Cause = c.String(),
                        Weight = c.Double(),
                        Employee_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Nodes", t => t.NodeId, cascadeDelete: true)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId)
                .Index(t => t.NodeId)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.Organisations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        ShortName = c.String(),
                        FullName = c.String(),
                        Address = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OriginTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OwnTransport",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardId = c.String(maxLength: 128),
                        LongRangeCardId = c.String(maxLength: 128),
                        TruckNo = c.String(),
                        TrailerNo = c.String(),
                        Driver = c.String(),
                        TypeId = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.CardId)
                .ForeignKey("dbo.Cards", t => t.LongRangeCardId)
                .Index(t => t.CardId)
                .Index(t => t.LongRangeCardId);
            
            CreateTable(
                "dbo.PackingTare",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Weight = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Partners",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        ShortName = c.String(),
                        FullName = c.String(),
                        Address = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                        CarrierDriverId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QueuePatternItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        PriorityId = c.Int(nullable: false),
                        PartnerId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Partners", t => t.PartnerId)
                .Index(t => t.PartnerId);
            
            CreateTable(
                "dbo.PhoneDictionaries",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        EmployeePosition = c.String(),
                        IsVisibleForSingleWindow = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhoneInformTicketAssignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneDictionaryId = c.Int(nullable: false),
                        TicketId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhoneDictionaries", t => t.PhoneDictionaryId, cascadeDelete: true)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.PhoneDictionaryId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.QueueRegisters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegisterTime = c.DateTime(nullable: false),
                        SMSTimeAllowed = c.DateTime(),
                        IsAllowedToEnterTerritory = c.Boolean(nullable: false),
                        IsSMSSend = c.Boolean(nullable: false),
                        RouteTemplateId = c.Int(nullable: false),
                        TicketContainerId = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        TrailerPlate = c.String(),
                        TruckPlate = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RouteTemplates", t => t.RouteTemplateId, cascadeDelete: true)
                .ForeignKey("dbo.TicketContainer", t => t.TicketContainerId, cascadeDelete: true)
                .Index(t => t.RouteTemplateId)
                .Index(t => t.TicketContainerId);
            
            CreateTable(
                "dbo.ReasonForRefunds",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreRegistrationAdminEmail = c.String(),
                        AdminEmail = c.String(),
                        QueueDisplayText = c.String(),
                        LabDataExpireMinutes = c.Int(nullable: false),
                        QueueEntranceTimeout = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        ShortName = c.String(),
                        FullName = c.String(),
                        Address = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                        CustomerId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subdivisions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        ShortName = c.String(),
                        FullName = c.String(),
                        Address = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SupplyTransportTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SupplyTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TelegramBots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Token = c.String(),
                        ChatId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketPackingTare",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        PackingTareId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PackingTare", t => t.PackingTareId, cascadeDelete: true)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.TicketId)
                .Index(t => t.PackingTareId);
            
            CreateTable(
                "dbo.TrailersBlackListRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrailerNo = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransportBlackListRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransportNo = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.YearOfHarvests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        IsFolder = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketPackingTare", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.TicketPackingTare", "PackingTareId", "dbo.PackingTare");
            DropForeignKey("dbo.QueueRegisters", "TicketContainerId", "dbo.TicketContainer");
            DropForeignKey("dbo.QueueRegisters", "RouteTemplateId", "dbo.RouteTemplates");
            DropForeignKey("dbo.PhoneInformTicketAssignments", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.PhoneInformTicketAssignments", "PhoneDictionaryId", "dbo.PhoneDictionaries");
            DropForeignKey("dbo.QueuePatternItems", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.OwnTransport", "LongRangeCardId", "dbo.Cards");
            DropForeignKey("dbo.OwnTransport", "CardId", "dbo.Cards");
            DropForeignKey("dbo.OpDataEvents", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.OpDataEvents", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.OpDataEvents", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.NodeProcessingMessages", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.Messages", "CardId", "dbo.Cards");
            DropForeignKey("dbo.DriverCheckInOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.DriverCheckInOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.DriverCheckInOpDatas", "DriverPhotoId", "dbo.DriverPhotoes");
            DropForeignKey("dbo.DriverPhotoes", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Cards", "ParentCardId", "dbo.Cards");
            DropForeignKey("dbo.EmployeeRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoleAssignments", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UnloadGuideOpDatas", "Node_Id", "dbo.Nodes");
            DropForeignKey("dbo.LoadGuideOpDatas", "Node_Id1", "dbo.Nodes");
            DropForeignKey("dbo.LoadGuideOpDatas", "Node_Id", "dbo.Nodes");
            DropForeignKey("dbo.OpCameraImages", "CentralLabOpData_Id", "dbo.CentralLabOpDatas");
            DropForeignKey("dbo.OpCameraImages", "LabFacelessOpDataId", "dbo.LabFacelessOpDatas");
            DropForeignKey("dbo.LabFacelessOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.OpVisas", "MixedFeedSiloId", "dbo.MixedFeedSiloes");
            DropForeignKey("dbo.UnloadPointOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.OpVisas", "UnloadPointOpDataId", "dbo.UnloadPointOpDatas");
            DropForeignKey("dbo.OpCameraImages", "UnloadPointOpDataId", "dbo.UnloadPointOpDatas");
            DropForeignKey("dbo.UnloadPointOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.UnloadGuideOpDatas", "UnloadPointNodeId", "dbo.Nodes");
            DropForeignKey("dbo.UnloadGuideOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.OpVisas", "UnloadGuideOpDataId", "dbo.UnloadGuideOpDatas");
            DropForeignKey("dbo.OpCameraImages", "UnloadGuideOpDataId", "dbo.UnloadGuideOpDatas");
            DropForeignKey("dbo.UnloadGuideOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.TicketFiles", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.TrafficHistories", "TicketContainerId", "dbo.TicketContainer");
            DropForeignKey("dbo.TrafficHistories", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.Tickets", "TicketContainerId", "dbo.TicketContainer");
            DropForeignKey("dbo.Cards", "TicketContainerId", "dbo.TicketContainer");
            DropForeignKey("dbo.SecurityCheckReviewOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.OpVisas", "SecurityCheckReviewOpDataId", "dbo.SecurityCheckReviewOpDatas");
            DropForeignKey("dbo.OpCameraImages", "SecurityCheckReviewOpData_Id", "dbo.SecurityCheckReviewOpDatas");
            DropForeignKey("dbo.SecurityCheckReviewOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.SecurityCheckOutOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.OpVisas", "SecurityCheckOutOpDataId", "dbo.SecurityCheckOutOpDatas");
            DropForeignKey("dbo.OpCameraImages", "SecurityCheckOutOpDataId", "dbo.SecurityCheckOutOpDatas");
            DropForeignKey("dbo.SecurityCheckOutOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.SecurityCheckInOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.OpVisas", "SecurityCheckInOpDataId", "dbo.SecurityCheckInOpDatas");
            DropForeignKey("dbo.OpCameraImages", "SecurityCheckInOpDataId", "dbo.SecurityCheckInOpDatas");
            DropForeignKey("dbo.SecurityCheckInOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.ScaleOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.OpVisas", "ScaleOpData_Id", "dbo.ScaleOpDatas");
            DropForeignKey("dbo.OpCameraImages", "ScaleOpDataId", "dbo.ScaleOpDatas");
            DropForeignKey("dbo.ScaleOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.Tickets", "RouteTemplateId", "dbo.RouteTemplates");
            DropForeignKey("dbo.SingleWindowOpDatas", "RouteTemplate_Id", "dbo.RouteTemplates");
            DropForeignKey("dbo.SingleWindowOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.OpVisas", "SingleWindowOpDataId", "dbo.SingleWindowOpDatas");
            DropForeignKey("dbo.OpCameraImages", "SingleWindowOpDataId", "dbo.SingleWindowOpDatas");
            DropForeignKey("dbo.SingleWindowOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.NonStandartOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.OpVisas", "NonStandartOpData_Id", "dbo.NonStandartOpDatas");
            DropForeignKey("dbo.OpCameraImages", "NonStandartOpDataId", "dbo.NonStandartOpDatas");
            DropForeignKey("dbo.NonStandartOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.Tickets", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.LoadPointOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.OpVisas", "LoadPointOpDataId", "dbo.LoadPointOpDatas");
            DropForeignKey("dbo.OpCameraImages", "LoadPointOpDataId", "dbo.LoadPointOpDatas");
            DropForeignKey("dbo.LoadPointOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.LoadPointOpDatas", "MixedFeedSiloId", "dbo.MixedFeedSiloes");
            DropForeignKey("dbo.MixedFeedSiloes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.LoadGuideOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.LabFacelessOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.CentralLabOpDatas", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.OpVisas", "LoadGuideOpDataId", "dbo.LoadGuideOpDatas");
            DropForeignKey("dbo.OpCameraImages", "LoadGuideOpDataId", "dbo.LoadGuideOpDatas");
            DropForeignKey("dbo.LoadGuideOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.LoadGuideOpDatas", "LoadPointNodeId", "dbo.Nodes");
            DropForeignKey("dbo.OpVisas", "LabFacelessOpDataComponentId", "dbo.LabFacelessOpDataComponents");
            DropForeignKey("dbo.OpVisas", "LabFacelessOpDataId", "dbo.LabFacelessOpDatas");
            DropForeignKey("dbo.OpVisas", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.OpVisas", "CentralLabOpData_Id", "dbo.CentralLabOpDatas");
            DropForeignKey("dbo.LabFacelessOpDataComponents", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.LabFacelessOpDataComponents", "LabFacelessOpDataId", "dbo.LabFacelessOpDatas");
            DropForeignKey("dbo.Devices", "DeviceStateId", "dbo.DeviceStates");
            DropForeignKey("dbo.Devices", "DeviceParamId", "dbo.DeviceParams");
            DropForeignKey("dbo.Devices", "ParentDeviceId", "dbo.Devices");
            DropForeignKey("dbo.OpCameraImages", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.CentralLabOpDatas", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.RoleAssignments", "NodeId", "dbo.Nodes");
            DropForeignKey("dbo.EmployeeRoles", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Cards", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.TicketPackingTare", new[] { "PackingTareId" });
            DropIndex("dbo.TicketPackingTare", new[] { "TicketId" });
            DropIndex("dbo.QueueRegisters", new[] { "TicketContainerId" });
            DropIndex("dbo.QueueRegisters", new[] { "RouteTemplateId" });
            DropIndex("dbo.PhoneInformTicketAssignments", new[] { "TicketId" });
            DropIndex("dbo.PhoneInformTicketAssignments", new[] { "PhoneDictionaryId" });
            DropIndex("dbo.QueuePatternItems", new[] { "PartnerId" });
            DropIndex("dbo.OwnTransport", new[] { "LongRangeCardId" });
            DropIndex("dbo.OwnTransport", new[] { "CardId" });
            DropIndex("dbo.OpDataEvents", new[] { "Employee_Id" });
            DropIndex("dbo.OpDataEvents", new[] { "NodeId" });
            DropIndex("dbo.OpDataEvents", new[] { "TicketId" });
            DropIndex("dbo.NodeProcessingMessages", new[] { "NodeId" });
            DropIndex("dbo.Messages", new[] { "CardId" });
            DropIndex("dbo.DriverPhotoes", new[] { "DeviceId" });
            DropIndex("dbo.DriverCheckInOpDatas", new[] { "TicketId" });
            DropIndex("dbo.DriverCheckInOpDatas", new[] { "DriverPhotoId" });
            DropIndex("dbo.DriverCheckInOpDatas", new[] { "NodeId" });
            DropIndex("dbo.UnloadPointOpDatas", new[] { "TicketId" });
            DropIndex("dbo.UnloadPointOpDatas", new[] { "NodeId" });
            DropIndex("dbo.UnloadGuideOpDatas", new[] { "Node_Id" });
            DropIndex("dbo.UnloadGuideOpDatas", new[] { "TicketId" });
            DropIndex("dbo.UnloadGuideOpDatas", new[] { "NodeId" });
            DropIndex("dbo.UnloadGuideOpDatas", new[] { "UnloadPointNodeId" });
            DropIndex("dbo.TicketFiles", new[] { "TicketId" });
            DropIndex("dbo.TrafficHistories", new[] { "NodeId" });
            DropIndex("dbo.TrafficHistories", new[] { "TicketContainerId" });
            DropIndex("dbo.SecurityCheckReviewOpDatas", new[] { "TicketId" });
            DropIndex("dbo.SecurityCheckReviewOpDatas", new[] { "NodeId" });
            DropIndex("dbo.SecurityCheckOutOpDatas", new[] { "TicketId" });
            DropIndex("dbo.SecurityCheckOutOpDatas", new[] { "NodeId" });
            DropIndex("dbo.SecurityCheckInOpDatas", new[] { "TicketId" });
            DropIndex("dbo.SecurityCheckInOpDatas", new[] { "NodeId" });
            DropIndex("dbo.ScaleOpDatas", new[] { "TicketId" });
            DropIndex("dbo.ScaleOpDatas", new[] { "NodeId" });
            DropIndex("dbo.SingleWindowOpDatas", new[] { "RouteTemplate_Id" });
            DropIndex("dbo.SingleWindowOpDatas", new[] { "TicketId" });
            DropIndex("dbo.SingleWindowOpDatas", new[] { "NodeId" });
            DropIndex("dbo.NonStandartOpDatas", new[] { "TicketId" });
            DropIndex("dbo.NonStandartOpDatas", new[] { "NodeId" });
            DropIndex("dbo.MixedFeedSiloes", new[] { "ProductId" });
            DropIndex("dbo.LoadPointOpDatas", new[] { "TicketId" });
            DropIndex("dbo.LoadPointOpDatas", new[] { "NodeId" });
            DropIndex("dbo.LoadPointOpDatas", new[] { "MixedFeedSiloId" });
            DropIndex("dbo.Tickets", new[] { "NodeId" });
            DropIndex("dbo.Tickets", new[] { "RouteTemplateId" });
            DropIndex("dbo.Tickets", new[] { "TicketContainerId" });
            DropIndex("dbo.LoadGuideOpDatas", new[] { "Node_Id1" });
            DropIndex("dbo.LoadGuideOpDatas", new[] { "Node_Id" });
            DropIndex("dbo.LoadGuideOpDatas", new[] { "TicketId" });
            DropIndex("dbo.LoadGuideOpDatas", new[] { "NodeId" });
            DropIndex("dbo.LoadGuideOpDatas", new[] { "LoadPointNodeId" });
            DropIndex("dbo.OpVisas", new[] { "ScaleOpData_Id" });
            DropIndex("dbo.OpVisas", new[] { "NonStandartOpData_Id" });
            DropIndex("dbo.OpVisas", new[] { "CentralLabOpData_Id" });
            DropIndex("dbo.OpVisas", new[] { "LabFacelessOpDataComponentId" });
            DropIndex("dbo.OpVisas", new[] { "MixedFeedSiloId" });
            DropIndex("dbo.OpVisas", new[] { "LoadGuideOpDataId" });
            DropIndex("dbo.OpVisas", new[] { "LoadPointOpDataId" });
            DropIndex("dbo.OpVisas", new[] { "UnloadGuideOpDataId" });
            DropIndex("dbo.OpVisas", new[] { "UnloadPointOpDataId" });
            DropIndex("dbo.OpVisas", new[] { "SecurityCheckReviewOpDataId" });
            DropIndex("dbo.OpVisas", new[] { "SecurityCheckOutOpDataId" });
            DropIndex("dbo.OpVisas", new[] { "SecurityCheckInOpDataId" });
            DropIndex("dbo.OpVisas", new[] { "SingleWindowOpDataId" });
            DropIndex("dbo.OpVisas", new[] { "LabFacelessOpDataId" });
            DropIndex("dbo.OpVisas", new[] { "EmployeeId" });
            DropIndex("dbo.LabFacelessOpDataComponents", new[] { "NodeId" });
            DropIndex("dbo.LabFacelessOpDataComponents", new[] { "LabFacelessOpDataId" });
            DropIndex("dbo.LabFacelessOpDatas", new[] { "TicketId" });
            DropIndex("dbo.LabFacelessOpDatas", new[] { "NodeId" });
            DropIndex("dbo.Devices", new[] { "DeviceStateId" });
            DropIndex("dbo.Devices", new[] { "DeviceParamId" });
            DropIndex("dbo.Devices", new[] { "ParentDeviceId" });
            DropIndex("dbo.OpCameraImages", new[] { "CentralLabOpData_Id" });
            DropIndex("dbo.OpCameraImages", new[] { "SecurityCheckReviewOpData_Id" });
            DropIndex("dbo.OpCameraImages", new[] { "NonStandartOpDataId" });
            DropIndex("dbo.OpCameraImages", new[] { "LoadPointOpDataId" });
            DropIndex("dbo.OpCameraImages", new[] { "LoadGuideOpDataId" });
            DropIndex("dbo.OpCameraImages", new[] { "UnloadPointOpDataId" });
            DropIndex("dbo.OpCameraImages", new[] { "UnloadGuideOpDataId" });
            DropIndex("dbo.OpCameraImages", new[] { "ScaleOpDataId" });
            DropIndex("dbo.OpCameraImages", new[] { "SecurityCheckOutOpDataId" });
            DropIndex("dbo.OpCameraImages", new[] { "SecurityCheckInOpDataId" });
            DropIndex("dbo.OpCameraImages", new[] { "SingleWindowOpDataId" });
            DropIndex("dbo.OpCameraImages", new[] { "LabFacelessOpDataId" });
            DropIndex("dbo.OpCameraImages", new[] { "DeviceId" });
            DropIndex("dbo.CentralLabOpDatas", new[] { "TicketId" });
            DropIndex("dbo.CentralLabOpDatas", new[] { "NodeId" });
            DropIndex("dbo.RoleAssignments", new[] { "NodeId" });
            DropIndex("dbo.RoleAssignments", new[] { "RoleId" });
            DropIndex("dbo.EmployeeRoles", new[] { "RoleId" });
            DropIndex("dbo.EmployeeRoles", new[] { "EmployeeId" });
            DropIndex("dbo.Cards", new[] { "ParentCardId" });
            DropIndex("dbo.Cards", new[] { "TicketContainerId" });
            DropIndex("dbo.Cards", new[] { "EmployeeId" });
            DropTable("dbo.YearOfHarvests");
            DropTable("dbo.TransportBlackListRecords");
            DropTable("dbo.TrailersBlackListRecords");
            DropTable("dbo.TicketPackingTare");
            DropTable("dbo.TelegramBots");
            DropTable("dbo.SupplyTypes");
            DropTable("dbo.SupplyTransportTypes");
            DropTable("dbo.Subdivisions");
            DropTable("dbo.Stocks");
            DropTable("dbo.Settings");
            DropTable("dbo.Routes");
            DropTable("dbo.ReasonForRefunds");
            DropTable("dbo.QueueRegisters");
            DropTable("dbo.PhoneInformTicketAssignments");
            DropTable("dbo.PhoneDictionaries");
            DropTable("dbo.QueuePatternItems");
            DropTable("dbo.Partners");
            DropTable("dbo.PackingTare");
            DropTable("dbo.OwnTransport");
            DropTable("dbo.OriginTypes");
            DropTable("dbo.Organisations");
            DropTable("dbo.OpDataEvents");
            DropTable("dbo.NodeProcessingMessages");
            DropTable("dbo.Messages");
            DropTable("dbo.MeasureUnits");
            DropTable("dbo.LabInfectionedСlassifier");
            DropTable("dbo.LabImpurityСlassifier");
            DropTable("dbo.LabHumidityСlassifier");
            DropTable("dbo.FixedAssets");
            DropTable("dbo.ExternalUsers");
            DropTable("dbo.DriversBlackListRecords");
            DropTable("dbo.DriverPhotoes");
            DropTable("dbo.DriverCheckInOpDatas");
            DropTable("dbo.DeliveryBillTypes");
            DropTable("dbo.DeliveryBillStatus");
            DropTable("dbo.Crops");
            DropTable("dbo.Contracts");
            DropTable("dbo.UnloadPointOpDatas");
            DropTable("dbo.UnloadGuideOpDatas");
            DropTable("dbo.TicketFiles");
            DropTable("dbo.TrafficHistories");
            DropTable("dbo.TicketContainer");
            DropTable("dbo.SecurityCheckReviewOpDatas");
            DropTable("dbo.SecurityCheckOutOpDatas");
            DropTable("dbo.SecurityCheckInOpDatas");
            DropTable("dbo.ScaleOpDatas");
            DropTable("dbo.SingleWindowOpDatas");
            DropTable("dbo.RouteTemplates");
            DropTable("dbo.NonStandartOpDatas");
            DropTable("dbo.Products");
            DropTable("dbo.MixedFeedSiloes");
            DropTable("dbo.LoadPointOpDatas");
            DropTable("dbo.Tickets");
            DropTable("dbo.LoadGuideOpDatas");
            DropTable("dbo.OpVisas");
            DropTable("dbo.LabFacelessOpDataComponents");
            DropTable("dbo.LabFacelessOpDatas");
            DropTable("dbo.DeviceStates");
            DropTable("dbo.DeviceParams");
            DropTable("dbo.Devices");
            DropTable("dbo.OpCameraImages");
            DropTable("dbo.CentralLabOpDatas");
            DropTable("dbo.Nodes");
            DropTable("dbo.RoleAssignments");
            DropTable("dbo.Roles");
            DropTable("dbo.EmployeeRoles");
            DropTable("dbo.Employees");
            DropTable("dbo.Cards");
            DropTable("dbo.Budgets");
            DropTable("dbo.AcceptancePoints");
        }
    }
}

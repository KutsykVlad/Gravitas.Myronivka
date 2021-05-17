using System.Data.Entity;
using Gravitas.DAL.Mapping;
using Gravitas.DAL.Mapping.MixedFeed;
using Gravitas.DAL.Mapping.PackingTare;
using Gravitas.DAL.Mapping.PhoneInformTicketAssignment;
using Gravitas.DAL.Mapping.PreRegistration;
using Gravitas.DAL.Mapping.Settings;
using Gravitas.Model;
using Gravitas.Model.DomainModel.MixedFeed.DAO;
using Gravitas.Model.DomainModel.OpDataEvent.DAO;
using Gravitas.Model.DomainModel.OwnTransport.DAO;
using Gravitas.Model.DomainModel.PackingTare.DAO;
using Gravitas.Model.DomainModel.PhoneInformTicketAssignment.DAO;
using Gravitas.Model.DomainModel.PreRegistration.DAO;
using Gravitas.Model.DomainModel.Settings.DAO;

namespace Gravitas.DAL
{
    public class GravitasDbContext : BaseDbContext<GravitasDbContext>
    {
        public GravitasDbContext() : base("name=GravitasDbContext")
        {
        }

        public DbSet<PreRegisterQueue> PreRegisterQueues { get; set; }
        public DbSet<MixedFeedSiloDevice> MixedFeedSiloDevices { get; set; }
        public DbSet<ExternalData.ExternalUser> ExternalUsers { get; set; }
        public DbSet<OpVisa> OpVisas { get; set; }
        public DbSet<OpRoutineTransition> OpRoutineTransitions { get; set; }
        public DbSet<TrafficHistory> TrafficHistories { get; set; }
        public DbSet<RoleAssignment> RoleAssignments { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<TicketPackingTare> TicketPackingTares { get; set; }
        public DbSet<OpDataEvent> OpDataEvents { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<SingleWindowOpData> SingleWindowOpDatas { get; set; }
        public DbSet<Settings> Set { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketContainer> TicketContainers { get; set; }
        public DbSet<DeviceParam> DeviceParams { get; set; }
        public DbSet<RouteTemplate> RouteTemplates { get; set; }
        public DbSet<MixedFeedGuideOpData> MixedFeedGuideOpDatas { get; set; }
        public DbSet<UnloadGuideOpData> UnloadGuideOpDatas { get; set; }
        public DbSet<LoadGuideOpData> LoadGuideOpDatas { get; set; }
        public DbSet<LabFacelessOpData> LabFacelessOpDatas { get; set; }
        public DbSet<SecurityCheckReviewOpData> SecurityCheckReviewOpDatas { get; set; }
        public DbSet<ExternalData.AcceptancePoint> AcceptancePoints { get; set; }
        public DbSet<ExternalData.Budget> Budgets { get; set; }
        public DbSet<OpCameraImage> OpCameraImages { get; set; }
        public DbSet<ScaleOpData> ScaleOpDatas { get; set; }
        public DbSet<ExternalData.Contract> Contracts { get; set; }
        public DbSet<ExternalData.Crop> Crops { get; set; }
        public DbSet<ExternalData.DeliveryBillStatus> DeliveryBillStatuses { get; set; }
        public DbSet<ExternalData.DeliveryBillType> DeliveryBillTypes { get; set; }
        public DbSet<ExternalData.Employee> Employees { get; set; }
        public DbSet<ExternalData.FixedAsset> FixedAssets { get; set; }
        public DbSet<ExternalData.LabImpurityСlassifier> LabImpurityСlassifiers { get; set; }
        public DbSet<ExternalData.LabHumidityСlassifier> LabHumidityСlassifiers { get; set; }
        public DbSet<ExternalData.LabInfectionedСlassifier> LabInfectionedСlassifiers { get; set; }
        public DbSet<ExternalData.MeasureUnit> MeasureUnits { get; set; }
        public DbSet<ExternalData.OriginType> OriginTypes { get; set; }
        public DbSet<ExternalData.Product> Products { get; set; }
        public DbSet<ExternalData.ReasonForRefund> ReasonForRefunds { get; set; }
        public DbSet<ExternalData.Route> Routes { get; set; }
        public DbSet<ExternalData.Stock> Stocks { get; set; }
        public DbSet<ExternalData.Organisation> Organisations { get; set; }
        public DbSet<ExternalData.Partner> Partners { get; set; }
        public DbSet<QueueRegister> QueueRegisters { get; set; }
        public DbSet<ExternalData.Subdivision> Subdivisions { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceState> DeviceStates { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<OwnTransport> OwnTransports { get; set; }
        public DbSet<PackingTare> PackingTares { get; set; }
        public DbSet<PhoneInformTicketAssignment> PhoneInformTicketAssignments { get; set; }
        public DbSet<CentralLabOpData> CentralLabOpDatas { get; set; }
        public DbSet<LabFacelessOpDataComponent> LabFacelessOpDataComponents { get; set; }
        public DbSet<LoadPointOpData> LoadPointOpDatas { get; set; }
        public DbSet<MixedFeedLoadOpData> MixedFeedLoadOpDatas { get; set; }
        public DbSet<NonStandartOpData> NonStandartOpDatas { get; set; }
        public DbSet<SecurityCheckOutOpData> SecurityCheckOutOpDatas { get; set; }
        public DbSet<QueuePatternItem> QueuePatternItems { get; set; }
        public DbSet<MixedFeedSilo> MixedFeedSilos { get; set; }
        public DbSet<OpRoutineState> OpRoutineStates { get; set; }
        public DbSet<PhoneDictionary> PhoneDictionaries { get; set; }
        public DbSet<TicketFile> TicketFiles { get; set; }
        public DbSet<UnloadPointOpData> UnloadPointOpDatas { get; set; }
        public DbSet<SmsTemplate> SmsTemplates { get; set; }
        public DbSet<PreRegisterProduct> PreRegisterProducts { get; set; }
        public DbSet<OpDataState> OpDataStates { get; set; }
        public DbSet<ExternalData.SupplyTransportType> SupplyTransportTypes { get; set; }
        public DbSet<ExternalData.SupplyType> SupplyTypes { get; set; }
        public DbSet<ExternalData.YearOfHarvest> YearOfHarvests { get; set; }
        public DbSet<SecurityCheckInOpData> SecurityCheckInOpDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CardMap());
            modelBuilder.Configurations.Add(new CardTypeMap());
            modelBuilder.Configurations.Add(new TicketMap());
            modelBuilder.Configurations.Add(new TicketStatusMap());
            modelBuilder.Configurations.Add(new TicketConteinerMap());
            modelBuilder.Configurations.Add(new TicketContainerStatusMap());
            modelBuilder.Configurations.Add(new TicketFileMap());
            modelBuilder.Configurations.Add(new TicketFileTypeMap());
            modelBuilder.Configurations.Add(new OpRoutineMap());
            modelBuilder.Configurations.Add(new OpRoutineStateMap());
            modelBuilder.Configurations.Add(new OpRoutineProcessorMap());
            modelBuilder.Configurations.Add(new OpRoutineTransitionMap());
            modelBuilder.Configurations.Add(new OpVisaMap());
            modelBuilder.Configurations.Add(new OpCameraImageMap());
            modelBuilder.Configurations.Add(new PhoneDictionaryMap());
            modelBuilder.Configurations.Add(new PhoneInformTicketAssignmentMap());
            modelBuilder.Configurations.Add(new MixedFeedSiloMap());
            modelBuilder.Configurations.Add(new MixedFeedSiloDeviceMap());
            modelBuilder.Configurations.Add(new NodeMap());
            modelBuilder.Configurations.Add(new EndPointNodeMap());
            modelBuilder.Configurations.Add(new RouteTemplateMap());
            modelBuilder.Configurations.Add(new DeviceMap());
            modelBuilder.Configurations.Add(new DeviceTypeMap());
            modelBuilder.Configurations.Add(new DeviceParamMap());
            modelBuilder.Configurations.Add(new DeviceStateMap());
            modelBuilder.Configurations.Add(new OrganizationUnitMap());
            modelBuilder.Configurations.Add(new OrganizationUnitTypeMap());
            modelBuilder.Configurations.Add(new TrafficHistoryMap());
            modelBuilder.Configurations.Add(new PriorityMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new QueuePatternItemMap());
            modelBuilder.Configurations.Add(new SmsTemplateMap());
            modelBuilder.Configurations.Add(new OpDataStateMap());
            modelBuilder.Configurations.Add(new LabFacelessOpDataMap());
            modelBuilder.Configurations.Add(new CentralLabOpDataMap());
            modelBuilder.Configurations.Add(new LabFacelessOpDataComponentMap());
            modelBuilder.Configurations.Add(new UnloadGuideOpDataMap());
            modelBuilder.Configurations.Add(new UnloadPointOpDataMap());
            modelBuilder.Configurations.Add(new LoadGuideOpDataMap());
            modelBuilder.Configurations.Add(new LoadPointOpDataMap());
            modelBuilder.Configurations.Add(new ScaleOpDataMap());
            modelBuilder.Configurations.Add(new SecurityCheckInOpDataMap());
            modelBuilder.Configurations.Add(new SecurityCheckOutOpDataMap());
            modelBuilder.Configurations.Add(new SecurityCheckReviewOpDataMap());
            modelBuilder.Configurations.Add(new SingleWindowOpDataMap());
            modelBuilder.Configurations.Add(new NonStandartOpDataMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.AcceptancePointMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.BudgetMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.CropMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.ContractMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.EmployeeMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.FixedAssetMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.OrganizationMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.PartnerMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.ProductMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.ReasonForRefundMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.RouteMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.StockMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.SubdivisionMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.ExternalUserMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.YearOfHarvestMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.MeasureUnitMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new AssignmentMap());
            modelBuilder.Configurations.Add(new EmployeeRolesMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.DeliveryBillStatusMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.DeliveryBillTypeMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.LabDeviceResultTypeMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.LabHumidityClassifierMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.LabImpurityClassifierMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.LabInfectionedClassifierMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.OriginTypeMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.SupplyTypeMap());
            modelBuilder.Configurations.Add(new ExternalDataMap.SupplyTransportTypeMap());
            modelBuilder.Configurations.Add(new DriverBlackListMap());
            modelBuilder.Configurations.Add(new PartnerBlackListMap());
            modelBuilder.Configurations.Add(new TrailerBlackListMap());
            modelBuilder.Configurations.Add(new TransportBlackListMap());
            modelBuilder.Configurations.Add(new QueueRegisterMap());
            modelBuilder.Configurations.Add(new MixedFeedGuideOpDataMap());
            modelBuilder.Configurations.Add(new MixedFeedLoadOpDataMap());
            modelBuilder.Configurations.Add(new OpDataEventMap());
            modelBuilder.Configurations.Add(new PreRegisterProductMap());
            modelBuilder.Configurations.Add(new PreRegisterCompanyMap());
            modelBuilder.Configurations.Add(new OwnTransportMap());
            modelBuilder.Configurations.Add(new PackingTareMap());
            modelBuilder.Configurations.Add(new TicketPackingTareMap());
            modelBuilder.Configurations.Add(new SettingsMap());
            modelBuilder.Configurations.Add(new PreRegisterQueueMap());
        }
    }
}
using System.Data.Entity;
using Gravitas.DAL.Mapping.BlackList;
using Gravitas.DAL.Mapping.Card;
using Gravitas.DAL.Mapping.Device;
using Gravitas.DAL.Mapping.EmployeeRoles;
using Gravitas.DAL.Mapping.EndPointNode;
using Gravitas.DAL.Mapping.ExternalData;
using Gravitas.DAL.Mapping.MixedFeed;
using Gravitas.DAL.Mapping.OpData;
using Gravitas.DAL.Mapping.OpData.NodeOpData;
using Gravitas.DAL.Mapping.OpDataEvent;
using Gravitas.DAL.Mapping.OrganizationUnit;
using Gravitas.DAL.Mapping.OwnTransport;
using Gravitas.DAL.Mapping.PackingTare;
using Gravitas.DAL.Mapping.PhoneDictionary;
using Gravitas.DAL.Mapping.PhoneInformTicketAssignment;
using Gravitas.DAL.Mapping.PreRegistration;
using Gravitas.DAL.Mapping.Queue;
using Gravitas.DAL.Mapping.RouteTemplate;
using Gravitas.DAL.Mapping.Settings;
using Gravitas.DAL.Mapping.Ticket;
using Gravitas.DAL.Mapping.Traffic;
using Gravitas.Model.DomainModel.BlackList.DAO;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Device.DAO;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;
using Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO;
using Gravitas.Model.DomainModel.ExternalData.Budget.DAO;
using Gravitas.Model.DomainModel.ExternalData.Contract.DAO;
using Gravitas.Model.DomainModel.ExternalData.Crop.DAO;
using Gravitas.Model.DomainModel.ExternalData.DeliveryBill.DAO;
using Gravitas.Model.DomainModel.ExternalData.Employee.DAO;
using Gravitas.Model.DomainModel.ExternalData.ExternalUser.DAO;
using Gravitas.Model.DomainModel.ExternalData.FixedAsset.DAO;
using Gravitas.Model.DomainModel.ExternalData.LabClassifier.DAO;
using Gravitas.Model.DomainModel.ExternalData.MeasureUnit.DAO;
using Gravitas.Model.DomainModel.ExternalData.Organization.DAO;
using Gravitas.Model.DomainModel.ExternalData.Partner.DAO;
using Gravitas.Model.DomainModel.ExternalData.Product.DAO;
using Gravitas.Model.DomainModel.ExternalData.ReasonForRefund.DAO;
using Gravitas.Model.DomainModel.ExternalData.Route.DAO;
using Gravitas.Model.DomainModel.ExternalData.Stock.DAO;
using Gravitas.Model.DomainModel.ExternalData.Subdivision.DAO;
using Gravitas.Model.DomainModel.ExternalData.SupplyTransportType.DAO;
using Gravitas.Model.DomainModel.ExternalData.SupplyType.DAO;
using Gravitas.Model.DomainModel.ExternalData.YearOfHarvest.DAO;
using Gravitas.Model.DomainModel.MixedFeed.DAO;
using Gravitas.Model.DomainModel.OpCameraImage;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpDataEvent.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainModel.OrganizationUnit.DAO;
using Gravitas.Model.DomainModel.OwnTransport.DAO;
using Gravitas.Model.DomainModel.PackingTare.DAO;
using Gravitas.Model.DomainModel.PhoneDictionary.DAO;
using Gravitas.Model.DomainModel.PhoneInformTicketAssignment.DAO;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
using Gravitas.Model.DomainModel.PreRegistration.DAO;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Model.DomainModel.Settings.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainModel.Traffic.DAO;

namespace Gravitas.DAL.DbContext
{
    public class GravitasDbContext : System.Data.Entity.DbContext
    {
        public GravitasDbContext() : base("name=GravitasDbContext")
        {
        }

        public DbSet<Settings> Settings { get; set; }
        public DbSet<PreRegisterQueue> PreRegisterQueues { get; set; }
        public DbSet<MixedFeedSiloDevice> MixedFeedSiloDevices { get; set; }
        public DbSet<ExternalUser> ExternalUsers { get; set; }
        public DbSet<DriversBlackListRecord> DriversBlackListRecords { get; set; }
        public DbSet<TransportBlackListRecord> TransportBlackListRecords { get; set; }
        public DbSet<TrailersBlackListRecord> TrailersBlackListRecords { get; set; }
        public DbSet<OpVisa> OpVisas { get; set; }
        public DbSet<TrafficHistory> TrafficHistories { get; set; }
        public DbSet<RoleAssignment> RoleAssignments { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<TicketPackingTare> TicketPackingTares { get; set; }
        public DbSet<OpDataEvent> OpDataEvents { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Model.DomainModel.Node.DAO.Node> Nodes { get; set; }
        public DbSet<SingleWindowOpData> SingleWindowOpDatas { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketContainer> TicketContainers { get; set; }
        public DbSet<DeviceParam> DeviceParams { get; set; }
        public DbSet<RouteTemplate> RouteTemplates { get; set; }
        public DbSet<UnloadGuideOpData> UnloadGuideOpDatas { get; set; }
        public DbSet<LoadGuideOpData> LoadGuideOpDatas { get; set; }
        public DbSet<LabFacelessOpData> LabFacelessOpDatas { get; set; }
        public DbSet<SecurityCheckReviewOpData> SecurityCheckReviewOpDatas { get; set; }
        public DbSet<AcceptancePoint> AcceptancePoints { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<OpCameraImage> OpCameraImages { get; set; }
        public DbSet<ScaleOpData> ScaleOpDatas { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<DeliveryBillStatus> DeliveryBillStatuses { get; set; }
        public DbSet<DeliveryBillType> DeliveryBillTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FixedAsset> FixedAssets { get; set; }
        public DbSet<LabImpurityСlassifier> LabImpurityСlassifiers { get; set; }
        public DbSet<LabHumidityСlassifier> LabHumidityСlassifiers { get; set; }
        public DbSet<LabInfectionedСlassifier> LabInfectionedСlassifiers { get; set; }
        public DbSet<MeasureUnit> MeasureUnits { get; set; }
        public DbSet<OriginType> OriginTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ReasonForRefund> ReasonForRefunds { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<QueueRegister> QueueRegisters { get; set; }
        public DbSet<Subdivision> Subdivisions { get; set; }
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
        public DbSet<PhoneDictionary> PhoneDictionaries { get; set; }
        public DbSet<TicketFile> TicketFiles { get; set; }
        public DbSet<UnloadPointOpData> UnloadPointOpDatas { get; set; }
        public DbSet<PreRegisterProduct> PreRegisterProducts { get; set; }
        public DbSet<SupplyTransportType> SupplyTransportTypes { get; set; }
        public DbSet<SupplyType> SupplyTypes { get; set; }
        public DbSet<YearOfHarvest> YearOfHarvests { get; set; }
        public DbSet<SecurityCheckInOpData> SecurityCheckInOpDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CardMap());
            modelBuilder.Configurations.Add(new TicketMap());
            modelBuilder.Configurations.Add(new TicketContainerMap());
            modelBuilder.Configurations.Add(new TicketFileMap());
            modelBuilder.Configurations.Add(new OpVisaMap());
            modelBuilder.Configurations.Add(new OpCameraImageMap());
            modelBuilder.Configurations.Add(new PhoneDictionaryMap());
            modelBuilder.Configurations.Add(new PhoneInformTicketAssignmentMap());
            modelBuilder.Configurations.Add(new MixedFeedSiloMap());
            modelBuilder.Configurations.Add(new MixedFeedSiloDeviceMap());
            modelBuilder.Configurations.Add(new EndPointNodeMap());
            modelBuilder.Configurations.Add(new RouteTemplateMap());
            modelBuilder.Configurations.Add(new DeviceMap());
            modelBuilder.Configurations.Add(new DeviceParamMap());
            modelBuilder.Configurations.Add(new DeviceStateMap());
            modelBuilder.Configurations.Add(new OrganizationUnitMap());
            modelBuilder.Configurations.Add(new OrganizationUnitTypeMap());
            modelBuilder.Configurations.Add(new TrafficHistoryMap());
            modelBuilder.Configurations.Add(new QueuePatternItemMap());
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
            modelBuilder.Configurations.Add(new AcceptancePointMap());
            modelBuilder.Configurations.Add(new BudgetMap());
            modelBuilder.Configurations.Add(new CropMap());
            modelBuilder.Configurations.Add(new ContractMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new FixedAssetMap());
            modelBuilder.Configurations.Add(new OrganizationMap());
            modelBuilder.Configurations.Add(new PartnerMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ReasonForRefundMap());
            modelBuilder.Configurations.Add(new RouteMap());
            modelBuilder.Configurations.Add(new StockMap());
            modelBuilder.Configurations.Add(new SubdivisionMap());
            modelBuilder.Configurations.Add(new ExternalUserMap());
            modelBuilder.Configurations.Add(new YearOfHarvestMap());
            modelBuilder.Configurations.Add(new MeasureUnitMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new AssignmentMap());
            modelBuilder.Configurations.Add(new EmployeeRolesMap());
            modelBuilder.Configurations.Add(new DeliveryBillStatusMap());
            modelBuilder.Configurations.Add(new DeliveryBillTypeMap());
            modelBuilder.Configurations.Add(new LabDeviceResultTypeMap());
            modelBuilder.Configurations.Add(new LabHumidityClassifierMap());
            modelBuilder.Configurations.Add(new LabImpurityClassifierMap());
            modelBuilder.Configurations.Add(new LabInfectionedClassifierMap());
            modelBuilder.Configurations.Add(new OriginTypeMap());
            modelBuilder.Configurations.Add(new SupplyTypeMap());
            modelBuilder.Configurations.Add(new SupplyTransportTypeMap());
            modelBuilder.Configurations.Add(new DriverBlackListMap());
            modelBuilder.Configurations.Add(new PartnerBlackListMap());
            modelBuilder.Configurations.Add(new TrailerBlackListMap());
            modelBuilder.Configurations.Add(new TransportBlackListMap());
            modelBuilder.Configurations.Add(new QueueRegisterMap());
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
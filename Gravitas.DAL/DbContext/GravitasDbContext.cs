using System.Data.Entity;
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
        public DbSet<NonStandartOpData> NonStandartOpDatas { get; set; }
        public DbSet<SecurityCheckOutOpData> SecurityCheckOutOpDatas { get; set; }
        public DbSet<QueuePatternItem> QueuePatternItems { get; set; }
        public DbSet<MixedFeedSilo> MixedFeedSilos { get; set; }
        public DbSet<PhoneDictionary> PhoneDictionaries { get; set; }
        public DbSet<TicketFile> TicketFiles { get; set; }
        public DbSet<UnloadPointOpData> UnloadPointOpDatas { get; set; }
        public DbSet<SupplyTransportType> SupplyTransportTypes { get; set; }
        public DbSet<SupplyType> SupplyTypes { get; set; }
        public DbSet<YearOfHarvest> YearOfHarvests { get; set; }
        public DbSet<SecurityCheckInOpData> SecurityCheckInOpDatas { get; set; }
    }
}
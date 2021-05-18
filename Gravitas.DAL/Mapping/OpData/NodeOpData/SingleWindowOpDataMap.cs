using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class SingleWindowOpDataMap : BaseOpDataMap<SingleWindowOpData>
    {
        public SingleWindowOpDataMap()
        {
            ToTable("opd.SingleWindowOpData");

            HasRequired(e => e.OpDataState)
                .WithMany(e => e.SingleWindowOpDataSet)
                .HasForeignKey(e => e.StateId);

            HasOptional(e => e.Ticket)
                .WithMany(e => e.SingleWindowOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.SingleWindowOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.SingleWindowOpData)
                .HasForeignKey(e => e.SingleWindowOpDataId);

            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.SingleWindowOpData)
                .HasForeignKey(e => e.SingleWindowOpDataId);

            Property(e => e.EditDate)
                .HasColumnType("datetime2");
            Property(e => e.CreteDate)
                .HasColumnType("datetime2");
            Property(e => e.DocNetDateTime)
                .HasColumnType("datetime2");
            Property(e => e.TripTicketDateTime)
                .HasColumnType("datetime2");
            Property(e => e.WarrantDateTime)
                .HasColumnType("datetime2");
            Property(e => e.WarrantDateTime)
                .HasColumnType("datetime2");
            Property(e => e.RegistrationDateTime)
                .HasColumnType("datetime2");
            Property(e => e.IncomeDocDateTime)
                .HasColumnType("datetime2");

            Property(e => e.OrganizationId)
                .HasMaxLength(250);
            Property(e => e.CreateOperatorId)
                .HasMaxLength(250);
            Property(e => e.EditOperatorId)
                .HasMaxLength(250);
            Property(e => e.DocumentTypeId)
                .HasMaxLength(250);
            Property(e => e.StockId)
                .HasMaxLength(250);
            Property(e => e.ReceiverTypeId)
                .HasMaxLength(250);
            Property(e => e.ReceiverId)
                .HasMaxLength(250);
            Property(e => e.ReceiverAnaliticsId)
                .HasMaxLength(250);
            Property(e => e.ProductId)
                .HasMaxLength(250);
            Property(e => e.HarvestId)
                .HasMaxLength(250);
            Property(e => e.DriverOneId)
                .HasMaxLength(250);
            Property(e => e.DriverTwoId)
                .HasMaxLength(250);
            Property(e => e.TransportId)
                .HasMaxLength(250);
            Property(e => e.HiredDriverCode)
                .HasMaxLength(250);
            Property(e => e.HiredTransportNumber)
                .HasMaxLength(250);
            Property(e => e.IncomeInvoiceSeries)
                .HasMaxLength(250);
            Property(e => e.IncomeInvoiceNumber)
                .HasMaxLength(250);
            Property(e => e.ReceiverDepotId)
                .HasMaxLength(250);
            Property(e => e.CarrierCode)
                .HasMaxLength(250);
            Property(e => e.BuyBudgetId)
                .HasMaxLength(250);
            Property(e => e.SellBudgetId)
                .HasMaxLength(250);
            Property(e => e.KeeperOrganizationId)
                .HasMaxLength(250);
            Property(e => e.OrderCode)
                .HasMaxLength(250);
            Property(e => e.SupplyCode)
                .HasMaxLength(250);
            Property(e => e.SupplyTypeId)
                .HasMaxLength(250);
            Property(e => e.TrailerId)
                .HasMaxLength(250);
            Property(e => e.TrailerNumber)
                .HasMaxLength(250);
            Property(e => e.TripTicketNumber)
                .HasMaxLength(250);
            Property(e => e.WarrantSeries)
                .HasMaxLength(250);
            Property(e => e.WarrantNumber)
                .HasMaxLength(250);
            Property(e => e.WarrantManagerName)
                .HasMaxLength(250);
            Property(e => e.RuleNumber)
                .HasMaxLength(250);
            Property(e => e.Comments)
                .HasMaxLength(250);
            Property(e => e.SupplyTransportTypeId)
                .HasMaxLength(250);
            Property(e => e.BatchNumber)
                .HasMaxLength(250);
            Property(e => e.CarrierRouteId)
                .HasMaxLength(250);
            Property(e => e.DeliveryBillId)
                .HasMaxLength(250);
            Property(e => e.DeliveryBillCode)
                .HasMaxLength(250);
            Property(e => e.InformationCarrier)
                .HasMaxLength(250);
        }
    }
}
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.DeliveryBill.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class DeliveryBillStatusMap : EntityTypeConfiguration<DeliveryBillStatus>
    {
        public DeliveryBillStatusMap()
        {
            ToTable("ext.DeliveryBillStatus");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Name)
                .HasMaxLength(250);
        }
    }
}
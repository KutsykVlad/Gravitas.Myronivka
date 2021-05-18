using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.DeliveryBill.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class DeliveryBillTypeMap : EntityTypeConfiguration<DeliveryBillType>
    {
        public DeliveryBillTypeMap()
        {
            ToTable("ext.DeliveryBillType");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Name)
                .HasMaxLength(250);
        }
    }
}
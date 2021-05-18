using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.SupplyTransportType.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class SupplyTransportTypeMap : EntityTypeConfiguration<SupplyTransportType>
    {
        public SupplyTransportTypeMap()
        {
            ToTable("ext.SupplyTransportType");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Name)
                .HasMaxLength(250);
        }
    }
}
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.SupplyType.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class SupplyTypeMap : EntityTypeConfiguration<SupplyType>
    {
        public SupplyTypeMap()
        {
            ToTable("ext.SupplyType");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Name)
                .HasMaxLength(250);
        }
    }
}
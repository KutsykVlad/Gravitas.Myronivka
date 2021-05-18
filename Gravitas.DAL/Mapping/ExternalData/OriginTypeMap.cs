using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.Organization.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class OriginTypeMap : EntityTypeConfiguration<OriginType>
    {
        public OriginTypeMap()
        {
            ToTable("ext.OrganisationType");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Name)
                .HasMaxLength(250);
        }
    }
}
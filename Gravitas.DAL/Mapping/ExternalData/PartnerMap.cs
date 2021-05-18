using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.Partner.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class PartnerMap : EntityTypeConfiguration<Partner>
    {
        public PartnerMap()
        {
            ToTable("ext.Partner");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Code)
                .HasMaxLength(250);

            Property(e => e.ShortName)
                .HasMaxLength(250);

            Property(e => e.FullName)
                .HasMaxLength(250);

            Property(e => e.Address)
                .HasMaxLength(250);

            Property(e => e.ParentId)
                .HasMaxLength(250);
        }
    }
}
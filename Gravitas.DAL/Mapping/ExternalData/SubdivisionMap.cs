using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.Subdivision.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class SubdivisionMap : EntityTypeConfiguration<Subdivision>
    {
        public SubdivisionMap()
        {
            ToTable("ext.Subdivision");

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
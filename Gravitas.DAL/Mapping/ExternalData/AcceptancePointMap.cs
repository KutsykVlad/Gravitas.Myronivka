using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class AcceptancePointMap : EntityTypeConfiguration<AcceptancePoint>
    {
        public AcceptancePointMap()
        {
            ToTable("ext.AcceptancePoint");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Code)
                .HasMaxLength(250);

            Property(e => e.Name)
                .HasMaxLength(250);

            Property(e => e.ParentId)
                .HasMaxLength(250);
        }
    }
}
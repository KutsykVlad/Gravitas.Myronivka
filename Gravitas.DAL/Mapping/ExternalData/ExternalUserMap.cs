using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.ExternalUser.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class ExternalUserMap : EntityTypeConfiguration<ExternalUser>
    {
        public ExternalUserMap()
        {
            ToTable("ext.User");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Code)
                .HasMaxLength(250);

            Property(e => e.ShortName)
                .HasMaxLength(250);

            Property(e => e.FullName)
                .HasMaxLength(250);

            Property(e => e.EmployeeId)
                .HasMaxLength(250);

            Property(e => e.ParentId)
                .HasMaxLength(250);
        }
    }
}
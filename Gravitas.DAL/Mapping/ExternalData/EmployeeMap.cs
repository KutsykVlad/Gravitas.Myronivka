using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.Employee.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            ToTable("ext.Employee");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Code)
                .HasMaxLength(250);

            Property(e => e.ShortName)
                .HasMaxLength(250);

            Property(e => e.FullName)
                .HasMaxLength(250);

            Property(e => e.Position)
                .HasMaxLength(250);

            Property(e => e.Email)
                .HasMaxLength(250);

            Property(e => e.PhoneNo)
                .HasMaxLength(250);

            Property(e => e.ParentId)
                .HasMaxLength(250);

            HasMany(e => e.CardSet)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId);
        }
    }
}
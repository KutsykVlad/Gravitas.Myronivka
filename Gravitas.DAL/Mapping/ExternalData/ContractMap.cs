using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.ExternalData.Contract.DAO;

namespace Gravitas.DAL.Mapping.ExternalData
{
    public class ContractMap : EntityTypeConfiguration<Contract>
    {
        public ContractMap()
        {
            ToTable("ext.Contract");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasMaxLength(250);

            Property(e => e.Code)
                .HasMaxLength(250);

            Property(e => e.Name)
                .HasMaxLength(250);

            Property(e => e.StartDateTime)
                .HasColumnType("datetime2");

            Property(e => e.StopDateTime)
                .HasColumnType("datetime2");

            Property(e => e.ManagerId)
                .HasMaxLength(250);
        }
    }
}
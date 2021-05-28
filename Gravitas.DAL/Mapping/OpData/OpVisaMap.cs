using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.OpVisa.DAO;

namespace Gravitas.DAL.Mapping.OpData
{
    class OpVisaMap : EntityTypeConfiguration<OpVisa>
    {
        public OpVisaMap()
        {
            ToTable("OpVisa");

            Property(e => e.DateTime)
                .HasColumnType("datetime2");

            HasOptional(e => e.Employee)
                .WithMany(e => e.OpVisaSet)
                .HasForeignKey(e => e.EmployeeId);

            Property(e => e.OpRoutineStateId)
                .IsRequired();
        }
    }
}
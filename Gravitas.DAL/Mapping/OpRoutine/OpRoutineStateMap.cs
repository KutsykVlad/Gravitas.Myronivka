using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.OpRoutine.DAO;

namespace Gravitas.DAL.Mapping.OpRoutine
{
    class OpRoutineStateMap : EntityTypeConfiguration<OpRoutineState>
    {
        public OpRoutineStateMap()
        {
            ToTable("OpRoutineState");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
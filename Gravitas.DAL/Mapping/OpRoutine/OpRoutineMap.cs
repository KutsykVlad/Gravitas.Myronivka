using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.DAL.Mapping._Base;

namespace Gravitas.DAL.Mapping.OpRoutine
{
    class OpRoutineMap : BaseEntityMap<Model.DomainModel.OpRoutine.DAO.OpRoutine, int>
    {
        public OpRoutineMap()
        {
            ToTable("OpRoutine");

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.Description)
                .HasMaxLength(50);

            HasMany(e => e.OpRoutineStateSet)
                .WithRequired(e => e.OpRoutine)
                .WillCascadeOnDelete(false);
        }
    }
}
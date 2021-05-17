using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
	class OpRoutineStateMap : EntityTypeConfiguration<OpRoutineState> {
		public OpRoutineStateMap() {
			this.ToTable("OpRoutineState");
			
			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(100);
			
			this.HasMany(e => e.OpRoutineTransitionStartSet)
				.WithOptional(e => e.OpRoutineStateStart)
				.HasForeignKey(e => e.StartStateId)
				.WillCascadeOnDelete(false);

			this.HasMany(e => e.OpRoutineTransitionStopSet)
				.WithOptional(e => e.OpRoutineStateStop)
				.HasForeignKey(e => e.StopStateId)
				.WillCascadeOnDelete(false);
		}
	}
}

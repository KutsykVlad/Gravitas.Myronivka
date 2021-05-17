using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
	class OpRoutineProcessorMap : BaseEntityMap<OpRoutineProcessor, long> {
		public OpRoutineProcessorMap() {
			this.ToTable("OpRoutineStateProcessor");

			this.HasMany(e => e.OpRoutineTransitionSet)
				.WithRequired(e => e.OpRoutineProcessor)
				.HasForeignKey(e => e.ProcessorId)
				.WillCascadeOnDelete(false);
		}
	}
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	class OpRoutineTransitionMap : BaseEntityMap<OpRoutineTransition, long> {

		public OpRoutineTransitionMap() {
			this.ToTable("OpRoutineTransition");

			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(80);
		}
	}
}

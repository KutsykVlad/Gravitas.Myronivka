using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	class OpRoutineMap : BaseEntityMap<OpRoutine, long> {

		public OpRoutineMap() {
			this.ToTable("OpRoutine");

			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			this.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(50);

			this.Property(e => e.Description)
				.HasMaxLength(50);

			this.HasMany(e => e.OpRoutineStateSet)
				.WithRequired(e => e.OpRoutine)
				.WillCascadeOnDelete(false);			
		}
	}
}

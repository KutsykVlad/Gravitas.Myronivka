using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.OpDataEvent.DAO;

namespace Gravitas.DAL.Mapping {

	class OpDataEventMap : EntityTypeConfiguration<OpDataEvent> {

		public OpDataEventMap() {
			this.ToTable("OpDataEvent");

			this.Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

//		    this.HasOptional(e => e.Employee)
//		        .WithMany(e => e.CardSet)
//		        .HasForeignKey(e => e.EmployeeId);

		}
	}
}

using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	class CardMap : EntityTypeConfiguration<Card> {

		public CardMap() {
			this.ToTable("Card");

			this.HasKey(p => p.Id);
			this.Property(e => e.Id).HasMaxLength(50);

		    this.HasOptional(e => e.Employee)
		        .WithMany(e => e.CardSet)
		        .HasForeignKey(e => e.EmployeeId);

		}
	}
}

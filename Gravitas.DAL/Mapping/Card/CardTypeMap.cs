using System.Data.Entity.ModelConfiguration;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
	class CardTypeMap : EntityTypeConfiguration<CardType> {
		public CardTypeMap() {
			this.ToTable("CardType");

			this.HasMany(e => e.CardSet)
				.WithRequired(e => e.CardType)
				.HasForeignKey(e => e.TypeId)
				.WillCascadeOnDelete(false);
		}
	}
}

using System.Data.Entity.ModelConfiguration;
using Gravitas.Model.DomainModel.Card.DAO;

namespace Gravitas.DAL.Mapping.Card
{
    class CardTypeMap : EntityTypeConfiguration<CardType>
    {
        public CardTypeMap()
        {
            ToTable("CardType");

            HasMany(e => e.CardSet)
                .WithRequired(e => e.CardType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
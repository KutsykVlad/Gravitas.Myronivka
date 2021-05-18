using System.Data.Entity.ModelConfiguration;

namespace Gravitas.DAL.Mapping.Card
{
    class CardMap : EntityTypeConfiguration<Model.DomainModel.Card.DAO.Card>
    {
        public CardMap()
        {
            ToTable("Card");

            HasKey(p => p.Id);
            Property(e => e.Id).HasMaxLength(50);

            HasOptional(e => e.Employee)
                .WithMany(e => e.CardSet)
                .HasForeignKey(e => e.EmployeeId);
        }
    }
}
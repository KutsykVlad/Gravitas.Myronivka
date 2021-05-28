using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class SecurityCheckReviewOpDataMap : BaseOpDataMap<SecurityCheckReviewOpData>
    {
        public SecurityCheckReviewOpDataMap()
        {
            ToTable("opd.SecurityCheckReviewOpData");

            Property(e => e.StateId)
                .IsRequired();

            HasOptional(e => e.Ticket)
                .WithMany(e => e.SecurityCheckReviewOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.SecurityCheckReviewOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.SecurityCheckReviewOpData)
                .HasForeignKey(e => e.SecurityCheckReviewOpDataId);
        }
    }
}
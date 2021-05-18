using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class MixedFeedLoadOpDataMap : BaseOpDataMap<MixedFeedLoadOpData>
    {
        public MixedFeedLoadOpDataMap()
        {
            ToTable("opd.MixedFeedLoadOpData");

            HasRequired(e => e.OpDataState)
                .WithMany(e => e.MixedFeedLoadOpDataSet)
                .HasForeignKey(e => e.StateId);

            HasOptional(e => e.Ticket)
                .WithMany(e => e.MixedFeedLoadOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.MixedFeedLoadOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.MixedFeedLoadOpData)
                .HasForeignKey(e => e.MixedFeedLoadOpDataId);

            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.MixedFeedLoadOpData)
                .HasForeignKey(e => e.MixedFeedLoadOpDataId);
        }
    }
}
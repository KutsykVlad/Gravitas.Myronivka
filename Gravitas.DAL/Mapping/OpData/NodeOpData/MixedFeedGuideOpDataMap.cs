using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class MixedFeedGuideOpDataMap : BaseOpDataMap<MixedFeedGuideOpData>
    {
        public MixedFeedGuideOpDataMap()
        {
            ToTable("opd.MixedFeedGuideOpData");

            HasRequired(e => e.OpDataState)
                .WithMany(e => e.MixedFeedGuideOpDataSet)
                .HasForeignKey(e => e.StateId);
            
            HasOptional(e => e.Ticket)
                .WithMany(e => e.MixedFeedGuideOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.MixedFeedGuideOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.MixedFeedGuideOpData)
                .HasForeignKey(e => e.MixedFeedGuideOpDataId);
            
            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.MixedFeedGuideOpData)
                .HasForeignKey(e => e.MixedFeedGuideOpDataId);
        }
    }
}
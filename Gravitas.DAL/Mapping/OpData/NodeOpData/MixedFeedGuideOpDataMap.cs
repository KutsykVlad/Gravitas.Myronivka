using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping
{
    class MixedFeedGuideOpDataMap : BaseOpDataMap<MixedFeedGuideOpData>
    {
        public MixedFeedGuideOpDataMap()
        {
            this.ToTable("opd.MixedFeedGuideOpData");

            this.HasRequired(e => e.OpDataState)
                .WithMany(e => e.MixedFeedGuideOpDataSet)
                .HasForeignKey(e => e.StateId);
            
            this.HasOptional(e => e.Ticket)
                .WithMany(e => e.MixedFeedGuideOpDataSet)
                .HasForeignKey(e => e.TicketId);

            this.HasOptional(e => e.Node)
                .WithMany(e => e.MixedFeedGuideOpDataSet)
                .HasForeignKey(e => e.NodeId);

            this.HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.MixedFeedGuideOpData)
                .HasForeignKey(e => e.MixedFeedGuideOpDataId);
            
            this.HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.MixedFeedGuideOpData)
                .HasForeignKey(e => e.MixedFeedGuideOpDataId);
        }
    }
}
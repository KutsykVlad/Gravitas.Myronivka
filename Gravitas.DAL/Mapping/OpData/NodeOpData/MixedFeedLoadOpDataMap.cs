using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping
{
    class MixedFeedLoadOpDataMap : BaseOpDataMap<MixedFeedLoadOpData>
    {
        public MixedFeedLoadOpDataMap()
        {
            this.ToTable("opd.MixedFeedLoadOpData");

            this.HasRequired(e => e.OpDataState)
                .WithMany(e => e.MixedFeedLoadOpDataSet)
                .HasForeignKey(e => e.StateId);

            this.HasOptional(e => e.Ticket)
                .WithMany(e => e.MixedFeedLoadOpDataSet)
                .HasForeignKey(e => e.TicketId);

            this.HasOptional(e => e.Node)
                .WithMany(e => e.MixedFeedLoadOpDataSet)
                .HasForeignKey(e => e.NodeId);

            this.HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.MixedFeedLoadOpData)
                .HasForeignKey(e => e.MixedFeedLoadOpDataId);

            this.HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.MixedFeedLoadOpData)
                .HasForeignKey(e => e.MixedFeedLoadOpDataId);
        }
    }
}
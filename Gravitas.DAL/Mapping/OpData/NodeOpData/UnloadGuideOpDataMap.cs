using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class UnloadGuideOpDataMap : BaseOpDataMap<UnloadGuideOpData>
    {
        public UnloadGuideOpDataMap()
        {
            ToTable("opd.UnloadGuideOpData");

            HasRequired(e => e.OpDataState)
                .WithMany(e => e.UnloadGuideOpDataSet)
                .HasForeignKey(e => e.StateId);

            HasOptional(e => e.Ticket)
                .WithMany(e => e.UnloadGuideOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.UnloadGuideOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.UnloadGuideOpData)
                .HasForeignKey(e => e.UnloadGuideOpDataId);

            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.UnloadGuideOpData)
                .HasForeignKey(e => e.UnloadGuideOpDataId);
        }
    }
}
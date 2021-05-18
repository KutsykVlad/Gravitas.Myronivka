using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class LoadGuideOpDataMap : BaseOpDataMap<LoadGuideOpData>
    {
        public LoadGuideOpDataMap()
        {
            ToTable("opd.LoadGuideOpData");

            HasRequired(e => e.OpDataState)
                .WithMany(e => e.LoadGuideOpDataSet)
                .HasForeignKey(e => e.StateId);
            
            HasOptional(e => e.Node)
                .WithMany(e => e.LoadGuideOpDataSet)
                .HasForeignKey(e => e.LoadPointNodeId);

            HasOptional(e => e.Ticket)
                .WithMany(e => e.LoadGuideOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.LoadGuideOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.LoadGuideOpData)
                .HasForeignKey(e => e.LoadGuideOpDataId);

            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.LoadGuideOpData)
                .HasForeignKey(e => e.LoadGuideOpDataId);
        }
    }
}
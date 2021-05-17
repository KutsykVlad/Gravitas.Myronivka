using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
    class LoadGuideOpDataMap : BaseOpDataMap<LoadGuideOpData>
    {
        public LoadGuideOpDataMap()
        {
            this.ToTable("opd.LoadGuideOpData");

            this.HasRequired(e => e.OpDataState)
                .WithMany(e => e.LoadGuideOpDataSet)
                .HasForeignKey(e => e.StateId);
            
            this.HasOptional(e => e.Node)
                .WithMany(e => e.LoadGuideOpDataSet)
                .HasForeignKey(e => e.LoadPointNodeId);

            this.HasOptional(e => e.Ticket)
                .WithMany(e => e.LoadGuideOpDataSet)
                .HasForeignKey(e => e.TicketId);

            this.HasOptional(e => e.Node)
                .WithMany(e => e.LoadGuideOpDataSet)
                .HasForeignKey(e => e.NodeId);

            this.HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.LoadGuideOpData)
                .HasForeignKey(e => e.LoadGuideOpDataId);

            this.HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.LoadGuideOpData)
                .HasForeignKey(e => e.LoadGuideOpDataId);
        }
    }
}
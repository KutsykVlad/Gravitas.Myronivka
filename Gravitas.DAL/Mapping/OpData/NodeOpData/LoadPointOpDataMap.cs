using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
    class LoadPointOpDataMap : BaseOpDataMap<LoadPointOpData>
    {
        public LoadPointOpDataMap()
        {
            this.ToTable("opd.LoadPointOpData");

            this.HasRequired(e => e.OpDataState)
                .WithMany(e => e.LoadPointOpDataSet)
                .HasForeignKey(e => e.StateId);

            this.HasOptional(e => e.Ticket)
                .WithMany(e => e.LoadPointOpDataSet)
                .HasForeignKey(e => e.TicketId);

            this.HasOptional(e => e.Node)
                .WithMany(e => e.LoadPointOpDataSet)
                .HasForeignKey(e => e.NodeId);

            this.HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.LoadPointOpData)
                .HasForeignKey(e => e.LoadPointOpDataId);

            this.HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.LoadPointOpData)
                .HasForeignKey(e => e.LoadPointOpDataId);
        }
    }
}
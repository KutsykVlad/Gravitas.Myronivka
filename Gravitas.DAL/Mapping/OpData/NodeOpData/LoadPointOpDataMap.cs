using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class LoadPointOpDataMap : BaseOpDataMap<LoadPointOpData>
    {
        public LoadPointOpDataMap()
        {
            ToTable("opd.LoadPointOpData");

            HasRequired(e => e.OpDataState)
                .WithMany(e => e.LoadPointOpDataSet)
                .HasForeignKey(e => e.StateId);

            HasOptional(e => e.Ticket)
                .WithMany(e => e.LoadPointOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.LoadPointOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.LoadPointOpData)
                .HasForeignKey(e => e.LoadPointOpDataId);

            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.LoadPointOpData)
                .HasForeignKey(e => e.LoadPointOpDataId);
        }
    }
}
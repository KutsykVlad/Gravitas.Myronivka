using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class UnloadPointOpDataMap : BaseOpDataMap<UnloadPointOpData>
    {
        public UnloadPointOpDataMap()
        {
            ToTable("opd.UnloadPointOpData");

            Property(e => e.StateId)
                .IsRequired();

            HasOptional(e => e.Ticket)
                .WithMany(e => e.UnloadPointOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.UnloadPointOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.UnloadPointOpData)
                .HasForeignKey(e => e.UnloadPointOpDataId);

            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.UnloadPointOpData)
                .HasForeignKey(e => e.UnloadPointOpDataId);
        }
    }
}
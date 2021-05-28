using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class LabFacelessOpDataMap : BaseOpDataMap<LabFacelessOpData>
    {
        public LabFacelessOpDataMap()
        {
            ToTable("opd.LabFacelessOpData");

            Property(e => e.StateId)
                .IsRequired();
            
            HasMany(e => e.LabFacelessOpDataComponentSet)
                .WithRequired(e => e.LabFacelessOpData)
                .HasForeignKey(e => e.LabFacelessOpDataId);

            HasOptional(e => e.Ticket)
                .WithMany(e => e.LabFacelessOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.LabFacelessOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.LabFacelessOpData)
                .HasForeignKey(e => e.LabFacelessOpDataId);

            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.LabFacelessOpData)
                .HasForeignKey(e => e.LabFacelessOpDataId);
        }
    }
}
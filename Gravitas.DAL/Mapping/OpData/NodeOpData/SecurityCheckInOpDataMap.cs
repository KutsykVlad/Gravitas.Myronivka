using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class SecurityCheckInOpDataMap : BaseOpDataMap<SecurityCheckInOpData>
    {
        public SecurityCheckInOpDataMap()
        {
            ToTable("opd.SecurityCheckInOpData");

            Property(e => e.StateId)
                .IsRequired();

            HasOptional(e => e.Ticket)
                .WithMany(e => e.SecurityCheckInOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.SecurityCheckInOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.SecurityCheckInOpData)
                .HasForeignKey(e => e.SecurityCheckInOpDataId);

            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.SecurityCheckInOpData)
                .HasForeignKey(e => e.SecurityCheckInOpDataId);
        }
    }
}
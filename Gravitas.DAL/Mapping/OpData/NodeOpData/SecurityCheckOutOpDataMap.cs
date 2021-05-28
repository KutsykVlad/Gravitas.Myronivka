using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class SecurityCheckOutOpDataMap : BaseOpDataMap<SecurityCheckOutOpData>
    {
        public SecurityCheckOutOpDataMap()
        {
            ToTable("opd.SecurityCheckOutOpData");

            Property(e => e.StateId)
                .IsRequired();

            HasOptional(e => e.Ticket)
                .WithMany(e => e.SecurityCheckOutOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.SecurityCheckOutOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.SecurityCheckOutOpData)
                .HasForeignKey(e => e.SecurityCheckOutOpDataId);

            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.SecurityCheckOutOpData)
                .HasForeignKey(e => e.SecurityCheckOutOpDataId);
        }
    }
}
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class CentralLabOpDataMap : BaseOpDataMap<CentralLabOpData>
    {
        public CentralLabOpDataMap()
        {
            ToTable("opd.CentralLabOpData");

            Property(e => e.SampleCheckInDateTime)
                .HasColumnType("datetime2")
                .IsOptional();
            
            Property(e => e.SampleCheckOutTime)
                .HasColumnType("datetime2")
                .IsOptional();

            HasRequired(e => e.OpDataState)
                .WithMany(e => e.CentralLabOpDataSet)
                .HasForeignKey(e => e.StateId);

            HasOptional(e => e.Ticket)
                .WithMany(e => e.CentralLabOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.CentralLabOpSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.CentralLabOpData)
                .HasForeignKey(e => e.CentralLaboratoryOpData);
        }
    }
}

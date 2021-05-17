using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
    class CentralLabOpDataMap : BaseOpDataMap<CentralLabOpData>
    {
        public CentralLabOpDataMap()
        {
            this.ToTable("opd.CentralLabOpData");

            this.Property(e => e.SampleCheckInDateTime)
                .HasColumnType("datetime2")
                .IsOptional();
            this.Property(e => e.SampleCheckOutTime)
                .HasColumnType("datetime2")
                .IsOptional();

            this.HasRequired(e => e.OpDataState)
                .WithMany(e => e.CentralLabOpDataSet)
                .HasForeignKey(e => e.StateId);

            this.HasOptional(e => e.Ticket)
                .WithMany(e => e.CentralLabOpDataSet)
                .HasForeignKey(e => e.TicketId);

            this.HasOptional(e => e.Node)
                .WithMany(e => e.CentralLabOpSet)
                .HasForeignKey(e => e.NodeId);

            this.HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.CentralLabOpData)
                .HasForeignKey(e => e.CentralLaboratoryOpData);
        }
    }
}

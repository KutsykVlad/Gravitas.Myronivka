using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping.OpData.NodeOpData
{
    class ScaleOpDataMap : BaseOpDataMap<ScaleOpData>
    {
        public ScaleOpDataMap()
        {
            ToTable("opd.ScaleOpData");

            Property(e => e.TruckWeightDateTime)
                .IsOptional();
            
            Property(e => e.TruckWeightValue)
                .IsOptional();
            
            Property(e => e.TruckWeightIsAccepted)
                .IsOptional();

            Property(e => e.GuardianPresence)
                .HasColumnName("GuardPresence");
            
            Property(e => e.TrailerIsAvailable)
                .HasColumnName("TrailerAvailable");

            Property(e => e.TrailerWeightDateTime)
                .IsOptional();
            
            Property(e => e.TrailerWeightValue)
                .IsOptional();
            
            Property(e => e.TrailerWeightIsAccepted)
                .IsOptional();

            HasRequired(e => e.OpDataState)
                .WithMany(e => e.ScaleOpDataSet)
                .HasForeignKey(e => e.StateId);

            HasOptional(e => e.Ticket)
                .WithMany(e => e.ScaleOpDataSet)
                .HasForeignKey(e => e.TicketId);

            HasOptional(e => e.Node)
                .WithMany(e => e.ScaleOpDataSet)
                .HasForeignKey(e => e.NodeId);

            HasMany(e => e.OpVisaSet)
                .WithOptional(e => e.ScaleOpData)
                .HasForeignKey(e => e.ScaleTareOpDataId);

            HasMany(e => e.OpCameraSet)
                .WithOptional(e => e.ScaleOpData)
                .HasForeignKey(e => e.ScaleOpDataId);
        }
    }
}
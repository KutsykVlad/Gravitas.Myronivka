using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping {

	class ScaleOpDataMap : BaseOpDataMap<ScaleOpData> {

		public ScaleOpDataMap() {

			this.ToTable("opd.ScaleOpData");

			this.Property(e => e.TruckWeightDateTime)
				.IsOptional();
			this.Property(e => e.TruckWeightValue)
				.IsOptional();
			this.Property(e => e.TruckWeightIsAccepted)
				.IsOptional();

		    this.Property(e => e.GuardianPresence)
		        .HasColumnName("GuardPresence");
		    this.Property(e => e.TrailerIsAvailable)
		        .HasColumnName("TrailerAvailable");

			this.Property(e => e.TrailerWeightDateTime)
				.IsOptional();
			this.Property(e => e.TrailerWeightValue)
				.IsOptional();
			this.Property(e => e.TrailerWeightIsAccepted)
				.IsOptional();

			this.HasRequired(e => e.OpDataState)
				.WithMany(e => e.ScaleOpDataSet)
				.HasForeignKey(e => e.StateId);

			this.HasOptional(e => e.Ticket)
				.WithMany(e => e.ScaleOpDataSet)
				.HasForeignKey(e => e.TicketId);

			this.HasOptional(e => e.Node)
				.WithMany(e => e.ScaleOpDataSet)
				.HasForeignKey(e => e.NodeId);

			this.HasMany(e => e.OpVisaSet)
				.WithOptional(e => e.ScaleOpData)
				.HasForeignKey(e => e.ScaleTareOpDataId);

			this.HasMany(e => e.OpCameraSet)
				.WithOptional(e => e.ScaleOpData)
				.HasForeignKey(e => e.ScaleOpDataId);
		}
	}
}
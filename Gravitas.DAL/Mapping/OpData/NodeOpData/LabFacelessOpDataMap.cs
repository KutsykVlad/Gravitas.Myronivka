using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	class LabFacelessOpDataMap : BaseOpDataMap<LabFacelessOpData> {

		public LabFacelessOpDataMap() {
			this.ToTable("opd.LabFacelessOpData");

			this.HasRequired(e => e.OpDataState)
				.WithMany(e => e.LabFacelessOpDataSet)
				.HasForeignKey(e => e.StateId);

			this.HasMany(e => e.LabFacelessOpDataComponentSet)
				.WithRequired(e => e.LabFacelessOpData)
				.HasForeignKey(e => e.LabFacelessOpDataId);

			this.HasOptional(e => e.Ticket)
				.WithMany(e => e.LabFacelessOpDataSet)
				.HasForeignKey(e => e.TicketId);

			this.HasOptional(e => e.Node)
				.WithMany(e => e.LabFacelessOpDataSet)
				.HasForeignKey(e => e.NodeId);

			this.HasMany(e => e.OpVisaSet)
				.WithOptional(e => e.LabFacelessOpData)
				.HasForeignKey(e => e.LabFacelessOpDataId);

			this.HasMany(e => e.OpCameraSet)
				.WithOptional(e => e.LabFacelessOpData)
				.HasForeignKey(e => e.LabFacelessOpDataId);
		}
	}
}

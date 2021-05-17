using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping {

	class UnloadGuideOpDataMap : BaseOpDataMap<UnloadGuideOpData> {

		public UnloadGuideOpDataMap() {

			this.ToTable("opd.UnloadGuideOpData");

			this.HasRequired(e => e.OpDataState)
				.WithMany(e => e.UnloadGuideOpDataSet)
				.HasForeignKey(e => e.StateId);

			this.HasOptional(e => e.Ticket)
				.WithMany(e => e.UnloadGuideOpDataSet)
				.HasForeignKey(e => e.TicketId);

			this.HasOptional(e => e.Node)
				.WithMany(e => e.UnloadGuideOpDataSet)
				.HasForeignKey(e => e.NodeId);

			this.HasMany(e => e.OpVisaSet)
				.WithOptional(e => e.UnloadGuideOpData)
				.HasForeignKey(e => e.UnloadGuideOpDataId);

			this.HasMany(e => e.OpCameraSet)
				.WithOptional(e => e.UnloadGuideOpData)
				.HasForeignKey(e => e.UnloadGuideOpDataId);
		}
	}
}
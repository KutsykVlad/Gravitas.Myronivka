using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping {

	class UnloadPointOpDataMap : BaseOpDataMap<UnloadPointOpData> {

		public UnloadPointOpDataMap() {

			this.ToTable("opd.UnloadPointOpData");

			this.HasRequired(e => e.OpDataState)
				.WithMany(e => e.UnloadPointOpDataSet)
				.HasForeignKey(e => e.StateId);

			this.HasOptional(e => e.Ticket)
				.WithMany(e => e.UnloadPointOpDataSet)
				.HasForeignKey(e => e.TicketId);

			this.HasOptional(e => e.Node)
				.WithMany(e => e.UnloadPointOpDataSet)
				.HasForeignKey(e => e.NodeId);

			this.HasMany(e => e.OpVisaSet)
				.WithOptional(e => e.UnloadPointOpData)
				.HasForeignKey(e => e.UnloadPointOpDataId);

			this.HasMany(e => e.OpCameraSet)
				.WithOptional(e => e.UnloadPointOpData)
				.HasForeignKey(e => e.UnloadPointOpDataId);
		}
	}
}
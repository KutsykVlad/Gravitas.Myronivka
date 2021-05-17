using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;

namespace Gravitas.DAL.Mapping {

	class SecurityCheckInOpDataMap : BaseOpDataMap<SecurityCheckInOpData> {

		public SecurityCheckInOpDataMap() {

			this.ToTable("opd.SecurityCheckInOpData");

			this.HasRequired(e => e.OpDataState)
				.WithMany(e => e.SecurityCheckInOpDataSet)
				.HasForeignKey(e => e.StateId);

			this.HasOptional(e => e.Ticket)
				.WithMany(e => e.SecurityCheckInOpDataSet)
				.HasForeignKey(e => e.TicketId);

			this.HasOptional(e => e.Node)
				.WithMany(e => e.SecurityCheckInOpDataSet)
				.HasForeignKey(e => e.NodeId);

			this.HasMany(e => e.OpVisaSet)
				.WithOptional(e => e.SecurityCheckInOpData)
				.HasForeignKey(e => e.SecurityCheckInOpDataId);

			this.HasMany(e => e.OpCameraSet)
				.WithOptional(e => e.SecurityCheckInOpData)
				.HasForeignKey(e => e.SecurityCheckInOpDataId);
		}
	}
}
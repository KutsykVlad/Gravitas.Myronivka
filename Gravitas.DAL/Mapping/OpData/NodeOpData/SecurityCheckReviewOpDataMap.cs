using Gravitas.Model;

namespace Gravitas.DAL.Mapping
{
	class SecurityCheckReviewOpDataMap : BaseOpDataMap<SecurityCheckReviewOpData>
	{
		public SecurityCheckReviewOpDataMap()
		{
			this.ToTable("opd.SecurityCheckReviewOpData");

			this.HasRequired(e => e.OpDataState)
				.WithMany(e => e.SecurityCheckReviewOpDataSet)
				.HasForeignKey(e => e.StateId);

			this.HasOptional(e => e.Ticket)
				.WithMany(e => e.SecurityCheckReviewOpDataSet)
				.HasForeignKey(e => e.TicketId);

			this.HasOptional(e => e.Node)
				.WithMany(e => e.SecurityCheckReviewOpDataSet)
				.HasForeignKey(e => e.NodeId);

			this.HasMany(e => e.OpVisaSet)
				.WithOptional(e => e.SecurityCheckReviewOpData)
				.HasForeignKey(e => e.SecurityCheckReviewOpDataId);
		}
	}
}
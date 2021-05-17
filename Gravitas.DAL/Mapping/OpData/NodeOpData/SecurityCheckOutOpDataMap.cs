using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Gravitas.Model;

namespace Gravitas.DAL.Mapping {

	class SecurityCheckOutOpDataMap : BaseOpDataMap<SecurityCheckOutOpData> {

		public SecurityCheckOutOpDataMap() {

			this.ToTable("opd.SecurityCheckOutOpData");

			this.HasRequired(e => e.OpDataState)
				.WithMany(e => e.SecurityCheckOutOpDataSet)
				.HasForeignKey(e => e.StateId);

			this.HasOptional(e => e.Ticket)
				.WithMany(e => e.SecurityCheckOutOpDataSet)
				.HasForeignKey(e => e.TicketId);

			this.HasOptional(e => e.Node)
				.WithMany(e => e.SecurityCheckOutOpDataSet)
				.HasForeignKey(e => e.NodeId);

			this.HasMany(e => e.OpVisaSet)
				.WithOptional(e => e.SecurityCheckOutOpData)
				.HasForeignKey(e => e.SecurityCheckOutOpDataId);

			this.HasMany(e => e.OpCameraSet)
				.WithOptional(e => e.SecurityCheckOutOpData)
				.HasForeignKey(e => e.SecurityCheckOutOpDataId);

		}
	}
}
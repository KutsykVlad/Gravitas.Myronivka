using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Gravitas.Model;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

namespace Gravitas.Platform.Web.ViewModel {

	public static partial class LaboratoryInVms {

		public class PrintCollisionInitVm {
			public long NodeId { get; set; }
			public long TicketId { get; set; }

			[Required]
			[DisplayName("Електронна адреса")]
			public string Email1 { get; set; }
			
			[Required]
			[DisplayName("Контактний номер")]
			public string Phone1 { get; set; }

			public string Email2 { get; set; }
			public string Phone2 { get; set; }
			public string Email3 { get; set; }
			public string Phone3 { get; set; }
			
			public string Comment { get; set; }

			[DisplayName("Доступні менеджери")]
			public string Manager1 { get; set; }
			public string Manager2 { get; set; }
			public string Manager3 { get; set; }
			public Dictionary<string, ExternalData.Employee> ManagerList = new Dictionary<string, ExternalData.Employee>();
		}
	}
}
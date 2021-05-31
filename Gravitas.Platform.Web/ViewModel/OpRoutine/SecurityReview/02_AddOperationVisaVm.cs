using Gravitas.Infrastructure.Platform.Manager.OpData;

namespace Gravitas.Platform.Web.ViewModel
{
	public static partial class SecurityReviewVms
	{
		public class AddOperationVisaVm
		{
			public int NodeId { get; set; }
			
			public BasicTicketContainerData TruckBaseInfo { get; set; }
		}
	}
}
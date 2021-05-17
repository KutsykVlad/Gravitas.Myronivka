using System;
using System.ComponentModel;
using Gravitas.Infrastructure.Platform.Manager;

namespace Gravitas.Platform.Web.ViewModel
{
	public static partial class SecurityReviewVms
	{
		public class AddOperationVisaVm
		{
			public long NodeId { get; set; }
			
			public BasicTicketContainerData TruckBaseInfo { get; set; }
		}
	}
}
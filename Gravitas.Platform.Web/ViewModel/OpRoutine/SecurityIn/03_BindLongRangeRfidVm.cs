using System.ComponentModel;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.OpData;

namespace Gravitas.Platform.Web.ViewModel {
    public static partial class SecurityInVms {
        public class BindLongRangeRfidVm {
            public long NodeId { get; set; }

            public BasicTicketContainerData TruckBaseInfo { get; set; }
        }
    }
}
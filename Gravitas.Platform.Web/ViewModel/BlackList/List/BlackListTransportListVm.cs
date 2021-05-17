using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel
{
    public class BlackListTransportListVm
    {
        public List<BlackListTransportRecordVm> Items { get; set; }

        public BlackListTransportRecordVm TransportToAdd { get; set; }
    }
}
using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.BlackList.Record;

namespace Gravitas.Platform.Web.ViewModel.BlackList.List
{
    public class BlackListTransportListVm
    {
        public List<BlackListTransportRecordVm> Items { get; set; }

        public BlackListTransportRecordVm TransportToAdd { get; set; }
    }
}
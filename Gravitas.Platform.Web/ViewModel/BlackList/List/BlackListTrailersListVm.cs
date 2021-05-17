using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel
{
    public class BlackListTrailersListVm
    {
        public List<BlackListTrailerRecordVm> Items { get; set; }

        public BlackListTrailerRecordVm TrailerToAdd { get; set; }
    }
}
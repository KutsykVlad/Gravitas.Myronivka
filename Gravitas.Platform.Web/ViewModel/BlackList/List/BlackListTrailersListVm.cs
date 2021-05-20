using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.BlackList.Record;

namespace Gravitas.Platform.Web.ViewModel.BlackList.List
{
    public class BlackListTrailersListVm
    {
        public List<BlackListTrailerRecordVm> Items { get; set; }

        public BlackListTrailerRecordVm TrailerToAdd { get; set; }
    }
}
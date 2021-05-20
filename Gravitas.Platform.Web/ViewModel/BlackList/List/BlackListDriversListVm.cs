using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.BlackList.Record;

namespace Gravitas.Platform.Web.ViewModel.BlackList.List
{
    public class BlackListDriversListVm
    {
        public List<BlackListDriverRecordVm> Items { get; set; }

        public BlackListDriverRecordVm DriverToAdd { get; set; }
    }
}
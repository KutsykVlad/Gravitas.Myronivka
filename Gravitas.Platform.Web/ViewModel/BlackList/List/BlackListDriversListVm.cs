using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel
{
    public class BlackListDriversListVm
    {
        public List<BlackListDriverRecordVm> Items { get; set; }

        public BlackListDriverRecordVm DriverToAdd { get; set; }
    }
}
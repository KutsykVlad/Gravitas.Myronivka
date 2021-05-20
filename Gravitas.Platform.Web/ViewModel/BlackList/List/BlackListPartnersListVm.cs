using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.BlackList.Record;

namespace Gravitas.Platform.Web.ViewModel.BlackList.List
{
    public class BlackListPartnersListVm
    {
        public List<BlackListPartnerRecordVm> Items { get; set; }

        public BlackListPartnerRecordToAddVm PartnerToAdd { get; set; }
    }
}
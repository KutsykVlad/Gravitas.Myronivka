using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel
{
    public class BlackListPartnersListVm
    {
        public List<BlackListPartnerRecordVm> Items { get; set; }

        public BlackListPartnerRecordToAddVm PartnerToAdd { get; set; }
    }
}
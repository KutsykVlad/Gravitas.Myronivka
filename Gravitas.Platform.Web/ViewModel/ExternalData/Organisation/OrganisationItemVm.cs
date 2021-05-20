using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.Device._Base;

namespace Gravitas.Platform.Web.ViewModel.ExternalData.Organisation
{
    public class OrganisationItemsVm : BaseEntityVm<string>
    {
        public IEnumerable<OrganisationItemVm> Items { get; set; }
    }
}
using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel
{
    public class OrganisationItemsVm : BaseEntityVm<string>
    {
        public IEnumerable<OrganisationItemVm> Items { get; set; }
    }
}
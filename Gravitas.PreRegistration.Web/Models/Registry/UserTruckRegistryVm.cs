using System.Collections.Generic;

namespace Gravitas.PreRegistration.Web.Models.Registry
{
    public class UserTruckRegistryVm
    {
        public List<RegistryItemVm> Items { get; set; }
        public string PartnerId { get; set; }
    }
}
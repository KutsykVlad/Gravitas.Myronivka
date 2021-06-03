using System.Collections.Generic;
using System.Web.Mvc;
using Gravitas.Model.DomainValue;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class IdleVm
        {
            public int NodeId { get; set; }
            public SingleWindowRegisterFilter SelectedFilterId { get; set; }
            public List<SelectListItem> FilterItems { get; set; }
        }
    }
}
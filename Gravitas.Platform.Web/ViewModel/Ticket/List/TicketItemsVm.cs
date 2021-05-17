using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel
{
    public class TicketItemsVm
    {
        public IEnumerable<TicketItemVm> Items { get; set; }
        public int Count { get; set; }
    }
}
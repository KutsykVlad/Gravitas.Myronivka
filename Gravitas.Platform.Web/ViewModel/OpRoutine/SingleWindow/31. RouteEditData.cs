using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class Route
        {
            public int NodeId { get; set; }
            public int? TicketId { get; set; }

            public IEnumerable<SelectListItem> Items { get; set; }

            [DisplayName("Доступні маршрути")] 
            public int SelectedId { get; set; }

            public string RouteJson { get; set; }
        }
    }
}
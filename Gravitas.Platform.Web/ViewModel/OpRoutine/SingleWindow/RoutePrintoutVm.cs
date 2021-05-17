using System;
using System.Collections.Generic;
using System.ComponentModel;
using Gravitas.Infrastructure.Platform.Manager.Routes;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class RoutePrintoutVm
        {
            [DisplayName("Транспорт No.")]
            public string TransportNo { get; set; }
            [DisplayName("Причеп No.")]
            public string TrailerNo { get; set; }
            [DisplayName("Картка")]
            public string CardNumber { get; set; }

            public DateTime Date { get; set; }
            
            public List<RouteNodes> Route { get; set; }
        }
    }
}
using System.Collections.Generic;
using Gravitas.Infrastructure.Platform.Manager.Routes;

namespace Gravitas.Platform.Web.ViewModel.RouteEditor
{
    public class CreateMainRouteViewModel
    {
        public bool IsMain { get; set; }
        public List<List<RouteJsonConverter.NodeList>> RouteNodes { get; set; }
        public long Id { get; set; }
        public bool IsInQueue { get; set; }
        public bool IsTechnological { get; set; }
    }
}
using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.Dashboard
{
    public class TruckViewModel
    {
        public string TruckNo { get; set; }
        public string TrailerNo { get; set; }
        public string PhoneNo { get; set; }
        public string DriverName { get; set; }
        public int DocumentTypeId { get; set; }
        public long LastNodeId { get; set; }
        public List<long> FutureNodeIds { get; set; }
    }
}
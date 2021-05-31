using System.Collections.Generic;
using Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class UnloadPointType2Vms
    {
        public class SelectAcceptancePoint
        {
            public string AcceptancePointCode { get; set; }
            public List<AcceptancePoint> AcceptancePointsList { get; set; }
            public int NodeId { get; set; }
        }
    }
}
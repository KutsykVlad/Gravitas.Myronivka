using System.Collections.Generic;
using static Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class UnloadPointType2Vms
    {
        public class SelectAcceptancePoint
        {
            public string AcceptancePointCode { get; set; }
            public List<AcceptancePoint> AcceptancePointsList { get; set; }
            public long NodeId { get; set; }
        }
    }
}
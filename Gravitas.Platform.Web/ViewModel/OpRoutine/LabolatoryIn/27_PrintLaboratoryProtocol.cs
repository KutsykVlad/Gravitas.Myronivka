using System;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class LaboratoryInVms
    {
        public class PrintLaboratoryProtocol
        {
            public int NodeId { get; set; }
            public Guid OpDataId { get; set; }
        }
    }
}
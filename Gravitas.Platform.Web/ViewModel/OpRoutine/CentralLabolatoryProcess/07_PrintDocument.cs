using System;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class CentralLaboratoryProcess
    {
        public class PrintDocumentVm
        {
            public int NodeId { get; set; }
            public int TicketId { get; set; }
            public Guid OpDataId { get; set; }
        }
    }
}
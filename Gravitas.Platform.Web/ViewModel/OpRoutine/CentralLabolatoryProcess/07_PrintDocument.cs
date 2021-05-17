using System;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class CentralLaboratoryProcess
    {
        public class PrintDocumentVm
        {
            public long NodeId { get; set; }
            public long TicketId { get; set; }
            public Guid OpDataId { get; set; }
        }
    }
}
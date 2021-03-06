using System.Collections.Generic;
using Gravitas.DAL.Repository.OwnTransport.Models;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class OwnTransportVm
        {
            public int NodeId { get; set; }
            public List<OwnTransportViewModel> Items { get; set; }
        }
    }
}
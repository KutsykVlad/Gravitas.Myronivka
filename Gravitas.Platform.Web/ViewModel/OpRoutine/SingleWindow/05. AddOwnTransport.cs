using System.Collections.Generic;
using System.Web.Mvc;
using Gravitas.DAL.Repository.OwnTransport.Models;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class SingleWindowVms
    {
        public class AddOwnTransportVm
        {
            public int NodeId { get; set; }
            public List<SelectListItem> Types { get; set; }
            public OwnTransportViewModel Transport { get; set; }
        }
    }
}
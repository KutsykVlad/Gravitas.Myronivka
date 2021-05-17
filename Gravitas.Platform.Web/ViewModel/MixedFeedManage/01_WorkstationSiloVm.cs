using System.Collections.Generic;
using Gravitas.Model.DomainModel.MixedFeed.DAO;

namespace Gravitas.Platform.Web.ViewModel
{
    public static partial class MixedFeedManageVms
    {
        public class WorkstationSiloVm
        {
            public IEnumerable<SiloDetailVm> Items { get; set; }
        }
    }
}
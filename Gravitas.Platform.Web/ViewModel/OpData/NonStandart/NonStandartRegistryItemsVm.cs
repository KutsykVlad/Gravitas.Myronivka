using System;
using System.Collections.Generic;

namespace Gravitas.Platform.Web.ViewModel.OpData.NonStandart
{
    public class NonStandartRegistryItemsVm
    {
        public ICollection<NonStandartRegistryItemVm> Items { get; set; }
        public int Count { get; set; }

        public int PrevPage { get; set; }
        public int NextPage { get; set; }

        public int? RelatedNodeId { get; set; }

        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public IEnumerable<NodeInfo> Nodes { get; set; }
    }
}
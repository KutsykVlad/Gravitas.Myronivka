using System.Collections.Generic;

namespace Gravitas.Model.DomainModel.OpData.TDO.Json
{
    public class ProductContentList
    {
        public ICollection<ProductContentItem> Items { get; set; }
        public int Count { get; set; }
    }
}
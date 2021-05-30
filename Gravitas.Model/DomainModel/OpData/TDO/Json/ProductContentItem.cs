using System;

namespace Gravitas.Model.DomainModel.OpData.TDO.Json
{
    public class ProductContentItem
    {
        public int No { get; set; }
        public string OrderNumber { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid UnitId { get; set; }
        public string UnitName { get; set; }
        public double Quantity { get; set; }
    }
}
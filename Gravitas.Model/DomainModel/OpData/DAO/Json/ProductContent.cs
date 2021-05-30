using System;
using Newtonsoft.Json;

namespace Gravitas.Model.DomainModel.OpData.DAO.Json
{
    public class ProductContent
    {
        [JsonProperty("No")] 
        public int No { get; set; }
        
        [JsonProperty("OrderNumber")]
        public string OrderNumber { get; set; }
        
        [JsonProperty("ProductID")] 
        public Guid ProductId { get; set; }
        
        [JsonProperty("UnitID")] 
        public Guid UnitId { get; set; }
        
        [JsonProperty("Quantity")]
        public double Quantity { get; set; }
    }
}
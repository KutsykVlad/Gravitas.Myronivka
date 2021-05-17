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
        public string ProductId { get; set; }
        
        [JsonProperty("UnitID")] 
        public string UnitId { get; set; }
        
        [JsonProperty("Quantity")]
        public double Quantity { get; set; }
    }
}
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetDeliveryBillViaIdDto
        {
            public class Request
            {
                [JsonProperty("ID")]
                public string DeliveryBillId { get; set; }
            }

            public class Response : GetDeliveryBill.Response
            {
            }
        }
    }
}
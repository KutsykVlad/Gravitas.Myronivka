using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetBillFileDto
        {
            public class Request
            {
                [JsonProperty("ID")]
                public string DeliveryBillId { get; set; }

                [JsonProperty("PrintoutTypeId")]
                public string PrintoutTypeId { get; set; }

                public static class PrintoutType
                {
                    public static string QualityBill => "QualityBill";
                    public static string QualityBillV2 => "QualityBillV2";
                    public static string DeliveryBill => "DeliveryBill";
                    public static string CompositeDeliveryBill => "CompositeDeliveryBill";
                    public static string ActOfDisagreementBill => "ActOfDisagreementBill";
                    public static string GetInvoiceBillFile => "GetInvoiceBillFile";
                    public static string TechnologicalFile => "TechnologicalFile";
                }
            }

            public class Response : BaseResponseDto
            {
                [JsonProperty("TicketGuid")]
                public string TicketGuid { get; set; }

                [JsonProperty("ImageBytes")]
                public string Base64Content { get; set; }
            }
        }
    }
}
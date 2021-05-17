using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class PostImageFileDto
        {
            public class Request
            {
                // 106
                [JsonProperty("ID")]
                public string Id { get; set; }

                [JsonProperty("ImageName")]
                public string ImageName { get; set; }

                [JsonProperty("CreateDate")]
                public string CreateDate { get; set; }

                [JsonProperty("ImageBytes")]
                public string Base64Content { get; set; }

                [JsonProperty("IPAddressCamera")]
                public string IpAddressCamera { get; set; }
            }

            public class Response : BaseResponseDto
            {
                [JsonProperty("ErrorCode")]
                public new int ErrorCode { get; set; }

                [JsonProperty("ErrorMsg")]
                public new string ErrorMsg { get; set; }
            }
        }
    }
}
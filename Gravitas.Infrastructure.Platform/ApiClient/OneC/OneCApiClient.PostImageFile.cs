using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using NLog;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PostImageFileDto.Response PostImageFileJsonResponse(PostImageFileDto.Request request, int timeoutInSeconds)
        {
            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri = new Uri($"{BaseAddress}/PostImageFile?format=json");
                requestMessage.Method = HttpMethod.Post;
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
                    "application/json");

                using (var response = Send(requestMessage, timeoutInSeconds))
                {
                    var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            Logger.Info($"PostImageFile: Upload file success. FileName={request.ImageName}");
                            return JsonConvert.DeserializeObject<PostImageFileDto.Response>(responseJson);
                        default:
                            if (!responseJson.Contains("\"ErrorCode\": \"0\"") || !responseJson.Contains("\"ErrorMsg\": \"\""))
                            {
                                Logger.Error($"PostImageFile: Upload file error. Error={responseJson}");
                            }
                            break;
                    }
                }
            }

            return null;
        }
    }
}
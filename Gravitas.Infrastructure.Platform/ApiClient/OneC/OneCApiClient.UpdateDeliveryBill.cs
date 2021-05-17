using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient : BaseApiClient
    {
        public UpdateDeliveryBillDto.Response PostUpdateDeliveryBill(UpdateDeliveryBillDto.Request request,
            int timeoutInSeconds)
        {
            string responseJson;
            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri = new Uri($"{BaseAddress}/UpdateDeliveryBill?format=json");
                requestMessage.Method = HttpMethod.Post;
                requestMessage.Content = new StringContent(
                    JsonConvert.SerializeObject(
                        request,
                        new JsonSerializerSettings
                        {
                            DateFormatString = "yyyy-MM-dd HH:mm:ss", Formatting = Formatting.Indented
                        }
                    ));

                using (var response = Send(requestMessage, timeoutInSeconds))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.BadRequest:
                            responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            break;
                        default:
                            throw new HttpRequestException();
                    }
                }
            }

            var responseDto = JsonConvert.DeserializeObject<UpdateDeliveryBillDto.Response>(responseJson);
            return responseDto;
        }
    }
}
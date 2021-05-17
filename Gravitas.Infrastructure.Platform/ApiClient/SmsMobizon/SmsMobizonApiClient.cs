using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.SmsMobizon
{
    public partial class SmsMobizonApiClient : BaseApiClient, ISmsMobizonApiClient
    {
        private readonly string _token;

        public SmsMobizonApiClient(string uriString, string token) : base(uriString)
        {
            _token = token;
        }

        public GetBalanceDto.Response GetBalance()
        {
            GetBalanceDto.Response responseDto;

            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri = new Uri($"{BaseAddress}/service/user/getownbalance?apiKey={_token}");
                requestMessage.Method = HttpMethod.Post;

                using (var response = Send(requestMessage))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            responseDto = JsonConvert.DeserializeObject<GetBalanceDto.Response>(responseJson);
                            break;
                        default:
                            throw new HttpRequestException(); // DeezzeInvalidResponseException(response);
                    }
                }
            }

            return responseDto;
        }

        public GetMessageStatusDto.Response GetMessageStatus(string[] ids)
        {
            GetMessageStatusDto.Response responseDto;

            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri = new Uri($"{BaseAddress}/service/Message/GetSMSStatus?apiKey={_token}&ids={string.Join(",", ids)}");
                requestMessage.Method = HttpMethod.Post;

                using (var response = Send(requestMessage))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            responseDto = JsonConvert.DeserializeObject<GetMessageStatusDto.Response>(responseJson);
                            break;
                        default:
                            throw new HttpRequestException(); // DeezzeInvalidResponseException(response);
                    }
                }
            }

            return responseDto;
        }

        public SendMessageDto.Response SendSms(string recipient, string text)
        {
            SendMessageDto.Response responseDto;

            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri = new Uri($"{BaseAddress}/service/Message/SendSMSMessage?apiKey={_token}");
                requestMessage.Method = HttpMethod.Post;
                requestMessage.Content = new StringContent(
                    JsonConvert.SerializeObject(
                        new SendMessageDto.Request
                        {
                            Recipient = recipient, Text = text
                        }));

                using (var response = Send(requestMessage))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            responseDto = JsonConvert.DeserializeObject<SendMessageDto.Response>(responseJson);
                            break;
                        default:
                            throw new HttpRequestException(); // DeezzeInvalidResponseException(response);
                    }
                }
            }

            return responseDto;
        }
    }
}
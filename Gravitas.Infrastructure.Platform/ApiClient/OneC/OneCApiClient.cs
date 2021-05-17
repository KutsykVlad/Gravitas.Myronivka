using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Model;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public OneCApiClient(string uriString) : base(uriString)
        {
        }

        public GetDeliveryBill.Response GetDeliveryBillViaSupplyCode(string supplyCode)
        {
            GetDeliveryBill.Response responseDto = null;

            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri = new Uri($"{BaseAddress}/GetDeliveryBillViaSupplyCode?format=json&SupplyCode={supplyCode}");
                requestMessage.Method = HttpMethod.Get;

                using (var response = Send(requestMessage))
                {
                    if (response != null)
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK:
                            case HttpStatusCode.BadRequest:
                                var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                responseDto =
                                    JsonConvert.DeserializeObject<GetDeliveryBill.Response>(responseJson);
                                break;
                            case HttpStatusCode.InternalServerError:
                                responseDto = new GetDeliveryBill.Response
                                {
                                    ErrorMsg = "Внутрішня помилка 1С сервера."
                                };
                                break;
                            default:
                                responseDto = new GetDeliveryBill.Response
                                {
                                    ErrorMsg = "Відсутня база даних."
                                };
                                break;
                        }
                }
            }

            return responseDto;
        }

        public GetDeliveryBill.Response GetDeliveryBillViaBarCode(string barCode)
        {
            GetDeliveryBill.Response responseDto = null;

            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri = new Uri($"{BaseAddress}/GetDeliveryBillViaBarcode?format=json&BarCode={barCode}");
                requestMessage.Method = HttpMethod.Get;

                using (var response = Send(requestMessage))
                {
                    if (response != null)
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK:
                            case HttpStatusCode.BadRequest:
                                var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                responseDto =
                                    JsonConvert.DeserializeObject<GetDeliveryBill.Response>(responseJson);

                                if(responseDto.DocumentTypeId == Dom.ExternalData.DeliveryBill.Type.Outgoing)
                                {
                                    responseDto.DocumentTypeId = Dom.ExternalData.DeliveryBill.Type.Incoming;
                                }

                                break;
                            case HttpStatusCode.InternalServerError:
                                responseDto = new GetDeliveryBill.Response
                                {
                                    ErrorMsg = "Внутрішня помилка 1С сервера."
                                };
                                break;
                            default:
                                responseDto = new GetDeliveryBill.Response
                                {
                                    ErrorMsg = "Відсутня база даних."
                                };
                                break;
                        }
                }
            }

            return responseDto;
        }

        public GetBillFileDto.Response GetBillFile(GetBillFileDto.Request request)
        {
            GetBillFileDto.Response responseDto;

            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri =
                    new Uri($"{BaseAddress}/GetBillFile?format=json&Id={request.DeliveryBillId}&PrintoutTypeId={request.PrintoutTypeId}");
                requestMessage.Method = HttpMethod.Get;

                using (var response = Send(requestMessage))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.BadRequest:
                            var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            responseDto = JsonConvert.DeserializeObject<GetBillFileDto.Response>(responseJson);
                            break;
                        case HttpStatusCode.InternalServerError:
                            responseDto = new GetBillFileDto.Response
                            {
                                ErrorMsg = "Внутрішня помилка 1С сервера."
                            };
                            break;
                        default:
                            responseDto = new GetBillFileDto.Response
                            {
                                ErrorMsg = "Відсутня база даних."
                            };
                            break;
                    }
                }
            }

            return responseDto;
        }

        public ChangeSupplyCodeDto.Response ChangeSupplyCode(ChangeSupplyCodeDto.Request request)
        {
            ChangeSupplyCodeDto.Response responseDto = null;
            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri = new Uri($"{BaseAddress}/ChangeSupplyCode?format=json");
                requestMessage.Method = HttpMethod.Post;
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                using (var response = Send(requestMessage))
                {
                    if (response != null)
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK:
                            case HttpStatusCode.BadRequest:
                                var responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                responseDto = JsonConvert.DeserializeObject<ChangeSupplyCodeDto.Response>(responseJson);
                                break;
                            case HttpStatusCode.InternalServerError:
                                responseDto = new ChangeSupplyCodeDto.Response
                                {
                                    ErrorMsg = "Внутрішня помилка 1С сервера."
                                };
                                break;
                            default:
                                responseDto = new ChangeSupplyCodeDto.Response
                                {
                                    ErrorMsg = "Відсутня база даних."
                                };
                                break;
                        }
                }
            }

            return responseDto;
        }

        public HttpStatusCode UpdateDictionaries()
        {
            try
            {
                using (var requestMessage = new HttpRequestMessage())
                {
                    requestMessage.RequestUri = new Uri($"{GlobalConfigurationManager.ConnectApiHost}/StartLight?dictionary=All");
                    requestMessage.Method = HttpMethod.Get;

                    using (var response = Send(requestMessage))
                    {
                        if (response != null) return response.StatusCode;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error($"OneCApiClient: UpdateDictionaries: Error while retrieving data: {e}");
            }

            return HttpStatusCode.NoContent;
        }
    }
}
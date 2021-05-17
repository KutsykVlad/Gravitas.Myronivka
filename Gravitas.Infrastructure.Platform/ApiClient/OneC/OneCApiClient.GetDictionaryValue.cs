using System;
using System.Net;
using System.Net.Http;
using Gravitas.Model;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        private string GetDictionaryValueJsonResponse(GetDictionaryValueDto.Request request)
        {
            string responseJson;
            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri =
                    new Uri($"{BaseAddress}/GetDictionaryValue?format=json&DictionaryName={request.Dictionary}&ItemId={request.ItemId}");
                requestMessage.Method = HttpMethod.Get;

                Logger.Trace($"OneC Uri: {requestMessage.RequestUri}");

                using (var response = Send(requestMessage))
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Logger.Trace($"OneC Result: {content}");
                    Logger.Trace($"OneC Result(StatusCode): {response.StatusCode}");

                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            responseJson = content;
                            break;
                        default:
                            throw new HttpRequestException();
                    }
                }
            }

            return responseJson;
        }

        public ExternalData.AcceptancePoint GetAcceptancePoint(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetAcceptancePointValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetAcceptancePointValueDto.Response>(responseJson);

            return new ExternalData.AcceptancePoint
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.Budget GetBudget(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetBudgetValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetBudgetValueDto.Response>(responseJson);

            return new ExternalData.Budget
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.Contract GetContract(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetContractValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetContractValueDto.Response>(responseJson);

            return new ExternalData.Contract
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                StartDateTime = responseDto.StartDateTime,
                StopDateTime = responseDto.StopDateTime,
                ManagerId = responseDto.ManagerId
            };
        }

        public ExternalData.Crop GetCrop(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetCropValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetCropValueDto.Response>(responseJson);

            return new ExternalData.Crop
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.Employee GetEmployee(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetEmployeeValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetEmployeeValueDto.Response>(responseJson);

            return new ExternalData.Employee
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                ShortName = responseDto.ShortName,
                FullName = responseDto.FullName,
                Position = responseDto.Position,
                Email = responseDto.Email,
                PhoneNo = responseDto.PhoneNo,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.FixedAsset GetFixedAsset(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetFixedAssetValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetFixedAssetValueDto.Response>(responseJson);

            return new ExternalData.FixedAsset
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                TypeCode = responseDto.TypeCode,
                Model = responseDto.Model,
                Brand = responseDto.Brand,
                RegistrationNo = responseDto.RegistrationNo,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.Organisation GetOrganisation(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetOrganisationValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetOrganisationValueDto.Response>(responseJson);

            return new ExternalData.Organisation
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                ShortName = responseDto.ShortName,
                FullName = responseDto.FullName,
                Address = responseDto.Address,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.Partner GetPartner(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetPartnerValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetPartnerValueDto.Response>(responseJson);

            return new ExternalData.Partner
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                ShortName = responseDto.ShortName,
                FullName = responseDto.FullName,
                Address = responseDto.Address,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId,
                CarrierDriverId = responseDto.CarrierDriverId
            };
        }

        public ExternalData.Product GetProduct(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetProductValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetProductValueDto.Response>(responseJson);

            return new ExternalData.Product
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                ShortName = responseDto.ShortName,
                FullName = responseDto.FullName,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.ReasonForRefund GetReasonForRefund(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetReasonForRefundValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetReasonForRefundValueDto.Response>(responseJson);

            return new ExternalData.ReasonForRefund
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.Route GetRoute(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetRouteValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetRouteValueDto.Response>(responseJson);

            return new ExternalData.Route
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.Stock GetStock(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetStockValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetStockValueDto.Response>(responseJson);

            return new ExternalData.Stock
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                ShortName = responseDto.ShortName,
                FullName = responseDto.FullName,
                Address = responseDto.Address,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId,
                CustomerId = responseDto.CustomerId
            };
        }

        public ExternalData.Subdivision GetSubdivision(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetSubdivisionValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetSubdivisionValueDto.Response>(responseJson);

            return new ExternalData.Subdivision
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                ShortName = responseDto.ShortName,
                FullName = responseDto.FullName,
                Address = responseDto.Address,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.ExternalUser GetUser(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetUserValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetUserValueDto.Response>(responseJson);

            return new ExternalData.ExternalUser
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                ShortName = responseDto.ShortName,
                FullName = responseDto.FullName,
                EmployeeId = responseDto.EmployeeId,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.YearOfHarvest GetYearOfHarvest(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetYearOfHarvestValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetYearOfHarvestValueDto.Response>(responseJson);

            return new ExternalData.YearOfHarvest
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ExternalData.MeasureUnit GetUnit(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetUnitValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetUnitValueDto.Response>(responseJson);

            return new ExternalData.MeasureUnit
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                ShortName = responseDto.ShortName,
                FullName = responseDto.FullName,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }
    }
}
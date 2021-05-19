using System;
using System.Net;
using System.Net.Http;
using Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO;
using Gravitas.Model.DomainModel.ExternalData.Budget.DAO;
using Gravitas.Model.DomainModel.ExternalData.Contract.DAO;
using Gravitas.Model.DomainModel.ExternalData.Crop.DAO;
using Gravitas.Model.DomainModel.ExternalData.Employee.DAO;
using Gravitas.Model.DomainModel.ExternalData.ExternalUser.DAO;
using Gravitas.Model.DomainModel.ExternalData.FixedAsset.DAO;
using Gravitas.Model.DomainModel.ExternalData.MeasureUnit.DAO;
using Gravitas.Model.DomainModel.ExternalData.Organization.DAO;
using Gravitas.Model.DomainModel.ExternalData.Partner.DAO;
using Gravitas.Model.DomainModel.ExternalData.Product.DAO;
using Gravitas.Model.DomainModel.ExternalData.ReasonForRefund.DAO;
using Gravitas.Model.DomainModel.ExternalData.Stock.DAO;
using Gravitas.Model.DomainModel.ExternalData.Subdivision.DAO;
using Gravitas.Model.DomainModel.ExternalData.YearOfHarvest.DAO;
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

        public AcceptancePoint GetAcceptancePoint(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetAcceptancePointValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetAcceptancePointValueDto.Response>(responseJson);

            return new AcceptancePoint
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public Budget GetBudget(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetBudgetValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetBudgetValueDto.Response>(responseJson);

            return new Budget
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public Contract GetContract(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetContractValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetContractValueDto.Response>(responseJson);

            return new Contract
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                StartDateTime = responseDto.StartDateTime,
                StopDateTime = responseDto.StopDateTime,
                ManagerId = responseDto.ManagerId
            };
        }

        public Crop GetCrop(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetCropValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetCropValueDto.Response>(responseJson);

            return new Crop
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public Employee GetEmployee(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetEmployeeValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetEmployeeValueDto.Response>(responseJson);

            return new Employee
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

        public FixedAsset GetFixedAsset(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetFixedAssetValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetFixedAssetValueDto.Response>(responseJson);

            return new FixedAsset
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

        public Organisation GetOrganisation(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetOrganisationValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetOrganisationValueDto.Response>(responseJson);

            return new Organisation
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

        public Partner GetPartner(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetPartnerValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetPartnerValueDto.Response>(responseJson);

            return new Partner
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

        public Product GetProduct(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetProductValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetProductValueDto.Response>(responseJson);

            return new Product
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                ShortName = responseDto.ShortName,
                FullName = responseDto.FullName,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public ReasonForRefund GetReasonForRefund(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetReasonForRefundValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetReasonForRefundValueDto.Response>(responseJson);

            return new ReasonForRefund
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        // public Route GetRoute(string id)
        // {
        //     var responseJson =
        //         GetDictionaryValueJsonResponse(new GetRouteValueDto.Request
        //         {
        //             ItemId = id
        //         });
        //     var responseDto = JsonConvert.DeserializeObject<GetRouteValueDto.Response>(responseJson);
        //
        //     return new Route
        //     {
        //         Id = responseDto.Id,
        //         Code = responseDto.Code,
        //         Name = responseDto.Name,
        //         IsFolder = responseDto.IsFolder == 1,
        //         ParentId = responseDto.ParentId
        //     };
        // }

        public Stock GetStock(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetStockValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetStockValueDto.Response>(responseJson);

            return new Stock
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

        public Subdivision GetSubdivision(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetSubdivisionValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetSubdivisionValueDto.Response>(responseJson);

            return new Subdivision
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

        public ExternalUser GetUser(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetUserValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetUserValueDto.Response>(responseJson);

            return new ExternalUser
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

        public YearOfHarvest GetYearOfHarvest(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetYearOfHarvestValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetYearOfHarvestValueDto.Response>(responseJson);

            return new YearOfHarvest
            {
                Id = responseDto.Id,
                Code = responseDto.Code,
                Name = responseDto.Name,
                IsFolder = responseDto.IsFolder == 1,
                ParentId = responseDto.ParentId
            };
        }

        public MeasureUnit GetUnit(string id)
        {
            var responseJson =
                GetDictionaryValueJsonResponse(new GetUnitValueDto.Request
                {
                    ItemId = id
                });
            var responseDto = JsonConvert.DeserializeObject<GetUnitValueDto.Response>(responseJson);

            return new MeasureUnit
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
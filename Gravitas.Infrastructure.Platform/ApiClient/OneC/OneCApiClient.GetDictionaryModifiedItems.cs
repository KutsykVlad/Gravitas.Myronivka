using System;
using System.Collections.Generic;
using System.Linq;
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
using Gravitas.Model.DomainModel.ExternalData.Route.DAO;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public string GetDictionaryModifiedItemsJsonResponse(GetDictionaryModifiedItemsDto.Request request)
        {
            string responseJson;
            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.RequestUri = new Uri($"{BaseAddress}/GetDictionaryModifiedItems?format=json&DictionaryName={request.Dictionary}");
                requestMessage.Method = HttpMethod.Get;

                using (var response = Send(requestMessage))
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            responseJson = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                            break;
                        default:
                            throw new HttpRequestException();
                    }
                }
            }

            return responseJson;
        }

        public List<AcceptancePoint> GetAcceptancePointModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetAcceptancePointModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetAcceptancePointModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e =>
                new AcceptancePoint
                {
                    Id = e.Id,
                    Code = e.Code,
                    Name = e.Name,
                    IsFolder = e.IsFolder == 1,
                    ParentId = e.ParentId
                }).ToList();
        }

        public List<Budget> GetBudgetModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetBudgetModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetBudgetModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new Budget
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<Contract> GetContractModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetContractModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetContractModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new Contract
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                StartDateTime = e.StartDateTime,
                StopDateTime = e.StopDateTime,
                ManagerId = e.ManagerId
            }).ToList();
        }

        public List<Crop> GetCropModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetCropModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetCropModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new Crop
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<Employee> GetEmployeeModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetEmployeeModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetEmployeeModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new Employee
            {
                Id = e.Id,
                Code = e.Code,
                ShortName = e.ShortName,
                FullName = e.FullName,
                Position = e.Position,
                Email = e.Email,
                PhoneNo = e.PhoneNo,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<FixedAsset> GetFixedAssetModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetFixedAssetModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetFixedAssetModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new FixedAsset
            {
                Id = e.Id,
                Code = e.Code,
                TypeCode = e.TypeCode,
                Model = e.Model,
                Brand = e.Brand,
                RegistrationNo = e.RegistrationNo,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<Organisation> GetOrganisationModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetOrganisationModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetOrganisationModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new Organisation
            {
                Id = e.Id,
                Code = e.Code,
                ShortName = e.ShortName,
                FullName = e.FullName,
                Address = e.Address,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<Partner> GetPartnerModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetPartnerModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetPartnerModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new Partner
            {
                Id = e.Id,
                Code = e.Code,
                ShortName = e.ShortName,
                FullName = e.FullName,
                Address = e.Address,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId,
                CarrierDriverId = e.CarrierDriverId
            }).ToList();
        }

        public List<Product> GetProductModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetProductModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetProductModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new Product
            {
                Id = e.Id,
                Code = e.Code,
                ShortName = e.ShortName,
                FullName = e.FullName,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<ReasonForRefund> GetReasonForRefundModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetReasonForRefundModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetReasonForRefundModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ReasonForRefund
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<Route> GetRouteModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetRouteModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetRouteModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new Route
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<Stock> GetStockModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetStockModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetStockModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new Stock
            {
                Id = e.Id,
                Code = e.Code,
                ShortName = e.ShortName,
                FullName = e.FullName,
                Address = e.Address,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId,
                CustomerId = e.CustomerId
            }).ToList();
        }

        public List<Subdivision> GetSubdivisionModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetSubdivisionModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetSubdivisionModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new Subdivision
            {
                Id = e.Id,
                Code = e.Code,
                ShortName = e.ShortName,
                FullName = e.FullName,
                Address = e.Address,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<ExternalUser> GetUserModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetUserModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetUserModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalUser
            {
                Id = e.Id,
                Code = e.Code,
                ShortName = e.ShortName,
                FullName = e.FullName,
                EmployeeId = e.EmployeeId,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<YearOfHarvest> GetYearOfHarvestModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetYearOfHarvestModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetYearOfHarvestModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new YearOfHarvest
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<MeasureUnit> GetUnitModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetUnitModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetUnitModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new MeasureUnit
            {
                Id = e.Id,
                Code = e.Code,
                ShortName = e.ShortName,
                FullName = e.FullName,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }
    }
}
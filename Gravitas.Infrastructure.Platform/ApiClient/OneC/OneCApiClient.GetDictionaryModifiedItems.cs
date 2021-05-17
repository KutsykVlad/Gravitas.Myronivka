using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Gravitas.Model;
using Newtonsoft.Json;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

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

        public List<ExternalData.AcceptancePoint> GetAcceptancePointModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetAcceptancePointModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetAcceptancePointModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e =>
                new ExternalData.AcceptancePoint
                {
                    Id = e.Id,
                    Code = e.Code,
                    Name = e.Name,
                    IsFolder = e.IsFolder == 1,
                    ParentId = e.ParentId
                }).ToList();
        }

        public List<ExternalData.Budget> GetBudgetModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetBudgetModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetBudgetModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.Budget
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<ExternalData.Contract> GetContractModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetContractModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetContractModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.Contract
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                StartDateTime = e.StartDateTime,
                StopDateTime = e.StopDateTime,
                ManagerId = e.ManagerId
            }).ToList();
        }

        public List<ExternalData.Crop> GetCropModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetCropModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetCropModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.Crop
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<ExternalData.Employee> GetEmployeeModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetEmployeeModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetEmployeeModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.Employee
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

        public List<ExternalData.FixedAsset> GetFixedAssetModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetFixedAssetModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetFixedAssetModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.FixedAsset
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

        public List<ExternalData.Organisation> GetOrganisationModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetOrganisationModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetOrganisationModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.Organisation
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

        public List<ExternalData.Partner> GetPartnerModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetPartnerModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetPartnerModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.Partner
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

        public List<ExternalData.Product> GetProductModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetProductModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetProductModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.Product
            {
                Id = e.Id,
                Code = e.Code,
                ShortName = e.ShortName,
                FullName = e.FullName,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<ExternalData.ReasonForRefund> GetReasonForRefundModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetReasonForRefundModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetReasonForRefundModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.ReasonForRefund
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<ExternalData.Route> GetRouteModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetRouteModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetRouteModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.Route
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<ExternalData.Stock> GetStockModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetStockModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetStockModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.Stock
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

        public List<ExternalData.Subdivision> GetSubdivisionModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetSubdivisionModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetSubdivisionModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.Subdivision
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

        public List<ExternalData.ExternalUser> GetUserModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetUserModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetUserModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.ExternalUser
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

        public List<ExternalData.YearOfHarvest> GetYearOfHarvestModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetYearOfHarvestModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetYearOfHarvestModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.YearOfHarvest
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                IsFolder = e.IsFolder == 1,
                ParentId = e.ParentId
            }).ToList();
        }

        public List<ExternalData.MeasureUnit> GetUnitModifiedItems()
        {
            var responseJson =
                GetDictionaryModifiedItemsJsonResponse(new GetUnitModifiedItemsDto.Request());
            var responseDto = JsonConvert.DeserializeObject<GetUnitModifiedItemsDto.Response>(responseJson);

            return responseDto.Items.Select(e => new ExternalData.MeasureUnit
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
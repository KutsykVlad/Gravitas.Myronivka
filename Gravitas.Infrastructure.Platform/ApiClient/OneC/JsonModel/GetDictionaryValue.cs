using System;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetDictionaryValueDto
        {
            public static class DictionaryType
            {
                public static string Organization = @"Organizations";
                public static string Stock = @"Stocks";
                public static string Subdivision = @"Subdivisions";
                public static string Partner = @"Partners";
                public static string User = @"Users";
                public static string Employee = @"Employees";
                public static string AcceptancePoint = @"AcceptancePoints";
                public static string Crop = @"Crops";
                public static string Route = @"Routes";
                public static string Budget = @"Budgets";
                public static string ReasonForRefund = @"ReasonsForRefunds";
                public static string YearOfHarvest = @"YearsOfHarvest";
                public static string Product = @"Products";
                public static string FixedAsset = @"FixedAssets";
                public static string Contract = @"Contracts";
            }

            public class Request
            {
                [JsonProperty("Dictionary")]
                public virtual string Dictionary { get; set; }

                [JsonProperty("ItemId")]
                public string ItemId { get; set; }
            }

            public static class Response
            {
                public class Dictionary : BaseResponseDto
                {
                }

                public class DictionaryType1 : Dictionary
                {
                    [JsonProperty("Id")]
                    public string Id { get; set; }

                    [JsonProperty("Code")]
                    public string Code { get; set; }

                    [JsonProperty("ShortName")]
                    public string ShortName { get; set; }

                    [JsonProperty("FullName")]
                    public string FullName { get; set; }

                    [JsonProperty("Address")]
                    public string Address { get; set; }

                    [JsonProperty("IsFolder")]
                    public int IsFolder { get; set; }

                    [JsonProperty("ParentId")]
                    public string ParentId { get; set; }

                    [JsonProperty("CarrierDriverId")]
                    public string CarrierDriverId { get; set; }

                    [JsonProperty("CustomerId")]
                    public string CustomerId { get; set; }
                }

                // 2.1
                public class DictionaryType2 : Dictionary
                {
                    [JsonProperty("Id")]
                    public string Id { get; set; }

                    [JsonProperty("Code")]
                    public string Code { get; set; }

                    [JsonProperty("ShortName")]
                    public string ShortName { get; set; }

                    [JsonProperty("FullName")]
                    public string FullName { get; set; }

                    [JsonProperty("Employees")]
                    public string EmployeeId { get; set; }

                    [JsonProperty("IsFolder")]
                    public int IsFolder { get; set; }

                    [JsonProperty("ParentId")]
                    public string ParentId { get; set; }
                }

                // 2.2
                public class DictionaryType3 : Dictionary
                {
                    [JsonProperty("Id")]
                    public string Id { get; set; }

                    [JsonProperty("Code")]
                    public string Code { get; set; }

                    [JsonProperty("ShortName")]
                    public string ShortName { get; set; }

                    [JsonProperty("FullName")]
                    public string FullName { get; set; }

                    [JsonProperty("Position")]
                    public string Position { get; set; }

                    [JsonProperty("Email")]
                    public string Email { get; set; }

                    [JsonProperty("Phone")]
                    public string PhoneNo { get; set; }

                    [JsonProperty("IsFolder")]
                    public int IsFolder { get; set; }

                    [JsonProperty("ParentId")]
                    public string ParentId { get; set; }
                }

                public class DictionaryType4 : Dictionary
                {
                    [JsonProperty("Id")]
                    public string Id { get; set; }

                    [JsonProperty("Code")]
                    public string Code { get; set; }

                    [JsonProperty("Name")]
                    public string Name { get; set; }

                    [JsonProperty("IsFolder")]
                    public int IsFolder { get; set; }

                    [JsonProperty("ParentId")]
                    public string ParentId { get; set; }
                }

                public class DictionaryType5 : Dictionary
                {
                    [JsonProperty("Id")]
                    public string Id { get; set; }

                    [JsonProperty("Code")]
                    public string Code { get; set; }

                    [JsonProperty("ShortName")]
                    public string ShortName { get; set; }

                    [JsonProperty("FullName")]
                    public string FullName { get; set; }

                    [JsonProperty("IsFolder")]
                    public int IsFolder { get; set; }

                    [JsonProperty("ParentId")]
                    public string ParentId { get; set; }
                }

                public class DictionaryType6 : Dictionary
                {
                    [JsonProperty("Id")]
                    public string Id { get; set; }

                    [JsonProperty("Code")]
                    public string Code { get; set; }

                    [JsonProperty("Brand")]
                    public string Brand { get; set; }

                    [JsonProperty("Model")]
                    public string Model { get; set; }

                    [JsonProperty("Kind")]
                    public string TypeCode { get; set; }

                    [JsonProperty("RegistrationNumber")]
                    public string RegistrationNo { get; set; }

                    [JsonProperty("IsFolder")]
                    public int IsFolder { get; set; }

                    [JsonProperty("ParentId")]
                    public string ParentId { get; set; }
                }

                public class DictionaryType7 : Dictionary
                {
                    [JsonProperty("Id")]
                    public string Id { get; set; }

                    [JsonProperty("Code")]
                    public string Code { get; set; }

                    [JsonProperty("Name")]
                    public string Name { get; set; }

                    [JsonProperty("StartDateTime")]
                    public DateTime? StartDateTime { get; set; }

                    [JsonProperty("StopDateTime")]
                    public DateTime? StopDateTime { get; set; }

                    [JsonProperty("ManagerId")]
                    public string ManagerId { get; set; }
                }
            }
        }
    }
}
using System;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.OneC
{
    public partial class OneCApiClient
    {
        public static class GetDictionaryValueDto
        {
            public class Request
            {
                [JsonProperty("Dictionary")]
                public virtual string Dictionary { get; set; }

                [JsonProperty("ItemId")]
                public Guid ItemId { get; set; }
            }

            public static class Response
            {
                public class Dictionary : OneC.OneCApiClient.BaseResponseDto
                {
                }

                public class DictionaryType1 : Dictionary
                {
                    [JsonProperty("Id")]
                    public Guid Id { get; set; }

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
                    public Guid? ParentId { get; set; }

                    [JsonProperty("CarrierDriverId")]
                    public Guid? CarrierDriverId { get; set; }

                    [JsonProperty("CustomerId")]
                    public Guid? CustomerId { get; set; }
                }

                // 2.1
                public class DictionaryType2 : Dictionary
                {
                    [JsonProperty("Id")]
                    public Guid Id { get; set; }

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
                    public Guid? ParentId { get; set; }
                }

                // 2.2
                public class DictionaryType3 : Dictionary
                {
                    [JsonProperty("Id")]
                    public Guid Id { get; set; }

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
                    public Guid? ParentId { get; set; }
                }

                public class DictionaryType4 : Dictionary
                {
                    [JsonProperty("Id")]
                    public Guid Id { get; set; }

                    [JsonProperty("Code")]
                    public string Code { get; set; }

                    [JsonProperty("Name")]
                    public string Name { get; set; }

                    [JsonProperty("IsFolder")]
                    public int IsFolder { get; set; }

                    [JsonProperty("ParentId")]
                    public Guid? ParentId { get; set; }
                }

                public class DictionaryType5 : Dictionary
                {
                    [JsonProperty("Id")]
                    public Guid Id { get; set; }

                    [JsonProperty("Code")]
                    public string Code { get; set; }

                    [JsonProperty("ShortName")]
                    public string ShortName { get; set; }

                    [JsonProperty("FullName")]
                    public string FullName { get; set; }

                    [JsonProperty("IsFolder")]
                    public int IsFolder { get; set; }

                    [JsonProperty("ParentId")]
                    public Guid? ParentId { get; set; }
                }

                public class DictionaryType6 : Dictionary
                {
                    [JsonProperty("Id")]
                    public Guid Id { get; set; }

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
                    public Guid? ParentId { get; set; }
                }

                public class DictionaryType7 : Dictionary
                {
                    [JsonProperty("Id")]
                    public Guid Id { get; set; }

                    [JsonProperty("Code")]
                    public string Code { get; set; }

                    [JsonProperty("Name")]
                    public string Name { get; set; }

                    [JsonProperty("StartDateTime")]
                    public DateTime? StartDateTime { get; set; }

                    [JsonProperty("StopDateTime")]
                    public DateTime? StopDateTime { get; set; }

                    [JsonProperty("ManagerId")]
                    public Guid? ManagerId { get; set; }
                }
            }
        }
    }
}
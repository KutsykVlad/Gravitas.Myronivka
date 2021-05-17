using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.ApiClient.SmsMobizon
{
    public partial class SmsMobizonApiClient
    {
        public static class GetMessageStatusDto
        {
            public class Response
            {
                [JsonProperty("code")]
                public int Code { get; set; }

                [JsonProperty("data")]
                public Data[] DataArray { get; set; }

                [JsonProperty("message")]
                public string Message { get; set; }

                public static class MessageStatus
                {
                    public static class New
                    {
                        public const string Code = "NEW";
                        public const bool IsFinal = false;
                    }

                    public static class Enqueued
                    {
                        public const string Code = "ENQUEUD";
                        public const bool IsFinal = false;
                    }

                    public static class Accepted
                    {
                        public const string Code = "ACCEPTD";
                        public const bool IsFinal = false;
                    }

                    public static class Undelivered
                    {
                        public const string Code = "UNDELIV";
                        public const bool IsFinal = true;
                    }

                    public static class Rejected
                    {
                        public const string Code = "REJECTD";
                        public const bool IsFinal = true;
                    }

                    public static class PardialDlivered
                    {
                        public const string Code = "PDLIVRD";
                        public const bool IsFinal = false;
                    }

                    public static class Delivered
                    {
                        public const string Code = "DELIVRD";
                        public const bool IsFinal = true;
                    }

                    public static class Expired
                    {
                        public const string Code = "EXPIRED";
                        public const bool IsFinal = true;
                    }

                    public static class Deleted
                    {
                        public const string Code = "DELETED";
                        public const bool IsFinal = true;
                    }
                }

                public class Data
                {
                    [JsonProperty("id")]
                    public int Id { get; set; }

                    [JsonProperty("status")]
                    public string Status { get; set; }

                    [JsonProperty("segCnt")]
                    public int SegCnt { get; set; }

                    [JsonProperty("startSendTs")]
                    public string StartSendTs { get; set; }

                    [JsonProperty("statusUpdateTs")]
                    public string StatusUpdateTs { get; set; }
                }
            }
        }
    }
}
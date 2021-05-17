using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.RouteEditor;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.Manager.Routes
{
    public static class Serialize
    {
        public static string SerializeRoute(this RouteJsonConverter.Route self)
        {
            return JsonConvert.SerializeObject(self);
        }
    }

    public static class Deserialize
    {
        public static RouteJsonConverter.Route DeserializeRoute(this string json)
        {
            return JsonConvert.DeserializeObject<RouteJsonConverter.Route>(json);
        }
    }

    public class RouteJsonConverter
    {
        public class Route
        {
            [JsonProperty("disableAppend")]
            public bool DisableAppend { get; set; }

            [JsonProperty("groupDictionary")]
            public Dictionary<string, GroupDictionary> GroupDictionary { get; set; }
        }

        public class GroupDictionary
        {
            [JsonProperty("groupId")]
            public long GroupId { get; set; }

            [JsonProperty("nodeList")]
            public NodeList[] NodeList { get; set; }

            [JsonProperty("active")]
            public bool Active { get; set; }

            [JsonProperty("quotaEnabled")]
            public bool QuotaEnabled { get; set; }
        }

        public class NodeList
        {
            [JsonProperty("id")]
            public long Id { get; set; }
            
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("nodeGroupId")]
            public long NodeGroupId { get; set; }
            
            [JsonProperty("reloadRoute")]
            public SecondaryRoute ReloadRoute { get; set; }
            
            [JsonProperty("partLoadRoute")]
            public SecondaryRoute PartLoadRoute { get; set; }
            
            [JsonProperty("partUnloadRoute")]
            public SecondaryRoute PartUnloadRoute { get; set; }
            
            [JsonProperty("moveRoute")] 
            public SecondaryRoute MoveRoute { get; set; }
            
            [JsonProperty("rejectRoute")] 
            public SecondaryRoute RejectRoute { get; set; }
            
            [JsonProperty("processId")]
            public int ProcessId { get; set; }
        }
    }
}
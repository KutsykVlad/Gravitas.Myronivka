using Newtonsoft.Json;

namespace Gravitas.Model
{
    public class BaseJsonConverter<T> where T : class
    {
        public static T FromJson(string jsonData)
        {
            T result = null;
            try
            {
                result = JsonConvert.DeserializeObject<T>(jsonData);
            }
            catch
            {
                /* ignore */
            }

            return result;
        }

        public static string ToJson(T objData)
        {
            return objData == null
                ? string.Empty
                : JsonConvert.SerializeObject(objData);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
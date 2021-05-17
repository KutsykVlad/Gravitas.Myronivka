using System.IO;
using System.Net;
using System.Text;
using Gravitas.Infrastrructure.Common;
using VkModuleRelayStateXml = Gravitas.Core.Manager.VkModuleSocket1.VkModuleRelayStateXml;

namespace Gravitas.Core.Manager.VkModuleRelay
{
    public static class VkModuleRelayHelper
    {
        private static string GetWebResponse(HttpWebRequest request)
        {
            string responseStr = null;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                {
                    Stream objStream = response.GetResponseStream();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var stream = new MemoryStream())
                        {
                            byte[] buffer = new byte[2048]; // read in chunks of 2KB
                            int bytesRead;
                            while ((bytesRead = objStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                stream.Write(buffer, 0, bytesRead);
                            }

                            responseStr = Encoding.ASCII.GetString(stream.ToArray());
                        }
                    }
                }
            }
            catch
            {
                /*ignore*/
            }

            return responseStr;
        }

        public static T GetVkModuleRelayState<T>(IPAddress host, string user, string pass)
            where T : VkModuleRelayStateXml, new()
        {
            T state = new T();

            var request = (HttpWebRequest) WebRequest.Create($"http://{host}/protect/status.xml");
            request.Credentials = new NetworkCredential(user, pass);
            request.Timeout = 5000;

            try
            {
                string responseStr = GetWebResponse(request);
                state = SerializationHelper.DeserializeFromString<T>(responseStr);
                state.ErrCode = 0;
            }
            catch
            {
                /*ignore*/
            }

            return state;
        }

        public static T ChangeVkModuleRelayState<T>(IPAddress host, string user, string pass, int outNo, int timeout = 0)
            where T : VkModuleRelayStateXml, new()
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create($"http://{host}/protect/leds.cgi?led={outNo}&timeout={timeout}");
            request.Credentials = new NetworkCredential(user, pass);
            request.Timeout = 5000;

            GetWebResponse(request);

            T state = GetVkModuleRelayState<T>(host, user, pass);
            return state;
        }
    }
}
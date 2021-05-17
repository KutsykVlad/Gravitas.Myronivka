using System.ComponentModel;
using System.Configuration;

namespace Gravitas.Infrastructure.Common.Configuration
{
    public class GlobalConfigurationManager
    {
        public static string SignalRHost => Read("SignalRHost");
        public static string OneCApiHost => Read("OneCApiHost");
        public static string OneCApiLogin => Read("OneCApiLogin");
        public static string OneCApiPassword => Read("OneCApiPassword");
        public static string CollisionsReceiveHost => Read("CollisionsReceiveHost");
        public static string CollisionsConfirmHost => Read("CollisionsConfirmHost");
        public static string AdminPassword => Read("AdminPassword");
        public static string SmsApiHost => Read("SmsApiHost");
        public static string SmsApiToken => Read("SmsApiToken");
        public static int TrailerDisplayTime => Read<int>("TrailerDisplayTime");
        public static int MaximumGrossWeightDifference => Read<int>("MaximumGrossWeightDifference");
        public static int WeighbridgeCheckScaleReloadTime => Read<int>("WeighbridgeCheckScaleReloadTime");
        public static int WeighbridgeCheckScaleEmptyTimeout => Read<int>("WeighbridgeCheckScaleEmptyTimeout");
        public static int DeviceMonitorDelay => Read<int>("DeviceMonitorDelay");
        public static int ScaleMinLoad => Read<int>("ScaleMinLoad");
        public static double MaximumScaleWeight => Read<double>("MaximumScaleWeight");
        public static int NonStandartPassTimeout => Read<int>("NonStandartPassTimeout");
        public static string CameraImageBasePath => Read("CameraImageBasePath");
        public static string CameraImageNamePrefix => Read("CameraImageNamePrefix");
        public static string TicketFilesPath => Read("TicketFilesPath");
        public static string TicketFilesPrefix => Read("TicketFilesPrefix");
        public static string Test => Read("test");
        public static string SchemeHttp => Read("SchemeHttp");
        public static string SchemeHttps => Read("SchemeHttps");
        public static string JsonContentType => Read("SchemeHttps");
        public static string XmlContentType => Read("SchemeHttps");
        public static string PdfContentType => Read("SchemeHttps");
        public static string ExcelContentType => Read("SchemeHttps");
        public static string EmailHost => Read("EmailHost");
        public static string RootEmail => Read("RootEmail");
        public static string RootEmailLogin => Read("RootEmailLogin");
        public static string RootEmailPassword => Read("RootEmailPassword");
        public static string RootEmailDestination => Read("RootEmailDestination");
        public static string ConnectApiHost => Read("ConnectApiHost");

        private static string Read(string key) => ConfigurationManager.AppSettings[key];

        private static T Read<T>(string key)
        {
            var paramStr = ConfigurationManager.AppSettings[key];

            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T) converter.ConvertFromString(paramStr);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
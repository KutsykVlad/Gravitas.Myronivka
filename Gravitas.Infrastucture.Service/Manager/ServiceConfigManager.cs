using System.Configuration;
using System.Collections.Specialized;

namespace Gravitas.Infrastucture.Service.Manager
{
	public static class ServiceConfigManager
	{

		private static string _serviceName;
		private static string _serviceDisplayName;
		private static string _serviceDescription;
		private static int _maxBatchSize;
		private static int _batchIntervalInSeconds;
		private static int _processStarterCooldownMs;

		public static NameValueCollection AppSettings { get; set; }

		static ServiceConfigManager() {
			AppSettings = new NameValueCollection(ConfigurationManager.AppSettings);

			_serviceName = AppSettings["ServiceName"];
			_serviceDisplayName = AppSettings["ServiceDisplayName"];
			_serviceDescription = AppSettings["ServiceDescription"];

			if (!int.TryParse(AppSettings["MaxBatchSize"], out _maxBatchSize))
				_maxBatchSize = 50;

			if (!int.TryParse(AppSettings["BatchIntervalInSeconds"], out _batchIntervalInSeconds))
				_batchIntervalInSeconds = 30;

			if (!int.TryParse(AppSettings["ProcessStarterCooldownMs"], out _processStarterCooldownMs))
				_processStarterCooldownMs = 0;
		}

		public static string ServiceName => _serviceName;

		public static string ServiceDisplayName => _serviceDisplayName;

		public static string ServiceDescription => _serviceDescription;

		public static int MaxBatchSize => _maxBatchSize;

		public static int BatchIntervalInSeconds => _batchIntervalInSeconds;

		public static int ProcessStarterCooldownMs => _processStarterCooldownMs;

		public static void LoadConfiguration(Configuration config)
		{
			_serviceName = config.AppSettings.Settings["ServiceName"].Value;
			_serviceDisplayName = config.AppSettings.Settings["ServiceDisplayName"].Value;
			_serviceDescription = config.AppSettings.Settings["ServiceDescription"].Value;
		}
	}
}

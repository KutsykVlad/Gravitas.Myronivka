using Gravitas.Infrastructure.Common.Configuration;

namespace Gravitas.Model
{
	public static partial class Dom
	{
		public static class Common {

			public class ContentType {

				public static string JsonContentType = GlobalConfigurationManager.JsonContentType;
				public static string XmlContentType = GlobalConfigurationManager.XmlContentType;
				public static string PdfContentType = GlobalConfigurationManager.PdfContentType;
				public static string ExcelContentType = GlobalConfigurationManager.ExcelContentType;
			}

			public class UriScheme {

				public static string SchemeHttp = GlobalConfigurationManager.SchemeHttp;
				public static string SchemeHttps = GlobalConfigurationManager.SchemeHttps;
			}
		}
	}
}



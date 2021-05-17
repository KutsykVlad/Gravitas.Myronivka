using System.Xml.Serialization;

namespace Gravitas.DeviceSync.Manager {
	[XmlRoot(ElementName = "response")]
	public class VkModule2In2OutStateXml : VkModuleRelayStateXml
	{

		//public static class ErrorCode {
		//	public static int Ok = 0;
		//	public static int Error = 1;
		//}

		//[XmlIgnore]
		//public int ErrCode { get; set; } = ErrorCode.Error;

		[XmlIgnore]
		public bool Out0 { get; set; }
		[XmlIgnore]
		public bool Out1 { get; set; }

		[XmlIgnore]
		public bool In0 { get; set; }
		[XmlIgnore]
		public bool In1 { get; set; }

		#region Xml Searilisation Properties
		[XmlElement(ElementName = "led0")]
		public string Out0Name
		{
			get => Out0 ? "1" : "0";
			set => Out0 = value.Trim().ToLowerInvariant() == "1";
		}
		[XmlElement(ElementName = "led1")]
		public string Out1Name
		{
			get => Out1 ? "1" : "0";
			set => Out1 = value.Trim().ToLowerInvariant() == "1";
		}
		[XmlElement(ElementName = "btn0")]
		public string In0Name
		{
			get => In0 ? "dn" : "up";
			set => In0 = value.Trim().ToLowerInvariant() == "dn";
		}
		[XmlElement(ElementName = "btn1")]
		public string In1Name
		{
			get => In1 ? "dn" : "up";
			set => In1 = value.Trim().ToLowerInvariant() == "dn";
		}
		#endregion
	}
}


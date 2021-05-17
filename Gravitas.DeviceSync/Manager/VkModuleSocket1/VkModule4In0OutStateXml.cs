using System.Xml.Serialization;

namespace Gravitas.DeviceSync.Manager {

	public class VkModuleRelayStateXml {

		public static class ErrorCode {
			public static int Ok = 0;
			public static int Error = 1;
		}

		[XmlIgnore]
		public int ErrCode { get; set; } = ErrorCode.Error;

		public VkModuleRelayStateXml() { }

		protected string GetInName(bool value) {
			return value ? "dn" : "up";
		}

		protected bool GetInValue(string name) {
			return name.Trim().ToLowerInvariant() == "dn";
		}
	}

	[XmlRoot(ElementName = "response")]
	public class VkModule4In0OutStateXml : VkModuleRelayStateXml {
		
		[XmlIgnore]
		public bool In0 { get; set; }
		[XmlIgnore]
		public bool In1 { get; set; }
		[XmlIgnore]
		public bool In2 { get; set; }
		[XmlIgnore]
		public bool In3 { get; set; }

		#region Xml Searilisation Properties
		[XmlElement(ElementName = "btn0")]
		public string In0Name {
			get => GetInName(In0);
			set => In0 = GetInValue(name: value);
		}
		[XmlElement(ElementName = "btn1")]
		public string In1Name {
			get => GetInName(In1);
			set => In1 = GetInValue(name: value);
		}
		[XmlElement(ElementName = "btn2")]
		public string In2Name {
			get => GetInName(In2);
			set => In2 = GetInValue(name: value);
		}
		[XmlElement(ElementName = "btn3")]
		public string In3Name {
			get => GetInName(In3);
			set => In3 = GetInValue(name: value);
		}

		public VkModule4In0OutStateXml() {

		}
		#endregion
	}
}


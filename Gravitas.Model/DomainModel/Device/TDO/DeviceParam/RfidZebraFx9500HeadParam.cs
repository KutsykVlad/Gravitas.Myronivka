namespace Gravitas.Model {

	public class RfidZebraFx9500HeadParam : BaseJsonConverter<RfidZebraFx9500HeadParam>
	{

		public string IpAddress { get; set; }
		public int IpPort { get; set; }
		public int Timeout { get; set; }
	}
}

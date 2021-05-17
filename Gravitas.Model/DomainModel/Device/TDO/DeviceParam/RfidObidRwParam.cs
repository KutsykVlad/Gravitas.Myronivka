namespace Gravitas.Model {

	public class RfidObidRwParam : BaseJsonConverter<RfidObidRwParam> {

		public string IpAddress { get; set; }
		public int Port { get; set; }
	}
}

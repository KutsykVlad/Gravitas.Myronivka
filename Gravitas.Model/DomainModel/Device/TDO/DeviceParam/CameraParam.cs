namespace Gravitas.Model {

	public class CameraParam : BaseJsonConverter<CameraParam> {

		public string IpAddress { get; set; }
		public int Port { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
	}
}

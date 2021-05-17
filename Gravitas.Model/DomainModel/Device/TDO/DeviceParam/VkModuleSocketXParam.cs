namespace Gravitas.Model {

	public class VkModuleSocketXParam : BaseJsonConverter<VkModuleSocketXParam> {

		public string IpAddress { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public int Timeout { get; set; }
	}
}

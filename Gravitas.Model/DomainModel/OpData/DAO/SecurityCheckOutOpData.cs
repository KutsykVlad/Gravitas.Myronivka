namespace Gravitas.Model {

	public  class SecurityCheckOutOpData : BaseOpData {

		public bool? IsCameraImagesConfirmed { get; set; }

		public int? SealCount { get; set; }
		public string SealList { get; set; }
	}
}

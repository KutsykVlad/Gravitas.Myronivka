using Gravitas.Model;

namespace Gravitas.Platform.Web.ViewModel {

	public class NodeItemVm : BaseEntityVm<long> {

		public string Name { get; set; }
		public bool IsStart { get; set; }
		public bool IsFinish { get; set; }
	}
}

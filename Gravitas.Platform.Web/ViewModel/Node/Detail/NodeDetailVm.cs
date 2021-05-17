using Gravitas.Model;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Node.TDO.Json;

namespace Gravitas.Platform.Web.ViewModel 
{
	public class NodeDetailVm : BaseEntity<int>
	{
		public long? OrganisationUnitId { get; set; }
		public long? OpRoutineId { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public bool IsActive { get; set; }
		public bool IsStart { get; set; }
		public bool IsFinish { get; set; }
		public int Quota { get; set; }
		public NodeConfig Config { get; set; }
		public NodeContext Context { get; set; }
		public string ProcessingMessage { get; set; }
	}
}

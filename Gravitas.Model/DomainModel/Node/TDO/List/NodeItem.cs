using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.Dto {

	public class NodeItem : BaseEntity<int> {

		public string Code { get; set; }
		public string Name { get; set; }

		public long? OpRoutineId { get; set; }
		public string OpRoutineName { get; set; }
	}
}

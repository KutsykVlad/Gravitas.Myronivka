using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model {

	[Table("dev.NodeConstraint")]
	public partial class NodeConstraint : BaseEntity<long> {
		
		public long NodeId { get; set; }
		public long? NomenclatureId { get; set; }

		//public virtual Nomenclature NomenclatureSet { get; set; }
		public virtual Node Node { get; set; }
	}
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravitas.Model {

	public partial class OpRoutine : BaseEntity<long> {

		public OpRoutine() {
			OpRoutineStateSet = new HashSet<OpRoutineState>();
			NodeSet = new HashSet<Node>();
		}

		public string Name { get; set; }
		public string Description { get; set; }
		public int StateCount { get; set; }

		// Navigation properties
		public virtual ICollection<OpRoutineState> OpRoutineStateSet { get; set; }
		public virtual ICollection<Node> NodeSet { get; set; }
		
	}
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model {
	
	public partial class OpRoutineProcessor : BaseEntity<int> {

		public OpRoutineProcessor() {
			OpRoutineTransitionSet = new HashSet<OpRoutineTransition>();
		}
		
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		// Navigation properties
		public virtual ICollection<OpRoutineTransition> OpRoutineTransitionSet { get; set; }
	}
}

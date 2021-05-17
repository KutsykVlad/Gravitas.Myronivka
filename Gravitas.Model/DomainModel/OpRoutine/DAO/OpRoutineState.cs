using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model {
	
	public class OpRoutineState : BaseEntity<int> {

		public OpRoutineState() {
			OpRoutineTransitionStartSet = new HashSet<OpRoutineTransition>();
			OpRoutineTransitionStopSet = new HashSet<OpRoutineTransition>();
            OpVisaSet = new HashSet<OpVisa>();
		}
		
		public long OpRoutineId { get; set; }
		public int StateNo { get; set; }
		public string Name { get; set; }

		// Navigation Properties
		public virtual OpRoutine OpRoutine { get; set; }
		public virtual ICollection<OpRoutineTransition> OpRoutineTransitionStartSet { get; set; }
		public virtual ICollection<OpRoutineTransition> OpRoutineTransitionStopSet { get; set; }
        public virtual ICollection<OpVisa> OpVisaSet { get; set; }
	}
}

namespace Gravitas.Model {
	
	public partial class OpRoutineTransition : BaseEntity<long> {

		public long OpRoutineId { get; set; }
		public string Name { get; set; }
		public long? StartStateId { get; set; }
		public long? StopStateId { get; set; }
		public long? ProcessorId { get; set; }

		// Navigation properties
		public virtual OpRoutineState OpRoutineStateStart { get; set; }
		public virtual OpRoutineState OpRoutineStateStop { get; set; }
		public virtual OpRoutineProcessor OpRoutineProcessor { get; set; }
	}
}

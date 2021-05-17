using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.Dto
{
    public class Node : BaseEntity<int>
    {
        public long? OrganisationUnitId { get; set; }
        public long? OpRoutineId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long MaximumProcessingTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsStart { get; set; }
        public bool IsFinish { get; set; }
        public bool IsEmergency { get; set; }
        public int Quota { get; set; }
        public NodeConfig Config { get; set; }
        public NodeContext Context { get; set; }
        public NodeProcessingMsg ProcessingMessage { get; set; }
        public NodeGroup Group { get; set; }
    }
}
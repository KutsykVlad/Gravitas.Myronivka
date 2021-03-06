using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainValue;

namespace Gravitas.Model.DomainModel.Node.TDO.Detail
{
    public class NodeDetails : BaseEntity<int>
    {
        public int? OrganisationUnitId { get; set; }
        public int OpRoutineId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int MaximumProcessingTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmergency { get; set; }
        public int Quota { get; set; }
        public NodeConfig Config { get; set; }
        public NodeContext Context { get; set; }
        public NodeGroup Group { get; set; }
    }
}
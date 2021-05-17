using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Node.TDO.List
{
    public class NodeItem : BaseEntity<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int? OpRoutineId { get; set; }
        public string OpRoutineName { get; set; }
    }
}
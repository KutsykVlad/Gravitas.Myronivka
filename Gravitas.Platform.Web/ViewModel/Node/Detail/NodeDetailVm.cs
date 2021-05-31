using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.Node.TDO.Json;

namespace Gravitas.Platform.Web.ViewModel.Node.Detail
{
    public class NodeDetailVm : BaseEntity<int>
    {
        public int? OrganisationUnitId { get; set; }
        public int? OpRoutineId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public int Quota { get; set; }
        public NodeConfig Config { get; set; }
        public NodeContext Context { get; set; }
    }
}
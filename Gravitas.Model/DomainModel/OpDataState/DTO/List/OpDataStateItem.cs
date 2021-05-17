using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.OpDataState.DTO.List
{
    public class OpDataStateItem : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
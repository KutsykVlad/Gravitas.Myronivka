using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.OpDataState.DTO.Detail
{
    public class OpDataStateDetail : BaseEntity<DomainValue.OpDataState>
    {
        public string Name { get; set; }
    }
}
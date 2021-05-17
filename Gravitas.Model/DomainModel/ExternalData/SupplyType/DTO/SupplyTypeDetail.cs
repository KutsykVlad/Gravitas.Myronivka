using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.SupplyType.DTO
{
    public class SupplyTypeDetail : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}
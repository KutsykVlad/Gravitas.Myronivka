using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.SupplyTransportType.DTO.Detail
{
    public class SupplyTransportTypeDetail : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}
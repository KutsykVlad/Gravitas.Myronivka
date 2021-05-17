using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.SupplyTransportType.DTO.List
{
    public class SupplyTransportTypeItem : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}
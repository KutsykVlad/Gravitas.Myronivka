using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.SupplyTransportType.DAO
{
    public class SupplyTransportType : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.SupplyType.DAO
{
    public class SupplyType : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Organization.DAO
{
    public class OriginType : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}
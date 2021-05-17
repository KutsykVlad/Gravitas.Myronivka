using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Organization.DTO.Detail
{
    public class OriginTypeDetail : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}
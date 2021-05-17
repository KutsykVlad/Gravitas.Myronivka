using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Organization.DTO.List
{
    public class OriginTypeItem : BaseEntity<string>
    {
        public string Name { get; set; }
    }
}
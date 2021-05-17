using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Organization.DTO.List
{
    public class OriginTypeItems : BaseEntity<string>
    {
        public IEnumerable<OriginTypeItem> Items { get; set; }
    }
}
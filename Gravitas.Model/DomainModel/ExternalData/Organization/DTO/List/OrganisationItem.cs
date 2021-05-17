using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Organization.DTO.List
{
    public class OrganisationItems : BaseEntity<string>
    {
        public IEnumerable<OrganisationItem> Items { get; set; }
    }
}
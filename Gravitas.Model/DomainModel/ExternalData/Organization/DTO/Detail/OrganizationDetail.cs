using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Organization.DTO.Detail
{
    public class OrganisationDetail : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}
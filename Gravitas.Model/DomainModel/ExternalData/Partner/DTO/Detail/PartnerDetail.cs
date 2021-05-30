using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Partner.DTO.Detail
{
    public class PartnerDetail : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}
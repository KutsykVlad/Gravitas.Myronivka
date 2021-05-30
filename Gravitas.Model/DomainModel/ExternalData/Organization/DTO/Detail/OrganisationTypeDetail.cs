using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Organization.DTO.Detail
{
    public class OriginTypeDetail : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
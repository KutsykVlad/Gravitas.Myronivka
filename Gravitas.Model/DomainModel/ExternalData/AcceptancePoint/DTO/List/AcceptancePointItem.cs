using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DTO.List
{
    public class AcceptancePointItem : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
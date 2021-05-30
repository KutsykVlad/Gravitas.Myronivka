using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.SupplyTransportType.DTO.List
{
    public class SupplyTransportTypeItem : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
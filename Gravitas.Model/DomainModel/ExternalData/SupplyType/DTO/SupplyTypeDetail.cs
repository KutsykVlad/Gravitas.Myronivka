using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.SupplyType.DTO
{
    public class SupplyTypeDetail : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
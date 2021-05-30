using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.SupplyTransportType.DAO
{
    public class SupplyTransportType : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
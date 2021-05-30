using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.SupplyType.DAO
{
    public class SupplyType : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO
{
    public class AcceptancePoint : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsFolder { get; set; }
        public Guid? ParentId { get; set; }
    }
}
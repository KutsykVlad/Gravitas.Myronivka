using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Route.DAO
{
    public class Route : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsFolder { get; set; }
        public Guid? ParentId { get; set; }
    }
}
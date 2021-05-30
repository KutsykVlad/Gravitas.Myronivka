using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Route.DTO
{
    public class RouteDetail : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Contract.DAO
{
    public class Contract : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? StopDateTime { get; set; }
        public Guid? ManagerId { get; set; }
    }
}
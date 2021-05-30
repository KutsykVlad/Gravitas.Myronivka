using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Organization.DAO
{
    public class Organisation : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

        public bool IsFolder { get; set; }
        public Guid? ParentId { get; set; }
    }
}
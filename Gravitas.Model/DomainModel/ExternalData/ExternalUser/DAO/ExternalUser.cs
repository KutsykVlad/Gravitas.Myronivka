using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.ExternalUser.DAO
{
    public class ExternalUser : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string EmployeeId { get; set; }
        public bool IsFolder { get; set; }
        public Guid? ParentId { get; set; }
    }
}
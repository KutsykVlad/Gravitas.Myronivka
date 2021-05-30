using System;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.ExternalData.Employee.DTO.List
{
    public class EmployeeItem : BaseEntity<Guid>
    {
        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public bool IsFolder { get; set; }
    }
}
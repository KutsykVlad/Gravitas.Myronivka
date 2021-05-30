using System;
using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.EmployeeRoles.DAO;

namespace Gravitas.Model.DomainModel.ExternalData.Employee.DAO
{
    public class Employee : BaseEntity<Guid>
    {
        public Employee()
        {
            CardSet = new HashSet<Card.DAO.Card>();
            OpVisaSet = new HashSet<OpVisa.DAO.OpVisa>();
            EmployeeRoles = new HashSet<EmployeeRole>();
        }

        public string Code { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public bool IsFolder { get; set; }
        public Guid? ParentId { get; set; }

        public virtual ICollection<Card.DAO.Card> CardSet { get; set; }
        public virtual ICollection<OpVisa.DAO.OpVisa> OpVisaSet { get; set; }
        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
    }
}
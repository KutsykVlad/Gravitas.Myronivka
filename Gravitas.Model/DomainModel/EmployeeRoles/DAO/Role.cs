using System.Collections.Generic;
using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.EmployeeRoles.DAO
{
    public class Role: BaseEntity<int>
    {
        public Role()
        {
            Assignments = new HashSet<RoleAssignment>();
            EmployeeRoles = new HashSet<EmployeeRole>();
        }

        public string Name { get; set; }

        public virtual ICollection<RoleAssignment> Assignments { get; set; }
        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
    }
}

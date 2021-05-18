using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.ExternalData.Employee.DAO;

namespace Gravitas.Model.DomainModel.EmployeeRoles.DAO
{
    public class EmployeeRole: BaseEntity<int>
    {
        public string EmployeeId { get; set; }
        public int RoleId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}

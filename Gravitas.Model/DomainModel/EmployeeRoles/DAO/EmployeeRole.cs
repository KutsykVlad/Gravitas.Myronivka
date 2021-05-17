using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.EmployeeRoles.DAO
{
    public class EmployeeRole: BaseEntity<int>
    {
        public string EmployeeId { get; set; }
        public int RoleId { get; set; }

        public virtual ExternalData.AcceptancePoint.DAO.ExternalData.Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}

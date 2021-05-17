using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.EmployeeRoles.DAO
{
    public class RoleAssignment: BaseEntity<int>
    {
        public int RoleId { get; set; }
        public int NodeId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Node.DAO.Node Node { get; set; }

    }
}

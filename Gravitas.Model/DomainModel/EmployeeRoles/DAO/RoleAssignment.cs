using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model
{
    public class RoleAssignment: BaseEntity<int>
    {
        public long RoleId { get; set; }
        public long NodeId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Node Node { get; set; }

    }
}

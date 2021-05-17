namespace Gravitas.Model
{
    public class RoleAssignment: BaseEntity<long>
    {
        public long RoleId { get; set; }
        public long NodeId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Node Node { get; set; }

    }
}

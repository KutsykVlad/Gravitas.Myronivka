using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model
{
    public class EmployeeRole: BaseEntity<int>
    {
        public string EmployeeId { get; set; }
        public long RoleId { get; set; }

        public virtual ExternalData.Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}

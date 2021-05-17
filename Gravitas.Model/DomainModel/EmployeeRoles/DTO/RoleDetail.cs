using System.Collections.Generic;

namespace Gravitas.Model.DomainModel.EmployeeRoles.DTO
{
    public class RoleDetail
    {
        public RoleDetail()
        {
            Nodes = new List<int>();
        }

        public int RoleId { get; set; }

        public string Name { get; set; }

        public List<int> Nodes { get; set; }
    }
}

using System.Collections.Generic;

namespace Gravitas.Model.Dto
{
    public class RoleDetail
    {
        public RoleDetail()
        {
            Nodes = new List<long>();
        }

        public long RoleId { get; set; }

        public string Name { get; set; }

        public List<long> Nodes { get; set; }
    }
}

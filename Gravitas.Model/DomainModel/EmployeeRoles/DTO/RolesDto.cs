using System.Collections.Generic;

namespace Gravitas.Model.Dto
{
    public class RolesDto
    {
        public RolesDto()
        {
            Items = new List<RoleDetail>();
        }

        public List<RoleDetail> Items { get; set; }

        public int Count { get; set; }
    }
}

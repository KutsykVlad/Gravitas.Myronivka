using System.Collections.Generic;
using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel
{
    public class RoleVm
    {
        [DisplayName("Ідентифікатор ролі")]
        public long RoleId { get; set; }
        [DisplayName("Назва ролі")]
        public string RoleName { get; set; }

        public List<AssignmentVm> Assignments { get; set; } = new List<AssignmentVm>();
    }
}
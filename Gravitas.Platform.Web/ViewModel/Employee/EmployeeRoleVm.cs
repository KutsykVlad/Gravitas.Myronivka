using System.ComponentModel;

namespace Gravitas.Platform.Web.ViewModel.Employee
{
    public class EmployeeRoleVm
    {

        [DisplayName("Ідентифікатор ролі")]
        public long RoleId { get; set; }
        [DisplayName("Назва ролі")]
        public string RoleName { get; set; }

        public bool IsApplied { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using Gravitas.Platform.Web.ViewModel.User;

namespace Gravitas.Platform.Web.ViewModel.Employee
{
    public class EmployeeDetailsVm
    {
        public string Id { get; set; }

        [DisplayName("Ім'я")]
        public string ShortName { get; set; }

        [DisplayName("Повне ім'я")]
        public string FullName { get; set; }

        [DisplayName("Прив'язані картки")]
        public ICollection<string> CardIds { get; set; }

        public List<string> SelectedCards { get; set; } = new List<string>();

        [DisplayName("Прив'язані ролі")]
        public List<EmployeeRoleVm> Roles { get; set; } = new List<EmployeeRoleVm>();
        
        public CardProcessingMsgVm CardProcessingMsgError { get; set; }

        public int ReturnPage { get; set; }
    }
}
using System.Collections.Generic;
using Gravitas.Model;

namespace Gravitas.Platform.Web.ViewModel.Employee
{
    public class EmployeeListVm
    {
        public IEnumerable<EmployeeDetailsVm> Items { get; set; }

        public int PrevPage { get; set; }
        public int NextPage { get; set; }
        public int ItemsCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<Role> Roles { get; set; }
    }
}
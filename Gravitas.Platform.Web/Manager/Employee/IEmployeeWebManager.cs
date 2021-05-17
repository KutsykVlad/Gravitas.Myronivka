using System.Collections.Generic;
using Gravitas.Platform.Web.ViewModel.Employee;

namespace Gravitas.Platform.Web.Manager.Employee
{
    public interface IEmployeeWebManager
    {
        EmployeeListVm GetEmployeeList(string name = "", int pageNumber = 1, int pageSize = 25, int? roleId = null);
        EmployeeDetailsVm GetEmployeeDetails(string id);
        void UpdateEmployeeRoles(EmployeeDetailsVm employee);
        (bool, string) AssignCardToEmployee(string employeeId);
        (bool, string) UnAssignCardsFromEmployee(ICollection<string> cards);
    }
}

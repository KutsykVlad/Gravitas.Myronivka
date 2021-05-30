using System;
using System.Web.Mvc;
using Gravitas.Model;
using Gravitas.Platform.Web.Manager.Employee;
using Gravitas.Platform.Web.ViewModel.Employee;
using Gravitas.Platform.Web.ViewModel.User;

namespace Gravitas.Platform.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeWebManager _employeeWebManager;

        public EmployeeController(IEmployeeWebManager employeeWebManager)
        {
            _employeeWebManager = employeeWebManager;
        }

        public ActionResult List(int? page, string name, int? roleId)
        {
            return PartialView("EmployeeList", _employeeWebManager.GetEmployeeList(name, page ?? 1, 25, roleId));
        }

        public ActionResult Details(Guid employeeId, int errorType = 0, string errorMessage = "", int returnPage = 1)
        {
            var employeeDetailsVm = _employeeWebManager.GetEmployeeDetails(employeeId);
            employeeDetailsVm.ReturnPage = returnPage;

            if (errorType != 0)
                employeeDetailsVm.CardProcessingMsgError = new CardProcessingMsgVm
                    { Text = errorMessage, Time = DateTime.Now, TypeId = errorType };

            return PartialView("EmployeeDetails", employeeDetailsVm);
        }

        public ActionResult AssignCardToEmployee(Guid employeeId, int returnPage)
        {
            var (isSuccess, msg) = _employeeWebManager.AssignCardToEmployee(employeeId);

            return RedirectToAction("Details", new
            {
                employeeId,
                errorType = isSuccess
                    ? ProcessingMsgType.Success
                    : ProcessingMsgType.Error,
                errorMessage = msg,
                returnPage
            });
        }

        [HttpPost]
        public ActionResult DisAssignEmployeeCards(EmployeeDetailsVm vm)
        {
            var (isSuccess, msg) = _employeeWebManager.UnAssignCardsFromEmployee(vm.SelectedCards);
            var employeeId = vm.Id;
            var returnPage = vm.ReturnPage;
            return RedirectToAction("Details", new
            {
                employeeId,
                errorType = isSuccess
                    ? ProcessingMsgType.Success
                    : ProcessingMsgType.Error,
                errorMessage = msg,
                returnPage
            });
        }

        [HttpPost]
        public ActionResult ChangeEmployeeRoles(EmployeeDetailsVm employee)
        {
            _employeeWebManager.UpdateEmployeeRoles(employee);
            return RedirectToAction("Details", new {employeeId = employee.Id});
        }
    }
}
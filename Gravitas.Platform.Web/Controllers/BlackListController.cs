using System.Web.Mvc;
using Gravitas.Model;
using Gravitas.Platform.Web.Manager.BlackList;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers
{
    public class BlackListController : Controller
    {
        private readonly IBlackListManager _blackListManager;

        public BlackListController(IBlackListManager blackListManager)
        {
            _blackListManager = blackListManager;
        }

        public ActionResult Records(int errorType = 0, string errorMessage = "")
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login", "Admin");  
            } 
            var vm = _blackListManager.GetBlackListTable();
            vm.ErrorType = errorType;
            vm.ErrorMessage = errorMessage;
            return View("BlackListTable", vm);
        }

        [HttpPost]
        public ActionResult AddDriverToBlackList(BlackListDriverRecordVm vm)
        {
            (bool isSucess, string msg) insertResult = _blackListManager.AddDriverRecord(vm);
           
            return RedirectToAction("Records", new 
            {
                errorType = insertResult.isSucess
                    ? NodeData.ProcessingMsg.Type.Success
                    : NodeData.ProcessingMsg.Type.Error,
                errorMessage = insertResult.msg
            });
        }

        [HttpPost]
        public ActionResult AddTrailerToBlackList(BlackListTrailerRecordVm vm)
        {
            (bool isSucess, string msg) insertResult = _blackListManager.AddTrailerRecord(vm);

            return RedirectToAction("Records", new
            {
                errorType = insertResult.isSucess
                    ? NodeData.ProcessingMsg.Type.Success
                    : NodeData.ProcessingMsg.Type.Error,
                errorMessage = insertResult.msg
            });
        }

        [HttpPost]
        public ActionResult AddPartnerToBlackList(BlackListPartnerRecordToAddVm vm)
        {
            (bool isSucess, string msg) insertResult = _blackListManager.AddPartnerRecord(vm);

            return RedirectToAction("Records", new
            {
                errorType = insertResult.isSucess
                    ? NodeData.ProcessingMsg.Type.Success
                    : NodeData.ProcessingMsg.Type.Error,
                errorMessage = insertResult.msg
            });
        }

        [HttpPost]
        public ActionResult AddTransportToBlackList(BlackListTransportRecordVm vm)
        {
            (bool isSucess, string msg) insertResult = _blackListManager.AddTransportRecord(vm);

            return RedirectToAction("Records", new
            {
                errorType = insertResult.isSucess
                    ? NodeData.ProcessingMsg.Type.Success
                    : NodeData.ProcessingMsg.Type.Error,
                errorMessage = insertResult.msg
            });
        }

        public ActionResult DeleteDriverRecord(int id)
        {
            _blackListManager.DeleteBlackListDriverRecord(id);
            return RedirectToAction("Records");
        }

        public ActionResult DeletePartnerRecord(string id)
        {
            _blackListManager.DeleteBlackListPartnerRecord(id);
            return RedirectToAction("Records");
        }

        public ActionResult DeleteTrailerRecord(int id)
        {
            _blackListManager.DeleteBlackListTrailerRecord(id);
            return RedirectToAction("Records");
        }

        public ActionResult DeleteTransportRecord(int id)
        {
            _blackListManager.DeleteBlackListTransportRecord(id);
            return RedirectToAction("Records");
        }
    }
}
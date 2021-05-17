using System;
using System.Web.Mvc;
using Gravitas.Model;
using Gravitas.Platform.Web.Manager.User;
using Gravitas.Platform.Web.ViewModel.User;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Platform.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserWebManager _userWebManager;

        public UserController(IUserWebManager userWebManager)
        {
            _userWebManager = userWebManager;
        }

        public ActionResult List(int? page, string name)
        {
            return PartialView("UserList", _userWebManager.GetUserList(name, page ?? 1));
        }

        public ActionResult Details(string userid, int errorType = 0, string errorMessage = "", int returnPage = 1)
        {
            var userDetailVm = _userWebManager.GetUserDetails(userid);
            userDetailVm.ReturnPage = returnPage;

            if (errorType != 0)
                userDetailVm.CardProcessingMsgError = new CardProcessingMsgVm
                {
                    Text = errorMessage,
                    Time = DateTime.Now, 
                    TypeId = errorType
                };

            return PartialView("UserDetails", userDetailVm);
        }

        public ActionResult AssignCardToUser(string userId, int returnPage)
        {
            (bool isSucess, string msg) assignResult = _userWebManager.AssignCardToUser(userId);

            return RedirectToAction("Details", new
            {
                userId,
                errorType = assignResult.isSucess
                    ? Dom.User.ProcessingMsg.Type.Success
                    : Dom.User.ProcessingMsg.Type.Error,
                errorMessage = assignResult.msg,
                returnPage
            });
        }
    }
}
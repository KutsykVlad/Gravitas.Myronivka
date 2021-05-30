using System;
using System.Web.Mvc;
using Gravitas.Model;
using Gravitas.Platform.Web.Manager.User;
using Gravitas.Platform.Web.ViewModel.User;

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

        public ActionResult Details(Guid userid, int errorType = 0, string errorMessage = "", int returnPage = 1)
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

        public ActionResult AssignCardToUser(Guid userId, int returnPage)
        {
            (bool isSucess, string msg) assignResult = _userWebManager.AssignCardToUser(userId);

            return RedirectToAction("Details", new
            {
                userId,
                errorType = assignResult.isSucess
                    ? ProcessingMsgType.Success
                    : ProcessingMsgType.Error,
                errorMessage = assignResult.msg,
                returnPage
            });
        }
    }
}
using System.Net;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.MixedFeed.DAO;
using Gravitas.Model.DomainModel.MixedFeed.DTO;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.Manager.MixedFeedManage;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.Manager.Workstation;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers
{
    public class MixedFeedManageController : Controller
    {
        private readonly IMixedFeedWebManager _mixedFeedWebManager;

        public MixedFeedManageController(IMixedFeedWebManager mixedFeedWebManager)
        {
            _mixedFeedWebManager = mixedFeedWebManager;
        }

        #region 01_Workstation

        [HttpGet, ChildActionOnly]
        public ActionResult Workstation()
        {
            return PartialView("../MixedFeedManage/01_Workstation", _mixedFeedWebManager.GetSiloItems());
        }
        
        [HttpGet]
        public ActionResult Workstation_SelectSilo(long siloId)
        {
            _mixedFeedWebManager.Workstation_SelectSilo(siloId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        [HttpGet]
        public ActionResult SiloWorkstationItem(SiloDetailVm siloVm)
        {
            return PartialView("../MixedFeedManage/SiloWorkstationItem", siloVm);
        }

        #endregion

        #region 02_Edit

        [HttpGet, ChildActionOnly]
        public ActionResult Edit()
        {
            return PartialView("../MixedFeedManage/02_Edit", _mixedFeedWebManager.GetEditVm());
        }

        [HttpGet]
        public ActionResult Edit_Back()
        {
            _mixedFeedWebManager.Edit_Back();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        
        public ActionResult Edit_Clear()
        {
            _mixedFeedWebManager.Edit_Clear();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Save(MixedFeedManageVms.EditVm mixedFeedSilo)
        {
            _mixedFeedWebManager.Edit_Save(mixedFeedSilo);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
        
        #region 03_AddOperationVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddOperationVisa()
        {
            return PartialView("../MixedFeedManage/03_AddOperationVisa", new MixedFeedManageVms.AddOperationVisaVm { });
        }

        #endregion
    }
}
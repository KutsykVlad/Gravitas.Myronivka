using System;
using System.Web.Mvc;
using Gravitas.Platform.Web.Manager.OpData;

namespace Gravitas.Platform.Web.Controllers
{
    public class OpDataController : Controller
    {
        private readonly IOpDataWebManager _opDataWebManager;

        public OpDataController(IOpDataWebManager opDataWebManager)
        {
            _opDataWebManager = opDataWebManager;
        }

        public ActionResult List(long ticketId, bool showPhotoIcons = false, bool showFullPhotos = false)
        {
            var vm = _opDataWebManager.GetOpDataItems(ticketId);
            vm.ShowPhotoIcons = showPhotoIcons;
            vm.ShowFullPhotos = showFullPhotos;

            return PartialView("_List", vm);
        }

        public ActionResult LaboratoryIn_ListComponentItems(Guid opDataId)
        {
            var
                vm = _opDataWebManager.LaboratoryIn_GetListComponentItems(opDataId);

            return PartialView("../OpData/LabolatoryIn/_ListComponentItems", vm);
        }
    }
}
using System;
using System.Web.Mvc;
using Gravitas.Platform.Web.Manager.OpData;

namespace Gravitas.Platform.Web.Controllers
{
    public class OpCameraImageController : Controller
    {
        private readonly IOpDataWebManager _opDataWebManager;

        public OpCameraImageController(IOpDataWebManager opDataWebManager)
        {
            _opDataWebManager = opDataWebManager;
        }

        public ActionResult OperationFrameList(Guid opDataId)
        {
            var vm = _opDataWebManager.GetOpCameraImageItems(opDataId);

            return PartialView("../OpCameraImage/List/_OpCameraImageList", vm);
        }
        
        public ActionResult OperationFrameListLinks(Guid opDataId)
        {
            var vm = _opDataWebManager.GetOpCameraImageItems(opDataId);

            return PartialView("../OpCameraImage/List/_OpCameraImageListLinks", vm);
        }

        public ActionResult GetImageFromByteArray(string imgPath)
        {
            if (!System.IO.File.Exists(imgPath)) return null;

            var byteData = System.IO.File.ReadAllBytes(imgPath);
            var imreBase64Data = Convert.ToBase64String(byteData);
            ViewBag.ImageData = $"data:image/png;base64,{imreBase64Data}";
            return PartialView();
        }
    }
}
using System.Collections.Generic;
using System.Web.Mvc;
using Gravitas.DAL.Repository.PackingTare;
using Gravitas.Model.DomainModel.PackingTare.DTO;

namespace Gravitas.Platform.Web.Controllers
{
    public class PackingTareController : Controller
    {
        private readonly IPackingTareRepository _packingTareRepository;

        public PackingTareController(IPackingTareRepository packingTareRepository)
        {
            _packingTareRepository = packingTareRepository;
        }

        public ActionResult Get()
        {
            var model = _packingTareRepository.GetTareList();
            return View(model);
        }
        
        public ActionResult Remove(long id)
        {
            _packingTareRepository.Remove(id);
            return RedirectToAction(nameof(Get));
        }

        public ActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Add(PackingTareVm vm)
        {
            if (ModelState.IsValid)
            {
                var isAdded = _packingTareRepository.Add(vm);
                if (isAdded) return RedirectToAction(nameof(Get));
            }
        
            return View(vm);
        }
    }
}
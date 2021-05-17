using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.DAL.Repository.PreRegistration;
using Gravitas.Model;
using Gravitas.Model.DomainModel.PreRegistration.DAO;
using Gravitas.Model.DomainModel.PreRegistration.DTO;
using Gravitas.Platform.Web.ViewModel.PreRegistration;

namespace Gravitas.Platform.Web.Controllers.PreRegister
{
    public class PreRegisterProductsController : Controller
    {
        private readonly IPreRegistrationRepository _preRegistrationRepository;
        private readonly IRoutesRepository _routesRepository;

        public PreRegisterProductsController(IPreRegistrationRepository preRegistrationRepository, 
            IRoutesRepository routesRepository)
        {
            _preRegistrationRepository = preRegistrationRepository;
            _routesRepository = routesRepository;
        }

        public ActionResult Get()
        {
            var list = _preRegistrationRepository.GetProducts().ToList();
            return View(list);
        }
        
        [HttpGet]
        public ActionResult Add()
        {
            var vm = new AddProductVm
            {
                Routes = GetRouteDropdownItems()
            };
            return View(vm);
        }
        
        [HttpPost]
        public ActionResult Add(AddProductVm vm)
        {
            if (ModelState.IsValid)
            {
                var isRouteRegistered = _preRegistrationRepository.IsRouteRegistered(vm.RouteTemplateId);
                if (!isRouteRegistered)
                {
                    _preRegistrationRepository.AddProduct(new PreRegisterProduct
                    {
                        Title = vm.Title,
                        RouteTemplateId = vm.RouteTemplateId,
                        RouteTimeInMinutes = vm.RouteTimeInMinutes
                    });
                    return RedirectToAction(nameof(Get));
                }
            }

            vm.Routes = GetRouteDropdownItems();
            return View(vm);
        }
        
        public ActionResult Remove(long id)
        {
            _preRegistrationRepository.RemoveProduct(id);
            return RedirectToAction(nameof(Get));
        }

        private List<SelectListItem> GetRouteDropdownItems()
        {
            return _routesRepository
                .GetRoutes()
                .Where(x => x.IsMain)
                .Select(x => new SelectListItem
                {
                    Text = x.Name, 
                    Value = x.Id.ToString()
                })
                .ToList();
        }
    }
}
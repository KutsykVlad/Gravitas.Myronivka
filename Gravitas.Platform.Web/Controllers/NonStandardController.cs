using System;
using System.Web.Mvc;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager.OpData;
using Gravitas.Platform.Web.Models;
using Gravitas.Platform.Web.ViewModel.OpData.NonStandart;

namespace Gravitas.Platform.Web.Controllers
{
    public class NonStandardController : Controller
    {
        private readonly IOpDataWebManager _opDataWebManager;

        public NonStandardController(IOpDataWebManager opDataWebManager)
        {
            _opDataWebManager = opDataWebManager;
        }

        public ActionResult Registry()
        {
            var vm = _opDataWebManager.GetNonStandardRegistryItems(new NonStandartDataFilters
            {
                NodeId = (int)NodeIdValue.SecurityIn1,
                LeftScope = DateTime.Now,
                RightScope = DateTime.Now
            });

            return PartialView(nameof(Registry), vm);
        }

        [HttpPost]
        public ActionResult Registry(NonStandartRegistryItemsVm vm, int page = 1)
        {
            var viewModel =
                _opDataWebManager.GetNonStandardRegistryItems(
                    new NonStandartDataFilters
                    {
                        LeftScope = vm.BeginDate,
                        RightScope = vm.EndDate, 
                        NodeId = vm.RelatedNodeId
                    }, page);
            return PartialView(nameof(Registry), viewModel);
        }
    }
}
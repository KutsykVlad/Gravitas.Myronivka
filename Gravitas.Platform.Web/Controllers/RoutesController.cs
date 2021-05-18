using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.Routes;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.DAO;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager.Routes;
using Gravitas.Platform.Web.ViewModel.RouteEditor;
using Newtonsoft.Json;

namespace Gravitas.Platform.Web.Controllers
{
    public class RoutesController : Controller
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IRoutesRepository _routesRepository;
        private readonly IRoutesWebManager _routesWebManager;
        private readonly GravitasDbContext _context;

        public RoutesController(IRoutesRepository routesRepository, 
            INodeRepository nodeRepository,
            IRoutesWebManager routesWebManager,
            IRoutesInfrastructure routesInfrastructure, 
            GravitasDbContext context)
        {
            _routesRepository = routesRepository;
            _nodeRepository = nodeRepository;
            _routesWebManager = routesWebManager;
            _routesInfrastructure = routesInfrastructure;
            _context = context;
        }

        [HttpGet]
        public ActionResult List()
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login", "Admin");  
            } 
            return View("List", _routesWebManager.GetRoutesListVm());
        }

        [HttpGet]
        public ActionResult Delete(long routeId)
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login", "Admin");  
            } 
            if (!_context.Tickets.Any(t => t.RouteTemplateId == routeId)) _routesRepository.DeleteRoute(routeId);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult EditQuata(long routeId)
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login", "Admin");  
            } 
            var route = _routesRepository.GetRoute(routeId);

            var vm = new RouteQuataVm
            {
                RouteId = routeId,
                Name = route.Name,
                RouteJson = route.RouteConfig,
                SelectedGroups = new List<string>(),
                Quatas = route.RouteConfig
                    .DeserializeRoute()
                    .GroupDictionary
                    .Select(groupItem =>
                        new SelectListItem
                        {
                            Text = ((NodeGroup) groupItem.Value.GroupId).ToString().Replace("_", " "),
                            Value = groupItem.Key,
                            Selected = groupItem.Value.QuotaEnabled
                        }
                    )
                    .ToList()
            };

            foreach (var item in vm.Quatas)
                if (item.Selected)
                    vm.SelectedGroups.Add(item.Value);

            return View("Quata", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveQuata(RouteQuataVm routeQuataVm)
        {
            if (!ModelState.IsValid) return View("List", _routesWebManager.GetRoutesListVm());

            var route = _routesRepository.GetRoute(routeQuataVm.RouteId);

            var template = route.RouteConfig.DeserializeRoute();

            foreach (var key in template.GroupDictionary.Keys) template.GroupDictionary[key].QuotaEnabled = routeQuataVm.SelectedGroups.Contains(key);

            route.RouteConfig = template.SerializeRoute();
            _routesRepository.UpdateRoute(route);

            return View("List", _routesWebManager.GetRoutesListVm());
        }

        [HttpGet]
        public ActionResult Constructor(long? routeId)
        {
            if (Session["UserID"] == null)  
            {  
                return RedirectToAction("Login", "Admin");  
            } 
            var vm = new RouteConstructorVm
            {
                NodeItems = _nodeRepository.GetQuery<Node, long>()
                    .Select(item => new SelectListItem {Value = item.Id.ToString(), Text = item.Name})
                    .ToList(),
                RouteJson = @"{""disableAppend"":false,""groupDictionary"":{}}"
            };

            if (routeId.HasValue)
            {
                var routeTemplate = _context.RouteTemplates.First(x => x.Id == routeId.Value);
                vm.RouteJson = routeTemplate.RouteConfig;
                vm.Name = routeTemplate.Name;
            }

            return View("Constructor", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Constructor(RouteConstructorVm constructorVm)
        {
            if (!ModelState.IsValid) return View("Constructor", constructorVm);

            var routeTemplate = _routesRepository.GetFirstOrDefault<RouteTemplate, long>(item => item.RouteConfig == constructorVm.RouteJson);
            if (routeTemplate != null && constructorVm.Name == routeTemplate.Name) return RedirectToAction("Constructor", new {routeId = routeTemplate.Id});

            if (routeTemplate != null && constructorVm.RouteJson == routeTemplate.RouteConfig && constructorVm.Name != routeTemplate.Name)
            {
                routeTemplate.Name = constructorVm.Name;
                _routesRepository.Update<RouteTemplate, long>(routeTemplate);
            }
            else
            {
                _routesRepository.UpdateRoute(new RouteTemplate {RouteConfig = constructorVm.RouteJson, Name = constructorVm.Name});
            }

            routeTemplate = _routesRepository.GetFirstOrDefault<RouteTemplate, long>(item => item.RouteConfig == constructorVm.RouteJson);

            return RedirectToAction("Constructor", new {routeId = routeTemplate.Id});
        }

        public ActionResult RenderConstructor(string route)
        {
            return PartialView("_GetRouteView", new RenderVm
            {
                RouteJson = route,
                NodesJson = NodeItemsJson
            });
        }
        
        public ActionResult CreateMainRoute(long routeId)
        {
            var routeTemplate = _routesRepository.GetRoute(routeId);
            
            var route = routeTemplate.RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .Select(groupItem => groupItem
                    .Value
                    .NodeList
                    .Select(item => item)
                    .ToList())
                .ToList();
            foreach (var item in route)
            {
                foreach (var node in item)
                {
                    if (node.MoveRoute == null) node.MoveRoute = new SecondaryRoute();
                    if (node.PartLoadRoute == null) node.PartLoadRoute = new SecondaryRoute();
                    if (node.PartUnloadRoute == null) node.PartUnloadRoute = new SecondaryRoute();
                    if (node.RejectRoute == null) node.RejectRoute = new SecondaryRoute();
                    if (node.ReloadRoute == null) node.ReloadRoute = new SecondaryRoute();
                }
            }
            var model = new CreateMainRouteViewModel
            {
                Id = routeId,
                IsMain = routeTemplate.IsMain,
                IsInQueue = routeTemplate.IsInQueue,
                RouteNodes = route,
                IsTechnological = routeTemplate.IsTechnological
            };
            return View(nameof(CreateMainRoute), model);
        }

        [HttpPost]
        public ActionResult CreateMainRoute(CreateMainRouteViewModel vm)
        {
            var route = _routesRepository.GetRoute(vm.Id);
            var deserializedRoute = route.RouteConfig.DeserializeRoute();
            for (var i = 0; i < deserializedRoute.GroupDictionary.Count; i++)
            {
                deserializedRoute.GroupDictionary[(i+1).ToString()].NodeList = vm.RouteNodes[i].ToArray();
            }

            var serializedRoute = deserializedRoute.SerializeRoute();
            route.RouteConfig = serializedRoute;
            route.IsMain = vm.IsMain;
            route.IsInQueue = vm.IsInQueue;
            route.IsTechnological = vm.IsTechnological;
            _routesRepository.Update<RouteTemplate, long>(route);
            return RedirectToAction("CreateMainRoute", new { routeId = vm.Id });
        }

        public ActionResult GetRouteView(long routeId, long ticketId, bool disableAppend = false)
        {
            return PartialView("_GetRouteView", new RenderVm
            {
                RouteJson = _routesInfrastructure.MarkPassedNodes(ticketId, routeId, disableAppend),
                NodesJson = NodeItemsJson
            });
        }

        private string NodeItemsJson
        {
            get
            {
                var items = _nodeRepository.GetQuery<Node, long>()
                    .ToList()
                    .Select(x => new
                    {
                        id = x.Id, 
                        nodeGroupId = (int)x.NodeGroup,
                        name = x.Name,
                        processId = 0
                    })
                    .ToList();
                return JsonConvert.SerializeObject(items);
            }
        }

    }
}
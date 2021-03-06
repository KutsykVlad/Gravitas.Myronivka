using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model.DomainModel.Node.TDO.List;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Controllers.Routine
{
    public class LoadPointGuideController : Controller
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly GravitasDbContext _context;

        public LoadPointGuideController(IOpRoutineWebManager opRoutineWebManager,
            INodeRepository nodeRepository, 
            IRoutesInfrastructure routesInfrastructure, 
            GravitasDbContext context)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _nodeRepository = nodeRepository;
            _routesInfrastructure = routesInfrastructure;
            _context = context;
        }

        #region 01_Idle

        [HttpGet, ChildActionOnly]
        public ActionResult Idle(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new LoadPointGuideVms.IdleVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/LoadPointGuide/01_Idle", routineData);
        }

        [HttpGet]
        public ActionResult Idle_SelectTicketContainer(int? nodeId, int ticketContainerId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.LoadPointGuide_Idle_SelectTicketContainer(nodeId.Value, ticketContainerId);
            return RedirectToAction("Routine", "Node", new {Id = nodeId});
        }
        
        [HttpGet]
        public ActionResult Idle_SelectRejectedForUnload(int? nodeId, int ticketContainerId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.LoadPointGuide_Idle_SelectRejectedForUnload(nodeId.Value, ticketContainerId);
            return RedirectToAction("Routine", "Node", new {Id = nodeId});
        }
        
        [HttpGet]
        public ActionResult Idle_SelectRejectedForLoad(int? nodeId, int ticketContainerId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.LoadPointGuide_Idle_SelectRejectedForLoad(nodeId.Value, ticketContainerId);
            return RedirectToAction("Routine", "Node", new {Id = nodeId});
        }

        #endregion

        #region 02_BindLoadPoint

        [HttpGet, ChildActionOnly]
        public ActionResult BindLoadPoint(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDto.Context.TicketId.Value);
            if (ticket == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var groupId = NodeGroup.Load;
            var view = "../OpRoutine/LoadPointGuide/02_BindLoadPoint";
            if (nodeDto.Context.OpProcessData.HasValue)
            {
                if (nodeDto.Context.OpProcessData == (long) NodeIdValue.UnloadPointGuideEl23)
                {

                    groupId = NodeGroup.Unload;
                    view = "../OpRoutine/LoadPointGuide/02_BindUnloadPoint";
                }
                
                if (nodeDto.Context.OpProcessData == (long) NodeIdValue.LoadPointGuideEl23)
                {
                    groupId = NodeGroup.Load;
                    view = "../OpRoutine/LoadPointGuide/02_BindLoadPoint";
                }
            }
            
            var destinationPointList = _routesInfrastructure.GetNodesInGroup(ticket.SecondaryRouteTemplateId, groupId)
                ?? _routesInfrastructure.GetNodesInGroup(ticket.RouteTemplateId, groupId);

            ViewBag.NodeItems = new List<NodeItem>();
            if (destinationPointList != null)
            {
                ViewBag.NodeItems = _nodeRepository.GetNodeItems().Items
                    .Where(e => destinationPointList.Contains(e.Id))
                    .ToList();
            }
            var routineData = _opRoutineWebManager.LoadPointGuide_BindLoadPoint_GetVm(nodeId.Value);
            return PartialView(view, routineData);
        }

        [HttpGet]
        public ActionResult BindLoadPoint_Back(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.LoadPointGuide_BindLoadPoint_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult BindLoadPoint_Next(LoadPointGuideVms.BindDestPointVm vm)
        {
            if (vm.DestNodeId != 0) _opRoutineWebManager.LoadPointGuide_BindLoadPoint_Next(vm);
            return RedirectToAction("Routine", "Node", new {Id = vm.NodeId});
        }

        #endregion
        
        #region 03_AddOpVisa

        [HttpGet, ChildActionOnly]
        public ActionResult AddOpVisa(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var routineData = new LoadPointGuideVms.AddOpVisaVm {NodeId = nodeId.Value};
            return PartialView("../OpRoutine/LoadPointGuide/03_AddOpVisa", routineData);
        } 
        
        [HttpGet]
        public ActionResult AddOpVisa_Back(int? nodeId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            _opRoutineWebManager.AddOpVisa_Back(nodeId.Value);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        #endregion
    }
}
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Common.Attribute;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.ViewModel;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Platform.Web.Controllers
{
    public class NodeController : Controller
    {
        private readonly NodeRepository _nodeRepository;
        private readonly NodeWebManager _nodeWebManager;
        private readonly GravitasDbContext _context;

        public NodeController(
            NodeRepository nodeRepository,
            NodeWebManager nodeWebManager, 
            GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _nodeWebManager = nodeWebManager;
            _context = context;
        }

        public ActionResult NodeProgres(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var resultVm = _nodeWebManager.GetNodeProgress(id.Value);
            return PartialView("_NodeProgres", resultVm);
        }
        
        public ActionResult RoutineSingle(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nodeDto = _nodeRepository.GetNodeDto(id.Value);
            if (nodeDto?.Context == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            InitRoutineActionName(id.Value);
            ViewBag.NodeCode = nodeDto.Code;
            ViewBag.NodeName = nodeDto.Name;
            
            if (nodeDto.IsEmergency)
            {
                return PartialView("_NodeInEmergencyMode");
            }

            var vm = new NodeRoutineVm
            {
                NodeId = id.Value,
                NodeProcessingMsgVm = Mapper.Map<NodeProcessingMsgVm>(nodeDto.ProcessingMessage),
                WorkstationId = _context.Nodes.First(x => x.Id == id.Value).OrganisationUnitId ?? 0
            };

            return PartialView("_RoutineSingle", vm);
        }
        
        public ActionResult NodeProcessingMessage(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            return PartialView("_NodeProcessigMessage", Mapper.Map<NodeProcessingMsgVm>(nodeDto.ProcessingMessage));
        }
        
        public ActionResult Routine(long? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var nodeDto = _nodeRepository.GetNodeDto(id.Value);
            if (nodeDto?.Context == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.NodeCode = nodeDto.Code;
            ViewBag.NodeName = nodeDto.Name;
            
            var vm = new NodeRoutineVm
            {
                NodeId = id.Value,
                WorkstationId = _context.Nodes.First(x => x.Id == id.Value).OrganisationUnitId ?? 0
            };
            
            return View(vm);
        }

        private static string GetControllerName(Type type) => type.GetCustomAttribute<OpControllerNameAttribute>()
                                                                  .ControllerName;

        private static string GetActionName(Type type, long stateId) => type
                                                                        .GetFields(BindingFlags.Public |
                                                                                   BindingFlags.Static |
                                                                                   BindingFlags.FlattenHierarchy)
                                                                        .FirstOrDefault(e =>
                                                                            (e.GetRawConstantValue() as int?).HasValue
                                                                            && ((int?) e.GetRawConstantValue())
                                                                            .Value == stateId)
                                                                        ?.GetCustomAttribute<OpActionNameAttribute>()
                                                                        ?.ActionName;

        private void InitRoutineActionName(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (nodeDto.Context.OpRoutineStateId == null) return;

            string actionName = null;
            string controllerName = null;

            switch (nodeDto.OpRoutineId)
            {
                case Dom.OpRoutine.SingleWindow.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.SingleWindow));
                    actionName = GetActionName(typeof(Dom.OpRoutine.SingleWindow.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.SecurityIn.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.SecurityIn));
                    actionName = GetActionName(typeof(Dom.OpRoutine.SecurityIn.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.SecurityOut.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.SecurityOut));
                    actionName = GetActionName(typeof(Dom.OpRoutine.SecurityOut.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.SecurityReview.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.SecurityReview));
                    actionName = GetActionName(typeof(Dom.OpRoutine.SecurityReview.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.LabolatoryIn.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.LabolatoryIn));
                    actionName = GetActionName(typeof(Dom.OpRoutine.LabolatoryIn.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.LabolatoryOut.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.LabolatoryOut));
                    actionName = GetActionName(typeof(Dom.OpRoutine.LabolatoryOut.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.Weighbridge.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.Weighbridge));
                    actionName = GetActionName(typeof(Dom.OpRoutine.Weighbridge.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.UnloadPointGuide.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.UnloadPointGuide));
                    actionName = GetActionName(typeof(Dom.OpRoutine.UnloadPointGuide.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.UnloadPointType1.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.UnloadPointType1));
                    actionName = GetActionName(typeof(Dom.OpRoutine.UnloadPointType1.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.LoadCheckPoint.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.LoadCheckPoint));
                    actionName = GetActionName(typeof(Dom.OpRoutine.LoadCheckPoint.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.UnloadCheckPoint.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.UnloadCheckPoint));
                    actionName = GetActionName(typeof(Dom.OpRoutine.UnloadCheckPoint.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.LoadPointGuide.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.LoadPointGuide));
                    actionName = GetActionName(typeof(Dom.OpRoutine.LoadPointGuide.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.CentralLaboratorySamples.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.CentralLaboratorySamples));
                    actionName = GetActionName(typeof(Dom.OpRoutine.CentralLaboratorySamples.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.CentralLaboratoryProcess.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.CentralLaboratoryProcess));
                    actionName = GetActionName(typeof(Dom.OpRoutine.CentralLaboratoryProcess.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.LoadPointType1.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.LoadPointType1));
                    actionName = GetActionName(typeof(Dom.OpRoutine.LoadPointType1.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.UnloadPointType2.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.UnloadPointType2));
                    actionName = GetActionName(typeof(Dom.OpRoutine.UnloadPointType2.State),
                    nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.MixedFeedManage.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.MixedFeedManage));
                    actionName = GetActionName(typeof(Dom.OpRoutine.MixedFeedManage.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.MixedFeedGuide.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.MixedFeedGuide));
                    actionName = GetActionName(typeof(Dom.OpRoutine.MixedFeedGuide.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.MixedFeedLoad.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.MixedFeedLoad));
                    actionName = GetActionName(typeof(Dom.OpRoutine.MixedFeedLoad.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.UnloadPointGuide2.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.UnloadPointGuide2));
                    actionName = GetActionName(typeof(Dom.OpRoutine.UnloadPointGuide2.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case Dom.OpRoutine.LoadPointGuide2.Id:
                    controllerName = GetControllerName(typeof(Dom.OpRoutine.LoadPointGuide2));
                    actionName = GetActionName(typeof(Dom.OpRoutine.LoadPointGuide2.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
            }

            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
        }

        public void ClearNodeProcessingMessage(long nodeId) => _nodeRepository.ClearNodeProcessingMessage(nodeId);
    }
}
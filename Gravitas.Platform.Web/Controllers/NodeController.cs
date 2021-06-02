using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Node;
using Gravitas.Infrastructure.Common.Attribute;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel.Node.Detail;

namespace Gravitas.Platform.Web.Controllers
{
    public class NodeController : Controller
    {
        private readonly NodeRepository _nodeRepository;
        private readonly GravitasDbContext _context;

        public NodeController(
            NodeRepository nodeRepository,
            GravitasDbContext context)
        {
            _nodeRepository = nodeRepository;
            _context = context;
        }

        public ActionResult RoutineSingle(int? id)
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
                WorkstationId = _context.Nodes.First(x => x.Id == id.Value).OrganizationUnitId
            };

            return PartialView("_RoutineSingle", vm);
        }

        public ActionResult NodeProcessingMessageItems(int nodeId)
        {
            var messages = _context.NodeProcessingMessages
                .Where(x => x.NodeId == nodeId)
                .ToList();
            return PartialView("_NodeProcessigMessageItems", messages);
        }
        
        public ActionResult Routine(int? id)
        {
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var nodeDto = _nodeRepository.GetNodeDto(id.Value);
            if (nodeDto?.Context == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.NodeCode = nodeDto.Code;
            ViewBag.NodeName = nodeDto.Name;
            
            var vm = new NodeRoutineVm
            {
                NodeId = id.Value,
                WorkstationId = _context.Nodes.First(x => x.Id == id.Value).OrganizationUnitId
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

        private void InitRoutineActionName(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (nodeDto.Context.OpRoutineStateId == null) return;

            string actionName = null;
            string controllerName = null;

            switch (nodeDto.OpRoutineId)
            {
                case OpRoutine.SingleWindow.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.SingleWindow));
                    actionName = GetActionName(typeof(OpRoutine.SingleWindow.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.SecurityIn.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.SecurityIn));
                    actionName = GetActionName(typeof(OpRoutine.SecurityIn.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.SecurityOut.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.SecurityOut));
                    actionName = GetActionName(typeof(OpRoutine.SecurityOut.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.SecurityReview.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.SecurityReview));
                    actionName = GetActionName(typeof(OpRoutine.SecurityReview.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.LaboratoryIn.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.LaboratoryIn));
                    actionName = GetActionName(typeof(OpRoutine.LaboratoryIn.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.Weighbridge.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.Weighbridge));
                    actionName = GetActionName(typeof(OpRoutine.Weighbridge.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.UnloadPointGuide.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.UnloadPointGuide));
                    actionName = GetActionName(typeof(OpRoutine.UnloadPointGuide.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.UnloadPointType1.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.UnloadPointType1));
                    actionName = GetActionName(typeof(OpRoutine.UnloadPointType1.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.LoadCheckPoint.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.LoadCheckPoint));
                    actionName = GetActionName(typeof(OpRoutine.LoadCheckPoint.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.UnloadCheckPoint.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.UnloadCheckPoint));
                    actionName = GetActionName(typeof(OpRoutine.UnloadCheckPoint.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.LoadPointGuide.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.LoadPointGuide));
                    actionName = GetActionName(typeof(OpRoutine.LoadPointGuide.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.CentralLaboratorySamples.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.CentralLaboratorySamples));
                    actionName = GetActionName(typeof(OpRoutine.CentralLaboratorySamples.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.CentralLaboratoryProcess.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.CentralLaboratoryProcess));
                    actionName = GetActionName(typeof(OpRoutine.CentralLaboratoryProcess.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.LoadPointType1.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.LoadPointType1));
                    actionName = GetActionName(typeof(OpRoutine.LoadPointType1.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.UnloadPointType2.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.UnloadPointType2));
                    actionName = GetActionName(typeof(OpRoutine.UnloadPointType2.State),
                    nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.MixedFeedManage.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.MixedFeedManage));
                    actionName = GetActionName(typeof(OpRoutine.MixedFeedManage.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.MixedFeedGuide.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.MixedFeedGuide));
                    actionName = GetActionName(typeof(OpRoutine.MixedFeedGuide.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.MixedFeedLoad.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.MixedFeedLoad));
                    actionName = GetActionName(typeof(OpRoutine.MixedFeedLoad.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.UnloadPointGuide2.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.UnloadPointGuide2));
                    actionName = GetActionName(typeof(OpRoutine.UnloadPointGuide2.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
                case OpRoutine.LoadPointGuide2.Id:
                    controllerName = GetControllerName(typeof(OpRoutine.LoadPointGuide2));
                    actionName = GetActionName(typeof(OpRoutine.LoadPointGuide2.State),
                        nodeDto.Context.OpRoutineStateId.Value);
                    break;
            }

            ViewBag.ActionName = actionName;
            ViewBag.ControllerName = controllerName;
        }

        public void ClearNodeProcessingMessage(int nodeId) => _nodeRepository.ClearNodeProcessingMessage(nodeId);
    }
}
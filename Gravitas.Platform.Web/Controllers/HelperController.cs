using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.PackingTare;
using Gravitas.Infrastructure.Platform.Manager.Node;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.PackingTare.DTO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel.Helper;

namespace Gravitas.Platform.Web.Controllers
{
    public class HelperController : Controller
    {
        private readonly INodeManager _nodeManager;
        private readonly IPackingTareRepository _packingTareRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly IOpDataRepository _opDataRepository;
        
        public HelperController(INodeManager nodeManager, 
            IPackingTareRepository packingTareRepository, 
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository)
        {
            _nodeManager = nodeManager;
            _packingTareRepository = packingTareRepository;
            _nodeRepository = nodeRepository;
            _opDataRepository = opDataRepository;
        }

        public ActionResult GetShrotLink(int nodeId)
        {
            var nodes = new HashSet<int>
            {
                (int) NodeIdValue.UnloadPoint13, 
                (int) NodeIdValue.UnloadPoint14, 
                (int) NodeIdValue.LoadPoint40, 
                (int) NodeIdValue.LoadPoint60,
                (int) NodeIdValue.LoadPoint30,
                (int) NodeIdValue.LoadPoint31,
                (int) NodeIdValue.LoadPoint32,
                (int) NodeIdValue.LoadPoint33
            };
            var vm = new GetShrotLinkVm
            {
                Links = new List<ShrotLink>()
            };
            if (nodeId == (int) NodeIdValue.LoadPointGuideShrotHuskOil)
            {
                vm.Links.AddRange(nodes.Select(x => new ShrotLink
                {
                    Link = Url.Action("Routine", "Node", new {id = (int) x}),
                    Title = _nodeManager.GetNodeName(x)
                }));
            }
            if (nodeId == (int) NodeIdValue.UnloadPoint50 || nodeId == (int) NodeIdValue.LoadPoint72)
            {
                vm.Links.Add(new ShrotLink
                {
                    Link = Url.Action("Routine", "Node", new {id = (int) NodeIdValue.UnloadPointGuideLowerArea}),
                    Title = _nodeManager.GetNodeName((int) NodeIdValue.UnloadPointGuideLowerArea)
                });
                vm.Links.Add(new ShrotLink
                {
                    Link = Url.Action("Routine", "Node", new {id = (int) NodeIdValue.LoadPointGuideLowerArea}),
                    Title = _nodeManager.GetNodeName((int) NodeIdValue.LoadPointGuideLowerArea)
                });
            }
            if (nodes.Contains(nodeId))
            {
                vm.Links.Add(new ShrotLink
                {
                    Link = Url.Action("Routine", "Node", new {id = (int) NodeIdValue.LoadPointGuideShrotHuskOil}),
                    Title = _nodeManager.GetNodeName((int) NodeIdValue.LoadPointGuideShrotHuskOil)
                });
            }
            return PartialView("_GetLinkTemplate", vm);
        }
        
        public ActionResult GetLabLink(long nodeId)
        {
            var vm = new GetShrotLinkVm
            {
                Links = new List<ShrotLink>()
            };
            if (nodeId == (int) NodeIdValue.UnloadPoint31)
            {
                vm.Links.Add(new ShrotLink
                {
                    Link = Url.Action("Routine", "Node", new {id = (int) NodeIdValue.CentralLaboratoryGetCustomStore}),
                    Title = _nodeManager.GetNodeName((int) NodeIdValue.CentralLaboratoryGetCustomStore)
                });
            } 
            if (nodeId == (int) NodeIdValue.CentralLaboratoryGetCustomStore)
            {
                vm.Links.Add(new ShrotLink
                {
                    Link = Url.Action("Routine", "Node", new {id = (int) NodeIdValue.UnloadPoint31}),
                    Title = _nodeManager.GetNodeName((int) NodeIdValue.UnloadPoint31)
                });
            }
            if (nodeId == (int) NodeIdValue.UnloadPoint30)
            {
                vm.Links.Add(new ShrotLink
                {
                    Link = Url.Action("Routine", "Node", new {id = (int) NodeIdValue.CentralLaboratoryGetTruckRamp}),
                    Title = _nodeManager.GetNodeName((int) NodeIdValue.CentralLaboratoryGetTruckRamp)
                });
            } 
            if (nodeId == (int) NodeIdValue.CentralLaboratoryGetTruckRamp)
            {
                vm.Links.Add(new ShrotLink
                {
                    Link = Url.Action("Routine", "Node", new {id = (int) NodeIdValue.UnloadPoint30}),
                    Title = _nodeManager.GetNodeName((int) NodeIdValue.UnloadPoint30)
                });
            }
            return PartialView("_GetLinkTemplate", vm);
        }

        [HttpGet]
        public ActionResult GetTareValue(int? nodeId, int returnRoutineStateId)
        {
            if (nodeId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketId == null) throw new ArgumentException();
            
            var vm = new GetTareValue
            {
                ReturnRoutineStateId = returnRoutineStateId, 
                NodeId = nodeId.Value, 
                TareItems = _packingTareRepository.GetTareList()
                    .Select(x => new TicketPackingTareVm
                    {
                        PackingId = x.Id,
                        PackingTitle = x.Title
                    })
                    .ToArray()
            };

            var ticketPackingItems = _packingTareRepository.GetTicketTareList(nodeDto.Context.TicketId.Value);
            if (ticketPackingItems.Any())
            {
                foreach (var ticketPackingItem in ticketPackingItems)
                {
                    vm.TareItems.Where(x => x.PackingId == ticketPackingItem.PackingId).ToList().ForEach(x => x.Count = ticketPackingItem.Count);
                }
            }

            return PartialView(nameof(GetTareValue), vm);
        }

        [HttpPost]
        public ActionResult GetTareValue_Confirm(GetTareValue vm)
        {
            if (vm == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto?.Context.TicketId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            if (vm.TareItems.Any(x => x.Count > 0))
            {
                var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId);
                singleWindowOpData.PackingWeightValue = 0;

                _packingTareRepository.RemoveTicketTare(nodeDto.Context.TicketId.Value);
                foreach (var tareItem in vm.TareItems)
                {
                    if (tareItem.Count == 0) continue;

                    _packingTareRepository.AddTicketTare(nodeDto.Context.TicketId.Value, tareItem.PackingId, tareItem.Count);
                    singleWindowOpData.PackingWeightValue =+ (double) tareItem.Count * _packingTareRepository.GetTareWeight(tareItem.PackingId);
                }
                _opDataRepository.Update<SingleWindowOpData, Guid>(singleWindowOpData);
            }
            
            nodeDto.Context.OpRoutineStateId = vm.ReturnRoutineStateId;
            var result = _nodeRepository.UpdateNodeContext(vm.NodeId, nodeDto.Context);

            if (result) SignalRInvoke.ReloadHubGroup(vm.NodeId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager.TicketContainer;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.Shared;
using Gravitas.Platform.Web.ViewModel.TicketContainer;
using Gravitas.Platform.Web.ViewModel.TicketContainer.List;

namespace Gravitas.Platform.Web.Controllers
{
    public class TicketContainerController : Controller
    {
        private readonly IOpDataRepository _opDataRepository;
        private readonly ITicketContainerWebManager _ticketContainerRegistryManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly GravitasDbContext _context;

        public TicketContainerController(
            ITicketContainerWebManager ticketContainerRegistryManager,
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            GravitasDbContext context)
        {
            _ticketContainerRegistryManager = ticketContainerRegistryManager;
            _opDataRepository = opDataRepository;
            _ticketRepository = ticketRepository;
            _context = context;
        }


        public ActionResult GetRegistry(GetRegistryVm vm)
        {
            return PartialView("_GetRegistry", vm);
        }

        #region SingleWindow

        [HttpPost]
        public ActionResult SingleWindowTicketContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(400);
            var ticketContainersIds = FilterSingleWindowTicketContainers(detailActionLink.SingleWindowRegisterFilter);
            var resultItems = _ticketContainerRegistryManager.GetSingleWindowTicketContainerItemsVm(ticketContainersIds);

            var vm = new SingleWindowTicketContainerListVm
            {
                DetailActionLink = detailActionLink,
                Items = resultItems
            };

            return PartialView("_SingleWindowTicketContainerList", vm);
        }

        #endregion

        #region Laboratory

        public ActionResult LabFacelessTicketContainerList(ActionLinkVm detailActionLink)
        {
            return PartialView(
                "_LabFacelessTicketContainerList", new LabFacelessTicketContainerListVm
                {
                    DetailActionLink = detailActionLink,
                    Items = _ticketContainerRegistryManager.GetLabFacelessTicketContainerItemsVm()
                });
        }
        
        public ActionResult SelfServiceLabTicketContainerList(ActionLinkVm detailActionLink)
        {
            return PartialView(
                "_SelfServiceLabTicketContainerList", new LabFacelessTicketContainerListVm
                {
                    DetailActionLink = detailActionLink,
                    Items = _ticketContainerRegistryManager.GetSelfServiceLabTicketContainerItemsVm()
                });
        }

        #endregion

        #region Central Laboratory

        [HttpPost]
        public ActionResult CentralLabTicketContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activeTicketContainers = GetRelatedTicketContainers(detailActionLink.NodeId.Value).ToList();
            return PartialView(
                "_CentralLabTicketContainerList", new CentralLabTicketContainerListVm
                {
                    DetailActionLink = detailActionLink,
                    Items = _ticketContainerRegistryManager.GetCentralLabTicketContainerListVm(activeTicketContainers)
                });
        }

        #endregion

        #region Unload

        [HttpPost]
        public ActionResult UnloadGuideTicketContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activeTicketContainers = GetRelatedTicketContainers(detailActionLink.NodeId.Value).ToList();
            var items = activeTicketContainers
                .Where(item =>
                {
                    var tmpTicket = _ticketRepository.GetTicketInContainer(item, TicketStatus.Processing)
                                    ?? _ticketRepository.GetTicketInContainer(item, TicketStatus.ToBeProcessed);
    
                    var loadPointArrival = _opDataRepository.GetFirstOrDefault<UnloadPointOpData, Guid>(x => x.TicketId == tmpTicket.Id);
                    return loadPointArrival == null;
                })
                .ToList();

            return PartialView("_UnloadGuideTicketContainerList", new UnloadGuideTicketContainerListVm
            {
                DetailActionLink = detailActionLink,
                Items = _ticketContainerRegistryManager.GetUnloadGuideTicketContainerItemsVm(items)
            });
        }

        [HttpPost]
        public ActionResult UnloadPointContainerList(string nodes, ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activeTicketContainers = GetRelatedTicketContainers(detailActionLink.NodeId.Value).ToList();
            return PartialView(
                "_UnloadPointTicketContainerList", new UnloadPointTicketContainerListVm
                {
                    DetailActionLink = detailActionLink,
                    Items = _ticketContainerRegistryManager.GetUnloadPointTicketContainerItemsVm(activeTicketContainers, detailActionLink.NodeId.Value)
                });
        }
        
        [HttpPost]
        public ActionResult UnloadQueueContainerList(string nodes, ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return PartialView(
                "_UnloadQueueTicketContainerList", new UnloadQueueTicketContainerListVm
                {
                    DetailActionLink = detailActionLink,
                    Items = _ticketContainerRegistryManager.GetUnloadQueueTicketContainerItemsVm()
                });
        }

        #endregion

        #region Load

        public ActionResult LoadGuideTicketContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activeTicketContainers = GetRelatedTicketContainers(detailActionLink.NodeId.Value);
            var items = activeTicketContainers
                .Where(item =>
                {
                    var tmpTicket = _ticketRepository.GetTicketInContainer(item, TicketStatus.Processing)
                                    ?? _ticketRepository.GetTicketInContainer(item, TicketStatus.ToBeProcessed);

                    var loadPointArrival = _opDataRepository.GetFirstOrDefault<LoadPointOpData, Guid>(x => x.TicketId == tmpTicket.Id);
                    return loadPointArrival == null;
                })
                .ToList();

            var itemsVm = _ticketContainerRegistryManager.GetLoadGuideTicketContainerItemsVm(items);
            
            return PartialView("_LoadGuideTicketContainerList", new LoadGuideTicketContainerListVm
            {
                DetailActionLink = detailActionLink,
                BudgeCount = itemsVm.Count(x => string.IsNullOrEmpty(x.LoadNodeName)),
                Items = itemsVm
            });
        }

        public ActionResult LoadPointContainerList(string nodes, ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activeTicketContainers = GetRelatedTicketContainers(detailActionLink.NodeId.Value).ToList();
            return PartialView(
                "_LoadPointTicketContainerList", new LoadPointTicketContainerListVm
                {
                    DetailActionLink = detailActionLink,
                    Items = _ticketContainerRegistryManager.GetLoadPointTicketContainerItemsVm(activeTicketContainers, detailActionLink.NodeId.Value)
                });
        }

        #endregion

        #region Rejected

        public ActionResult RejectedLoadGuideContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activeTicketContainers = GetRelatedTicketContainers(detailActionLink.NodeId.Value).ToList();
            var sortedTicketContainers = new List<int>();

            foreach (var activeTicketContainer in activeTicketContainers)
            {
                var ticket = _ticketRepository.GetFirstOrDefault<Ticket, int>(item =>
                    item.TicketContainerId == activeTicketContainer && item.StatusId == TicketStatus.Processing);
                if (ticket == null) continue;

                var routeMapStatus = ticket.RouteType;
                if (routeMapStatus != RouteType.PartLoad && routeMapStatus != RouteType.Reload) continue;
                
                var centralOpData = _opDataRepository.GetLastOpData<CentralLabOpData>(ticket.Id, OpDataState.Rejected);
                var scaleOpData = _opDataRepository.GetLastOpData<ScaleOpData>(ticket.Id, OpDataState.Canceled);
                if (centralOpData != null || scaleOpData != null)
                {
                    var data = centralOpData?.CheckOutDateTime ?? scaleOpData?.CheckOutDateTime;
                    var loadOpData = _opDataRepository.GetLastOpData<LoadPointOpData>(ticket.Id, OpDataState.Processed);
                    if (data > loadOpData?.CheckOutDateTime) sortedTicketContainers.Add(activeTicketContainer);
                }
            }

            var items = _ticketContainerRegistryManager.GetRejectedLoadGuideTicketContainerItemsVm(sortedTicketContainers);

            return PartialView("_RejectedLoadGuideTicketContainerList", new LoadGuideTicketContainerListVm
            {
                DetailActionLink = detailActionLink,
                BudgeCount = items.Count(x => string.IsNullOrEmpty(x.LoadNodeName)),
                Items = items
            });
        }

        public ActionResult RejectedUnloadGuideContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activeTicketContainers = GetRelatedTicketContainers(detailActionLink.NodeId.Value).ToList();
            var sortedTicketContainers = new List<int>();

            foreach (var activeTicketContainer in activeTicketContainers)
            {
                var ticket = _ticketRepository.GetFirstOrDefault<Ticket, int>(item =>
                    item.TicketContainerId == activeTicketContainer && item.StatusId == TicketStatus.Processing);
                if (ticket == null) continue;

                var routeMapStatus = ticket.RouteType;
                if (routeMapStatus != RouteType.PartUnload 
                    && routeMapStatus != RouteType.Reload 
                    && routeMapStatus != RouteType.Move) continue;
                
                var centralOpData = _opDataRepository.GetLastOpData<CentralLabOpData>(ticket.Id, OpDataState.Rejected);
                var scaleOpData = _opDataRepository.GetLastOpData<ScaleOpData>(ticket.Id, OpDataState.Canceled);
                if (centralOpData != null || scaleOpData != null)
                {
                    var data = centralOpData?.CheckOutDateTime ?? scaleOpData?.CheckOutDateTime;
                    var unloadOpData = _opDataRepository.GetLastOpData<UnloadPointOpData>(ticket.Id, OpDataState.Processed);
                    if (data > unloadOpData?.CheckOutDateTime || unloadOpData == null) sortedTicketContainers.Add(activeTicketContainer);
                }
            }

            var items = _ticketContainerRegistryManager.GetRejectedUnloadGuideTicketContainerItemsVm(sortedTicketContainers);

            return PartialView("_RejectedUnloadGuideTicketContainerList", new UnloadGuideTicketContainerListVm
            {
                DetailActionLink = detailActionLink,
                BudgeCount = items.Count(x => string.IsNullOrEmpty(x.UnloadNodeName)),
                Items = items
            });
        }

        #endregion

        #region Rejected MixedFeed

        public ActionResult RejectedMixedFeedLoadTicketContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activeTicketContainers = GetRelatedTicketContainers(detailActionLink.NodeId.Value).ToList();
            var sortedTicketContainers = new List<int>();

            foreach (var activeTicketContainer in activeTicketContainers)
            {
                var ticket = _ticketRepository.GetFirstOrDefault<Ticket, int>(item =>
                    item.TicketContainerId == activeTicketContainer && item.StatusId == TicketStatus.Processing);
                if (ticket == null) continue;

                var routeMapStatus = ticket.RouteType;
                if (routeMapStatus != RouteType.PartLoad && routeMapStatus != RouteType.Reload) continue;

                var scaleOpData = _opDataRepository.GetLastOpData<ScaleOpData>(ticket.Id, OpDataState.Canceled);
                if (scaleOpData != null)
                {
                    var data = scaleOpData.CheckOutDateTime;
                    var loadOpData = _opDataRepository.GetLastOpData<LoadPointOpData>(ticket.Id, OpDataState.Processed);
                    if (data > loadOpData?.CheckOutDateTime) sortedTicketContainers.Add(activeTicketContainer);
                }
            }

            var itemsVm = _ticketContainerRegistryManager.GetRejectedMixedFeedLoadTicketContainerItemsVm(sortedTicketContainers);

            return PartialView("_RejectedMixedFeedLoadTicketContainerList", new MixedFeedGuideTicketContainerListVm
            {
                DetailActionLink = detailActionLink,
                BudgeCount = itemsVm.Count(x => string.IsNullOrEmpty(x.LoadNodeName)),
                Items = itemsVm
            });
        }
        
        public ActionResult RejectedMixedFeedUnLoadTicketContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activeTicketContainers = GetRelatedTicketContainers(detailActionLink.NodeId.Value).ToList();
            var sortedTicketContainers = new List<int>();

            foreach (var activeTicketContainer in activeTicketContainers)
            {
                var ticket = _ticketRepository.GetFirstOrDefault<Ticket, int>(item =>
                    item.TicketContainerId == activeTicketContainer && item.StatusId == TicketStatus.Processing);
                if (ticket == null) continue;

                var routeMapStatus = ticket.RouteType;
                if (routeMapStatus != RouteType.PartUnload && routeMapStatus != RouteType.Reload) continue;

                var scaleOpData = _opDataRepository.GetLastOpData<ScaleOpData>(ticket.Id, OpDataState.Canceled);
                if (scaleOpData != null)
                {
                    var data = scaleOpData.CheckOutDateTime;
                    var loadOpData = _opDataRepository.GetLastOpData<LoadPointOpData>(ticket.Id, OpDataState.Processed);
                    if (data > loadOpData?.CheckOutDateTime) sortedTicketContainers.Add(activeTicketContainer);
                }
            }

            var itemsVm = _ticketContainerRegistryManager.GetRejectedMixedFeedUnloadTicketContainerItemsVm(sortedTicketContainers);
            
            return PartialView("_RejectedMixedFeedUnLoadTicketContainerList", new MixedFeedUnloadTicketContainerListVm
            {
                DetailActionLink = detailActionLink,
                BudgeCount = 0,
                Items = itemsVm
            });
        }

        #endregion

        #region MixedFeed load

        public ActionResult MixedFeedGuideTicketContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var items = GetRelatedTicketContainers(detailActionLink.NodeId.Value).ToList();
            var itemsVm = _ticketContainerRegistryManager.GetMixedFeedGuideTicketContainerItemsVm(items);
            return PartialView("_MixedFeedGuideTicketContainerList", new MixedFeedGuideTicketContainerListVm
            {
                DetailActionLink = detailActionLink,
                BudgeCount = itemsVm.Count(x => string.IsNullOrEmpty(x.LoadNodeName)),
                Items = itemsVm
            });
        }

        public ActionResult MixedFeedLoadTicketContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var activeTicketContainers = GetRelatedTicketContainers(detailActionLink.NodeId.Value).ToList();
            return PartialView(
                "_MixedFeedLoadTicketContainerList", new MixedFeedLoadTicketContainerListVm
                {
                    DetailActionLink = detailActionLink,
                    Items = _ticketContainerRegistryManager.GetMixedFeedLoadTicketContainerItemsVm(activeTicketContainers, detailActionLink.NodeId.Value)
                });
        }

        #endregion
        
        private IEnumerable<int> GetRelatedTicketContainers(int nodeId)
        {
            var nodeList = new List<int>();
            switch (nodeId)
            {
                case (int)NodeIdValue.LoadPointGuideMPZ:
                    nodeList.AddRange(new List<int> 
                    {
                        (int) NodeIdValue.LoadPoint60,
                        (int) NodeIdValue.LoadPoint61,
                        (int) NodeIdValue.LoadPoint62
                    });
                    break; 
                case (int)NodeIdValue.MixedFeedGuide:
                    nodeList.AddRange(new List<int> 
                    {
                        (int) NodeIdValue.MixedFeedLoad1,
                        (int) NodeIdValue.MixedFeedLoad2,
                        (int) NodeIdValue.MixedFeedLoad3,
                        (int) NodeIdValue.MixedFeedLoad4,
                        (int) NodeIdValue.MixedFeedGuide,
                        (int) NodeIdValue.UnloadPoint30
                    });
                    break; 
                case (int)NodeIdValue.CentralLaboratoryProcess1:
                case (int)NodeIdValue.CentralLaboratoryProcess2:
                case (int)NodeIdValue.CentralLaboratoryProcess3:
                    nodeList.AddRange(new List<int> 
                    {
                        (int) NodeIdValue.CentralLaboratoryGetOil1,
                        (int) NodeIdValue.CentralLaboratoryGetOil2,
                        (int) NodeIdValue.CentralLaboratoryGetShrot,
                        (int) NodeIdValue.CentralLaboratoryGetCustomStore,
                        (int) NodeIdValue.CentralLaboratoryGetTruckRamp
                    });
                    break; 
                default:
                    nodeList.AddRange(new List<int> { nodeId });
                    break; 
            }

            return GetNodesContainers(nodeList.Any() ? nodeList : null);
        }

        private IEnumerable<int> GetNodesContainers(List<int> nodeList)
        {
            return _ticketContainerRegistryManager
                   .GetActiveTicketContainers(nodeList)
                   .ToList();
        }
        
        private IEnumerable<int> FilterSingleWindowTicketContainers(SingleWindowRegisterFilter filter)
        {
            var result = new List<int>();

            var tickets = 
                (from card in _context.Cards
                    join ticket in _context.Tickets on card.TicketContainerId equals ticket.TicketContainerId 
                    where card.TicketContainerId.HasValue
                        && (filter == 0 || (ticket.StatusId == TicketStatus.Processing
                                            || ticket.StatusId == TicketStatus.ToBeProcessed
                                            || ticket.StatusId == TicketStatus.New))
                    select ticket)
                .GroupBy(x => x.TicketContainerId)
                .Select(x => x.OrderByDescending(z => z.StatusId).FirstOrDefault())
                .ToList();

            foreach (var ticket in tickets.Where(ticket => ticket != null))
            {
                if (filter == 0)
                {
                    result.Add(ticket.TicketContainerId);
                    continue;
                }

                var opData = _opDataRepository.GetLastOpData(ticket.Id);
                var singleWindowNodeGroup = _context.SingleWindowOpDatas
                    .Where(x => x.TicketId == ticket.Id)
                    .Select(x => x.Node.NodeGroup)
                    .First();

                switch (filter)
                {
                    case SingleWindowRegisterFilter.OwnOnly:
                        if (opData.Node.NodeGroup == singleWindowNodeGroup) result.Add(ticket.TicketContainerId);
                        break;
                }
            }

            return result;
        }
    }
}
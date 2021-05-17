using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.PreRegistration.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.TicketContainer;
using Gravitas.Platform.Web.ViewModel.TicketContainer.List;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Platform.Web.Controllers
{
    public class TicketContainerController : Controller
    {
        private readonly IOpDataRepository _opDataRepository;
        private readonly ITicketContainerWebManager _ticketContainerRegistryManager;
        private readonly ITicketWebManager _ticketManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly GravitasDbContext _context;

        public TicketContainerController(
            ITicketWebManager ticketManager,
            ITicketContainerWebManager ticketContainerRegistryManager,
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            GravitasDbContext context)
        {
            _ticketManager = ticketManager;
            _ticketContainerRegistryManager = ticketContainerRegistryManager;
            _opDataRepository = opDataRepository;
            _ticketRepository = ticketRepository;
            _context = context;
        }


        public ActionResult GetRegistry(GetRegistryVm vm)
        {
            return PartialView("_GetRegistry", vm);
        }
        
        public ActionResult TicketList(long? id)
        {
            return id == null
                ? (ActionResult) new HttpStatusCodeResult(HttpStatusCode.BadRequest)
                : PartialView("_TicketList", _ticketManager.GetTicketItemsVm(id.Value));
        }

        #region SingleWindow

        private static readonly Dictionary<int, int> NodeFilters = new Dictionary<int, int>
        {
            {(int)NodeIdValue.SingleWindowFirst, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowSecond, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowThird, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowReadonly1, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowReadonly2, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowReadonly3, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowReadonly4, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowReadonly5, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowReadonly6, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowReadonly7, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowReadonly8, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowReadonly9, (int)SingleWindowRegisterFilter.InQueue},
            {(int)NodeIdValue.SingleWindowReadonly10, (int)SingleWindowRegisterFilter.InQueue}
        };

        private readonly List<SelectListItem> _filterItems = new List<SelectListItem>
        {
            new SelectListItem {Text = @"Всі", Value = ((int) SingleWindowRegisterFilter.All).ToString()},
            new SelectListItem {Text = @"В черзі", Value = ((int) SingleWindowRegisterFilter.InQueue).ToString()},
            new SelectListItem {Text = @"На території", Value = ((int) SingleWindowRegisterFilter.Inside).ToString()},
            new SelectListItem {Text = @"Пройдений маршрут", Value = ((int) SingleWindowRegisterFilter.Completed).ToString()}
        };
        
        [HttpPost]
        public ActionResult SingleWindowTicketContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(400);
            var ticketContainersIds = FilterSingleWindowTicketContainers((int) detailActionLink.NodeId);
            var resultItems = _ticketContainerRegistryManager.GetSingleWindowTicketContainerItemsVm(ticketContainersIds);
            var selectedFilter = NodeFilters[(int) detailActionLink.NodeId];

            var vm = new SingleWindowTicketContainerListVm
            {
                DetailActionLink = detailActionLink,
                Items = resultItems,
                FilterItems = _filterItems,
                SelectedFilterId = selectedFilter
            };

            return PartialView("_SingleWindowTicketContainerList", vm);
        }

        public void SingleWindowRegistryFilter(int selectedFilterId, int nodeId)
        {
            NodeFilters[nodeId] = selectedFilterId;
            SignalRInvoke.ReloadHubGroup(nodeId);
        }
        
        [HttpPost]
        public ActionResult PreRegisterTicketContainerList(ActionLinkVm detailActionLink)
        {
            if (!detailActionLink.NodeId.HasValue) return new HttpStatusCodeResult(400);

            var items = _opDataRepository.GetQuery<PreRegisterQueue, long>()
                .ToList()
                .Where(x => x.RegisterDateTime.AddMinutes(30) > DateTime.Now)
                .Select(x => new PreRegisterTicketContainerItemVm
                {
                    PredictionEntranceTime = x.RegisterDateTime, 
                    PhoneNo = x.PhoneNo,
                    Notice = x.Notice,
                    TruckNumber = x.TruckNumber
                })
                .ToList();

            var vm = new PreRegisterTicketContainerListVm
            {
                DetailActionLink = detailActionLink,
                Items = items
            };

            return PartialView("_PreRegisterTicketContainerList", vm);
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
                    var tmpTicket = _ticketRepository.GetTicketInContainer(item, Dom.Ticket.Status.Processing)
                                    ?? _ticketRepository.GetTicketInContainer(item, Dom.Ticket.Status.ToBeProcessed);
    
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
                    var tmpTicket = _ticketRepository.GetTicketInContainer(item, Dom.Ticket.Status.Processing)
                                    ?? _ticketRepository.GetTicketInContainer(item, Dom.Ticket.Status.ToBeProcessed);

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
            var sortedTicketContainers = new List<long>();

            foreach (var activeTicketContainer in activeTicketContainers)
            {
                var ticket = _ticketRepository.GetFirstOrDefault<Ticket, long>(item =>
                    item.ContainerId == activeTicketContainer && item.StatusId == Dom.Ticket.Status.Processing);
                if (ticket == null) continue;

                var routeMapStatus = ticket.RouteType;
                if (routeMapStatus != Dom.Route.Type.PartLoad && routeMapStatus != Dom.Route.Type.Reload) continue;
                
                var centralOpData = _opDataRepository.GetLastOpData<CentralLabOpData>(ticket.Id, Dom.OpDataState.Rejected);
                var scaleOpData = _opDataRepository.GetLastOpData<ScaleOpData>(ticket.Id, Dom.OpDataState.Canceled);
                if (centralOpData != null || scaleOpData != null)
                {
                    var data = centralOpData?.CheckOutDateTime ?? scaleOpData?.CheckOutDateTime;
                    var loadOpData = _opDataRepository.GetLastOpData<LoadPointOpData>(ticket.Id, Dom.OpDataState.Processed);
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
            var sortedTicketContainers = new List<long>();

            foreach (var activeTicketContainer in activeTicketContainers)
            {
                var ticket = _ticketRepository.GetFirstOrDefault<Ticket, long>(item =>
                    item.ContainerId == activeTicketContainer && item.StatusId == Dom.Ticket.Status.Processing);
                if (ticket == null) continue;

                var routeMapStatus = ticket.RouteType;
                if (routeMapStatus != Dom.Route.Type.PartUnload 
                    && routeMapStatus != Dom.Route.Type.Reload 
                    && routeMapStatus != Dom.Route.Type.Move) continue;
                
                var centralOpData = _opDataRepository.GetLastOpData<CentralLabOpData>(ticket.Id, Dom.OpDataState.Rejected);
                var scaleOpData = _opDataRepository.GetLastOpData<ScaleOpData>(ticket.Id, Dom.OpDataState.Canceled);
                if (centralOpData != null || scaleOpData != null)
                {
                    var data = centralOpData?.CheckOutDateTime ?? scaleOpData?.CheckOutDateTime;
                    var unloadOpData = _opDataRepository.GetLastOpData<UnloadPointOpData>(ticket.Id, Dom.OpDataState.Processed);
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
            var sortedTicketContainers = new List<long>();

            foreach (var activeTicketContainer in activeTicketContainers)
            {
                var ticket = _ticketRepository.GetFirstOrDefault<Ticket, long>(item =>
                    item.ContainerId == activeTicketContainer && item.StatusId == Dom.Ticket.Status.Processing);
                if (ticket == null) continue;

                var routeMapStatus = ticket.RouteType;
                if (routeMapStatus != Dom.Route.Type.PartLoad && routeMapStatus != Dom.Route.Type.Reload) continue;

                var scaleOpData = _opDataRepository.GetLastOpData<ScaleOpData>(ticket.Id, Dom.OpDataState.Canceled);
                if (scaleOpData != null)
                {
                    var data = scaleOpData.CheckOutDateTime;
                    var loadOpData = _opDataRepository.GetLastOpData<MixedFeedLoadOpData>(ticket.Id, Dom.OpDataState.Processed);
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
            var sortedTicketContainers = new List<long>();

            foreach (var activeTicketContainer in activeTicketContainers)
            {
                var ticket = _ticketRepository.GetFirstOrDefault<Ticket, long>(item =>
                    item.ContainerId == activeTicketContainer && item.StatusId == Dom.Ticket.Status.Processing);
                if (ticket == null) continue;

                var routeMapStatus = ticket.RouteType;
                if (routeMapStatus != Dom.Route.Type.PartUnload && routeMapStatus != Dom.Route.Type.Reload) continue;

                var scaleOpData = _opDataRepository.GetLastOpData<ScaleOpData>(ticket.Id, Dom.OpDataState.Canceled);
                if (scaleOpData != null)
                {
                    var data = scaleOpData.CheckOutDateTime;
                    var loadOpData = _opDataRepository.GetLastOpData<MixedFeedLoadOpData>(ticket.Id, Dom.OpDataState.Processed);
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
        
        private IEnumerable<long> GetRelatedTicketContainers(long nodeId)
        {
            var nodeList = new List<long>();
            switch (nodeId)
            {
                case (long)NodeIdValue.LoadPointGuideMPZ:
                    nodeList.AddRange(new List<long> 
                    {
                        (long) NodeIdValue.LoadPoint60,
                        (long) NodeIdValue.LoadPoint61,
                        (long) NodeIdValue.LoadPoint62
                    });
                    break; 
                case (long)NodeIdValue.MixedFeedGuide:
                    nodeList.AddRange(new List<long> 
                    {
                        (long) NodeIdValue.MixedFeedLoad1,
                        (long) NodeIdValue.MixedFeedLoad2,
                        (long) NodeIdValue.MixedFeedLoad3,
                        (long) NodeIdValue.MixedFeedLoad4,
                        (long) NodeIdValue.MixedFeedGuide,
                        (long) NodeIdValue.UnloadPoint30
                    });
                    break; 
                case (long)NodeIdValue.CentralLaboratoryProcess1:
                case (long)NodeIdValue.CentralLaboratoryProcess2:
                case (long)NodeIdValue.CentralLaboratoryProcess3:
                    nodeList.AddRange(new List<long> 
                    {
                        (long) NodeIdValue.CentralLaboratoryGetOil1,
                        (long) NodeIdValue.CentralLaboratoryGetOil2,
                        (long) NodeIdValue.CentralLaboratoryGetShrot,
                        (long) NodeIdValue.CentralLaboratoryGetCustomStore,
                        (long) NodeIdValue.CentralLaboratoryGetTruckRamp
                    });
                    break; 
                default:
                    nodeList.AddRange(new List<long> { nodeId });
                    break; 
            }

            return GetNodesContainers(nodeList.Any() ? nodeList : null);
        }

        private IEnumerable<long> GetNodesContainers(List<long> nodeList)
        {
            return _ticketContainerRegistryManager
                   .GetActiveTicketContainers(nodeList)
                   .ToList();
        }
        
        private IEnumerable<long> FilterSingleWindowTicketContainers(int nodeId)
        {
            var filterId = NodeFilters[nodeId];
            var result = new List<long>();
            
            var tickets = 
                (from card in _context.Cards
                    join ticket in _context.Tickets on card.TicketContainerId equals ticket.ContainerId 
                    where card.TicketContainerId.HasValue
                        && (filterId == 0 || (ticket.StatusId == Dom.Ticket.Status.Processing
                                          || ticket.StatusId == Dom.Ticket.Status.ToBeProcessed
                                          || ticket.StatusId == Dom.Ticket.Status.New))
                    select ticket)
                .GroupBy(x => x.ContainerId)
                .Select(x => x.OrderByDescending(z => z.StatusId)
                    .FirstOrDefault())
                .ToList();

            foreach (var ticket in tickets)
            {
                if (ticket == null) continue;
                
                if (filterId == 0)
                {
                    result.Add(ticket.ContainerId);
                    continue;
                }

                var opData = _opDataRepository.GetLastOpData(ticket.Id);

                switch (filterId)
                {
                    case (int)SingleWindowRegisterFilter.InQueue:
                        if (opData.Node.Id == (long) NodeIdValue.SingleWindowFirst 
                            || opData.Node.Id == (long) NodeIdValue.SingleWindowSecond 
                            || opData.Node.Id == (long) NodeIdValue.SingleWindowThird) result.Add(ticket.ContainerId);
                        break;
                    case (int)SingleWindowRegisterFilter.Inside:
                        if (opData.Node.Id != (long) NodeIdValue.SingleWindowFirst 
                            && opData.Node.Id != (long) NodeIdValue.SingleWindowSecond
                            && opData.Node.Id != (long) NodeIdValue.SingleWindowThird
                            && opData.Node.Id != (long) NodeIdValue.SecurityOut2
                            && opData.Node.Id != (long) NodeIdValue.SecurityOut1) result.Add(ticket.ContainerId);
                        break;
                    case (int)SingleWindowRegisterFilter.Completed:
                        if (ticket.StatusId == Dom.Ticket.Status.Closed 
                            || opData.Node.Id == (long) NodeIdValue.SecurityOut2 
                            || opData.Node.Id == (long) NodeIdValue.SecurityOut1) result.Add(ticket.ContainerId);
                        break;
                }
            }

            return result;
        }
    }
}
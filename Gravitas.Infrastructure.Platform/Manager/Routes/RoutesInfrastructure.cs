using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel.RouteEditor;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Infrastructure.Platform.Manager.Routes
{
    public class RoutesInfrastructure : IRoutesInfrastructure
    {
        private readonly IOpDataRepository _opDataRepository;
        private readonly IRoutesRepository _routesRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly INodeManager _nodeManager;
        private readonly IConnectManager _connectManager;
        private readonly GravitasDbContext _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public RoutesInfrastructure(ITicketRepository ticketRepository,
            IOpDataRepository opDataRepository,
            IRoutesRepository routesRepository, 
            INodeManager nodeManager, 
            IConnectManager connectManager, 
            GravitasDbContext context)
        {
            _ticketRepository = ticketRepository;
            _opDataRepository = opDataRepository;
            _routesRepository = routesRepository;
            _nodeManager = nodeManager;
            _connectManager = connectManager;
            _context = context;
        }

        public string MarkPassedNodes(long ticketId, long routeId, bool disableAppend = false)
        {
            var route = _routesRepository.GetRoute(routeId).RouteConfig.DeserializeRoute();
            var nodeId = _opDataRepository.GetLastOpData(ticketId).NodeId;
            if (nodeId == null) return null;

            route.DisableAppend = disableAppend;

            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null) return route.SerializeRoute();

            var i = 0;
            foreach (var group in route.GroupDictionary.Values)
            {
                group.Active = false;
                i++;
                if (i == ticket.RouteItemIndex) break;
            }

            return route.SerializeRoute();
        }

        public bool IsNodeAvailable(long nodeId, long routeTemplateId)
        {
            var template = _routesRepository.GetRoute(routeTemplateId);
            if (template == null)
            {
                return false;
            }
            var route = template
                .RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .Select(groupItem => groupItem
                    .Value
                    .NodeList
                    .Select(item => item.Id)
                    .ToList())
                .ToList();
            foreach (var r in route)
                if (r.Contains(nodeId))
                    return true;

            return false;
        }

        public bool IsTicketRejected(long ticketId)
        {
            var isRejected = _opDataRepository.GetLastOpData<CentralLabOpData>(ticketId, null)?.StateId == Dom.OpDataState.Rejected
                             || _opDataRepository.GetLastOpData<LabFacelessOpData>(ticketId, null)?.StateId == Dom.OpDataState.Rejected
                             || _opDataRepository.GetLastOpData<ScaleOpData>(ticketId, null)?.StateId == Dom.OpDataState.Rejected
                             || _opDataRepository.GetLastOpData<MixedFeedLoadOpData>(ticketId, null)?.StateId == Dom.OpDataState.Rejected
                             || _opDataRepository.GetLastOpData<LoadPointOpData>(ticketId, null)?.StateId == Dom.OpDataState.Rejected;
            return isRejected;
        }
        
        private int GetRouteLength(long routeTemplateId)
        {
            var length = _routesRepository
                .GetRoute(routeTemplateId)
                .RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .Count;

            return length;
        }

        public IEnumerable<RouteTemplate> GetRouteTemplates(int status, long? nodeId)
        {
            var result = new List<RouteTemplate>();
            switch (status)
            {
                case Dom.Route.Type.SingleWindow:
                    result.AddRange(_context.RouteTemplates.Where(item => item.IsMain).ToList());
                    break;
           
            }

            return result;
        }

        public string NormalizeRoute(string routeJson)
        {
            var route = routeJson.DeserializeRoute();
            route.DisableAppend = false;

            foreach (var group in route.GroupDictionary.Values)
            {
                group.Active = true;
                group.QuotaEnabled = true;
            }

            return route.SerializeRoute();
        }

        public void MoveForward(long ticketId, long nodeId)
        {
            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);

            var supplyCode = _context.SingleWindowOpDatas.Where(x => x.TicketId == ticketId)
                                                         .Select(c => c.SupplyCode)
                                                         .First();

            var nodeIsWeightbridge = nodeId == (long)NodeIdValue.Weighbridge5 || nodeId == (long)NodeIdValue.Weighbridge1 || nodeId == (long)NodeIdValue.Weighbridge2;

            var isTechRouteWeightbridge = supplyCode == Dom.SingleWindowOpData.TechnologicalSupplyCode && nodeIsWeightbridge;

            if (ticket == null || (ticket.NodeId == nodeId && !isTechRouteWeightbridge)) return;
            ticket.NodeId = nodeId;
            if (ticket.SecondaryRouteTemplateId == null)
            {
                ticket.RouteItemIndex += 1;
                _logger.Info($"RouteIndex incremented to {ticket.RouteItemIndex} on ticketId {ticket.Id}");
                _ticketRepository.Update<Ticket, long>(ticket);
                return;
            }
            
            if (ticket.SecondaryRouteItemIndex == GetRouteLength(ticket.SecondaryRouteTemplateId.Value) - 1)
            {
                ticket.SecondaryRouteTemplateId = null;
                ticket.SecondaryRouteItemIndex = 0;
            }

            if (ticket.SecondaryRouteTemplateId.HasValue)
            {
                ticket.SecondaryRouteItemIndex += 1;
                _logger.Info($"RouteIndex incremented to {ticket.SecondaryRouteItemIndex} on ticketId {ticket.Id}");
            }
            
            _ticketRepository.Update<Ticket, long>(ticket);
        }


        public void MoveBack(long ticketId)
        {
            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null) return;
            
            if (ticket.SecondaryRouteTemplateId == null)
            {
                ticket.RouteItemIndex -= 1;
                _logger.Info($"RouteIndex decremented to {ticket.RouteItemIndex} on ticketId {ticket.Id}");
                _ticketRepository.Update<Ticket, long>(ticket);
                return;
            }
            
            if (ticket.SecondaryRouteItemIndex == GetRouteLength(ticket.SecondaryRouteTemplateId.Value))
            {
                ticket.SecondaryRouteTemplateId = null;
                ticket.SecondaryRouteItemIndex = 0;
            }

            if (ticket.SecondaryRouteTemplateId.HasValue)
            {
                ticket.SecondaryRouteItemIndex -= 1;
                _logger.Info($"RouteIndex decremented to {ticket.SecondaryRouteItemIndex} on ticketId {ticket.Id}");
            }
            
            _ticketRepository.Update<Ticket, long>(ticket);
        }

        public bool SetSecondaryRoute(long ticketId, long nodeId, int type)
        {
            _logger.Info($"SetSecondaryRoute: {ticketId}");

            var secondaryRouteData = GetSecondaryRouteData(ticketId, nodeId, type);

            if (secondaryRouteData != null)
            {
                return ChangeRoute(ticketId, nodeId, secondaryRouteData, type);
            }

            return false;
        }

        private SecondaryRoute GetSecondaryRouteData(long ticketId, long nodeId, int routeType)
        {
            _logger.Debug($"GetSecondaryRouteData. TicketId =  {ticketId}");
            _logger.Debug($"GetSecondaryRouteData. NodeId =  {nodeId}");
            _logger.Debug($"GetSecondaryRouteData. RouteType =  {routeType}");

            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);
            if (ticket?.RouteTemplateId == null) return null;

            var route = _routesRepository
                .GetRoute(ticket.RouteTemplateId.Value)
                .RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .Select(x => x.Value.NodeList.ToList())
                .ToList();

            _logger.Debug($"GetSecondaryRouteData. RouteItemIndex =  {ticket.RouteItemIndex}");
            _logger.Debug($"GetSecondaryRouteData. Route[ticket.RouteItemIndex] =  {string.Join(",", route[ticket.RouteItemIndex].Select(x => x.Id))}");

            var node = route[ticket.RouteItemIndex].SingleOrDefault(x => x.Id == nodeId);
            if (node == null)
            {
                return null;
            }

            switch (routeType)
            {
                case Dom.Route.Type.Reload:
                    return node.ReloadRoute;
                case Dom.Route.Type.PartLoad:
                    return node.PartLoadRoute;
                case Dom.Route.Type.PartUnload:
                    return node.PartUnloadRoute;   
                case Dom.Route.Type.Move:
                    return node.MoveRoute; 
                case Dom.Route.Type.Reject:
                    return node.RejectRoute;
                default:
                    return null;
            }
        }

        public List<RouteNodes> GetRouteForPrintout(long ticketId)
        {
            var result = new List<RouteNodes>();
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null) return result;

            var routes = _context.Tickets
                .Where(x => x.ContainerId == ticket.ContainerId
                   && x.StatusId != Dom.Ticket.Status.Canceled
                   && x.RouteTemplateId.HasValue)
                .OrderBy(x => x.OrderNo)
                .Select(x => x.RouteTemplateId.Value)
                .ToList();

            for (var i = 0; i < routes.Count; i++)
            {
                var nodes = _routesRepository
                    .GetRoute(routes[i])
                    .RouteConfig
                    .DeserializeRoute()
                    .GroupDictionary
                    .Select(groupItem => groupItem
                        .Value
                        .NodeList
                        .Select(x => x.Id)
                        .ToList())
                    .ToList();

                for (var j = 0; j < nodes.Count; j++)
                {
                    if (i != routes.Count - 1 && j == nodes.Count - 1) continue;
                    if (i > 0 && j == 0)
                    {
                        var groups = _routesRepository
                            .GetRoute(routes[i])
                            .RouteConfig
                            .DeserializeRoute()
                            .GroupDictionary
                            .Select(groupItem => groupItem.Value.GroupId)
                            .ToList();

                        for (var k = 0; k < groups.Count - 1; k++)
                        {
                            if (groups[k] == (long) NodeGroup.WeighBridge)
                            {
                                j = k;
                                break;
                            }
                        }
                    }

                    result.Add(new RouteNodes
                    {
                        NodeTitles = new List<NodeTitle>()
                    });

                    var nodeTitles = nodes[j].Select(x => new NodeTitle
                    {
                        Title = _nodeManager.GetNodeName(x)
                    }).ToList();
                    
                    result.Last().NodeTitles.AddRange(nodeTitles);
                }
            }
            return result;
        }

        public int GetNodeProcess(long ticketId, long nodeId)
        {
            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null) throw new ArgumentException($"No route with ticket: {ticketId}");
            var route = _routesRepository
                .GetRoute((long) (ticket.SecondaryRouteTemplateId ?? ticket.RouteTemplateId))
                .RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .Select(groupItem => groupItem
                    .Value
                    .NodeList
                    .Select(x => new
                    {
                        x.Id,
                        x.ProcessId
                    })
                    .ToList())
                .ToList();

            var process =  route[ticket.SecondaryRouteTemplateId.HasValue ? ticket.SecondaryRouteItemIndex : ticket.RouteItemIndex]
                .SingleOrDefault(x => x.Id == nodeId);
            
            _logger.Trace($"GetNodeProcess: Process = {process}, TicketId = {ticketId}, NodeId = {nodeId}");

            return process?.ProcessId ?? 0;
        }

        public bool IsLastScaleProcess(long ticketId)
        {
            var result = true;
            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null) throw new ArgumentException($"No route with ticket: {ticketId}");
            var route = _routesRepository
                .GetRoute(ticket.RouteTemplateId.Value)
                .RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .ToList();

            for (var i = ticket.RouteItemIndex + 1; i < route.Count; i++)
            {
                if (route[i].Value.GroupId == (long) NodeGroup.WeighBridge) result = false;
            }

            _logger.Debug($"IsLastScaleProcess: result = {result}, TicketId = {ticketId}");

            return result;
        }
        
        public bool IsRouteWithoutGuide(long ticketId)
        {
            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);
            if (ticket?.RouteTemplateId == null) throw new ArgumentException($"No route with ticket: {ticketId}, or ticket has no route");
            var loadGuide = GetNodesInGroup(ticket.SecondaryRouteTemplateId ?? ticket.RouteTemplateId, NodeGroup.LoadGuide);
            var unloadGuide = GetNodesInGroup(ticket.SecondaryRouteTemplateId ?? ticket.RouteTemplateId, NodeGroup.UnloadGuide);
            var result = loadGuide == null && unloadGuide == null;
            _logger.Info($"IsRouteWithoutGuide: ticketId: {ticket.Id}, result: {result}");
            return result;
        }

        public void AssignSingleUnloadPoint(long ticketId, long nodeId)
        {
            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null) throw new ArgumentException($"No route with ticket: {ticketId}");
            if (ticket.SecondaryRouteTemplateId.HasValue)
            {
                return;
            }
            
            var route = _routesRepository
                .GetRoute(ticket.RouteTemplateId.Value)
                .RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .ToList();
            
            var unloadNodes = new List<long>();
            for (var i = ticket.RouteItemIndex; i < route.Count; i++)
            {
                if (route[i].Value.GroupId == (long) NodeGroup.Unload || route[i].Value.GroupId == (long) NodeGroup.UnloadGuide)
                {
                    unloadNodes.AddRange(route[i].Value.NodeList.Select(x => x.Id));
                }
            }
            
            _logger.Info($"AssignSingleUnloadPoint: ticketId: {ticket.Id}, unloadNodes: {string.Join(",", unloadNodes)}");
            
            var unloadPoints = unloadNodes
                .Select(x => _context.Nodes.First(z => z.Id == x))
                .Where(x => x.OpRoutineId == Dom.OpRoutine.UnloadPointType1.Id)
                .Select(x => x.Id)
                .ToList();
            
            var unloadGuidePoints = unloadNodes
                .Select(x => _context.Nodes.First(z => z.Id == x))
                .Where(x => x.OpRoutineId == Dom.OpRoutine.UnloadPointGuide.Id)
                .Select(x => x.Id)
                .ToList();
            
            _logger.Info($"AssignSingleUnloadPoint: ticketId: {ticket.Id}, unloadPoints: {string.Join(",", unloadPoints)}, unloadGuidePoints: {string.Join(",", unloadGuidePoints)}");

            if (unloadPoints.Count == 1 && !unloadGuidePoints.Any())
            {
                var u = unloadPoints.First();
                var unloadGuide = _context.UnloadGuideOpDatas.FirstOrDefault(x => x.TicketId == ticketId && x.UnloadPointNodeId == u);
                if (unloadGuide == null)
                {
                    var unloadGuideOpData = new UnloadGuideOpData
                    {
                        StateId = Dom.OpDataState.Processed,
                        NodeId =  nodeId,
                        TicketId = ticketId,
                        TicketContainerId = ticket.ContainerId,
                        CheckInDateTime = DateTime.Now,
                        CheckOutDateTime = DateTime.Now,
                        UnloadPointNodeId = u
                    };
                    _opDataRepository.Add<UnloadGuideOpData, Guid>(unloadGuideOpData);
                
                    _logger.Info($"AssignSingleUnloadPoint: Added unload op data: {ticket.Id}");
                }
                else
                {
                    _logger.Info($"AssignSingleUnloadPoint: Unload already added: {ticket.Id}, unloadGuide: {unloadGuide.Id}");
                }
            }
        }

        public void AddDestinationOpData(long ticketId, long nodeId)
        {
            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);
            if (ticket?.RouteTemplateId == null) throw new ArgumentException($"No route with ticket: {ticketId}, or ticket has no route");
            var loadPoints = GetNodesInGroup(ticket.SecondaryRouteTemplateId ?? ticket.RouteTemplateId, NodeGroup.Load);
            var unloadPoints = GetNodesInGroup(ticket.SecondaryRouteTemplateId ?? ticket.RouteTemplateId, NodeGroup.Unload);
            
            _logger.Info($"AddDestinationOpData: ticketId: {ticket.Id}");
            if (loadPoints != null && loadPoints.Count == 1)
            {
                var loadGuideOpData = new LoadGuideOpData
                {
                    StateId = Dom.OpDataState.Processed,
                    NodeId = nodeId,
                    TicketId = ticketId,
                    TicketContainerId = ticket.ContainerId,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now,
                    LoadPointNodeId = loadPoints.First()
                };
                _opDataRepository.Add<LoadGuideOpData, Guid>(loadGuideOpData);
                
                _logger.Info($"AddDestinationOpData: Added load op data: {ticket.Id}");

            }
            if (unloadPoints != null && unloadPoints.Count == 1)
            {
                var unloadGuideOpData = new UnloadGuideOpData
                {
                    StateId = Dom.OpDataState.Processed,
                    NodeId = nodeId,
                    TicketId = ticketId,
                    TicketContainerId = ticket.ContainerId,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now,
                    UnloadPointNodeId = unloadPoints.First()
                };
                _opDataRepository.Add<UnloadGuideOpData, Guid>(unloadGuideOpData);
                
                _logger.Info($"AddDestinationOpData: Added unload op data: {ticket.Id}");

            }
        }

        public List<long> GetNodesInGroup(long? routeId, NodeGroup groupId)
        {
            if (!routeId.HasValue) return null;

            return _routesRepository
                .GetRoute(routeId.Value)
                .RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .Where(x => x.Value.GroupId == (int) groupId)
                .Select(groupItem => groupItem
                    .Value
                    .NodeList
                    .Select(item => item.Id)
                    .ToList())
                .FirstOrDefault();
        }
        
        public List<long> GetNextNodes(long ticketId)
        {
            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null) return null;
            var isSecondary = ticket.SecondaryRouteTemplateId.HasValue;

            var route = _routesRepository
                .GetRoute((long) (isSecondary ? ticket.SecondaryRouteTemplateId : ticket.RouteTemplateId))
                .RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .Select(groupItem => groupItem
                    .Value
                    .NodeList
                    .Select(item => item.Id)
                    .ToList())
                .ToList();

            var opData = _opDataRepository.GetOpDataList(ticketId)
                .OrderByDescending(item => item.CheckOutDateTime)
                .FirstOrDefault(item => item.StateId != Dom.OpDataState.Canceled);

            var singleWindowOpData = _context.SingleWindowOpDatas.FirstOrDefault(x => x.TicketId == ticketId);
            if (singleWindowOpData?.DocumentTypeId == Dom.ExternalData.DeliveryBill.Type.Incoming && ticket.RouteType != Dom.Route.Type.Reject)
            {
                switch (opData?.Node.Id)
                {
                    case (int) NodeIdValue.Laboratory3:
                        return new List<long>
                        {
                            (int) NodeIdValue.Weighbridge3
                        };
                    case (int) NodeIdValue.Laboratory4:
                        return new List<long>
                        {
                            (int) NodeIdValue.Weighbridge4
                        };
                }
            }

            return route[Math.Min(isSecondary ? ticket.SecondaryRouteItemIndex : ticket.RouteItemIndex, route.Count - 1)];
        }

        private bool ChangeRoute(long ticketId, long nodeId, SecondaryRoute secondaryRouteData, int routeType)
        {
            _logger.Debug($"ChangeRoute. secondaryRouteData.RouteId =  {secondaryRouteData.RouteId}");
            _logger.Debug($"ChangeRoute. secondaryRouteData.ReturnRouteGroup =  {secondaryRouteData.ReturnRouteGroup}");
            var singleWindowOpData = _opDataRepository.GetFirstOrDefault<SingleWindowOpData, Guid>(item => item.TicketId == ticketId);
            if (singleWindowOpData == null)
            {
                _logger.Error($"Single window is null item TicketId {ticketId}");
                return false;
            }

            var ticket = _ticketRepository.GetEntity<Ticket, long>(ticketId);
            if (secondaryRouteData.RouteId != 0) ticket.SecondaryRouteTemplateId = secondaryRouteData.RouteId;
            ticket.RouteItemIndex = secondaryRouteData.ReturnRouteGroup;
            ticket.RouteType = routeType;
            ticket.NodeId = nodeId;
            _ticketRepository.Update<Ticket, long>(ticket);

            var nonStandardOpData = new NonStandartOpData
            {
                NodeId = nodeId,
                TicketContainerId = ticket.ContainerId,
                TicketId = ticketId,
                CheckInDateTime = DateTime.Now,
                CheckOutDateTime = DateTime.Now,
                StateId = Dom.OpDataState.Processed,
                Message = "Маршрут змінено."
            };
            _opDataRepository.Add<NonStandartOpData, Guid>(nonStandardOpData);

            if (!_connectManager.SendSms(Dom.Sms.Template.RouteChangeSms, ticketId, singleWindowOpData.ContactPhoneNo))
            {
                _logger.Error($"RotesInfrastructure. ChangeRoute: Sms hasn`t been sent on {singleWindowOpData.ContactPhoneNo}");

            }
            else
            {
                _logger.Info($"RotesInfrastructure. ChangeRoute: Sms was sent on {singleWindowOpData.ContactPhoneNo}");
            }

            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.DAO;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public partial class QueueManager
    {
        private static readonly object Locker = new object();
        
        public bool IsAllowedEnterTerritory(long ticketId)
        {
            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null) throw new Exception($"Non-existing ticketId: {ticketId}");
            
            var registerData = _opDataRepository.GetSingleOrDefault<QueueRegister, long>(t => t.TicketContainerId == ticket.ContainerId);
            var result = registerData?.IsAllowedToEnterTerritory;
            
            _logger.Debug($"IsAllowedEnterTerritory {result} for container id : {ticket.ContainerId}. ");
            
            return result ?? false;
        }

        public void RemoveFromQueue(long ticketContainerId)
        {
            _logger.Info($"RemoveFromQueue: ticketContainer = {ticketContainerId}");
            var route = _inTirs.SingleOrDefault(s => s.TicketContainerId == ticketContainerId) ?? _externalQueue.Get(ticketContainerId);
            _inTirs.Remove(route);

            _queueRegisterRepository.RemoveFromQueue(ticketContainerId);
            _queueLoadBalancer.RemoveRoute(ticketContainerId);
            _externalQueue.Remove(ticketContainerId);
        }
        
        private List<long> TicketsRoutes =>
            _context.QueueRegisters
                .AsNoTracking()
                .Where(x => x.TicketContainerId.HasValue 
                    && x.RouteTemplateId.HasValue 
                    && !x.IsAllowedToEnterTerritory)
                .Select(x => x.RouteTemplateId.Value)
                .Distinct()
                .ToList();

        private RouteInfo CreateRoute(Ticket ticket)
        {
            _logger.Info($"CreateRoute for TicketId: {ticket.Id} ");

            var result = new RouteInfo
            {
                GroupAlternativeNodes = new List<GroupAlternativeNodes>(),
                TicketContainerId = ticket.ContainerId,
                ActiveTicketId = ticket.Id,
                CurrentNode = (long) NodeIdValue.SingleWindowFirst
            };

            if (_nodeRepository.GetNodeDto((long?) NodeIdValue.SingleWindowSecond).Context.TicketContainerId == ticket.ContainerId)
                result.CurrentNode = (long) NodeIdValue.SingleWindowSecond;
                    
            if (_nodeRepository.GetNodeDto((long?) NodeIdValue.SingleWindowThird).Context.TicketContainerId == ticket.ContainerId)
                result.CurrentNode = (long) NodeIdValue.SingleWindowThird;
            
            var routeId = ticket.RouteTemplateId;
            if (routeId == null)
            {
                _logger.Debug($"Route not assigned for TicketId: {ticket.Id}");
                throw new Exception("Route not assigned for TicketId: {ticket.Id}");
            }

            result.TicketIds.Add(ticket.Id);
            result.GroupAlternativeNodes.AddRange(GetRouteGroupNodes(routeId.Value));

            return result;
        }

        private IEnumerable<GroupAlternativeNodes> GetRouteGroupNodes(long routeId)
        {
            return _routesRepository
                .GetRoute(routeId)
                .RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .Select(groupItem =>
                    new GroupAlternativeNodes
                    {
                        Nodes = groupItem
                            .Value
                            .NodeList
                            .Select(item => item.Id)
                            .ToList(),
                        QuotaEnabled = groupItem.Value.QuotaEnabled
                    }
                )
                .ToList();
        }

        private void UpdateQuata()
        {
            var nodes = _nodeRepository.GetQuery<Node, long>();
            _queueLoadBalancer.UpdateQuatas(nodes);
        }

        private void UpdateNodePriorityList()
        {
            foreach (var item in _endPointNodeLoad)
            {
                item.Trucks = 0;
                item.LoadInPercent = 0;
            }

            foreach (var inTir in _inTirs)
            {
                var endNodes = GetEndPointNodes(inTir.TicketContainerId);
                if (endNodes == null) continue;
                var nodes = _endPointNodeLoad.Where(item => endNodes.Contains(item.NodeId)).ToList();
                if (nodes.Any())
                    foreach (var node in nodes)
                    {
                        node.Trucks = node.Trucks + 1 / (float)endNodes.Count;
                        var nodeQuote = _queueLoadBalancer.NodesLoad[node.NodeId].MaxQuata;
                        node.LoadInPercent = node.Trucks / nodeQuote * 100;
                    }
            }

            _endPointNodeLoad = _endPointNodeLoad
                .Select(item =>
                {
                    if (item.Trucks > 0)
                    {
                        _logger.Info($"NodePriority: Node: {item.NodeId}. Percent: {item.LoadInPercent}%, {item.Trucks} trucks");
                    }
                    return item;
                })
                .OrderBy(item => item.LoadInPercent)
                .ToList();
        }

        private void SendSms(long ticketContainerId, long ticketId, int smsTemplate)
        {
            if (!_queueRegisterRepository.SMSAlreadySent(ticketContainerId))
            {
                _queueRegisterRepository.OnSMSSending(ticketContainerId);

                if (!_smsSendingEnabled) return;
                try
                {
                    _logger.Debug(!_connectManager.SendSms(smsTemplate, ticketId)
                        ? $"Message to  hasn`t been sent to TicketId: {ticketId} "
                        : $"Message to  has been sent to TicketId: {ticketId} ");
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                }
            }
        }

        private List<long> GetEndPointNodes(long ticketContainerId)
        {
            var ticket = _context.Tickets.FirstOrDefault(x =>
                             x.ContainerId == ticketContainerId && x.StatusId == Dom.Ticket.Status.Processing)
                         ?? _context.Tickets.FirstOrDefault(x =>
                             x.ContainerId == ticketContainerId && x.StatusId == Dom.Ticket.Status.ToBeProcessed);
            if (ticket != null)
            {
                var endpointNode = _opDataRepository.GetLastProcessed<MixedFeedGuideOpData>(ticket.Id)?.LoadPointNodeId
                                   ?? _opDataRepository.GetLastProcessed<UnloadGuideOpData>(ticket.Id)?.UnloadPointNodeId
                                   ?? _opDataRepository.GetLastProcessed<LoadGuideOpData>(ticket.Id)?.LoadPointNodeId;
                if (endpointNode != null) return new List<long> { endpointNode.Value };
            }
            var route = _inTirs.SingleOrDefault(s => s.TicketContainerId == ticketContainerId) ?? _externalQueue.Get(ticketContainerId);
            return route.PathNodes.FirstOrDefault(item =>
            {
                foreach (var endPointNodeLoad in _endPointNodeLoad)
                {
                    if (item.Contains(endPointNodeLoad.NodeId)) return true;
                }

                return false;
            });
        }

        private bool IsNodeAvailable(long nodeId, long routeTemplateId)
        {
            var route = _routesRepository
                .GetRoute(routeTemplateId)
                .RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .Select(groupItem => groupItem
                    .Value
                    .NodeList
                    .Select(item => item.Id)
                    .ToList())
                .ToList();

            return route.Any(r => r.Contains(nodeId));
        }

        private void AddLoad(RouteInfo routeInfo)
        {
            _logger.Info($"Adding load. TicketContainerId: {routeInfo.TicketContainerId}. Node {routeInfo.CurrentNode}");
            var item = _externalQueue.Get(routeInfo.TicketContainerId);
            if (item != null)
            {
                _logger.Info($"Adding load. Moving from external to internal tirs: {routeInfo.TicketContainerId}.");
                _externalQueue.Remove(item);
                _inTirs.Add(item);
            }
            else
            {
                _logger.Info($"Adding load. Route is not in external tirs: {routeInfo.TicketContainerId}.");
                item = _inTirs.SingleOrDefault(t => t.TicketContainerId == routeInfo.TicketContainerId);
                if (item == null)
                {
                    _logger.Info($"Adding load. Route is not in external tirs neither in internal tirs: {routeInfo.TicketContainerId}.");
                    var ticket = _context.Tickets.AsNoTracking().First(x => x.Id == routeInfo.ActiveTicketId);
                    item = CreateRoute(ticket);
                    _inTirs.Add(item);
                }
            }

            _queueLoadBalancer.AddRouteLoad(item);

            if (!_queueRegisterRepository.IsAllowedToEnter(routeInfo.TicketContainerId))
            {
                _logger.Info($"AddLoad. Add information for information board. TicketContainerId: {routeInfo.TicketContainerId}");
                _queueRegisterRepository.CalledFromQueue(routeInfo.TicketContainerId);
            }

            SendSms(routeInfo.TicketContainerId, routeInfo.ActiveTicketId, Dom.Sms.Template.EntranceApprovalSms);
        }

        private void CheckFreeLoad()
        {
            lock (Locker)
            {
                UpdateNodePriorityList();
                _externalQueue.PrintAll();
                var inTirs = _inTirs.Select(x => x.TicketContainerId).ToArray();
                if (inTirs.Length > 0) _logger.Debug($"_InTirs = { string.Join(", ", inTirs)}");
                
                var routesPriority = new Dictionary<long, float>();
                if (TicketsRoutes == null || !TicketsRoutes.Any()) return;

                foreach (var nodePriority in _endPointNodeLoad.Where(x => _nodeRepository.GetNodeDto(x.NodeId).IsActive))
                {
                    var routes = TicketsRoutes
                        .Where(x => IsNodeAvailable(nodePriority.NodeId, x))
                        .ToArray();
                    foreach (var routeId in routes)
                    {
                        if (routesPriority.TryGetValue(routeId, out var percent))
                        {
                            routesPriority[routeId] = (percent + nodePriority.LoadInPercent) / 2;
                        }
                        else
                        {
                            routesPriority.Add(routeId, nodePriority.LoadInPercent); 
                        }
                    }
                }

                foreach (var routeId in routesPriority.OrderBy(x => x.Value))
                {
                    var externalQueue = _externalQueue.GetQueue(routeId.Key);
                    if (externalQueue.Any() && _queueLoadBalancer.IsPlace(externalQueue.First()))
                    {
                        AddLoad(externalQueue.First());
                    }
                }
            }
        }
    }
}
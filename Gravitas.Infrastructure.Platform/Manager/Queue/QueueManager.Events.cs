using System;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainValue;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public partial class QueueManager
    {
        public void OnNodeArrival(long ticketId, long nodeId)
        {
            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(e => e.Id == ticketId);

            if (ticket == null) return;
            _logger.Info($"Arrival to node. TicketContainerId: {ticket.ContainerId}, TicketId: {ticketId}, NodeId:{nodeId}");

            var ticketContainerId = ticket.ContainerId;

            var route = _inTirs.SingleOrDefault(s => s.TicketContainerId == ticketContainerId) ??
                        _externalQueue.Get(ticketContainerId);
            _logger.Info($"Arrival to node. route = {route != null}");

            if (route == null) return;

            route.CurrentNode = nodeId;

            if (_queueNotRecoveryMode) _trafficRepository.OnNodeArrival(
                new TrafficRecord
                {
                    TicketContainerId = ticketContainerId,
                    CurrentNodeId = route.CurrentNode, 
                    EntranceTime = DateTime.Now
                });
            _queueLoadBalancer.UpdateLoadOnNodeArrival(route, ticket.RouteItemIndex);

            if (nodeId == (int) NodeIdValue.SecurityOut2 || nodeId == (int) NodeIdValue.SecurityOut1 || ticket.StatusId == Dom.Ticket.Status.Completed)
            {
                _logger.Info($"Removing from load balancer: {ticketContainerId}");
                _queueLoadBalancer.RemoveRoute(route);
                _inTirs.Remove(route);
            }

            if ((nodeId == (int) NodeIdValue.SecurityIn1 || nodeId == (int) NodeIdValue.SecurityIn2) && _queueNotRecoveryMode)
            {
                _logger.Info($"Removing from information board: {ticketContainerId}");
                _queueRegisterRepository.RemoveFromQueue(ticketContainerId);
            }
            
//            CheckFreeLoad();
        }

        public void OnRouteAssigned(Ticket ticket)
        {
            _logger.Info($"OnRouteAssigned. TicketContainerId: {ticket.ContainerId}");
            var result = CreateRoute(ticket);

            //As this method is called all the time
            if (_inTirs.Any(s => s.TicketContainerId == ticket.ContainerId))
            {
                OnRouteUpdated(ticket);
                return;
            }

            if (_externalQueue.Get(ticket.ContainerId) != null)
            {
                //Remove if it is already in queue
                _externalQueue.Remove(ticket.ContainerId);
            }
            else
            {
                //Put in the history only if this is new registration 
                if (_queueNotRecoveryMode)
                {
                    var record = new TrafficRecord
                    {
                        TicketContainerId = ticket.ContainerId, CurrentNodeId = (long) NodeIdValue.SingleWindowFirst, EntranceTime = DateTime.Now
                    };

                    if (_nodeRepository.GetNodeDto((long?) NodeIdValue.SingleWindowSecond).Context.TicketContainerId == ticket.ContainerId)
                        record.CurrentNodeId = (long) NodeIdValue.SingleWindowSecond;
                    
                    if (_nodeRepository.GetNodeDto((long?) NodeIdValue.SingleWindowThird).Context.TicketContainerId == ticket.ContainerId)
                        record.CurrentNodeId = (long) NodeIdValue.SingleWindowThird;

                    _trafficRepository.OnNodeArrival(record);
                }
            }

            _externalQueue.Add(result);

            if (_queueNotRecoveryMode)
            {
                var singleWindow = _context.SingleWindowOpDatas.AsNoTracking().FirstOrDefault(x => x.TicketId == result.ActiveTicketId);
                if (singleWindow == null) return;
                _queueRegisterRepository.Register(new QueueRegister
                {
                    TicketContainerId = result.TicketContainerId, 
                    RouteTemplateId = ticket.RouteTemplateId,
                    PhoneNumber = singleWindow.ContactPhoneNo
                });
            }
            
//            CheckFreeLoad();
        }
        
        public void OnRouteUpdated(Ticket ticket)
        {
            _logger.Info($"UpdateRoute for TicketContainerId: {ticket.ContainerId} ");

            var newRoute = CreateRoute(ticket);
            var firstTicketId = newRoute.ActiveTicketId;

            //Current node
            var opData = _opDataRepository.GetLastOpData(firstTicketId);

            if (opData == null)
            {
                _logger.Debug($"Opdata is null. Not valid data for TicketContainerId: {firstTicketId}. Clean up data and restart core");
                throw new Exception($"Not valid data for TicketContainerId: {firstTicketId}. Clean up data and restart core.");
            }

            var oldRoute = _inTirs.FirstOrDefault(p => p.TicketContainerId == ticket.ContainerId);
            if (oldRoute != null)
            {
                _inTirs.Remove(oldRoute);
                _queueLoadBalancer.RemoveRoute(oldRoute);
            }

            _inTirs.Add(newRoute);
            _queueLoadBalancer.AddRouteLoad(newRoute);
        }
        
        public void OnImmediateEntranceAccept(long ticketContainerId)
        {
            _logger.Trace($"ImmediateEntrance. TicketContainerId: {ticketContainerId}");
            var ticket = _ticketRepository.GetTicketInContainer(ticketContainerId, Dom.Ticket.Status.ToBeProcessed);
            if (!ticket.RouteTemplateId.HasValue) throw new Exception("Can't add QueueRegister without RouteTemplateId to queue");
            _queueRegisterRepository.Register(new QueueRegister
            {
                TicketContainerId = ticketContainerId, RouteTemplateId = ticket.RouteTemplateId
            });

            var item = CreateRoute(ticket);
            if (item != null) AddLoad(item);
            
//            CheckFreeLoad();
        }
    }
}
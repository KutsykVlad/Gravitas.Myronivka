using System;
using System.Data.Entity;
using System.Linq;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainModel.Traffic.TDO;
using Gravitas.Model.DomainValue;
using TicketStatus = Gravitas.Model.DomainValue.TicketStatus;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public partial class QueueManager
    {
        public void OnNodeArrival(int ticketId, int nodeId)
        {
            var ticket = _context.Tickets.AsNoTracking().FirstOrDefault(e => e.Id == ticketId);

            if (ticket == null) return;
            _logger.Info($"Arrival to node. TicketContainerId: {ticket.TicketContainerId}, TicketId: {ticketId}, NodeId:{nodeId}");

            var ticketContainerId = ticket.TicketContainerId;

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

            if (nodeId == (int) NodeIdValue.SecurityOut2 || nodeId == (int) NodeIdValue.SecurityOut1 || ticket.StatusId == TicketStatus.Completed)
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
        }

        public void OnRouteAssigned(Ticket ticket)
        {
            _logger.Info($"OnRouteAssigned. TicketContainerId: {ticket.TicketContainerId}");
            var result = CreateRoute(ticket);

            //As this method is called all the time
            if (_inTirs.Any(s => s.TicketContainerId == ticket.TicketContainerId))
            {
                OnRouteUpdated(ticket);
                return;
            }

            if (_externalQueue.Get(ticket.TicketContainerId) != null)
            {
                //Remove if it is already in queue
                _externalQueue.Remove(ticket.TicketContainerId);
            }
            else
            {
                //Put in the history only if this is new registration 
                if (_queueNotRecoveryMode)
                {
                    var record = new TrafficRecord
                    {
                        TicketContainerId = ticket.TicketContainerId, CurrentNodeId = (int) NodeIdValue.SingleWindowFirstType1, EntranceTime = DateTime.Now
                    };

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
                    RouteTemplateId = ticket.RouteTemplateId.Value,
                    PhoneNumber = singleWindow.ContactPhoneNo
                });
            }
        }
        
        public void OnRouteUpdated(Ticket ticket)
        {
            _logger.Info($"UpdateRoute for TicketContainerId: {ticket.TicketContainerId} ");

            var newRoute = CreateRoute(ticket);
            var firstTicketId = newRoute.ActiveTicketId;

            //Current node
            var opData = _opDataRepository.GetLastOpData(firstTicketId);

            if (opData == null)
            {
                _logger.Debug($"Opdata is null. Not valid data for TicketContainerId: {firstTicketId}. Clean up data and restart core");
                throw new Exception($"Not valid data for TicketContainerId: {firstTicketId}. Clean up data and restart core.");
            }

            var oldRoute = _inTirs.FirstOrDefault(p => p.TicketContainerId == ticket.TicketContainerId);
            if (oldRoute != null)
            {
                _inTirs.Remove(oldRoute);
                _queueLoadBalancer.RemoveRoute(oldRoute);
            }

            _inTirs.Add(newRoute);
            _queueLoadBalancer.AddRouteLoad(newRoute);
        }
        
        public void OnImmediateEntranceAccept(int ticketContainerId)
        {
            _logger.Trace($"ImmediateEntrance. TicketContainerId: {ticketContainerId}");
            var ticket = _ticketRepository.GetTicketInContainer(ticketContainerId, TicketStatus.ToBeProcessed);
            if (!ticket.RouteTemplateId.HasValue) throw new Exception("Can't add QueueRegister without RouteTemplateId to queue");
            _queueRegisterRepository.Register(new QueueRegister
            {
                TicketContainerId = ticketContainerId, RouteTemplateId = ticket.RouteTemplateId.Value
            });

            var item = CreateRoute(ticket);
            if (item != null) AddLoad(item);
        }
    }
}
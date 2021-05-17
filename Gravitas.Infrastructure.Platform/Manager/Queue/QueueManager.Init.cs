using System.Linq;
using System.Text;
using Gravitas.Model;
using Gravitas.Model.DomainValue;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public partial class QueueManager
    {
        public void RestoreState()
        {
            _smsSendingEnabled = false;
            _queueNotRecoveryMode = false;
            
            _logger.Info("Restoring queue");

            var data = new StringBuilder("Current queue state. Outside: ");

            var containersWithCard = _context.Cards
                .AsNoTracking()
                .Where(x => x.TicketContainerId.HasValue && x.TypeId == Dom.Card.Type.TicketCard)
                .AsEnumerable()
                .Select(x => new
                {
                    TicketContainerId = x.TicketContainerId.Value,
                    Tickets = _context.Tickets.AsNoTracking().Where(z => z.ContainerId == x.TicketContainerId).ToList()
                })
                .OrderBy(x => x.TicketContainerId)
                .ToList();

            var i = containersWithCard.Where(x => x.Tickets.Any(z => z.StatusId == Dom.Ticket.Status.Processing)).ToList();
            
            var o = containersWithCard.Where(x => x.Tickets.Any(z => z.StatusId == Dom.Ticket.Status.ToBeProcessed)
                                                  && x.Tickets.All(z => z.StatusId != Dom.Ticket.Status.Completed 
                                                                        && z.StatusId != Dom.Ticket.Status.Processing 
                                                                        && z.StatusId != Dom.Ticket.Status.Closed)).ToList();

            foreach (var outside in o)
            {
                var ticket = outside.Tickets.FirstOrDefault(x => x.StatusId == Dom.Ticket.Status.ToBeProcessed);
                if (ticket == null)
                {
                    continue;
                }
                if (ticket.RouteTemplateId.HasValue && _routesInfrastructure.IsNodeAvailable((long) NodeIdValue.MixedFeedGuide, ticket.RouteTemplateId.Value))
                {
                    if (ticket.RouteType != Dom.Route.Type.MixedFeedLoad)
                    {
                        _mixedFeedTicketsWaitingList.Add(ticket.Id);
                    }
                    continue;
                }
                
                var opData = _opDataRepository.GetLastOpData(ticket.Id);
                if (opData == null) continue;

                if (opData.Node.Id == (int) NodeIdValue.SingleWindowFirst ||
                    opData.Node.Id == (int) NodeIdValue.SingleWindowSecond ||
                    opData.Node.Id == (int) NodeIdValue.SingleWindowThird ||
                    (opData.Node.Id == (int) NodeIdValue.MixedFeedGuide && ticket.RouteItemIndex == 0))
                {
                    var registerRecord = _nodeRepository.GetSingleOrDefault<QueueRegister, long>(item => item.TicketContainerId == ticket.ContainerId && item.IsAllowedToEnterTerritory);
                    if (registerRecord != null)
                    {
                        var route = CreateRoute(ticket);
                        _inTirs.Add(route);
                        AddLoad(route);
                    }
                }

                if (_inTirs.All(x => x.TicketContainerId != ticket.ContainerId))
                {
                    _externalQueue.Add(CreateRoute(ticket));
                }

                data.Append($"{ticket.ContainerId}, ");
            }

            data.Append("Inside: ");

            foreach (var inside in i)
            {
                var ticket = inside.Tickets.FirstOrDefault(x => x.StatusId == Dom.Ticket.Status.Processing);
                if (ticket == null)
                {
                    continue;
                }
                var opData = _opDataRepository.GetLastOpData(ticket.Id);
                data.Append($"{ticket.ContainerId}, ");

                var route = CreateRoute(_context.Tickets.AsNoTracking().First(x => x.Id == ticket.Id));
                _inTirs.Add(route);
                AddLoad(route);

                if (opData.Node.Id != (int) NodeIdValue.SingleWindowFirst || opData.Node.Id != (int) NodeIdValue.SingleWindowSecond || opData.Node.Id != (int) NodeIdValue.SingleWindowThird)
                    if (route.TicketIds.Any())
                    {
                        OnNodeArrival(ticket.Id, opData.Node.Id);
                    }
            }

            _logger.Debug(data);
            _logger.Debug("Restore. Checking free space.");
            _logger.Info("End of restoring queue");
            
            _queueNotRecoveryMode = true;
            _smsSendingEnabled = true;
            DoAsyncInfiniteChecks();
        }
    }
}
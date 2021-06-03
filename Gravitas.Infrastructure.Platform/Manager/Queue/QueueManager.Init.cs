using System.Linq;
using System.Text;
using Gravitas.Model.DomainModel.Queue.DAO;
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
                .Where(x => x.TicketContainerId.HasValue && x.TypeId == CardType.TicketCard)
                .AsEnumerable()
                .Select(x => new
                {
                    TicketContainerId = x.TicketContainerId.Value,
                    Tickets = _context.Tickets.AsNoTracking().Where(z => z.TicketContainerId == x.TicketContainerId).ToList()
                })
                .OrderBy(x => x.TicketContainerId)
                .ToList();

            var i = containersWithCard.Where(x => x.Tickets.Any(z => z.StatusId == TicketStatus.Processing)).ToList();
            
            var o = containersWithCard.Where(x => x.Tickets.Any(z => z.StatusId == TicketStatus.ToBeProcessed)
                                                  && x.Tickets.All(z => z.StatusId != TicketStatus.Completed 
                                                                        && z.StatusId != TicketStatus.Processing 
                                                                        && z.StatusId != TicketStatus.Closed)).ToList();

            foreach (var outside in o)
            {
                var ticket = outside.Tickets.FirstOrDefault(x => x.StatusId == TicketStatus.ToBeProcessed);
                if (ticket == null)
                {
                    continue;
                }
                if (ticket.RouteTemplateId.HasValue && _routesInfrastructure.IsNodeAvailable((int) NodeIdValue.MixedFeedGuide, ticket.RouteTemplateId.Value))
                {
                    if (ticket.RouteType != RouteType.MixedFeedLoad)
                    {
                        _mixedFeedTicketsWaitingList.Add(ticket.Id);
                    }
                    continue;
                }
                
                var opData = _opDataRepository.GetLastOpData(ticket.Id);
                if (opData == null) continue;

                if (opData.Node.Id == (int) NodeIdValue.SingleWindowFirstType1 ||
                    (opData.Node.Id == (int) NodeIdValue.MixedFeedGuide && ticket.RouteItemIndex == 0))
                {
                    var registerRecord = _nodeRepository.GetSingleOrDefault<QueueRegister, int>(item => item.TicketContainerId == ticket.TicketContainerId && item.IsAllowedToEnterTerritory);
                    if (registerRecord != null)
                    {
                        var route = CreateRoute(ticket);
                        _inTirs.Add(route);
                        AddLoad(route);
                    }
                }

                if (_inTirs.All(x => x.TicketContainerId != ticket.TicketContainerId))
                {
                    _externalQueue.Add(CreateRoute(ticket));
                }

                data.Append($"{ticket.TicketContainerId}, ");
            }

            data.Append("Inside: ");

            foreach (var inside in i)
            {
                var ticket = inside.Tickets.FirstOrDefault(x => x.StatusId == TicketStatus.Processing);
                if (ticket == null)
                {
                    continue;
                }
                var opData = _opDataRepository.GetLastOpData(ticket.Id);
                data.Append($"{ticket.TicketContainerId}, ");

                var route = CreateRoute(_context.Tickets.AsNoTracking().First(x => x.Id == ticket.Id));
                _inTirs.Add(route);
                AddLoad(route);

                if (opData.Node.Id != (int) NodeIdValue.SingleWindowFirstType1)
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
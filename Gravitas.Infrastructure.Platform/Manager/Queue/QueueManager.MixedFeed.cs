using System;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainValue;
using Newtonsoft.Json;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public partial class QueueManager
    {
        public long? GetFreeSiloDrive(string productId, long ticketId)
        {
            var load = _context.MixedFeedSilos
                .AsNoTracking()
                .Where(x => x.ProductId == productId)
                .AsEnumerable()
                .Select(x => new
                {
                    Drive = x.Drive,
                    Order = x.LoadQueue,
                    TrucksInside = _endPointNodeLoad.FirstOrDefault(z => z.NodeId == x.Drive).Trucks
                })
                .Distinct()
                .Where(x => _nodeRepository.GetNodeDto(x.Drive).IsActive)
                .OrderBy(x => x.TrucksInside)
                .ThenBy(x => x.Order)
                .FirstOrDefault();
            
            _logger.Info($"GetFreeSiloDrive. LoadIds: {JsonConvert.SerializeObject(load)}");

            if (!_mixedFeedTicketsWaitingList.Contains(ticketId)) _mixedFeedTicketsWaitingList.Add(ticketId);
 
            return load?.Drive;
        }
        
        public void CheckMixedFeedWaitingList()
        {
            _mixedFeedTicketsWaitingList = _mixedFeedTicketsWaitingList
                .Where(x =>
                {
                    var ticket = _context.Tickets.AsNoTracking().First(z => z.Id == x);
                    return _routesRepository.GetFirstOrDefault<QueueRegister, long>(z => z.TicketContainerId == ticket.ContainerId) == null;
                })
                .ToList();

            if (_mixedFeedTicketsWaitingList.Count > 0)
            {
                _logger.Info("CheckMixedFeedWaitingList. Trucks in Mixed Feed waiting list: " +
                             $"{string.Join(", ", _mixedFeedTicketsWaitingList.Select(x => x))}");
            }
            
            foreach (var ticketId in _mixedFeedTicketsWaitingList)
            {
                var productId = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId)?.ProductId;
                var ticket = _context.Tickets.AsNoTracking().First(x => x.Id == ticketId);

                var nodeId = GetFreeSiloDrive(productId, ticketId);
                if (nodeId.HasValue)
                {
                    var mixedFeedLoadOpData = _opDataRepository.GetLastProcessed<MixedFeedLoadOpData>(ticketId);
                    var mixedFeedGuideOpData = _opDataRepository.GetLastProcessed<MixedFeedGuideOpData>(ticketId);
                    if (mixedFeedGuideOpData == null || mixedFeedGuideOpData.CheckOutDateTime > mixedFeedLoadOpData?.CheckOutDateTime)
                    {
                        mixedFeedGuideOpData = new MixedFeedGuideOpData
                        {
                            StateId = Dom.OpDataState.Processed,
                            NodeId = (long?) NodeIdValue.MixedFeedGuide,
                            TicketId = ticketId,
                            TicketContainerId = ticket.ContainerId,
                            CheckInDateTime = DateTime.Now,
                            CheckOutDateTime = DateTime.Now
                        };
                    }
                    mixedFeedGuideOpData.LoadPointNodeId = nodeId.Value;
                    _opDataRepository.AddOrUpdate<MixedFeedGuideOpData, Guid>(mixedFeedGuideOpData);
                    
                    _connectManager.SendSms(Dom.Sms.Template.DestinationPointApprovalSms, ticketId);
                    
                    _routesInfrastructure.MoveForward(ticketId, (long) NodeIdValue.MixedFeedGuide);

                    if (ticket.RouteItemIndex == 0 && ticket.RouteType == Dom.Route.Type.MixedFeedLoad)
                    {
                        OnRouteAssigned(ticket);
                    }
                    _logger.Info($"CheckMixedFeedWaitingList. AvailableNode: {nodeId} for ticket: {ticketId}");
                }
            }
        }
    }
}
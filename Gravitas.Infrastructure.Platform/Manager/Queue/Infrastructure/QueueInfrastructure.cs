using System;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Queue;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager.Queue.Infrastructure
{
    public class QueueInfrastructure : IQueueInfrastructure
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IOpDataRepository _opDataRepository;
        private readonly IQueueRegisterRepository _queueRegisterRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly GravitasDbContext _context;

        public QueueInfrastructure(ITicketRepository ticketRepository,
            IOpDataRepository opDataRepository,
            IQueueRegisterRepository queueRegisterRepository, 
            GravitasDbContext context)
        {
            _ticketRepository = ticketRepository;
            _opDataRepository = opDataRepository;
            _queueRegisterRepository = queueRegisterRepository;
            _context = context;
        }

        public DateTime GetPredictionEntranceTime(int routeId)
        {
            var processedTickets = _context.Tickets
                .Where(x => x.RouteTemplateId == routeId 
                    && (x.StatusId == TicketStatus.Closed || x.StatusId == TicketStatus.Completed))
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id)
                .Take(3);

            var totalMinutes = new TimeSpan();
            var count = 0;
            foreach (var ticketId in processedTickets)
            {
                var checkInDateTime = _opDataRepository.GetLastProcessed<SecurityCheckInOpData>(ticketId)?.CheckInDateTime;
                var endPointDateTime = _opDataRepository.GetLastProcessed<LoadPointOpData>(ticketId)?.CheckOutDateTime 
                               ?? _opDataRepository.GetLastProcessed<UnloadPointOpData>(ticketId)?.CheckOutDateTime;

                if (!endPointDateTime.HasValue || !checkInDateTime.HasValue)
                {
                    continue;
                }

                count++;
                var processedMinutes = endPointDateTime.Value - checkInDateTime.Value;
                totalMinutes += processedMinutes;
            }
            
            var average = totalMinutes.TotalMilliseconds / count;
            var multiplier = GetTruckCountInQueue(routeId);

            var time = multiplier == 0 || count == 0 ? DateTime.Now : DateTime.Now.AddMilliseconds(average * multiplier);
            if (count != 0) _logger.Info($"GetPredictionEntranceTime: Average processing time for route = {routeId} is {TimeSpan.FromMilliseconds(average)}");
            _logger.Info($"GetPredictionEntranceTime: Queue for route = {routeId} is {multiplier}");
            _logger.Info($"GetPredictionEntranceTime: Time to enter on route {routeId} is {time}");
            return time;
        }

        public int GetTruckCountInQueue(int routeId)
        {
            var queueCount = _context.QueueRegisters.Count(x => x.RouteTemplateId == routeId && !x.IsAllowedToEnterTerritory);

            return queueCount;
        }

        public void ImmediateEntrance(int ticketContainer)
        {
            _logger.Info($"ImmediateEntrance (Web) for {ticketContainer} accepted.");
            var activeTicket = _ticketRepository.GetTicketInContainer(ticketContainer, TicketStatus.ToBeProcessed);
            if (activeTicket == null) return;

            _queueRegisterRepository.Register(new QueueRegister
            {
                TicketContainerId = ticketContainer, RouteTemplateId = activeTicket.RouteTemplateId.Value, IsAllowedToEnterTerritory = true
            });
        }
    }
}
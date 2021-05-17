using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager
{
    public class RoutesManager : IRoutesManager
    {
        private readonly INodeManager _nodeManager;
        private readonly IOpDataRepository _opDataRepository;
        private readonly IQueueManager _queue;
        private readonly IRoutesRepository _routesRepository;
        private readonly IConnectManager _smsManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly GravitasDbContext _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public RoutesManager(IRoutesRepository routesRepository,
            ITicketRepository ticketRepository,
            IOpDataRepository opDataRepository,
            IQueueManager queue, 
            IConnectManager smsManager,
            INodeManager nodeManager,
            IRoutesInfrastructure routesInfrastructure,
            GravitasDbContext context)
        {
            _routesRepository = routesRepository;
            _ticketRepository = ticketRepository;
            _opDataRepository = opDataRepository;
            _queue = queue;
            _smsManager = smsManager;
            _nodeManager = nodeManager;
            _routesInfrastructure = routesInfrastructure;
            _context = context;
        }

        public bool IsNodeNext(long ticketId, long nodeId, out string errorMessage)
        {
            errorMessage = "";
            var ticket = _context.Tickets.AsNoTracking().First(x => x.Id == ticketId);

            if (ticket.RouteTemplateId == null)
            {
                errorMessage = "Дана картка не містить маршрутів";
                return false;
            }

            var next = _routesInfrastructure.GetNextNodes(ticketId);

            var result = next.Contains(nodeId);

            if (result)
            {
                _queue.OnNodeArrival(ticketId, nodeId);
            }
            else
            {
                var lastNode = _opDataRepository.GetLastOpData(ticketId, Dom.OpDataState.Processed);
                var nextNodes = string.Empty;
                foreach (var node in next)
                {
                    nextNodes += _nodeManager.GetNodeName(node);
                    if (next.Last() != node) nextNodes += ", ";
                }

                errorMessage = "Невірний вузол згідно маршруту. " + (lastNode != null ? $"Наступна точка - {nextNodes}" : string.Empty);
            }

            return result;
        }
    }
}
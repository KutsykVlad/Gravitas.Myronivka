using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.Infrastructure.Platform.Manager.Node;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager.Routes
{
    public class RoutesManager : IRoutesManager
    {
        private readonly INodeManager _nodeManager;
        private readonly IOpDataRepository _opDataRepository;
        private readonly IQueueManager _queue;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly GravitasDbContext _context;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public RoutesManager(IOpDataRepository opDataRepository,
            IQueueManager queue, 
            INodeManager nodeManager,
            IRoutesInfrastructure routesInfrastructure,
            GravitasDbContext context)
        {
            _opDataRepository = opDataRepository;
            _queue = queue;
            _nodeManager = nodeManager;
            _routesInfrastructure = routesInfrastructure;
            _context = context;
        }

        public bool IsNodeNext(int ticketId, int nodeId, out string errorMessage)
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
                var lastNode = _opDataRepository.GetLastOpData(ticketId, OpDataState.Processed);
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
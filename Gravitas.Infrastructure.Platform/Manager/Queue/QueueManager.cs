using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public partial class QueueManager : IQueueManager
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        
        private readonly ExternalQueue _externalQueue;
        private readonly QueueLoadBalancer _queueLoadBalancer;
        private readonly IConnectManager _connectManager;
        private readonly INodeRepository _nodeRepository;
        private readonly IOpDataRepository _opDataRepository;
        private readonly IQueueRegisterRepository _queueRegisterRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IRoutesRepository _routesRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITrafficRepository _trafficRepository;
        private readonly GravitasDbContext _context;

        private List<EndPointNodeLoad> _endPointNodeLoad = new List<EndPointNodeLoad>();
        private List<long> _mixedFeedTicketsWaitingList = new List<long>();
        private readonly List<RouteInfo> _inTirs = new List<RouteInfo>();

        private bool _queueNotRecoveryMode;
        private bool _smsSendingEnabled;

        public QueueManager(INodeRepository nodeRepository,
            IConnectManager connectManager,
            IOpDataRepository opDataRepository,
            IRoutesRepository routesRepository,
            ITicketRepository ticketRepository,
            IQueueSettingsRepository queueSettingsRepository,
            ITrafficRepository trafficRepository,
            IExternalDataRepository externalDataRepository,
            IQueueRegisterRepository queueRegisterRepository,
            INodeManager nodeManager,
            IRoutesInfrastructure routesInfrastructure,
            GravitasDbContext context)
        {
            _connectManager = connectManager;
            _opDataRepository = opDataRepository;
            _routesRepository = routesRepository;
            _ticketRepository = ticketRepository;
            _nodeRepository = nodeRepository;
            _queueRegisterRepository = queueRegisterRepository;
            _routesInfrastructure = routesInfrastructure;
            _context = context;
            _trafficRepository = trafficRepository;

            var endPointNodes = nodeManager.GetEndPointNodes();
            foreach (var endPointNode in endPointNodes)
                _endPointNodeLoad.Add(new EndPointNodeLoad
                {
                    NodeId = endPointNode
                });

            _queueLoadBalancer = new QueueLoadBalancer(nodeRepository.GetQuery<Node, long>().ToList(), endPointNodes);
            _externalQueue = new ExternalQueue(queueSettingsRepository, _opDataRepository, externalDataRepository, context);
        }

        private async Task DoAsyncInfiniteChecks()
        {
            while (true)
                try
                {
                    UpdateQuata();
                    CheckMixedFeedWaitingList();
                    CheckFreeLoad();

                    await Task.Delay(60000);
                }
                catch (Exception e)
                {
                    _logger.Error($"Queue was terminated, error: {e}");
                }
        }
    }
}
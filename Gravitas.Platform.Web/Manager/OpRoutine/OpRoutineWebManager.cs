using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.BlackList;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.OpWorkflow.Routes;
using Gravitas.DAL.Repository.OwnTransport;
using Gravitas.DAL.Repository.PhoneInformTicketAssignment;
using Gravitas.DAL.Repository.Phones;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.DAL.Repository.Traffic;
using Gravitas.Infrastructure.Platform.Manager.CentralLaboratory;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.Node;
using Gravitas.Infrastructure.Platform.Manager.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.Scale;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Platform.Web.Manager.CollisionManager;
using Gravitas.Platform.Web.Manager.OpData;
using NLog;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager : IOpRoutineWebManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IBlackListRepository _blackListRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ICentralLaboratoryManager _centralLaboratoryManager;
        private readonly ICollisionWebManager _collisionWebManager;
        private readonly IConnectManager _connectManager;
        private readonly IExternalDataWebManager _externalDataManager;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly INodeManager _nodeManager;
        private readonly INodeRepository _nodeRepository;
        private readonly IOpDataManager _opDataManager;
        private readonly IOpDataRepository _opDataRepository;
        private readonly IOpDataWebManager _opDataWebManager;
        private readonly IOpRoutineManager _opRoutineManager;
        private readonly IPhonesRepository _phonesRepository; 
        private readonly IPhoneInformTicketAssignmentRepository _phoneInformTicketAssignmentRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IRoutesRepository _routesRepository;
        private readonly IScaleManager _scaleManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITrafficRepository _trafficRepository;
        private readonly GravitasDbContext _context;
        private readonly IOwnTransportRepository _ownTransportRepository;

        public OpRoutineWebManager(
            INodeRepository nodeRepository,
            IOpRoutineManager opRoutineManager,
            ICardRepository cardRepository,
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            IRoutesRepository routesRepository,
            IOpDataManager opDataManager,
            IConnectManager connectManager,
            IExternalDataRepository externalDataRepository,
            ICollisionWebManager collisionWebManager,
            IBlackListRepository blackListRepository,
            ITrafficRepository trafficRepository,
            INodeManager nodeManager,
            IOpDataWebManager opDataWebManager,
            IRoutesInfrastructure routesInfrastructure,
            IPhonesRepository phonesRepository,
            ICentralLaboratoryManager centralLaboratoryManager,
            IExternalDataWebManager externalDataManager,
            IScaleManager scaleManager,
            IPhoneInformTicketAssignmentRepository phoneInformTicketAssignmentRepository,
            GravitasDbContext context, 
            IOwnTransportRepository ownTransportRepository)

        {
            _nodeRepository = nodeRepository;
            _opRoutineManager = opRoutineManager;
            _cardRepository = cardRepository;
            _opDataRepository = opDataRepository;
            _ticketRepository = ticketRepository;
            _opDataManager = opDataManager;
            _connectManager = connectManager;
            _externalDataRepository = externalDataRepository;
            _routesRepository = routesRepository;
            _collisionWebManager = collisionWebManager;
            _blackListRepository = blackListRepository;
            _trafficRepository = trafficRepository;
            _nodeManager = nodeManager;
            _opDataWebManager = opDataWebManager;
            _routesInfrastructure = routesInfrastructure;
            _phonesRepository = phonesRepository;
            _centralLaboratoryManager = centralLaboratoryManager;
            _externalDataManager = externalDataManager;
            _scaleManager = scaleManager;
            _phoneInformTicketAssignmentRepository = phoneInformTicketAssignmentRepository;
            _context = context;
            _ownTransportRepository = ownTransportRepository;
        }

        private bool UpdateNodeContext(int nodeId, NodeContext newContext)
        {
            if (newContext.TicketContainerId != null)
                _trafficRepository.OnNodeHandle(newContext.TicketContainerId.Value, nodeId);

            var result = _nodeRepository.UpdateNodeContext(nodeId, newContext);

            if (!result)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, @"Не валідна спроба зміни стану вузла."));
                return false;
            }

            SignalRInvoke.ReloadHubGroup(nodeId);
            return true;
        }

        private void SendWrongContextMessage(int nodeId)
        {
            _opRoutineManager.UpdateProcessingMessage(nodeId,
                new NodeProcessingMsgItem(ProcessingMsgType.Error, @"Хибний контекст вузла"));
        }
    }
}
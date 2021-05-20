using System;
using System.Linq;
using System.Threading;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager.Node;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class MixedFeedLoadOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly INodeManager _nodeManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IRoutesManager _routesManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserManager _userManager;
        private readonly ICardManager _cardManager;
        private DateTime? _cleanupTimeStart;

        public MixedFeedLoadOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            IRoutesManager routesManager, 
            INodeManager nodeManager,
            IRoutesInfrastructure routesInfrastructure, 
            IUserManager userManager, 
            ICardManager cardManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _ticketRepository = ticketRepository;
            _routesManager = routesManager;
            _nodeManager = nodeManager;
            _routesInfrastructure = routesInfrastructure;
            _userManager = userManager;
            _cardManager = cardManager;
        }

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;

            var diValid = config.DI.ContainsKey(NodeData.Config.DI.LoopIncoming);
            var doValid = config.DO.ContainsKey(NodeData.Config.DO.EmergencyOff);
            var rfidValid = config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader)
                            && config.Rfid.ContainsKey(NodeData.Config.Rfid.OnGateReader);

            return diValid && doValid && rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.Workstation:
                    Workstation_Bind(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.Idle:
                    Workstation_Bind(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddOperationVisa:
                    AddOperationVisa(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.Cleanup:
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddCleanupVisa:
                    AddCleanupVisa(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddChangeStateVisa:
                    AddChangeStateVisa(_nodeDto);
                    break;
            }

            CheckForInactive(_nodeDto);
            CheckForCleanup(_nodeDto);
            CheckForCorrectLoad(_nodeDto);
            CheckLoopState(_nodeDto);
        }

    

        private void CheckLoopState(Node nodeDto)
        {
            if (!nodeDto.IsActive || !nodeDto.Context.TicketId.HasValue) return;
            
            _deviceManager.GetLoopState(nodeDto, out var incomingLoopState, out _, 10);
            if (incomingLoopState == false)
            {
                Thread.Sleep(5 * 1000);
                _deviceManager.GetLoopState(nodeDto, out var incomingLoopStateNext, out _, 10);
                
                if (incomingLoopStateNext == false)
                {
                    _opRoutineManager.UpdateProcessingMessage(
                        _context.Nodes.Where(x => x.OrganisationUnitId == nodeDto.OrganisationUnitId)
                            .Select(x => x.Id)
                            .ToList(), 
                        new NodeProcessingMsgItem(
                            NodeData.ProcessingMsg.Type.Error, $"Авто покинуло місце завантаження комбікорму до завершення процедури на {nodeDto.Name}."));

                    _nodeManager.ChangeNodeState(_nodeId, false);
                    Logger.Info($"MixedFeedLoad: CheckLoopState: Node {nodeDto.Name} state changed to false");
                    UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                }
            }
        }

        private void CheckForCorrectLoad(Node nodeDto)
        {
            if (_cleanupTimeStart.HasValue) return;

            var silosData = _context.MixedFeedSilos
                .AsNoTracking()
                .Where(x => x.Drive == nodeDto.Id)
                .AsEnumerable()
                .Select(x => new
                {
                    SiloId = x.Id,
                    x.ProductId,
                    Gates = _context.MixedFeedSiloDevices
                        .AsNoTracking()
                        .Where(z => z.MixedFeedSiloId == x.Id)
                        .Select(g => g.DeviceId)
                        .ToList()
                }).ToList();

            foreach (var data in silosData)
                foreach (var gate in data.Gates)
                {
                    var gateState = (DigitalInState) Program.GetDeviceState(gate);
                    if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, gateState, TimeSpan.FromSeconds(3)))
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, errMsgItem);
                        return;
                    }

                    if (gateState.InData.Value) continue;

                    if (!nodeDto.IsActive
                        || !nodeDto.Context.TicketId.HasValue
                        || _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId).ProductId != data.ProductId)
                    {
                        var lastOpData = _context.NonStandartOpDatas
                            .AsNoTracking()
                            .Where(x => x.NodeId == nodeDto.Id)
                            .OrderByDescending(x => x.CheckInDateTime)
                            .FirstOrDefault();
                        if (lastOpData?.CheckOutDateTime == null || lastOpData.CheckOutDateTime.Value.AddMinutes(1) < DateTime.Now)
                        {
                            Logger.Debug($"Несанкціоноване завантаження з силоса {data.SiloId}.");
                            Logger.Debug($"CheckForCorrectLoad: nodeDto.IsActive = {nodeDto.IsActive}.");
                            Logger.Debug($"CheckForCorrectLoad: nodeDto.Context.TicketId.HasValue = {nodeDto.Context.TicketId.HasValue}.");
                            Logger.Debug($"CheckForCorrectLoad: _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId).ProductId = {_opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context?.TicketId)?.ProductId}.");
                            Logger.Debug($"CheckForCorrectLoad: data.ProductId = {data.ProductId}.");
                            var nonStandardOpData = new NonStandartOpData
                            {
                                NodeId = nodeDto.Id,
                                CheckInDateTime = DateTime.Now,
                                CheckOutDateTime = DateTime.Now,
                                StateId = OpDataState.Processed,
                                Message = $"Несанкціоноване завантаження з силоса {data.SiloId}."
                            };
                            _nodeRepository.Add<NonStandartOpData, Guid>(nonStandardOpData);
                        }

                        _opRoutineManager.UpdateProcessingMessage(
                            _context.Nodes.Where(x => x.OrganisationUnitId == nodeDto.OrganisationUnitId)
                                .Select(x => x.Id)
                                .ToList(), 
                            new NodeProcessingMsgItem(
                                NodeData.ProcessingMsg.Type.Error, $"Несанкціоноване завантаження з силоса {data.SiloId}. Процес заблокований."));

                        
                        if (nodeDto.IsActive)
                        {
                            _nodeManager.ChangeNodeState(_nodeId, false);
                            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                        }
                        return;
                    }

                    var mixedFeedLoadOpData = _opDataRepository.GetLastOpData<MixedFeedLoadOpData>(nodeDto.Context.TicketId.Value, null);
                    if (!mixedFeedLoadOpData.MixedFeedSiloId.HasValue)
                    {
                        mixedFeedLoadOpData.MixedFeedSiloId = data.SiloId;
                        _opDataRepository.Update<MixedFeedLoadOpData, Guid>(mixedFeedLoadOpData);
                    }

                    if (!nodeDto.IsActive) _nodeManager.ChangeNodeState(_nodeId, true);
                }
        }

        private void CheckForInactive(Node nodeDto)
        {
            var turnOffOut = nodeDto.Config.DO[NodeData.Config.DO.EmergencyOff];
            var emergencyOffState = (DigitalOutState) Program.GetDeviceState(turnOffOut.DeviceId);
            if (emergencyOffState.OutData == null) emergencyOffState.OutData = new DigitalOutJsonState();
            
            emergencyOffState.OutData.Value = _cleanupTimeStart.HasValue || nodeDto.Context.TicketId.HasValue;

            Program.SetDeviceOutData(turnOffOut.DeviceId, emergencyOffState.OutData.Value);
        }

        private void CheckForCleanup(Node nodeDto)
        {
            if (_cleanupTimeStart.HasValue && nodeDto.Context.OpProcessData.HasValue
                                           && DateTime.Now > _cleanupTimeStart.Value.AddMinutes(nodeDto.Context.OpProcessData.Value))
            {
                _cleanupTimeStart = null;
                nodeDto.Context.OpProcessData = null;
                _nodeRepository.UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                if (nodeDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDto.OrganisationUnitId.Value);
            }
        }

        private void Workstation_Bind(Node nodeDto)
        {
            if (!nodeDto.IsActive || nodeDto.Context.TicketId.HasValue) return;

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDto);
            if (card == null) { return; }

            var mixedFeedGuideOpData = _opDataRepository.GetLastOpData<MixedFeedGuideOpData>(card.Ticket.Id, OpDataState.Processed);
            var loadPoint = _opDataRepository.GetLastOpData<MixedFeedLoadOpData>(card.Ticket.Id, OpDataState.Processed);
            if (mixedFeedGuideOpData is null || (loadPoint != null && mixedFeedGuideOpData.CheckOutDateTime < loadPoint.CheckOutDateTime))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Info, $"Автомобілю не призначена точка завантаження"));
                return;
            }
            if (mixedFeedGuideOpData.LoadPointNodeId != nodeDto.Id)
            {
                _opRoutineManager.UpdateProcessingMessage(
                    _context.Nodes.Where(x => x.OrganisationUnitId == nodeDto.OrganisationUnitId)
                        .Select(x => x.Id)
                        .ToList(), 
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Info,
                    $"Автомобілю який на {_nodeManager.GetNodeName(nodeDto.Id)} призначена точка завантаження " +
                    $"{_nodeManager.GetNodeName(mixedFeedGuideOpData.LoadPointNodeId)}"));
                _cardManager.SetRfidValidationDO(false, nodeDto);
                return;
            }

            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, errorMessage));
                return;
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDto);

            var mixedFeedLoadOpData = new MixedFeedLoadOpData
            {
                StateId = OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now
            };
            _ticketRepository.Add<MixedFeedLoadOpData, Guid>(mixedFeedLoadOpData);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Idle;
            nodeDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDto.Context.TicketId = card.Ticket.Id;
            nodeDto.Context.OpDataId = mixedFeedLoadOpData.Id;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);

            if (nodeDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDto.OrganisationUnitId.Value);
        }

        private void AddOperationVisa(Node nodeDto)
        {
            if (nodeDto?.Context?.TicketContainerId == null
                || nodeDto.Context?.TicketId == null
                || nodeDto.Context?.OpDataId == null)
                return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var mixedFeedLoad = _context.MixedFeedLoadOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (mixedFeedLoad == null) return;

            switch (mixedFeedLoad.StateId)
            {
                case OpDataState.Rejected:
                    _routesInfrastructure.SetSecondaryRoute(nodeDto.Context.TicketId.Value, nodeDto.Id, RouteType.Reject);
                    break;
                case OpDataState.Canceled:
                    break;
                default:
                    mixedFeedLoad.StateId = OpDataState.Processed;
                    var visa = new OpVisa
                    {
                        DateTime = DateTime.Now,
                        Message = "Підтвердження факту завантаження",
                        MixedFeedLoadOpDataId = nodeDto.Context.OpDataId.Value,
                        EmployeeId = card.EmployeeId,
                        OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddOperationVisa
                    };
                    _nodeRepository.Add<OpVisa, int>(visa);
                    _routesInfrastructure.MoveForward(nodeDto.Context.TicketId.Value, nodeDto.Id);
                    break;
            }

            mixedFeedLoad.CheckOutDateTime = DateTime.Now;
            _opDataRepository.Update<MixedFeedLoadOpData, Guid>(mixedFeedLoad);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Workstation;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            _nodeManager.ChangeNodeState(_nodeId, false);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context, false);
            if (nodeDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDto.OrganisationUnitId.Value);
        }

        private void AddCleanupVisa(Node nodeDto)
        {
            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = $"Запуск очищення на {nodeDto.Context.OpProcessData ?? 0} хв.",
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddCleanupVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _cleanupTimeStart = DateTime.Now;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Workstation;
            _nodeManager.ChangeNodeState(_nodeId, false);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        } 
        
        private void AddChangeStateVisa(Node nodeDto)
        {
            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = $"Вузол {nodeDto.Name} деактивовано",
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddChangeStateVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeManager.ChangeNodeState(nodeDto.Id, false);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Workstation;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
using System;
using System.Collections.Generic;
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
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;

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

        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.Workstation:
                    Workstation_Bind(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.Idle:
                    Workstation_Bind(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddOperationVisa:
                    AddOperationVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.Cleanup:
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddCleanupVisa:
                    AddCleanupVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddChangeStateVisa:
                    AddChangeStateVisa(NodeDetails);
                    break;
            }

            CheckForInactive(NodeDetails);
            CheckForCleanup(NodeDetails);
            CheckForCorrectLoad(NodeDetails);
            CheckLoopState(NodeDetails);
        }

    

        private void CheckLoopState(NodeDetails nodeDetailsDto)
        {
            if (!nodeDetailsDto.IsActive || !nodeDetailsDto.Context.TicketId.HasValue) return;
            
            _deviceManager.GetLoopState(nodeDetailsDto, out var incomingLoopState, out _, 10);
            if (incomingLoopState == false)
            {
                Thread.Sleep(5 * 1000);
                _deviceManager.GetLoopState(nodeDetailsDto, out var incomingLoopStateNext, out _, 10);
                
                if (incomingLoopStateNext == false)
                {
                    _opRoutineManager.UpdateProcessingMessage(
                        _context.Nodes.Where(x => x.OrganizationUnitId == nodeDetailsDto.OrganisationUnitId)
                            .Select(x => x.Id)
                            .ToList(), 
                        new NodeProcessingMsgItem(
                            ProcessingMsgType.Error, $"Авто покинуло місце завантаження комбікорму до завершення процедури на {nodeDetailsDto.Name}."));

                    _nodeManager.ChangeNodeState(_nodeId, false);
                    Logger.Info($"MixedFeedLoad: CheckLoopState: Node {nodeDetailsDto.Name} state changed to false");
                    UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
                }
            }
        }

        private void CheckForCorrectLoad(NodeDetails nodeDetailsDto)
        {
            if (_cleanupTimeStart.HasValue) return;

            var silosData = _context.MixedFeedSilos
                .AsNoTracking()
                .Where(x => x.Drive == nodeDetailsDto.Id)
                .AsEnumerable()
                .Select(x => new
                {
                    SiloId = x.Id,
                    x.ProductId,
                    Gates = GetDevicesForSilo(x.Id)
                }).ToList();

            foreach (var data in silosData)
                foreach (var gate in data.Gates)
                {
                    var gateState = (DigitalInState) Program.GetDeviceState(gate);
                    if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, gateState, TimeSpan.FromSeconds(3)))
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsgItem);
                        return;
                    }

                    if (gateState.InData.Value) continue;

                    if (!nodeDetailsDto.IsActive
                        || !nodeDetailsDto.Context.TicketId.HasValue
                        || _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDetailsDto.Context.TicketId).ProductId != data.ProductId)
                    {
                        var lastOpData = _context.NonStandartOpDatas
                            .AsNoTracking()
                            .Where(x => x.NodeId == nodeDetailsDto.Id)
                            .OrderByDescending(x => x.CheckInDateTime)
                            .FirstOrDefault();
                        if (lastOpData?.CheckOutDateTime == null || lastOpData.CheckOutDateTime.Value.AddMinutes(1) < DateTime.Now)
                        {
                            Logger.Debug($"Несанкціоноване завантаження з силоса {data.SiloId}.");
                            Logger.Debug($"CheckForCorrectLoad: nodeDto.IsActive = {nodeDetailsDto.IsActive}.");
                            Logger.Debug($"CheckForCorrectLoad: nodeDto.Context.TicketId.HasValue = {nodeDetailsDto.Context.TicketId.HasValue}.");
                            Logger.Debug($"CheckForCorrectLoad: _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId).ProductId = {_opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDetailsDto.Context?.TicketId)?.ProductId}.");
                            Logger.Debug($"CheckForCorrectLoad: data.ProductId = {data.ProductId}.");
                            var nonStandardOpData = new NonStandartOpData
                            {
                                NodeId = nodeDetailsDto.Id,
                                CheckInDateTime = DateTime.Now,
                                CheckOutDateTime = DateTime.Now,
                                StateId = OpDataState.Processed,
                                Message = $"Несанкціоноване завантаження з силоса {data.SiloId}."
                            };
                            _nodeRepository.Add<NonStandartOpData, Guid>(nonStandardOpData);
                        }

                        _opRoutineManager.UpdateProcessingMessage(
                            _context.Nodes.Where(x => x.OrganizationUnitId == nodeDetailsDto.OrganisationUnitId)
                                .Select(x => x.Id)
                                .ToList(), 
                            new NodeProcessingMsgItem(
                                ProcessingMsgType.Error, $"Несанкціоноване завантаження з силоса {data.SiloId}. Процес заблокований."));

                        
                        if (nodeDetailsDto.IsActive)
                        {
                            _nodeManager.ChangeNodeState(_nodeId, false);
                            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
                        }
                        return;
                    }

                    var mixedFeedLoadOpData = _opDataRepository.GetLastOpData<LoadPointOpData>(nodeDetailsDto.Context.TicketId.Value, null);
                    if (!mixedFeedLoadOpData.MixedFeedSiloId.HasValue)
                    {
                        mixedFeedLoadOpData.MixedFeedSiloId = data.SiloId;
                        _opDataRepository.Update<LoadPointOpData, Guid>(mixedFeedLoadOpData);
                    }

                    if (!nodeDetailsDto.IsActive) _nodeManager.ChangeNodeState(_nodeId, true);
                }
        }

        private List<int> GetDevicesForSilo(int siloId)
        {
            throw new NotImplementedException();
        }

        private void CheckForInactive(NodeDetails nodeDetailsDto)
        {
            var turnOffOut = nodeDetailsDto.Config.DO[NodeData.Config.DO.EmergencyOff];
            var emergencyOffState = (DigitalOutState) Program.GetDeviceState(turnOffOut.DeviceId);
            if (emergencyOffState.OutData == null) emergencyOffState.OutData = new DigitalOutJsonState();
            
            emergencyOffState.OutData.Value = _cleanupTimeStart.HasValue || nodeDetailsDto.Context.TicketId.HasValue;

            Program.SetDeviceOutData(turnOffOut.DeviceId, emergencyOffState.OutData.Value);
        }

        private void CheckForCleanup(NodeDetails nodeDetailsDto)
        {
            if (_cleanupTimeStart.HasValue && nodeDetailsDto.Context.OpProcessData.HasValue
                                           && DateTime.Now > _cleanupTimeStart.Value.AddMinutes(nodeDetailsDto.Context.OpProcessData.Value))
            {
                _cleanupTimeStart = null;
                nodeDetailsDto.Context.OpProcessData = null;
                _nodeRepository.UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
                if (nodeDetailsDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDetailsDto.OrganisationUnitId.Value);
            }
        }

        private void Workstation_Bind(NodeDetails nodeDetailsDto)
        {
            if (!nodeDetailsDto.IsActive || nodeDetailsDto.Context.TicketId.HasValue) return;

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDetailsDto);
            if (card == null) { return; }

            var mixedFeedGuideOpData = _opDataRepository.GetLastOpData<LoadGuideOpData>(card.Ticket.Id, OpDataState.Processed);
            var loadPoint = _opDataRepository.GetLastOpData<LoadPointOpData>(card.Ticket.Id, OpDataState.Processed);
            if (mixedFeedGuideOpData is null || (loadPoint != null && mixedFeedGuideOpData.CheckOutDateTime < loadPoint.CheckOutDateTime))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    ProcessingMsgType.Info, $"Автомобілю не призначена точка завантаження"));
                return;
            }
            if (mixedFeedGuideOpData.LoadPointNodeId != nodeDetailsDto.Id)
            {
                _opRoutineManager.UpdateProcessingMessage(
                    _context.Nodes.Where(x => x.OrganizationUnitId == nodeDetailsDto.OrganisationUnitId)
                        .Select(x => x.Id)
                        .ToList(), 
                    new NodeProcessingMsgItem(ProcessingMsgType.Info,
                    $"Автомобілю який на {_nodeManager.GetNodeName(nodeDetailsDto.Id)} призначена точка завантаження " +
                    $"{_nodeManager.GetNodeName(mixedFeedGuideOpData.LoadPointNodeId)}"));
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);
                return;
            }

            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDetailsDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error, errorMessage));
                return;
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDetailsDto);

            var mixedFeedLoadOpData = new LoadPointOpData
            {
                StateId = OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now
            };
            _ticketRepository.Add<LoadPointOpData, Guid>(mixedFeedLoadOpData);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Idle;
            nodeDetailsDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDetailsDto.Context.TicketId = card.Ticket.Id;
            nodeDetailsDto.Context.OpDataId = mixedFeedLoadOpData.Id;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);

            if (nodeDetailsDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDetailsDto.OrganisationUnitId.Value);
        }

        private void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketContainerId == null
                || nodeDetailsDto.Context?.TicketId == null
                || nodeDetailsDto.Context?.OpDataId == null)
                return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var mixedFeedLoad = _context.LoadPointOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (mixedFeedLoad == null) return;

            switch (mixedFeedLoad.StateId)
            {
                case OpDataState.Rejected:
                    _routesInfrastructure.SetSecondaryRoute(nodeDetailsDto.Context.TicketId.Value, nodeDetailsDto.Id, RouteType.Reject);
                    break;
                case OpDataState.Canceled:
                    break;
                default:
                    mixedFeedLoad.StateId = OpDataState.Processed;
                    var visa = new OpVisa
                    {
                        DateTime = DateTime.Now,
                        Message = "Підтвердження факту завантаження",
                        LoadPointOpDataId = nodeDetailsDto.Context.OpDataId.Value,
                        EmployeeId = card.EmployeeId,
                        OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddOperationVisa
                    };
                    _nodeRepository.Add<OpVisa, int>(visa);
                    _routesInfrastructure.MoveForward(nodeDetailsDto.Context.TicketId.Value, nodeDetailsDto.Id);
                    break;
            }

            mixedFeedLoad.CheckOutDateTime = DateTime.Now;
            _opDataRepository.Update<LoadPointOpData, Guid>(mixedFeedLoad);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Workstation;
            nodeDetailsDto.Context.TicketContainerId = null;
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.OpDataId = null;
            _nodeManager.ChangeNodeState(_nodeId, false);
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context, false);
            if (nodeDetailsDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDetailsDto.OrganisationUnitId.Value);
        }

        private void AddCleanupVisa(NodeDetails nodeDetailsDto)
        {
            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = $"Запуск очищення на {nodeDetailsDto.Context.OpProcessData ?? 0} хв.",
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddCleanupVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _cleanupTimeStart = DateTime.Now;

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Workstation;
            _nodeManager.ChangeNodeState(_nodeId, false);
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        } 
        
        private void AddChangeStateVisa(NodeDetails nodeDetailsDto)
        {
            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = $"Вузол {nodeDetailsDto.Name} деактивовано",
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddChangeStateVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeManager.ChangeNodeState(nodeDetailsDto.Id, false);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Workstation;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}
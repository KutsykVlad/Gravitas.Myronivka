using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.Core.Valves;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.Manager.Node;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;
using CardReadResult = Gravitas.Core.DeviceManager.Card.CardReadResult;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class UnloadPointType1OpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly INodeManager _nodeManager;
        private readonly IRoutesManager _routesManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly IExternalDataRepository _externalRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly ICardManager _cardManager;
        private readonly IUserManager _userManager;
        private readonly IUnloadPointManager _unloadPointManager;
        private readonly IValveService _valveService;

        public UnloadPointType1OpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            IRoutesManager routesManager,
            INodeManager nodeManager, 
            IExternalDataRepository externalRepository,
            IRoutesInfrastructure routesInfrastructure, 
            ICardManager cardManager, 
            IUserManager userManager,
            IUnloadPointManager unloadPointManager, 
            IValveService valveService) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _ticketRepository = ticketRepository;
            _routesManager = routesManager;
            _nodeManager = nodeManager;
            _externalRepository = externalRepository;
            _routesInfrastructure = routesInfrastructure;
            _cardManager = cardManager;
            _userManager = userManager;
            _unloadPointManager = unloadPointManager;
            _valveService = valveService;
        }

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;
            
            var rfidValid = config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader)
                && (config.Rfid.ContainsKey(NodeData.Config.Rfid.OnGateReader) || config.Rfid.ContainsKey(NodeData.Config.Rfid.LongRangeReader));

            return rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(NodeDetailsDto)) return;

            switch (NodeDetailsDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.UnloadPointType1.State.Workstation:
                    WatchBarrier(NodeDetailsDto);
                    Workstation(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointType1.State.Idle:
                    WatchBarrier(NodeDetailsDto);
                    Workstation(NodeDetailsDto);
                    break;      
                case Model.DomainValue.OpRoutine.UnloadPointType1.State.GetTareValue:
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointType1.State.AddOperationVisa:
                    AddOperationVisa(NodeDetailsDto);
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointType1.State.AddChangeStateVisa:
                    AddChangeStateVisa(NodeDetailsDto);
                    break;
            }
        }

        private void Workstation(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context.TicketContainerId.HasValue || !nodeDetailsDto.IsActive)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Info,
                    $"Вузол {nodeDetailsDto.Id} не активний або зайнятий."));
                return;
            }

            var card = GetTruckTicket(nodeDetailsDto);
            if (card?.Ticket == null) return;
            
            var unloadGuide = _opDataRepository.GetLastOpData<UnloadGuideOpData>(card.Ticket.Id, OpDataState.Processed);
            if (unloadGuide is null) return;
            var unloadPoint = _opDataRepository.GetLastOpData<UnloadPointOpData>(card.Ticket.Id, OpDataState.Processed);
            if (unloadPoint != null && unloadGuide.CheckOutDateTime < unloadPoint.CheckOutDateTime)
            {
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Info,
                    $"Автомобілю не призначена точка вивантаження"));
                return;
            }
            
            if (unloadGuide.UnloadPointNodeId != nodeDetailsDto.Id)
            {
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);

                _opRoutineManager.UpdateProcessingMessage(
                    _context.Nodes.Where(x => x.OrganizationUnitId == nodeDetailsDto.OrganisationUnitId)
                        .Select(x => x.Id)
                        .ToList(), 
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Info,
                        $"Автомобілю який на {_nodeManager.GetNodeName(nodeDetailsDto.Id)} призначена точка розвантаження " +
                        $"{_nodeManager.GetNodeName(unloadGuide.UnloadPointNodeId)}"));
                return;
            }
            
            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDetailsDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDetailsDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, errorMessage));
                return;
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDetailsDto);

            var isOpened = OpenBarrier(NodeDetailsDto);
            if (!isOpened)
            {
                return;
            }
            
            var valve = _valveService.GetUnloadValveByNodeId(_nodeId);
            Logger.Info($"UnloadPoint valve = {valve} for nodeId = {_nodeId}");
            
            var unloadPointOpData = new UnloadPointOpData
            {
                StateId = OpDataState.Init,
                NodeId = _nodeId,
                TicketId = card.Ticket.Id,
                CheckInDateTime = DateTime.Now,
                CheckOutDateTime = DateTime.Now,
                Valve = valve
            };
            _ticketRepository.Add<UnloadPointOpData, Guid>(unloadPointOpData);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.Idle;
            nodeDetailsDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDetailsDto.Context.TicketId = card.Ticket.Id;
            nodeDetailsDto.Context.OpDataId = unloadPointOpData.Id;
            
            if (!UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context)) return;

            if (nodeDetailsDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDetailsDto.OrganisationUnitId.Value);
        }

        private CardReadResult GetTruckTicket(NodeDetails nodeDetailsDto)
        {
            CardReadResult card = null;
            if (nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.LongRangeReader))
            {
                card = _cardManager.GetTruckCardByZebraReader(nodeDetailsDto);
            }

            if (nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.OnGateReader))
            {
                card = _cardManager.GetTruckCardByOnGateReader(nodeDetailsDto);
            }

            return card;
        }

        private void WatchBarrier(NodeDetails nodeDetailsDto)
        {
            if (!nodeDetailsDto.Config.DI.ContainsKey(NodeData.Config.DI.Barrier)) return;
            // Check DI
            var iBarrierConfig = nodeDetailsDto.Config.DI[NodeData.Config.DI.Barrier];

            var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

            if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, iBarrierState, TimeSpan.FromSeconds(3)))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsgItem);
                return;
            }

            if (iBarrierState.InData.Value != true) return;

            var isNewEntryNeeded = !_context.NonStandartOpDatas
                .Where(x => x.NodeId == nodeDetailsDto.Id
                            && x.CheckInDateTime != null)
                .AsNoTracking()
                .AsEnumerable()
                .Any(e =>
                    (DateTime.Now - e.CheckInDateTime.Value).TotalSeconds <
                    GlobalConfigurationManager.NonStandartPassTimeout);

            if (!isNewEntryNeeded) return;

            _opRoutineManager.LogNonStandardOp(nodeDetailsDto, new NonStandartOpData
            {
                NodeId = nodeDetailsDto.Id,
                TicketContainerId = nodeDetailsDto.Context.TicketContainerId,
                CheckInDateTime = DateTime.Now,
                CheckOutDateTime = DateTime.Now,
                StateId = OpDataState.Processed,
                Message = "Несанкціонований проїзд на пункт вивантаження"
            });
        }

        private bool OpenBarrier(NodeDetails nodeDetailsDto)
        {
            // Validate node context
            if (!nodeDetailsDto.Config.DI.ContainsKey(NodeData.Config.DI.Barrier)
                || !nodeDetailsDto.Config.DO.ContainsKey(NodeData.Config.DO.Barrier))
                return true;

            var iBarrierConfig = nodeDetailsDto.Config.DI[NodeData.Config.DI.Barrier];
            var oBarrierConfig = nodeDetailsDto.Config.DO[NodeData.Config.DO.Barrier];

            var oBarrierState = (DigitalOutState) Program.GetDeviceState(oBarrierConfig.DeviceId);
            if (oBarrierState.OutData == null) oBarrierState.OutData = new DigitalOutJsonState();

            Logger.Info($"UnloadPoint try to open barrier NodeId = {nodeDetailsDto.Id}, Device = {iBarrierConfig.DeviceId}");
            var startTime = DateTime.Now;
            
            Program.SetDeviceOutData(oBarrierState.Id, true);
            
            Thread.Sleep(3000);
            while (true)
            {
                var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

                if (iBarrierState?.InData?.Value == true)
                {
                    Thread.Sleep(2000);
                    Logger.Info($"UnloadPoint is opened barrier NodeId = {nodeDetailsDto.Id}, Device = {iBarrierConfig.DeviceId}");
                    break;
                }

                if (DateTime.Now > startTime.AddSeconds(20))
                {
                    Program.SetDeviceOutData(oBarrierState.Id, false);
                    Logger.Info($"UnloadPoint barier timeout = {nodeDetailsDto.Id}, Device = {iBarrierConfig.DeviceId}");
                    _opRoutineManager.UpdateProcessingMessage(
                        _context.Nodes.Where(x => x.OrganizationUnitId == nodeDetailsDto.OrganisationUnitId)
                            .Select(x => x.Id)
                            .ToList(), 
                        new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, $"Помилка відкриття шлагбауму на {nodeDetailsDto.Name}"));
                    
                    return false;
                }
            }

            Program.SetDeviceOutData(oBarrierState.Id, false);
            
            _nodeRepository.ClearNodeProcessingMessage(_nodeId);
            return true;
        }

        private void AddOperationVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.TicketId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;
            
            var singleWindowOpData = _opDataRepository.GetFirstOrDefault<SingleWindowOpData, Guid>(item => item.TicketId == nodeDetailsDto.Context.TicketId);

            singleWindowOpData.CollectionPointId = _externalRepository.GetFirstOrDefault<AcceptancePoint, string>(item =>
                item.Code == nodeDetailsDto.Code)?.Id;
            _opDataRepository.Update<SingleWindowOpData, Guid>(singleWindowOpData);

            var unloadResult = _unloadPointManager.ConfirmUnloadPoint(nodeDetailsDto.Context.TicketId.Value, card.EmployeeId);
            if (!unloadResult) return;
            
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.TicketId.Value);
            if (ticket == null) return;
            _routesInfrastructure.MoveForward(ticket.Id, nodeDetailsDto.Id);
            
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.Workstation;
            nodeDetailsDto.Context.TicketContainerId = null;
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.OpDataId = null;
            _nodeManager.ChangeNodeState(_nodeId, false);
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            if (nodeDetailsDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDetailsDto.OrganisationUnitId.Value); 
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
                OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.AddChangeStateVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeManager.ChangeNodeState(nodeDetailsDto.Id, false);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.Workstation;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}
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
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;
using CardReadResult = Gravitas.Core.DeviceManager.Card.CardReadResult;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

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
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.UnloadPointType1.State.Workstation:
                    WatchBarrier(_nodeDto);
                    Workstation(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointType1.State.Idle:
                    WatchBarrier(_nodeDto);
                    Workstation(_nodeDto);
                    break;      
                case Model.DomainValue.OpRoutine.UnloadPointType1.State.GetTareValue:
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointType1.State.AddOperationVisa:
                    AddOperationVisa(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.UnloadPointType1.State.AddChangeStateVisa:
                    AddChangeStateVisa(_nodeDto);
                    break;
            }
        }

        private void Workstation(Node nodeDto)
        {
            if (nodeDto.Context.TicketContainerId.HasValue || !nodeDto.IsActive)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Info,
                    $"Вузол {nodeDto.Id} не активний або зайнятий."));
                return;
            }

            var card = GetTruckTicket(nodeDto);
            if (card?.Ticket == null) return;
            
            var unloadGuide = _opDataRepository.GetLastOpData<UnloadGuideOpData>(card.Ticket.Id, OpDataState.Processed);
            if (unloadGuide is null) return;
            var unloadPoint = _opDataRepository.GetLastOpData<UnloadPointOpData>(card.Ticket.Id, OpDataState.Processed);
            if (unloadPoint != null && unloadGuide.CheckOutDateTime < unloadPoint.CheckOutDateTime)
            {
                _cardManager.SetRfidValidationDO(false, nodeDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Info,
                    $"Автомобілю не призначена точка вивантаження"));
                return;
            }
            
            if (unloadGuide.UnloadPointNodeId != nodeDto.Id)
            {
                _cardManager.SetRfidValidationDO(false, nodeDto);

                _opRoutineManager.UpdateProcessingMessage(
                    _context.Nodes.Where(x => x.OrganizationUnitId == nodeDto.OrganisationUnitId)
                        .Select(x => x.Id)
                        .ToList(), 
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Info,
                        $"Автомобілю який на {_nodeManager.GetNodeName(nodeDto.Id)} призначена точка розвантаження " +
                        $"{_nodeManager.GetNodeName(unloadGuide.UnloadPointNodeId)}"));
                return;
            }
            
            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDto.Id, out var errorMessage))
            {
                _cardManager.SetRfidValidationDO(false, nodeDto);

                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, errorMessage));
                return;
            }
            
            _cardManager.SetRfidValidationDO(true, nodeDto);

            var isOpened = OpenBarrier(_nodeDto);
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

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.Idle;
            nodeDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDto.Context.TicketId = card.Ticket.Id;
            nodeDto.Context.OpDataId = unloadPointOpData.Id;
            
            if (!UpdateNodeContext(nodeDto.Id, nodeDto.Context)) return;

            if (nodeDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDto.OrganisationUnitId.Value);
        }

        private CardReadResult GetTruckTicket(Node nodeDto)
        {
            CardReadResult card = null;
            if (nodeDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.LongRangeReader))
            {
                card = _cardManager.GetTruckCardByZebraReader(nodeDto);
            }

            if (nodeDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.OnGateReader))
            {
                card = _cardManager.GetTruckCardByOnGateReader(nodeDto);
            }

            return card;
        }

        private void WatchBarrier(Node nodeDto)
        {
            if (!nodeDto.Config.DI.ContainsKey(NodeData.Config.DI.Barrier)) return;
            // Check DI
            var iBarrierConfig = nodeDto.Config.DI[NodeData.Config.DI.Barrier];

            var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

            if (!_deviceRepository.IsDeviceStateValid(out var errMsgItem, iBarrierState, TimeSpan.FromSeconds(3)))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, errMsgItem);
                return;
            }

            if (iBarrierState.InData.Value != true) return;

            var isNewEntryNeeded = !_context.NonStandartOpDatas
                .Where(x => x.NodeId == nodeDto.Id
                            && x.CheckInDateTime != null)
                .AsNoTracking()
                .AsEnumerable()
                .Any(e =>
                    (DateTime.Now - e.CheckInDateTime.Value).TotalSeconds <
                    GlobalConfigurationManager.NonStandartPassTimeout);

            if (!isNewEntryNeeded) return;

            _opRoutineManager.LogNonStandardOp(nodeDto, new NonStandartOpData
            {
                NodeId = nodeDto.Id,
                TicketContainerId = nodeDto.Context.TicketContainerId,
                CheckInDateTime = DateTime.Now,
                CheckOutDateTime = DateTime.Now,
                StateId = OpDataState.Processed,
                Message = "Несанкціонований проїзд на пункт вивантаження"
            });
        }

        private bool OpenBarrier(Node nodeDto)
        {
            // Validate node context
            if (!nodeDto.Config.DI.ContainsKey(NodeData.Config.DI.Barrier)
                || !nodeDto.Config.DO.ContainsKey(NodeData.Config.DO.Barrier))
                return true;

            var iBarrierConfig = nodeDto.Config.DI[NodeData.Config.DI.Barrier];
            var oBarrierConfig = nodeDto.Config.DO[NodeData.Config.DO.Barrier];

            var oBarrierState = (DigitalOutState) Program.GetDeviceState(oBarrierConfig.DeviceId);
            if (oBarrierState.OutData == null) oBarrierState.OutData = new DigitalOutJsonState();

            Logger.Info($"UnloadPoint try to open barrier NodeId = {nodeDto.Id}, Device = {iBarrierConfig.DeviceId}");
            var startTime = DateTime.Now;
            
            Program.SetDeviceOutData(oBarrierState.Id, true);
            
            Thread.Sleep(3000);
            while (true)
            {
                var iBarrierState = (DigitalInState) Program.GetDeviceState(iBarrierConfig.DeviceId);

                if (iBarrierState?.InData?.Value == true)
                {
                    Thread.Sleep(2000);
                    Logger.Info($"UnloadPoint is opened barrier NodeId = {nodeDto.Id}, Device = {iBarrierConfig.DeviceId}");
                    break;
                }

                if (DateTime.Now > startTime.AddSeconds(20))
                {
                    Program.SetDeviceOutData(oBarrierState.Id, false);
                    Logger.Info($"UnloadPoint barier timeout = {nodeDto.Id}, Device = {iBarrierConfig.DeviceId}");
                    _opRoutineManager.UpdateProcessingMessage(
                        _context.Nodes.Where(x => x.OrganizationUnitId == nodeDto.OrganisationUnitId)
                            .Select(x => x.Id)
                            .ToList(), 
                        new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, $"Помилка відкриття шлагбауму на {nodeDto.Name}"));
                    
                    return false;
                }
            }

            Program.SetDeviceOutData(oBarrierState.Id, false);
            
            _nodeRepository.ClearNodeProcessingMessage(_nodeId);
            return true;
        }

        private void AddOperationVisa(Node nodeDto)
        {
            if (nodeDto.Context?.TicketId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;
            
            var singleWindowOpData = _opDataRepository.GetFirstOrDefault<SingleWindowOpData, Guid>(item => item.TicketId == nodeDto.Context.TicketId);

            singleWindowOpData.CollectionPointId = _externalRepository.GetFirstOrDefault<AcceptancePoint, string>(item =>
                item.Code == nodeDto.Code)?.Id;
            _opDataRepository.Update<SingleWindowOpData, Guid>(singleWindowOpData);

            var unloadResult = _unloadPointManager.ConfirmUnloadPoint(nodeDto.Context.TicketId.Value, card.EmployeeId);
            if (!unloadResult) return;
            
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDto.Context.TicketId.Value);
            if (ticket == null) return;
            _routesInfrastructure.MoveForward(ticket.Id, nodeDto.Id);
            
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.Workstation;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            _nodeManager.ChangeNodeState(_nodeId, false);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            if (nodeDto.OrganisationUnitId.HasValue) SignalRInvoke.ReloadHubGroup(nodeDto.OrganisationUnitId.Value); 
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
                OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.AddChangeStateVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeManager.ChangeNodeState(nodeDto.Id, false);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.Workstation;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
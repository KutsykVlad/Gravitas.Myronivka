using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Gravitas.Core.DeviceManager;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.OpWorkflow.Routes;
using Gravitas.DAL.Repository.Phones;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.Manager.Camera;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.Scale;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState.Json;
using Gravitas.Model.DomainModel.MixedFeed.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpDataEvent.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Model.DomainValue.NodeProcesses;
using Newtonsoft.Json;
using ICardManager = Gravitas.Core.DeviceManager.Card.ICardManager;
using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;
using TicketStatus = Gravitas.Model.DomainValue.TicketStatus;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class WeighbridgeOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly ICameraManager _cameraManager;
        private readonly ICardRepository _cardRepository;
        private readonly IConnectManager _connectManager;
        private readonly IRoutesManager _routesManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IPhonesRepository _phonesRepository;
        private readonly IOpDataManager _opDataManager;
        private readonly ICardManager _cardManager;
        private readonly IUserManager _userManager;
        private readonly IScaleManager _scaleManager;
        private readonly IRoutesRepository _routesRepository;
        
        public WeighbridgeOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ICardRepository cardRepository,
            ICameraManager cameraManager,
            ITicketRepository ticketRepository,
            IRoutesManager routesManager,
            IConnectManager connectManager,
            IRoutesInfrastructure routesInfrastructure,
            IPhonesRepository phonesRepository, 
            IOpDataManager opDataManager,
            ICardManager cardManager,
            IUserManager userManager,
            IScaleManager scaleManager,
            IRoutesRepository routesRepository)
            : base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _cardRepository = cardRepository;
            _cameraManager = cameraManager;
            _ticketRepository = ticketRepository;
            _routesManager = routesManager;
            _connectManager = connectManager;
            _routesInfrastructure = routesInfrastructure;
            _phonesRepository = phonesRepository;
            _opDataManager = opDataManager;
            _cardManager = cardManager;
            _userManager = userManager;
            _scaleManager = scaleManager;
            _routesRepository = routesRepository;
        }
        
        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;

            var diValid = config.DI.ContainsKey(NodeData.Config.DI.LoopIncoming)
                          && config.DI.ContainsKey(NodeData.Config.DI.LoopOutgoing);
            var doValid = config.DO.ContainsKey(NodeData.Config.DO.TrafficLightIncoming)
                          && config.DO.ContainsKey(NodeData.Config.DO.TrafficLightOutgoing);
            var rfidValid = config.Rfid.ContainsKey(NodeData.Config.Rfid.OnGateReader);
            var scaleValid = config.Scale.ContainsKey(NodeData.Config.Scale.Scale1);
            var cameraValid = config.Camera.ContainsKey(NodeData.Config.Camera.Camera1);

            return diValid && doValid && rfidValid && scaleValid && cameraValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;
            
            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.Weighbridge.State.Idle:
                    Idle(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.GetScaleZero:
                    GetScaleZero(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.OpenBarrierIn:
                    OpenBarrierIn(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.CheckScaleNotEmpty:
                    CheckScaleNotEmpty(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.GetTicketCard:
                    GetTicketCard(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.DriverTrailerEnableCheck:
                    ValidateTruckPresence(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.GuardianCardPrompt:
                    GetGuardianCard(_nodeDto);
                    ValidateTruckPresence(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.GuardianTruckVerification:
                    ValidateTruckPresence(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.GuardianTrailerEnableCheck:
                    ValidateTruckPresence(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.TruckWeightPrompt:
                    ValidateTruckPresence(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.GetGuardianTruckWeightPermission:
                    GetGuardianCard(_nodeDto);
                    ValidateTruckPresence(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.GetTruckWeight:
                    GetWeight(_nodeDto, true);
                    ValidateTruckPresence(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.TrailerWeightPrompt:
                    ValidateTruckPresence(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.GetGuardianTrailerWeightPermission:
                    GetGuardianCard(_nodeDto);
                    ValidateTruckPresence(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.GetTrailerWeight:
                    GetWeight(_nodeDto, false);
                    ValidateTruckPresence(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.WeightResultsValidation:
                    ValidateWeightResults(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.OpenBarrierOut:
                    OpenBarrierOut(_nodeDto);
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.CheckScaleEmpty:
                    CheckScaleEmpty(_nodeDto);
                    break;
            }
        }

        #region 01_Idle

        private void Idle(Node nodeDto)
        {
            _deviceManager.GetLoopState(nodeDto, out var incomingLoopState, out var outgoingLoopState, 5);

            if (nodeDto.Id == (long) NodeIdValue.Weighbridge5)
            {
                if (incomingLoopState == true && outgoingLoopState == true) return;
            } 
            else if (incomingLoopState != true && outgoingLoopState != true) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.GetScaleZero;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        #region 02_GetScaleZero

        private void GetScaleZero(Node nodeDto)
        {
            var scaleState = _deviceManager.GetScaleState(nodeDto);
            if (scaleState == null || scaleState.InData.IsScaleError || scaleState.InData.Value > 20) return;

            if (scaleState.OutData == null) scaleState.OutData = new ScaleOutJsonState();
            if (!scaleState.OutData.ZeroScaleCmd)
            {
                Program.SetDeviceOutData(nodeDto.Config.Scale.First().Value.DeviceId, true);
            }

            Thread.Sleep(1000);

            _deviceManager.GetLoopState(nodeDto, out _, out var outgoingLoopState, 5);

            Program.SetDeviceOutData(
                outgoingLoopState == (nodeDto.Id != (long) NodeIdValue.Weighbridge5)
                    ? nodeDto.Config.DO[NodeData.Config.DO.TrafficLightOutgoing].DeviceId
                    : nodeDto.Config.DO[NodeData.Config.DO.TrafficLightIncoming].DeviceId, true);
            Logger.Trace($"TurnLightsOn on {nodeDto.Id}");

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.OpenBarrierIn;
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        #region 03_OpenBarrierIn

        private void OpenBarrierIn(Node nodeDto)
        {
            if (nodeDto.Config == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.CheckScaleNotEmpty;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        #region 04_CheckScaleNotEmpty

        private void CheckScaleNotEmpty(Node nodeDto)
        {
            if (!nodeDto.Context.LastStateChangeTime.HasValue) return;
            
            if ((DateTime.Now - nodeDto.Context.LastStateChangeTime.Value).TotalSeconds > GlobalConfigurationManager.WeighbridgeCheckScaleReloadTime)
            {
                TurnLightsOff(nodeDto);
                Logger.Trace($"CheckScaleNotEmpty: Timeout on nodeId = {nodeDto.Id}. nodeDto.Context.LastStateChangeTime.Value = {nodeDto.Context.LastStateChangeTime.Value}");
                Logger.Trace($"CheckScaleNotEmpty: Timeout on nodeId = {nodeDto.Id}. GlobalConfigurationManager.WeighbridgeCheckScaleReloadTime = {GlobalConfigurationManager.WeighbridgeCheckScaleReloadTime}");
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.Idle;
                UpdateNodeContext(nodeDto.Id, nodeDto.Context);
               
                return;
            }
            
            var scaleState = _deviceManager.GetScaleState(nodeDto);
            if (scaleState?.InData == null) return;
            if (scaleState.InData.Value < 300)
            {
                return;
            }

            Logger.Trace($"CheckScaleNotEmpty: Validation on nodeId = {nodeDto.Id} is OK.");

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);

            TurnLightsOff(nodeDto);
            TurnLightsOff(nodeDto);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.GetTicketCard;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion
        
        #region 05_GetTicketCard

        private void GetTicketCard(Node nodeDto)
        {
            var scaleState = _deviceManager.GetScaleState(nodeDto);
            if (scaleState?.InData == null) return;
            if (scaleState.InData.Value < 300)
            {
                TurnLightsOff(nodeDto);
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.Idle;
                UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                return;
            }

            var card = _cardManager.GetTruckCardByOnGateReader(nodeDto);
            if (card == null) return;

            if (!_routesManager.IsNodeNext(card.Ticket.Id, nodeDto.Id, out var errorMessage))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, errorMessage));
                return;
            }

            TurnLightsOff(nodeDto);

            var scaleOpData = _opDataRepository.GetLastOpData<ScaleOpData>(card.Ticket.Id, OpDataState.Processing);
            if (scaleOpData != null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, $"Автомобіль в обробці на {scaleOpData.Node.Name}"));
                return;
            }

            var scaleOpType = GetScaleOpType(card.Ticket.Id, nodeDto.Id);

            scaleOpData = new ScaleOpData
            {
                NodeId = nodeDto.Id,
                TicketId = card.Ticket.Id,
                StateId = OpDataState.Processing,
                TypeId = scaleOpType,
                CheckInDateTime = DateTime.Now,
                TicketContainerId = card.Ticket.TicketContainerId
            };

            var previousScale = _opDataRepository.GetLastOpData<ScaleOpData>(card.Ticket.Id, OpDataState.Processed)
                                ?? _opDataRepository.GetLastOpData<ScaleOpData>(card.Ticket.Id, OpDataState.Rejected);
            if (previousScale != null)
            {
                scaleOpData.TrailerIsAvailable = previousScale.TrailerIsAvailable;
                if (!scaleOpData.TrailerIsAvailable)
                {
                    scaleOpData.TrailerWeightDateTime = scaleState.LastUpdate;
                    scaleOpData.TrailerWeightIsAccepted = true;
                    scaleOpData.TrailerWeightValue = 0;
                }
            }

            var validationResult = ValidateScaleData(nodeDto, scaleState.InData.Value, card.Ticket);
            if (!validationResult.IsValid)
            {
                scaleOpData.GuardianPresence = true;
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.GuardianCardPrompt;

                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Info, $"{validationResult.ValidationMessage} Очікуйте охоронця"));

                var securityPhoneNo = _phonesRepository.GetPhone(Phone.Security);
                if (!_connectManager.SendSms(SmsTemplate.InvalidPerimeterGuardianSms, card.Ticket.Id, securityPhoneNo, new Dictionary<string, object>
                {
                    {"ScaleValidationText", validationResult.ValidationMessage}
                }))
                    Logger.Info($"Weightbridge.OproutineProcessor: Message to {securityPhoneNo} hasn`t been sent");
            }
            else
            {
                Logger.Info($"DriverTrailerEnableCheck: previousScaleId = {previousScale?.Id}");
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.TruckWeightPrompt;
            }
            
            if (!scaleOpData.TrailerIsAvailable)
            {
                nodeDto.Context.OpProcessData = (int?) scaleState.InData.Value;
            }

            _phonesRepository.Add<ScaleOpData, Guid>(scaleOpData);

            nodeDto.Context.TicketContainerId = card.Ticket.TicketContainerId;
            nodeDto.Context.TicketId = card.Ticket.Id;
            nodeDto.Context.OpDataId = scaleOpData.Id;
            
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        #region 07_11_14_GetGuardianCard

        private void GetGuardianCard(Node nodeDto)
        {
            if (nodeDto?.Context?.OpDataId == null || nodeDto.Context.OpRoutineStateId == null) return;

            var card = _userManager.GetValidatedUsersCardByOnGateReader(nodeDto);
            if (card == null) return;

            var opVisa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підпис зважування",
                ScaleTareOpDataId = nodeDto.Context.OpDataId.Value,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = nodeDto.Context.OpRoutineStateId.Value
            };

            _cardRepository.Add<OpVisa, int>(opVisa);

            switch (nodeDto.Context.OpRoutineStateId.Value)
            {
                case Model.DomainValue.OpRoutine.Weighbridge.State.GuardianCardPrompt:
                    nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.GuardianTruckVerification;
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.GetGuardianTruckWeightPermission:
                    nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.GetTruckWeight;
                    break;
                case Model.DomainValue.OpRoutine.Weighbridge.State.GetGuardianTrailerWeightPermission:
                    nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.GetTrailerWeight;
                    break;
            }
            
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        #region 12_15_GetWeight

        private void GetWeight(Node nodeDto, bool isTruckWeighting)
        {
            if (nodeDto?.Context?.OpDataId == null || nodeDto.Context.TicketId == null) return;

            var scaleState = _deviceManager.GetScaleState(nodeDto);
            if (scaleState == null) return;

            var isScaleOk = _scaleManager.IsScaleStateOk(scaleState, nodeDto.Id);
            if (!isScaleOk) return;
            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Error, @"Помилка. Дані операції не знайдено."));
                return;
            }
            
            if (!scaleOpData.TrailerIsAvailable && (scaleState.InData.Value > nodeDto.Context.OpProcessData + 60 || scaleState.InData.Value < nodeDto.Context.OpProcessData - 60))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, @"Зміна ваги."));
                
                _opDataManager.AddEvent(new OpDataEvent
                {
                    Created = DateTime.Now,
                    NodeId = nodeDto.Id,
                    TypeOfTransaction = TypeOfTransaction.Other,
                    Cause = $"Зміна ваги під час зваження",
                    TicketId = nodeDto.Context.TicketId.Value,
                    Weight = scaleState.InData.Value,
                    OpDataEventType = (int) OpDataEventType.Weight
                });
                
                scaleOpData.CheckOutDateTime = DateTime.Now;
                scaleOpData.StateId =  OpDataState.Rejected;
                _opDataRepository.Update<ScaleOpData, Guid>(scaleOpData);
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.Idle;
                UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                return;
            }

            AddWeightEvent(nodeDto, scaleOpData.TypeId, scaleState.InData.Value, isTruckWeighting);

            AddTruckWeight(scaleOpData, scaleState, isTruckWeighting);

            scaleOpData.CheckOutDateTime = DateTime.Now;
            
            if (scaleOpData.StateId == OpDataState.Rejected)
            {
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.OpenBarrierOut;
            }
            else
            {
                scaleOpData.StateId = scaleOpData.TruckWeightIsAccepted == false || scaleOpData.TrailerWeightIsAccepted == false
                    ? OpDataState.Rejected
                    : OpDataState.Processing;
            }

            _opDataRepository.Update<ScaleOpData, Guid>(scaleOpData);

            var cameraImagesList = _cameraManager.GetSnapshots(nodeDto.Config);
            foreach (var camImageId in cameraImagesList)
            {
                var camImage = _context.OpCameraImages.First(x => x.Id == camImageId);
                camImage.ScaleOpDataId = scaleOpData.Id;
                _context.SaveChanges();
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void AddTruckWeight(ScaleOpData scaleOpData, ScaleState scaleState, bool isTruckWeighting)
        {
            var prevScale = _context.ScaleOpDatas.AsNoTracking().Where(x => x.TypeId == (scaleOpData.TypeId == ScaleOpDataType.Gross ? ScaleOpDataType.Tare : ScaleOpDataType.Gross)
                                                                           && x.TicketId == scaleOpData.TicketId)
                .OrderByDescending(x => x.CheckOutDateTime)
                .FirstOrDefault();
            var prevLab = _context.LabFacelessOpDatas.AsNoTracking().Where(x => x.TicketId == scaleOpData.TicketId)
                .OrderByDescending(x => x.CheckOutDateTime)
                .FirstOrDefault();
   
            var isRejected = prevScale?.StateId == OpDataState.Rejected || prevLab?.StateId == OpDataState.Rejected;
            Logger.Trace($"isRejected = {isRejected}");
            Logger.Trace($"prev id = {prevScale?.Id}");
            Logger.Trace($"isTruckWeighting = {isTruckWeighting}");
            Logger.Trace($"scaleState = {scaleState.InData.Value}");

            if (isTruckWeighting)
            {
                if (isRejected)
                {
                    scaleOpData.TruckWeightDateTime = DateTime.Now;
                    scaleOpData.TruckWeightValue = prevScale?.TruckWeightValue;
                }
                else
                {
                    scaleOpData.TruckWeightDateTime = scaleState.LastUpdate;
                    scaleOpData.TruckWeightValue = scaleState.InData.Value;
                }
                
                _nodeDto.Context.OpRoutineStateId = scaleOpData.TrailerIsAvailable == false
                    ? Model.DomainValue.OpRoutine.Weighbridge.State.WeightResultsValidation
                    : Model.DomainValue.OpRoutine.Weighbridge.State.TrailerWeightPrompt;
            }
            else
            {
                if (isRejected)
                {
                    scaleOpData.TrailerWeightDateTime = DateTime.Now;
                    scaleOpData.TrailerWeightValue = prevScale?.TrailerWeightValue;
                }
                else
                {
                    scaleOpData.TrailerWeightDateTime = scaleState.LastUpdate;
                    scaleOpData.TrailerWeightValue = scaleState.InData.Value;
                }
                
                _nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.WeightResultsValidation;
            }
        }

        #endregion

        #region 16_ValidateWeightResults

        private void ValidateWeightResults(Node nodeDto)
        {
            if (nodeDto?.Context?.TicketId == null || nodeDto.Context?.OpDataId == null) return;
            string result = "";

            var ticket = _context.Tickets.First(x => x.Id == nodeDto.Context.TicketId.Value);
            if (ticket.RouteTemplateId == null) return;

            var scaleValidationData = _scaleManager.GetLoadWeightValidationData(nodeDto.Context.TicketId.Value);
            var singleWindowOpData = _context.SingleWindowOpDatas.First(x => x.TicketId == nodeDto.Context.TicketId);
            var routeType = ticket.RouteType;
            var scaleOpData = _context.ScaleOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);

            var centralLabOpData = _opDataRepository.GetLastOpData<CentralLabOpData>(nodeDto.Context.TicketId, null);
            var lastTareScaleOpData = _context.ScaleOpDatas.AsNoTracking()
                .Where(x => x.TypeId == ScaleOpDataType.Tare)
                .OrderByDescending(x => x.CheckOutDateTime)
                .FirstOrDefault();
            
            var endpointState = _opDataRepository.GetLastProcessed<UnloadPointOpData>(ticket.Id)?.StateId
                               ?? _opDataRepository.GetLastProcessed<LoadPointOpData>(ticket.Id)?.StateId;

            if (singleWindowOpData.LoadTarget > 0
                && endpointState != OpDataState.Rejected
                && endpointState != OpDataState.Canceled
                && singleWindowOpData.DocumentTypeId == ExternalData.DeliveryBill.Type.Outgoing
                && scaleOpData.TypeId == ScaleOpDataType.Gross
                && (centralLabOpData == null || (ticket.RouteType == RouteType.Reload || centralLabOpData.StateId != OpDataState.Rejected))
                && lastTareScaleOpData?.StateId != OpDataState.Rejected)
            {
                result = _scaleManager.IsWeightResultsValid(scaleValidationData, nodeDto.Context.TicketId.Value);
                Logger.Debug($"WeighbridgeWeightResultsValidation: {JsonConvert.SerializeObject(scaleValidationData)}");
            }

            var isMixedFeedRoute = ticket.RouteType == RouteType.MixedFeedLoad;
            if (isMixedFeedRoute)
            {
                CheckRemoveMixedFeedFromSilo(ticket.Id, scaleValidationData.WeightOnTruck, nodeDto.Context.OpDataId.Value);
            }

            var mainCondition = string.IsNullOrWhiteSpace(result) && _routesInfrastructure.IsLastScaleProcess(ticket.Id);
            var moveCondition = routeType == RouteType.Move && scaleOpData.TypeId == ScaleOpDataType.Tare;
            if (moveCondition || mainCondition)
            {
                var newTicket = MoveToNextTicket(ticket);
                if (newTicket != null && _routesInfrastructure.IsRouteWithoutGuide(newTicket.Id))
                {
                    _routesInfrastructure.AddDestinationOpData(newTicket.Id, nodeDto.Id);
                }
            }

            if (!string.IsNullOrWhiteSpace(result))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Error, @"Ліміти не виконані"));
            }
            else
            {
                if (scaleOpData.TruckWeightValue.HasValue
                    && scaleOpData.TruckWeightDateTime.HasValue)
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                        NodeData.ProcessingMsg.Type.Success, @"Перевірка пройшла успішно"));
                }
                else
                {
                    Logger.Error($"Scale weight validation error: TruckWeight is NULL in the database. ScaleOpData.Id={scaleOpData.Id}");
                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                        NodeData.ProcessingMsg.Type.Error, @"Увага, не коректне зважування. Повторіть процедуру зважуванняя з початку."));
                    scaleOpData.StateId = OpDataState.Canceled;
                    _opDataRepository.Update<ScaleOpData, Guid>(scaleOpData);
                }
            }
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.OpenBarrierOut;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        #region 17_OpenBarrierOut

        private void OpenBarrierOut(Node nodeDto)
        {
            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Error, @"Помилка. Дані операції не знайдено"));
                return;
            }
            if (scaleOpData.StateId == OpDataState.Rejected)
            {
                _routesInfrastructure.SetSecondaryRoute(nodeDto.Context.TicketId.Value, nodeDto.Id, RouteType.Reject);
            }
            Thread.Sleep(12000);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.CheckScaleEmpty;
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        #region 18_CheckScaleEmpty

        private void CheckScaleEmpty(Node nodeDto)
        {
            if (nodeDto.Context.OpDataId == null || nodeDto.Context.TicketId == null) return;

            var scaleState = _deviceManager.GetScaleState(nodeDto);
            if (scaleState != null && (scaleState.InData.IsScaleError
                                       || (nodeDto.Id != (long)NodeIdValue.Weighbridge5 && scaleState.InData.Value > 1000))) return;
            
            var scaleOpData = _context.ScaleOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    NodeData.ProcessingMsg.Type.Error, @"Помилка. Дані операції не знайдено"));
                return;
            }

            if (scaleOpData.StateId == OpDataState.Processing)
            {
                _routesInfrastructure.AssignSingleUnloadPoint(nodeDto.Context.TicketId.Value, nodeDto.Id);
                scaleOpData.StateId = OpDataState.Processed;
                _opDataRepository.Update<ScaleOpData, Guid>(scaleOpData);
                _routesInfrastructure.MoveForward(nodeDto.Context.TicketId.Value, nodeDto.Id);
            }

            Thread.Sleep(GlobalConfigurationManager.WeighbridgeCheckScaleEmptyTimeout * 1000);
            TurnLightsOff(nodeDto);

            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            nodeDto.Context.OpProcessData = 0;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.Idle;
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        private void TurnLightsOff(Node nodeDto)
        {
            Logger.Trace($"TurnLightsOff on {nodeDto.Id}");
            Program.SetDeviceOutData(nodeDto.Config.DO[NodeData.Config.DO.TrafficLightIncoming].DeviceId, false);
            Program.SetDeviceOutData(nodeDto.Config.DO[NodeData.Config.DO.TrafficLightOutgoing].DeviceId, false);
        }
        
        private void ValidateTruckPresence(Node nodeDto)
        {
            if (nodeDto.Context.OpProcessData != null
                && nodeDto.Context.OpProcessData != 0
                &&!ValidateTruckPresence2(nodeDto))
            {
                TurnLightsOff(nodeDto);
                nodeDto.Context.TicketContainerId = null;
                nodeDto.Context.TicketId = null;
                nodeDto.Context.OpDataId = null;
                nodeDto.Context.OpProcessData = null;
                nodeDto.Context.OpProcessData = 0;
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.Weighbridge.State.Idle;
                _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
                UpdateNodeContext(nodeDto.Id, nodeDto.Context);
            }
        }
        
        private void AddWeightEvent(Node nodeDto, ScaleOpDataType typeId, double weight, bool isTruckWeighting)
        {
            var opDataEvent = new OpDataEvent
            {
                Created = DateTime.Now,
                NodeId = nodeDto.Id, 
                TicketId = nodeDto.Context.TicketId.Value,
                Weight = weight,
                OpDataEventType = (int) OpDataEventType.Weight
            };
            switch (typeId)
            {
                case ScaleOpDataType.Tare:
                    opDataEvent.TypeOfTransaction =
                        isTruckWeighting ? TypeOfTransaction.TareWeighting : TypeOfTransaction.TareTrailerWeighting;
                    opDataEvent.Cause = isTruckWeighting ? "Зважування тари автомобіля" : "Зважування тари причепа";
                    break;
                case ScaleOpDataType.Gross:
                    opDataEvent.TypeOfTransaction =
                        isTruckWeighting ? TypeOfTransaction.GrossWeighting : TypeOfTransaction.GrossTrailerWeighting;
                    opDataEvent.Cause = isTruckWeighting ? "Зважування брутто автомобіля" : "Зважування брутто причепа";
                    break;
            }

            if (!string.IsNullOrWhiteSpace(opDataEvent.Cause)) _opDataManager.AddEvent(opDataEvent);
        }

        private void CheckRemoveMixedFeedFromSilo(long ticketId, double scaleValue, Guid opDataId)
        {
            var lastOpDataList = _context.ScaleOpDatas.AsNoTracking().Where(x => x.TicketId == ticketId
                                                  && x.Id != opDataId
                                                  && x.TypeId == ScaleOpDataType.Gross
                                                  && x.StateId == OpDataState.Canceled);
            var tareOpDataList = _opDataRepository
                .GetFirstOrDefault<ScaleOpData, Guid>(x => x.TicketId == ticketId
                                                           && x.TypeId == ScaleOpDataType.Tare
                                                           && x.StateId == OpDataState.Processed);
            double previousScaleValue = 0;
            if (lastOpDataList.Any())
                foreach (var opData in lastOpDataList)
                {
                    previousScaleValue += (opData.TruckWeightValue - tareOpDataList.TruckWeightValue ?? 0) + (opData.TrailerWeightValue  - tareOpDataList.TrailerWeightValue ?? 0);
                }

            Logger.Info($"CheckRemoveMixedFeedFromSilo: Remove silo: TruckWeight = {scaleValue}, Previous weight = {previousScaleValue}, ticketId = {ticketId}");
            RemoveMixedFeedFromSilo(ticketId, scaleValue, previousScaleValue);
        }
        
        private void RemoveMixedFeedFromSilo(long ticketId, double scale, double previousScaleValue)
        {
            var loadOpData = _opDataRepository.GetLastProcessed<LoadPointOpData>(ticketId);
            if (loadOpData?.MixedFeedSiloId == null) return;

            var silo = _context.MixedFeedSilos.FirstOrDefault(x => x.Id == loadOpData.MixedFeedSiloId.Value);
            if (silo != null)
            {
                var maxHeight = 21;
                var siloRadius = Math.Sqrt(8 / 3.14159);
                var fullValue = 3.14159 * (siloRadius * siloRadius) * maxHeight;
                var fullWeight = fullValue * 650 / 1000;

                Logger.Info($"RemoveMixedFeedFromSilo: silo.SiloWeight = {silo.SiloWeight}");
                Logger.Info($"RemoveMixedFeedFromSilo: scale = {scale}");
                Logger.Info($"RemoveMixedFeedFromSilo: previousScaleValue = {previousScaleValue}");
                
                silo.SiloWeight -= ((float)scale - (float)previousScaleValue) / 1000;
                Logger.Info($"RemoveMixedFeedFromSilo: new SiloWeight = {silo.SiloWeight}");

                silo.SiloEmpty = (float) (maxHeight - (silo.SiloWeight/ (fullWeight / 100) / 100) * maxHeight);
                silo.SiloFull = maxHeight - silo.SiloEmpty;
                    
                _opDataRepository.Update<MixedFeedSilo, int>(silo);
            }
        }

        private Ticket MoveToNextTicket(Ticket ticket)
        {
            var nextTicket = _context.Tickets.AsNoTracking().Where(x => 
                    x.TicketContainerId == ticket.TicketContainerId 
                    && x.StatusId == TicketStatus.ToBeProcessed
                    && x.OrderNo > ticket.OrderNo)
                .OrderBy(x => x.OrderNo)
                .FirstOrDefault();
            Logger.Info($"MoveToNextTicket: New ticketId for ticket = {ticket.Id} is {nextTicket?.Id}");
            
            if (nextTicket == null)
            {
                return null;
            }
            if (nextTicket.RouteTemplateId == null) throw new ArgumentException();
                
            ticket.StatusId = TicketStatus.Completed;
            _ticketRepository.Update<Ticket, int>(ticket);
                
            var route = _routesRepository
                .GetRoute(nextTicket.RouteTemplateId.Value)
                .RouteConfig
                .DeserializeRoute()
                .GroupDictionary
                .Select(groupItem => groupItem.Value.GroupId)
                .ToList();

            for (int i = 0; i < route.Count - 1; i++)
            {
                if (route[i] == (long) NodeGroup.WeighBridge)
                {
                    nextTicket.RouteItemIndex = i;
                    break;
                }
            }
                
            nextTicket.StatusId = TicketStatus.Processing;
            _ticketRepository.Update<Ticket, int>(nextTicket);
            return nextTicket;
        }

        private ScaleValidationResult ValidateScaleData(Node node, double scaleValue, Ticket ticket)
        {
            var scaleOpType = GetScaleOpType(ticket.Id, node.Id);
            var previousScale = _opDataRepository.GetLastOpData<ScaleOpData>(ticket.Id, OpDataState.Processed)
                                ?? _opDataRepository.GetLastOpData<ScaleOpData>(ticket.Id, OpDataState.Rejected);
            
            var windowInOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticket.Id);
            if (windowInOpData is null) return null;

            var isRejected = _routesInfrastructure.IsTicketRejected(ticket.Id);
            
            var validator = new ScaleDataValidator(node,
                scaleValue,
                scaleOpType, 
                previousScale, 
                windowInOpData.IncomeDocGrossValue, 
                isRejected);
            return validator.Validate();
        }

        private ScaleOpDataType GetScaleOpType(int ticketId, int nodeId)
        {
            var process = _routesInfrastructure.GetNodeProcess(ticketId, nodeId);
            if (process != (int) ScaleProcess.Auto) return (ScaleOpDataType) process;
            
            var windowInOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId);
            if (windowInOpData is null) throw new Exception($"GetScaleOpType: SingleWindowOpData with ticket {ticketId} doesn't exist");

            ScaleOpDataType scaleOpType = (ScaleOpDataType) 0;
            var previousScaleOpData = _opDataRepository.GetLastOpData<ScaleOpData>(ticketId, OpDataState.Processed) 
                                      ?? _opDataRepository.GetLastOpData<ScaleOpData>(ticketId, OpDataState.Rejected);

            switch (windowInOpData.DocumentTypeId)
            {
                case ExternalData.DeliveryBill.Type.Incoming:
                    scaleOpType = previousScaleOpData?.TypeId == ScaleOpDataType.Gross
                        ? ScaleOpDataType.Tare
                        : ScaleOpDataType.Gross;
                    break;
                case ExternalData.DeliveryBill.Type.Outgoing:
                    scaleOpType = previousScaleOpData?.TypeId == ScaleOpDataType.Tare
                        ? ScaleOpDataType.Gross
                        : ScaleOpDataType.Tare;
                    break;
            }
            
            return scaleOpType;
        }

        private bool ValidateTruckPresence2(Node nodeDto)
        {
            if (ValidateTruckWeight(nodeDto))
            {
                return true;
            }
            if (nodeDto.Context.OpDataId == null || nodeDto.Context.TicketId == null) throw new ArgumentException($"Invalid Node context = {nodeDto.Name}");

            var scaleOpData = _context.ScaleOpDatas.AsNoTracking().FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (scaleOpData == null) throw new ArgumentException($"Invalid scaleOpData = {nodeDto.Name}");
            if (scaleOpData.StateId == OpDataState.Canceled)
            {
                return false;
            }
            var standardOpData = new NonStandartOpData
            {
                NodeId = nodeDto.Id,
                TicketContainerId = nodeDto.Context.TicketContainerId,
                TicketId = nodeDto.Context.TicketId,
                CheckInDateTime = DateTime.Now,
                CheckOutDateTime = DateTime.Now,
                StateId = OpDataState.Processed,
                Message = "Машина з'їхала з ваг в процесі зважування або вага її змінилася"
            };
            _opDataRepository.Add<NonStandartOpData, Guid>(standardOpData);

            scaleOpData.StateId = OpDataState.Canceled;
            scaleOpData.CheckOutDateTime = DateTime.Now;
            _opDataRepository.Update<ScaleOpData, Guid>(scaleOpData);
            
            _opDataManager.AddEvent(new OpDataEvent
            {
                Created = DateTime.Now,
                NodeId = nodeDto.Id,
                TypeOfTransaction = TypeOfTransaction.Other,
                Cause = "Машина з'їхала з ваг в процесі зважування або вага її змінилася",
                TicketId = nodeDto.Context.TicketId.Value,
                Weight = 0,
                OpDataEventType = (int) OpDataEventType.Weight
            });

            return false;
        }

        private bool ValidateTruckWeight(Node nodeDto)
        {
            var scaleState = _deviceManager.GetScaleState(nodeDto);
            if (scaleState?.InData == null) return false;

            return scaleState.InData.Value < nodeDto.Context.OpProcessData + 100 && scaleState.InData.Value > nodeDto.Context.OpProcessData - 100;
        }
    }
}
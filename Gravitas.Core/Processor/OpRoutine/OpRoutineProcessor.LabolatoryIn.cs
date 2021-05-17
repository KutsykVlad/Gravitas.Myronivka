﻿using System;
using System.Linq;
using Gravitas.Core.DeviceManager.Card;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using LabFacelessOpData = Gravitas.Model.LabFacelessOpData;
using LabFacelessOpDataComponent = Gravitas.Model.LabFacelessOpDataComponent;
using Node = Gravitas.Model.Dto.Node;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class LaboratoryInOpRoutineProcessor : BaseOpRoutineProcessor
    {
        private readonly ICardManager _cardManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IRoutesManager _routesManager;
        private readonly IUserManager _userManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUnloadPointManager _unloadPointManager;
        private readonly IConnectManager _connectManager;

        public LaboratoryInOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            IRoutesManager routesManager,
            IRoutesInfrastructure routesInfrastructure,
            ICardManager cardManager, 
            IUserManager userManager,
            IUnloadPointManager unloadPointManager,
            IConnectManager connectManager) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _ticketRepository = ticketRepository;
            _routesManager = routesManager;
            _routesInfrastructure = routesInfrastructure;
            _cardManager = cardManager;
            _userManager = userManager;
            _unloadPointManager = unloadPointManager;
            _connectManager = connectManager;
        }

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null) return false;

            var rfidValid = config.Rfid.ContainsKey(Dom.Node.Config.Rfid.TableReader);

            return rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.LabolatoryIn.State.Idle:
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.SampleReadTruckRfid:
                    SampleReadTruckRfid(_nodeDto);
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.SampleBindTray:
                    SampleBindTray(_nodeDto);
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.SampleBindAnalysisTray:
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.SampleAddOpVisa:
                    SampleAddOpVisa(_nodeDto);
                    break;

                case Dom.OpRoutine.LabolatoryIn.State.ResultReadTrayRfid:
                    ResultReadTrayRfid(_nodeDto);
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.ResultEditAnalysis:
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.ResultAddOpVisa:
                    ResultAddOpVisa(_nodeDto);
                    break;

                case Dom.OpRoutine.LabolatoryIn.State.PrintReadTrayRfid:
                    PrintReadTrayRfid(_nodeDto);
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisResults:
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisAddOpVisa:
                    PrintAnalysisAddOpVisa(_nodeDto);
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.PrintDataDisclose:
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.PrintCollisionInit:
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.PrintCollisionManage:
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.PrintAddOpVisa:
                    PrintAddOpVisa(_nodeDto);
                    break;
                case Dom.OpRoutine.LabolatoryIn.State.PrintLaboratoryProtocol:
                    break;
            }
        }

        private void SampleReadTruckRfid(Node nodeDto)
        {
            CardReadResult card = null;

            if (nodeDto.Config.Rfid.ContainsKey(Dom.Node.Config.Rfid.OnGateReader)) card = _cardManager.GetTruckCardByOnGateReader(nodeDto);
            if (nodeDto.Config.Rfid.ContainsKey(Dom.Node.Config.Rfid.LongRangeReader)) card = _cardManager.GetTruckCardByZebraReader(nodeDto);
            if (card == null) return;

            var hasBindedLabCards = _context.Cards.AsNoTracking().Any(x =>
                x.TicketContainerId == card.Ticket.ContainerId && x.TypeId == Dom.Card.Type.LaboratoryTray);
            if (hasBindedLabCards)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, "Авто знаходиться в обробці"));
                return;
            }
            
            var lastOpData = _opDataRepository.GetLastOpData<LabFacelessOpData>(card.Ticket.Id, Dom.OpDataState.Canceled);
            var isCanceledOpData = lastOpData != null;
            if (!isCanceledOpData && !_routesManager.IsNodeNext(card.Ticket.Id, nodeDto.Id, out var errorMessage))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, errorMessage));
                return;
            }

            var labFacelessOpData = new LabFacelessOpData
            {
                NodeId = nodeDto.Id,
                TicketId = card.Ticket.Id,
                StateId = Dom.OpDataState.Init,
                CheckInDateTime = DateTime.Now,
                CheckOutDateTime = DateTime.Now
            };
            _ticketRepository.Add<LabFacelessOpData, Guid>(labFacelessOpData);
            
            _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Info, @"Автомобіль прив'язано."));

            nodeDto.Context.TicketId = card.Ticket.Id;
            nodeDto.Context.OpDataId = labFacelessOpData.Id;
            nodeDto.Context.TicketContainerId = _context.Tickets.First(x => x.Id == card.Ticket.Id).ContainerId;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.SampleBindTray;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void SampleBindTray(Node nodeDto)
        {
            if (nodeDto?.Context?.TicketContainerId == null || nodeDto.Context?.TicketId == null) return;

            var card = _cardManager.GetLaboratoryTrayOnTableReader(nodeDto);
            if (card == null) return;
            if (card.ParentCardId != card.Id)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Info, "Використайте головну картку"));
                return;
            }
            if (card.TicketContainerId != null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, "Хибний маршрутний лист"));
                return;
            }

            var ticket = _ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value, Dom.Ticket.Status.Processing);
            if (ticket == null) return;

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            
            var lastOpData = _opDataRepository.GetLastOpData<LabFacelessOpData>(nodeDto.Context.TicketId.Value, Dom.OpDataState.Canceled);
            var isCanceledOpData = lastOpData != null;
            if (!isCanceledOpData) _routesInfrastructure.MoveForward(nodeDto.Context.TicketId.Value, nodeDto.Id);

            card.TicketContainerId = nodeDto.Context.TicketContainerId;
            _ticketRepository.Update<Card, string>(card);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.SampleBindAnalysisTray;
            nodeDto.Context.TicketId = ticket.Id;
            nodeDto.Context.OpProcessData = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void SampleAddOpVisa(Node nodeDto)
        {
            if (nodeDto.Context?.OpDataId == null || nodeDto.Context.TicketContainerId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var labFacelessOpData = _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (labFacelessOpData == null) return;

            var trayCardExist = _context.Cards.Any(x => x.TicketContainerId == nodeDto.Context.TicketContainerId && x.TypeId == Dom.Card.Type.LaboratoryTray);
            if (!trayCardExist)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, "Лоток не прив'язаний"));
                return;
            }

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Відбір проби.",
                LabFacelessOpDataId = labFacelessOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.SampleAddOpVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void ResultReadTrayRfid(Node nodeDto)
        {
            var card = _cardManager.GetLaboratoryTrayOnTableReader(nodeDto);
            if (!card.IsFree() || !card.TicketContainerId.HasValue) return;

            var ticket = _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, Dom.Ticket.Status.Processing);
            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error,
                        $@"Маршрут не знадено. Маршрутний лист Id:{card.TicketContainerId.Value}"));
                return;
            }

            var labFacelessOpData = _opDataRepository.GetLastOpData<LabFacelessOpData>(ticket.Id, Dom.OpDataState.Init);
            if (labFacelessOpData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error,
                        $@"Операцію відбору проб не знайдено в реєстрі. Маршрут Id:{ticket.Id}"));
                return;
            }

            var labFacelessOpDataComponent = _context.LabFacelessOpDataComponents
                .AsNoTracking()
                .FirstOrDefault(e =>
                    e.LabFacelessOpDataId == labFacelessOpData.Id
                    && e.AnalysisTrayRfid == card.Id
                    && e.StateId == Dom.OpDataState.Init);

            if (labFacelessOpDataComponent == null)
            {
                var tmpComponent = _context.LabFacelessOpDataComponents
                    .AsNoTracking()
                    .FirstOrDefault(e =>
                        e.LabFacelessOpDataId == labFacelessOpData.Id
                        && e.AnalysisTrayRfid == card.Id
                        && e.StateId == Dom.OpDataState.Processed);

                if (tmpComponent != null)
                {
                    labFacelessOpDataComponent = new LabFacelessOpDataComponent
                    {
                        LabFacelessOpDataId = tmpComponent.LabFacelessOpDataId,
                        StateId = Dom.OpDataState.Init,
                        NodeId = tmpComponent.NodeId,
                        AnalysisTrayRfid = tmpComponent.AnalysisTrayRfid,
                        AnalysisValueDescriptor = tmpComponent.AnalysisValueDescriptor,
                        CheckInDateTime = DateTime.Now
                    };

                    _opDataRepository.Add<LabFacelessOpDataComponent, long>(labFacelessOpDataComponent);

                    labFacelessOpDataComponent = _context.LabFacelessOpDataComponents
                        .AsNoTracking()
                        .FirstOrDefault(e =>
                            e.LabFacelessOpDataId == labFacelessOpData.Id
                            && e.AnalysisTrayRfid == card.Id
                            && e.StateId == Dom.OpDataState.Init);
                }
            }

            if (labFacelessOpDataComponent == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(
                        Dom.Node.ProcessingMsg.Type.Error,
                        $@"Операцію вводу результатів аналізу не знайдено в реєстрі. Операція відбору проб Id:{labFacelessOpData.Id}. Лоток для аналізу: {card.Id}"));
                return;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.ResultEditAnalysis;
            nodeDto.Context.TicketContainerId = card.TicketContainerId;
            nodeDto.Context.TicketId = ticket.Id;
            nodeDto.Context.OpDataId = labFacelessOpData.Id;
            nodeDto.Context.OpDataComponentId = labFacelessOpDataComponent.Id;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void ResultAddOpVisa(Node nodeDto)
        {
            if (nodeDto.Context?.OpDataComponentId == null)
                return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var labFacelessOpDataComponent = _context.LabFacelessOpDataComponents.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataComponentId.Value);
            if (labFacelessOpDataComponent == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Збереження результатів аналізу проби",
                LabFacelessOpDataComponentId = labFacelessOpDataComponent.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.ResultAddOpVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);

            labFacelessOpDataComponent.StateId = Dom.OpDataState.Processed;
            _ticketRepository.Update<LabFacelessOpDataComponent, long>(labFacelessOpDataComponent);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.Idle;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            nodeDto.Context.OpDataComponentId = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void PrintReadTrayRfid(Node nodeDto)
        {
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);

            var card = _cardManager.GetLaboratoryTrayOnTableReader(nodeDto);
            if (!card.IsFree() || !card.TicketContainerId.HasValue) return;

            var ticket = _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, Dom.Ticket.Status.Processing);
            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error,
                        $@"Маршрут не знадено. Маршрутний лист Id:{card.TicketContainerId.Value}"));
                return;
            }

            var labFacelessOpData = _opDataRepository.GetLastOpData<LabFacelessOpData>(ticket.Id, Dom.OpDataState.Init);
            if (labFacelessOpData == null) return;

            var labFacelessOpDataComponent = _context.LabFacelessOpDataComponents.AsNoTracking().FirstOrDefault(e =>
                    e.LabFacelessOpDataId == labFacelessOpData.Id);

            if (labFacelessOpDataComponent == null) return;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisResults;
            nodeDto.Context.TicketContainerId = card.TicketContainerId;
            nodeDto.Context.TicketId = ticket.Id;
            nodeDto.Context.OpDataId = labFacelessOpData.Id;
            nodeDto.Context.OpDataComponentId = labFacelessOpDataComponent.Id;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void PrintAnalysisAddOpVisa(Node nodeDto)
        {
            if (nodeDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;

            var labFacelessOpData = _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (labFacelessOpData == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підтвердження результатів",
                LabFacelessOpDataId = labFacelessOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAnalysisAddOpVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintDataDisclose;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private void PrintAddOpVisa(Node nodeDto)
        {
            if (nodeDto.Context?.TicketId == null || nodeDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;
            
            var labFacelessOpData = _context.LabFacelessOpDatas.AsNoTracking().FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (labFacelessOpData == null) return;
            
            var singleWindowOpData = _opDataRepository.GetFirstOrDefault<SingleWindowOpData, Guid>(
                item => item.TicketId == nodeDto.Context.TicketId.Value);
            if (singleWindowOpData.ReturnCauseId != null)
            {
                var lastOpData = _opDataRepository.GetLastOpData(singleWindowOpData.TicketId);
                Logger.Info($"PrintAddOpVisa: lastOpData?.NodeId = {lastOpData?.NodeId}");
                if (_context.NonStandartOpDatas.Any(x => x.Id == lastOpData.Id) || lastOpData?.NodeId != nodeDto.Id)
                {
                    _routesInfrastructure.MoveBack(nodeDto.Context.TicketId.Value);
                }
                _routesInfrastructure.MoveBack(nodeDto.Context.TicketId.Value);
                _routesInfrastructure.SetSecondaryRoute(nodeDto.Context.TicketId.Value, nodeDto.Id, Dom.Route.Type.Reject);
            }
            if (singleWindowOpData.ReturnCauseId == null && labFacelessOpData.StateId == Dom.OpDataState.Rejected)
            {
                var unloadGuide = new UnloadGuideOpData
                {
                    NodeId = nodeDto.Id,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now,
                    UnloadPointNodeId = (long) NodeIdValue.UnloadPoint13,
                    StateId = Dom.OpDataState.Processed,
                    TicketId = nodeDto.Context.TicketId,
                    TicketContainerId = nodeDto.Context.TicketContainerId
                };
                _opDataRepository.Add<UnloadGuideOpData, Guid>(unloadGuide);
                
                var loadGuide = new LoadGuideOpData
                {
                    NodeId = nodeDto.Id,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now,
                    LoadPointNodeId = _opDataRepository.GetLastProcessed<LoadGuideOpData>(nodeDto.Context.TicketId.Value).LoadPointNodeId,
                    StateId = Dom.OpDataState.Processed,
                    TicketId = nodeDto.Context.TicketId,
                    TicketContainerId = nodeDto.Context.TicketContainerId
                };
                _opDataRepository.Add<LoadGuideOpData, Guid>(loadGuide);
                
                var unloadResult = _unloadPointManager.ConfirmUnloadGuide(nodeDto.Context.TicketId.Value, card.EmployeeId);
                if (!unloadResult) return;
                _routesInfrastructure.SetSecondaryRoute(nodeDto.Context.TicketId.Value, nodeDto.Id, Dom.Route.Type.Reload);
                _connectManager.SendSms(Dom.Sms.Template.RouteChangeSms, nodeDto.Context.TicketId);

            }

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підпис підсумкових результатів",
                LabFacelessOpDataId = labFacelessOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintAddOpVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);

            // Unbind trays
            var unbindRfidList = _context.Cards.AsNoTracking().Where(e =>
                    e.TicketContainerId == nodeDto.Context.TicketContainerId
                    && e.TypeId == Dom.Card.Type.LaboratoryTray)
                .Select(e => e.Id)
                .ToList();
            
            _nodeRepository.UpdateNodeProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Info,
                $@"{unbindRfidList.Count} лотків відв'язано."));

            foreach (var rfid in unbindRfidList)
            {
                var tmpCard = _context.Cards.First(x => x.Id == rfid);
                tmpCard.TicketContainerId = null;
                _context.SaveChanges();
            }

            // Change node state
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LabolatoryIn.State.PrintLaboratoryProtocol;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
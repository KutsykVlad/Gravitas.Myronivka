using System;
using System.Linq;
using Gravitas.Core.DeviceManager.Card;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.UnloadPoint;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;
using CardType = Gravitas.Model.DomainValue.CardType;
using LabFacelessOpData = Gravitas.Model.DomainModel.OpData.DAO.LabFacelessOpData;
using LabFacelessOpDataComponent = Gravitas.Model.DomainModel.OpData.DAO.LabFacelessOpDataComponent;

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

        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.Idle:
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.SampleReadTruckRfid:
                    SampleReadTruckRfid(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.SampleBindTray:
                    SampleBindTray(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.SampleBindAnalysisTray:
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.SampleAddOpVisa:
                    SampleAddOpVisa(NodeDetails);
                    break;

                case Model.DomainValue.OpRoutine.LaboratoryIn.State.ResultReadTrayRfid:
                    ResultReadTrayRfid(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.ResultEditAnalysis:
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.ResultAddOpVisa:
                    ResultAddOpVisa(NodeDetails);
                    break;

                case Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintReadTrayRfid:
                    PrintReadTrayRfid(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintAnalysisResults:
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintAnalysisAddOpVisa:
                    PrintAnalysisAddOpVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintDataDisclose:
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintCollisionInit:
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintCollisionManage:
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintAddOpVisa:
                    PrintAddOpVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintLaboratoryProtocol:
                    break;
            }
        }

        private void SampleReadTruckRfid(NodeDetails nodeDetailsDto)
        {
            CardReadResult card = null;

            if (nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.OnGateReader)) card = _cardManager.GetTruckCardByOnGateReader(nodeDetailsDto);
            if (nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.LongRangeReader)) card = _cardManager.GetTruckCardByZebraReader(nodeDetailsDto);
            if (card == null) return;

            var hasBindedLabCards = _context.Cards.AsNoTracking().Any(x =>
                x.TicketContainerId == card.Ticket.TicketContainerId && x.TypeId == CardType.LaboratoryTray);
            if (hasBindedLabCards)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(ProcessingMsgType.Warning, "Авто знаходиться в обробці"));
                return;
            }
            
            var lastOpData = _opDataRepository.GetLastOpData<LabFacelessOpData>(card.Ticket.Id, OpDataState.Canceled);
            var isCanceledOpData = lastOpData != null;
            if (!isCanceledOpData && !_routesManager.IsNodeNext(card.Ticket.Id, nodeDetailsDto.Id, out var errorMessage))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(ProcessingMsgType.Error, errorMessage));
                return;
            }

            var labFacelessOpData = new LabFacelessOpData
            {
                NodeId = nodeDetailsDto.Id,
                TicketId = card.Ticket.Id,
                StateId = OpDataState.Init,
                CheckInDateTime = DateTime.Now,
                CheckOutDateTime = DateTime.Now
            };
            _ticketRepository.Add<LabFacelessOpData, Guid>(labFacelessOpData);
            
            _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                new NodeProcessingMsgItem(ProcessingMsgType.Info, @"Автомобіль прив'язано."));

            nodeDetailsDto.Context.TicketId = card.Ticket.Id;
            nodeDetailsDto.Context.OpDataId = labFacelessOpData.Id;
            nodeDetailsDto.Context.TicketContainerId = _context.Tickets.First(x => x.Id == card.Ticket.Id).TicketContainerId;
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.SampleBindTray;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void SampleBindTray(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketContainerId == null || nodeDetailsDto.Context?.TicketId == null) return;

            var card = _cardManager.GetLaboratoryTrayOnTableReader(nodeDetailsDto);
            if (card == null) return;
            if (card.ParentCardId != card.Id)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Info, "Використайте головну картку"));
                return;
            }
            if (card.TicketContainerId != null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning, "Хибний маршрутний лист"));
                return;
            }

            var ticket = _ticketRepository.GetTicketInContainer(nodeDetailsDto.Context.TicketContainerId.Value, TicketStatus.Processing);
            if (ticket == null) return;

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            
            var lastOpData = _opDataRepository.GetLastOpData<LabFacelessOpData>(nodeDetailsDto.Context.TicketId.Value, OpDataState.Canceled);
            var isCanceledOpData = lastOpData != null;
            if (!isCanceledOpData) _routesInfrastructure.MoveForward(nodeDetailsDto.Context.TicketId.Value, nodeDetailsDto.Id);

            card.TicketContainerId = nodeDetailsDto.Context.TicketContainerId;
            _ticketRepository.Update<Card, string>(card);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.SampleBindAnalysisTray;
            nodeDetailsDto.Context.TicketId = ticket.Id;
            nodeDetailsDto.Context.OpProcessData = null;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void SampleAddOpVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.OpDataId == null || nodeDetailsDto.Context.TicketContainerId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var labFacelessOpData = _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (labFacelessOpData == null) return;

            var trayCardExist = _context.Cards.Any(x => x.TicketContainerId == nodeDetailsDto.Context.TicketContainerId && x.TypeId == CardType.LaboratoryTray);
            if (!trayCardExist)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(ProcessingMsgType.Error, "Лоток не прив'язаний"));
                return;
            }

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Відбір проби.",
                LabFacelessOpDataId = labFacelessOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.SampleAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.Idle;
            nodeDetailsDto.Context.TicketContainerId = null;
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.OpDataId = null;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void ResultReadTrayRfid(NodeDetails nodeDetailsDto)
        {
            var card = _cardManager.GetLaboratoryTrayOnTableReader(nodeDetailsDto);
            if (!card.IsFree() || !card.TicketContainerId.HasValue) return;

            var ticket = _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, TicketStatus.Processing);
            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error,
                        $@"Маршрут не знадено. Маршрутний лист Id:{card.TicketContainerId.Value}"));
                return;
            }

            var labFacelessOpData = _opDataRepository.GetLastOpData<LabFacelessOpData>(ticket.Id, OpDataState.Init);
            if (labFacelessOpData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error,
                        $@"Операцію відбору проб не знайдено в реєстрі. Маршрут Id:{ticket.Id}"));
                return;
            }

            var labFacelessOpDataComponent = _context.LabFacelessOpDataComponents
                .AsNoTracking()
                .FirstOrDefault(e =>
                    e.LabFacelessOpDataId == labFacelessOpData.Id
                    && e.AnalysisTrayRfid == card.Id
                    && e.StateId == OpDataState.Init);

            if (labFacelessOpDataComponent == null)
            {
                var tmpComponent = _context.LabFacelessOpDataComponents
                    .AsNoTracking()
                    .FirstOrDefault(e =>
                        e.LabFacelessOpDataId == labFacelessOpData.Id
                        && e.AnalysisTrayRfid == card.Id
                        && e.StateId == OpDataState.Processed);

                if (tmpComponent != null)
                {
                    labFacelessOpDataComponent = new LabFacelessOpDataComponent
                    {
                        LabFacelessOpDataId = tmpComponent.LabFacelessOpDataId,
                        StateId = OpDataState.Init,
                        NodeId = tmpComponent.NodeId,
                        AnalysisTrayRfid = tmpComponent.AnalysisTrayRfid,
                        AnalysisValueDescriptor = tmpComponent.AnalysisValueDescriptor,
                        CheckInDateTime = DateTime.Now
                    };

                    _opDataRepository.Add<LabFacelessOpDataComponent, int>(labFacelessOpDataComponent);

                    labFacelessOpDataComponent = _context.LabFacelessOpDataComponents
                        .AsNoTracking()
                        .FirstOrDefault(e =>
                            e.LabFacelessOpDataId == labFacelessOpData.Id
                            && e.AnalysisTrayRfid == card.Id
                            && e.StateId == OpDataState.Init);
                }
            }

            if (labFacelessOpDataComponent == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(
                        ProcessingMsgType.Error,
                        $@"Операцію вводу результатів аналізу не знайдено в реєстрі. Операція відбору проб Id:{labFacelessOpData.Id}. Лоток для аналізу: {card.Id}"));
                return;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.ResultEditAnalysis;
            nodeDetailsDto.Context.TicketContainerId = card.TicketContainerId;
            nodeDetailsDto.Context.TicketId = ticket.Id;
            nodeDetailsDto.Context.OpDataId = labFacelessOpData.Id;
            nodeDetailsDto.Context.OpDataComponentId = labFacelessOpDataComponent.Id;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void ResultAddOpVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.OpDataComponentId == null)
                return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var labFacelessOpDataComponent = _context.LabFacelessOpDataComponents.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataComponentId.Value);
            if (labFacelessOpDataComponent == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Збереження результатів аналізу проби",
                LabFacelessOpDataComponentId = labFacelessOpDataComponent.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.ResultAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            labFacelessOpDataComponent.StateId = OpDataState.Processed;
            _ticketRepository.Update<LabFacelessOpDataComponent, int>(labFacelessOpDataComponent);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.Idle;
            nodeDetailsDto.Context.TicketId = null;
            nodeDetailsDto.Context.OpDataId = null;
            nodeDetailsDto.Context.OpDataComponentId = null;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void PrintReadTrayRfid(NodeDetails nodeDetailsDto)
        {
            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);

            var card = _cardManager.GetLaboratoryTrayOnTableReader(nodeDetailsDto);
            if (!card.IsFree() || !card.TicketContainerId.HasValue) return;

            var ticket = _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, TicketStatus.Processing);
            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error,
                        $@"Маршрут не знадено. Маршрутний лист Id:{card.TicketContainerId.Value}"));
                return;
            }

            var labFacelessOpData = _opDataRepository.GetLastOpData<LabFacelessOpData>(ticket.Id, OpDataState.Init);
            if (labFacelessOpData == null) return;

            var labFacelessOpDataComponent = _context.LabFacelessOpDataComponents.AsNoTracking().FirstOrDefault(e =>
                    e.LabFacelessOpDataId == labFacelessOpData.Id);

            if (labFacelessOpDataComponent == null) return;

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintAnalysisResults;
            nodeDetailsDto.Context.TicketContainerId = card.TicketContainerId;
            nodeDetailsDto.Context.TicketId = ticket.Id;
            nodeDetailsDto.Context.OpDataId = labFacelessOpData.Id;
            nodeDetailsDto.Context.OpDataComponentId = labFacelessOpDataComponent.Id;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void PrintAnalysisAddOpVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var labFacelessOpData = _context.LabFacelessOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (labFacelessOpData == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підтвердження результатів",
                LabFacelessOpDataId = labFacelessOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintAnalysisAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintDataDisclose;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void PrintAddOpVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.TicketId == null || nodeDetailsDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;
            
            var labFacelessOpData = _context.LabFacelessOpDatas.AsNoTracking().FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (labFacelessOpData == null) return;
            
            var singleWindowOpData = _opDataRepository.GetFirstOrDefault<SingleWindowOpData, Guid>(
                item => item.TicketId == nodeDetailsDto.Context.TicketId.Value);
            if (singleWindowOpData.ReturnCauseId != null)
            {
                var lastOpData = _opDataRepository.GetLastOpData(singleWindowOpData.TicketId);
                Logger.Info($"PrintAddOpVisa: lastOpData?.NodeId = {lastOpData?.NodeId}");
                if (_context.NonStandartOpDatas.Any(x => x.Id == lastOpData.Id) || lastOpData?.NodeId != nodeDetailsDto.Id)
                {
                    _routesInfrastructure.MoveBack(nodeDetailsDto.Context.TicketId.Value);
                }
                _routesInfrastructure.MoveBack(nodeDetailsDto.Context.TicketId.Value);
                _routesInfrastructure.SetSecondaryRoute(nodeDetailsDto.Context.TicketId.Value, nodeDetailsDto.Id, RouteType.Reject);
            }
            if (singleWindowOpData.ReturnCauseId == null && labFacelessOpData.StateId == OpDataState.Rejected)
            {
                var unloadGuide = new UnloadGuideOpData
                {
                    NodeId = nodeDetailsDto.Id,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now,
                    UnloadPointNodeId = (int) NodeIdValue.UnloadPoint13,
                    StateId = OpDataState.Processed,
                    TicketId = nodeDetailsDto.Context.TicketId,
                    TicketContainerId = nodeDetailsDto.Context.TicketContainerId
                };
                _opDataRepository.Add<UnloadGuideOpData, Guid>(unloadGuide);
                
                var loadGuide = new LoadGuideOpData
                {
                    NodeId = nodeDetailsDto.Id,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now,
                    LoadPointNodeId = _opDataRepository.GetLastProcessed<LoadGuideOpData>(nodeDetailsDto.Context.TicketId.Value).LoadPointNodeId,
                    StateId = OpDataState.Processed,
                    TicketId = nodeDetailsDto.Context.TicketId,
                    TicketContainerId = nodeDetailsDto.Context.TicketContainerId
                };
                _opDataRepository.Add<LoadGuideOpData, Guid>(loadGuide);
                
                var unloadResult = _unloadPointManager.ConfirmUnloadGuide(nodeDetailsDto.Context.TicketId.Value, card.EmployeeId.Value);
                if (!unloadResult) return;
                _routesInfrastructure.SetSecondaryRoute(nodeDetailsDto.Context.TicketId.Value, nodeDetailsDto.Id, RouteType.Reload);
                _connectManager.SendSms(SmsTemplate.RouteChangeSms, nodeDetailsDto.Context.TicketId);

            }

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підпис підсумкових результатів",
                LabFacelessOpDataId = labFacelessOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            // Unbind trays
            var unbindRfidList = _context.Cards.AsNoTracking().Where(e =>
                    e.TicketContainerId == nodeDetailsDto.Context.TicketContainerId
                    && e.TypeId == CardType.LaboratoryTray)
                .Select(e => e.Id)
                .ToList();
            
            _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(ProcessingMsgType.Info,
                $@"{unbindRfidList.Count} лотків відв'язано."));

            foreach (var rfid in unbindRfidList)
            {
                var tmpCard = _context.Cards.First(x => x.Id == rfid);
                tmpCard.TicketContainerId = null;
                _context.SaveChanges();
            }

            // Change node state
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.LaboratoryIn.State.PrintLaboratoryProtocol;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
    }
}
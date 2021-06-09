using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.Core.DeviceManager.Card;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.OpWorkflow.Routes;
using Gravitas.DAL.Repository.Phones;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainValue;
using CardType = Gravitas.Model.DomainValue.CardType;

namespace Gravitas.Core.Processor.OpRoutine
{
    class CentralLaboratoryProcessOpRoutineProcessor : BaseOpRoutineProcessor
    {

        private readonly ITicketRepository _ticketRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IConnectManager _connectManager;
        private readonly ICardManager _cardManager;
        private readonly IUserManager _userManager;
        private readonly IPhonesRepository _phonesRepository;
        private readonly IRoutesRepository _routesRepository;

        public CentralLaboratoryProcessOpRoutineProcessor(IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository, 
            INodeRepository nodeRepository, 
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            IRoutesInfrastructure routesInfrastructure, 
            IConnectManager connectManager, 
            IUserManager userManager,
            ICardManager cardManager,
            IPhonesRepository phonesRepository,
            IRoutesRepository routesRepository) :
            base(opRoutineManager, deviceManager, deviceRepository, nodeRepository, opDataRepository)
        {
            _ticketRepository = ticketRepository;
            _routesInfrastructure = routesInfrastructure;
            _connectManager = connectManager;
            _userManager = userManager;
            _cardManager = cardManager;
            _phonesRepository = phonesRepository;
            _routesRepository = routesRepository;
        }

        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId)
            {
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.Idle:
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.AddSample:
                    AddSample(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.AddSampleVisa:
                    AddSampleVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintLabel:
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose:
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionStartVisa:
                    PrintCollisionStartVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit:
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInitVisa:
                    PrintCollisionInitVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa:
                    PrintAddOpVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintDocument:
                    break;
            }
        }

        #region 02_AddSample

        void AddSample(NodeDetails nodeDetailsDto)
        {
            var card = GetCard(nodeDetailsDto);
            if (card?.TicketContainerId == null) return;
            
            if (!_opRoutineManager.IsRfidCardValid(out NodeProcessingMsgItem errMsgItem, card, CardType.LaboratoryTray))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsgItem);
                return;
            }

            if (!card.IsFree())
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning,
                        @"До картки не прив'язано маршрутного листа"));
                return;
            }

            var ticket = _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, TicketStatus.Processing);
            if (ticket == null) return;

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);

            var centralLabOpData = _context.CentralLabOpDatas.AsNoTracking()
                .Where(opData => opData.TicketId == ticket.Id)
                .OrderByDescending(data => data.SampleCheckInDateTime)
                .FirstOrDefault();

            if (centralLabOpData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning,
                        @"Помилка обробки картки"));
                return;
            }

            centralLabOpData.CheckInDateTime = DateTime.Now;
            centralLabOpData.NodeId = nodeDetailsDto.Id;
            _opDataRepository.Update<CentralLabOpData, Guid>(centralLabOpData);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.AddSampleVisa;
            nodeDetailsDto.Context.TicketContainerId = card.TicketContainerId;
            nodeDetailsDto.Context.OpDataId = centralLabOpData.Id;
            nodeDetailsDto.Context.TicketId = centralLabOpData.TicketId;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        #endregion

        #region 03_AddSampleVisa

        private void AddSampleVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;
            
            if (!_cardManager.IsLaboratoryEmployeeCard(card, nodeDetailsDto.Id))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Підпис може виконати тільки лаборант."));
                return;
            }
            
            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Реєстрація проби",
                CentralLaboratoryOpData = nodeDetailsDto.Context.OpDataId.Value,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.AddSampleVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            var centralLabOpData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (centralLabOpData == null)
            {
                return;
            }
            centralLabOpData.SampleCheckOutTime = DateTime.Now;
            _context.SaveChanges();
            
            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintLabel;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        #endregion
        
        #region 05_PrintCollisionStartVisa
        
        private void PrintCollisionStartVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;
            if (!_cardManager.IsLaboratoryEmployeeCard(card, nodeDetailsDto.Id))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Підпис може виконати тільки лаборант."));
                return;
            }
            
            var centralLabOpData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (centralLabOpData == null) return;
            
            var parameters = new Dictionary<string, object>
            {
                {"CollisionComment", centralLabOpData.LaboratoryComment}
            };

            try
            {
                _connectManager.SendSms(SmsTemplate.CentralLaboratoryCollisionSend, nodeDetailsDto.Context.TicketId, _phonesRepository.GetPhone(Phone.CentralLaboratoryWorker), parameters, cardId: card.Id);
                Logger.Info($"CentralLabolatoryProcess_PrintCollisionInit_Send: Sms was send");
            }
            catch (Exception e)
            {
                Logger.Error($"CentralLabolatoryProcess_PrintCollisionInit_Send: Error while sending sms: {e}");
            }


            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Відправлено майстру на погодження",
                CentralLaboratoryOpData = nodeDetailsDto.Context.OpDataId.Value,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionStartVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.Idle;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
        
        #endregion

        #region 07_PrintCollisionInitVisa
        
        private void PrintCollisionInitVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;
            
            if (!_cardManager.IsMasterEmployeeCard(card, nodeDetailsDto.Id))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(ProcessingMsgType.Warning,
                        @"Підпис може виконати тільки майстер."));
                return;
            }

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Відправлення на погодження",
                CentralLaboratoryOpData = nodeDetailsDto.Context?.OpDataId.Value,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInitVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
        
        #endregion
        
        #region 09_PrintAddOpVisa

        void PrintAddOpVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.TicketId == null || nodeDetailsDto.Context?.OpDataId == null) return;
            
            var centralLabOpData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (centralLabOpData == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;
            
            switch (centralLabOpData.StateId)
            {
                case OpDataState.Canceled:
                    if (!_cardManager.IsLaboratoryEmployeeCard(card, nodeDetailsDto.Id))
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                            new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Підпис може виконати тільки лаборант."));
                        return;
                    }
                    try
                    {
                        _connectManager.SendSms(SmsTemplate.ReturnToCollectSamples, nodeDetailsDto.Context.TicketId, cardId: card.Id);
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"CentralLabolatoryProcess_PrintCollisionManage_ReturnToCollectSamples: Error while sending sms: {e}");
                    }

                    break;
                case OpDataState.Rejected:
                    if (!_cardManager.IsMasterEmployeeCard(card, nodeDetailsDto.Id))
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                            new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Підпис може виконати тільки майстер."));
                        return;
                    }
                    
                    var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.TicketId.Value);
                    if (ticket == null) return;
                    var route = _routesRepository
                        .GetRoute(ticket.RouteTemplateId.Value)
                        .RouteConfig
                        .DeserializeRoute()
                        .GroupDictionary
                        .Select(x => x.Value.NodeList.ToList())
                        .ToList();

                    var sampleLabNode = route[ticket.RouteItemIndex].Single().Id;
                    Logger.Debug($"CentralLabolatoryProcess_PrintAddOpVisa: sampleLabNode = {sampleLabNode}");
                    
                    var isChanged = false;
                    if (nodeDetailsDto.Context.OpProcessData.HasValue && nodeDetailsDto.Context.OpProcessData == (int)RouteType.Move)
                    {
                        isChanged = _routesInfrastructure.SetSecondaryRoute(nodeDetailsDto.Context.TicketId.Value, sampleLabNode, RouteType.Move);
                    }
                    if (nodeDetailsDto.Context.OpProcessData.HasValue && nodeDetailsDto.Context.OpProcessData == (int)RouteType.Reload)
                    {
                        isChanged = _routesInfrastructure.SetSecondaryRoute(nodeDetailsDto.Context.TicketId.Value, sampleLabNode, RouteType.Reload);
                    }
                    if (!isChanged)
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                            ProcessingMsgType.Error, @"Немає відповідного маршруту."));
                        return;
                    }
                    break;
                default:
                    if (!_cardManager.IsLaboratoryEmployeeCard(card, nodeDetailsDto.Id))
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                            new NodeProcessingMsgItem(ProcessingMsgType.Warning, @"Підпис може виконати тільки лаборант."));
                        return;
                    }
                    _routesInfrastructure.MoveForward(nodeDetailsDto.Context.TicketId.Value, nodeDetailsDto.Id);
                    centralLabOpData.CheckOutDateTime = DateTime.Now;
                    centralLabOpData.StateId = OpDataState.Processed;
                    _opDataRepository.Update<CentralLabOpData, Guid>(centralLabOpData);
                    _connectManager.SendSms(SmsTemplate.CentralLaboratorySuccess, nodeDetailsDto.Context.TicketId, cardId: card.Id);
                    break;
            }

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підпис підсумкових результатів",
                CentralLaboratoryOpData = centralLabOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            nodeDetailsDto.Context.OpProcessData = null;
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.CentralLaboratoryProcess.State.PrintDocument;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        #endregion

        #region Functions

        private Card GetCard(NodeDetails nodeDetailsDto)
        {
            var obidRfidConfig = nodeDetailsDto.Config.Rfid[NodeData.Config.Rfid.TableReader];
            var obidRfidState = (RfidObidRwState)Program.GetDeviceState(obidRfidConfig.DeviceId);
            
            if (!_deviceRepository.IsDeviceStateValid(out NodeProcessingMsgItem errMsg, obidRfidState, TimeSpan.FromSeconds(obidRfidConfig.Timeout)))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsg);
                return null;
            }

            var card = _context.Cards.FirstOrDefault(e =>
                e.Id.Equals(obidRfidState.InData.Rifd, StringComparison.CurrentCultureIgnoreCase));

            return card;
        }

        #endregion
    }
}
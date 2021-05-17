using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.Core.DeviceManager.Card;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.ApiClient.Devices;
using Gravitas.Infrastructure.Platform.Manager;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.Dto;
using Node = Gravitas.Model.Dto.Node;

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

        public override bool ValidateNodeConfig(NodeConfig config)
        {
            if (config == null)
            {
                return false;
            }
            bool rfidValid = config.Rfid.ContainsKey(Dom.Node.Config.Rfid.TableReader);

            return rfidValid;
        }

        public override void Process()
        {
            ReadDbData();
            if (!ValidateNode(_nodeDto)) return;

            switch (_nodeDto.Context.OpRoutineStateId)
            {
                case Dom.OpRoutine.CentralLaboratoryProcess.State.Idle:
                    break;
                case Dom.OpRoutine.CentralLaboratoryProcess.State.AddSample:
                    AddSample(_nodeDto);
                    break;
                case Dom.OpRoutine.CentralLaboratoryProcess.State.AddSampleVisa:
                    AddSampleVisa(_nodeDto);
                    break;
                case Dom.OpRoutine.CentralLaboratoryProcess.State.PrintLabel:
                    break;
                case Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDataDisclose:
                    break;
                case Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionStartVisa:
                    PrintCollisionStartVisa(_nodeDto);
                    break;
                case Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit:
                    break;
                case Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInitVisa:
                    PrintCollisionInitVisa(_nodeDto);
                    break;
                case Dom.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa:
                    PrintAddOpVisa(_nodeDto);
                    break;
                case Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDocument:
                    break;
            }
        }

        #region 02_AddSample

        void AddSample(Node nodeDto)
        {
            var card = GetCard(nodeDto);
            if (card?.TicketContainerId == null) return;
            
            if (!_opRoutineManager.IsRfidCardValid(out NodeProcessingMsgItem errMsgItem, card, Dom.Card.Type.LaboratoryTray))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, errMsgItem);
                return;
            }

            if (!card.IsFree())
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning,
                        @"До картки не прив'язано маршрутного листа"));
                return;
            }

            var ticket = _ticketRepository.GetTicketInContainer(card.TicketContainerId.Value, Dom.Ticket.Status.Processing);
            if (ticket == null) return;

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);

            var centralLabOpData = _context.CentralLabOpDatas.AsNoTracking()
                .Where(opData => opData.TicketId == ticket.Id)
                .OrderByDescending(data => data.SampleCheckInDateTime)
                .FirstOrDefault();

            if (centralLabOpData == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning,
                        @"Помилка обробки картки"));
                return;
            }

            centralLabOpData.CheckInDateTime = DateTime.Now;
            centralLabOpData.NodeId = nodeDto.Id;
            _opDataRepository.Update<CentralLabOpData, Guid>(centralLabOpData);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.AddSampleVisa;
            nodeDto.Context.TicketContainerId = card.TicketContainerId;
            nodeDto.Context.OpDataId = centralLabOpData.Id;
            nodeDto.Context.TicketId = centralLabOpData.TicketId;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        #region 03_AddSampleVisa

        private void AddSampleVisa(Node nodeDto)
        {
            if (nodeDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;
            
            if (!_cardManager.IsLaboratoryEmployeeCard(card, nodeDto.Id))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, @"Підпис може виконати тільки лаборант."));
                return;
            }
            
            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Реєстрація проби",
                CentralLaboratoryOpData = nodeDto.Context.OpDataId.Value,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.AddSampleVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);

            var centralLabOpData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (centralLabOpData == null)
            {
                return;
            }
            centralLabOpData.SampleCheckOutTime = DateTime.Now;
            _context.SaveChanges();
            
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintLabel;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion
        
        #region 05_PrintCollisionStartVisa
        
        private void PrintCollisionStartVisa(Node nodeDto)
        {
            if (nodeDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;
            if (!_cardManager.IsLaboratoryEmployeeCard(card, nodeDto.Id))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, @"Підпис може виконати тільки лаборант."));
                return;
            }
            
            var centralLabOpData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (centralLabOpData == null) return;
            
            var parameters = new Dictionary<string, object>
            {
                {"CollisionComment", centralLabOpData.LaboratoryComment}
            };

            try
            {
                _connectManager.SendSms(Dom.Sms.Template.CentralLaboratoryCollisionSend, nodeDto.Context.TicketId, _phonesRepository.GetPhone(Dom.Phone.CentralLaboratoryWorker), parameters);
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
                CentralLaboratoryOpData = nodeDto.Context.OpDataId.Value,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionStartVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        #endregion

        #region 07_PrintCollisionInitVisa
        
        private void PrintCollisionInitVisa(Node nodeDto)
        {
            if (nodeDto.Context?.OpDataId == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;
            
            if (!_cardManager.IsMasterEmployeeCard(card, nodeDto.Id))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning,
                        @"Підпис може виконати тільки майстер."));
                return;
            }

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Відправлення на погодження",
                CentralLaboratoryOpData = nodeDto.Context?.OpDataId.Value,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInitVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintCollisionInit;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        #endregion
        
        #region 09_PrintAddOpVisa

        void PrintAddOpVisa(Node nodeDto)
        {
            if (nodeDto.Context?.TicketId == null || nodeDto.Context?.OpDataId == null) return;
            
            var centralLabOpData = _context.CentralLabOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (centralLabOpData == null) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDto);
            if (card == null) return;
            
            switch (centralLabOpData.StateId)
            {
                case Dom.OpDataState.Canceled:
                    if (!_cardManager.IsLaboratoryEmployeeCard(card, nodeDto.Id))
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                            new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, @"Підпис може виконати тільки лаборант."));
                        return;
                    }
                    try
                    {
                        _connectManager.SendSms(Dom.Sms.Template.ReturnToCollectSamples, nodeDto.Context.TicketId);
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"CentralLabolatoryProcess_PrintCollisionManage_ReturnToCollectSamples: Error while sending sms: {e}");
                    }

                    break;
                case Dom.OpDataState.Rejected:
                    if (!_cardManager.IsMasterEmployeeCard(card, nodeDto.Id))
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                            new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, @"Підпис може виконати тільки майстер."));
                        return;
                    }
                    
                    var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDto.Context.TicketId.Value);
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
                    if (nodeDto.Context.OpProcessData.HasValue && nodeDto.Context.OpProcessData == Dom.Route.Type.Move)
                    {
                        isChanged = _routesInfrastructure.SetSecondaryRoute(nodeDto.Context.TicketId.Value, sampleLabNode, Dom.Route.Type.Move);
                    }
                    if (nodeDto.Context.OpProcessData.HasValue && nodeDto.Context.OpProcessData == Dom.Route.Type.Reload)
                    {
                        isChanged = _routesInfrastructure.SetSecondaryRoute(nodeDto.Context.TicketId.Value, sampleLabNode, Dom.Route.Type.Reload);
                    }
                    if (!isChanged)
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                            Dom.Node.ProcessingMsg.Type.Error, @"Немає відповідного маршруту."));
                        return;
                    }
                    break;
                default:
                    if (!_cardManager.IsLaboratoryEmployeeCard(card, nodeDto.Id))
                    {
                        _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                            new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, @"Підпис може виконати тільки лаборант."));
                        return;
                    }
                    _routesInfrastructure.MoveForward(nodeDto.Context.TicketId.Value, nodeDto.Id);
                    centralLabOpData.CheckOutDateTime = DateTime.Now;
                    centralLabOpData.StateId = Dom.OpDataState.Processed;
                    _opDataRepository.Update<CentralLabOpData, Guid>(centralLabOpData);
                    _connectManager.SendSms(Dom.Sms.Template.CentralLaboratorySuccess, nodeDto.Context.TicketId);
                    break;
            }

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Підпис підсумкових результатів",
                CentralLaboratoryOpData = centralLabOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintAddOpVisa
            };
            _nodeRepository.Add<OpVisa, long>(visa);

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpProcessData = null;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.CentralLaboratoryProcess.State.PrintDocument;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        #endregion

        #region Functions

        private Card GetCard(Node nodeDto)
        {
            var obidRfidConfig = nodeDto.Config.Rfid[Dom.Node.Config.Rfid.TableReader];
            var obidRfidState = (RfidObidRwState)Program.GetDeviceState(obidRfidConfig.DeviceId);
            
            if (!_deviceRepository.IsDeviceStateValid(out NodeProcessingMsgItem errMsg, obidRfidState, TimeSpan.FromSeconds(obidRfidConfig.Timeout)))
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, errMsg);
                return null;
            }

            var card = _context.Cards.FirstOrDefault(e =>
                e.Id.Equals(obidRfidState.InData.Rifd, StringComparison.CurrentCultureIgnoreCase));

            return card;
        }

        #endregion
    }
}
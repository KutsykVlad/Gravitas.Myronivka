using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Gravitas.Core.DeviceManager.Device;
using Gravitas.Core.DeviceManager.User;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.Device;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.OpWorkflow.Routes;
using Gravitas.DAL.Repository.PhoneInformTicketAssignment;
using Gravitas.DAL.Repository.Phones;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.OneC;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.OpData;
using Gravitas.Infrastructure.Platform.Manager.OpRoutine;
using Gravitas.Infrastructure.Platform.Manager.Queue;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Device.TDO.DeviceState;
using Gravitas.Model.DomainModel.ExternalData.Contract.DAO;
using Gravitas.Model.DomainModel.ExternalData.Organization.DAO;
using Gravitas.Model.DomainModel.ExternalData.Partner.DAO;
using Gravitas.Model.DomainModel.ExternalData.Product.DAO;
using Gravitas.Model.DomainModel.ExternalData.Stock.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Detail;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpCameraImage;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpData.DAO.Base;
using Gravitas.Model.DomainModel.OpDataEvent.DAO;
using Gravitas.Model.DomainModel.OpVisa.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using Newtonsoft.Json;

namespace Gravitas.Core.Processor.OpRoutine
{
    internal class SingleWindowOpRoutineProcessor : BaseOpRoutineProcessor 
    {
        private readonly ICardRepository _cardRepository;
        private readonly IConnectManager _connectManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly IQueueManager _queueManager;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly IOpDataManager _opDataManager;
        private readonly IPhonesRepository _phonesRepository;
        private readonly IPhoneInformTicketAssignmentRepository _phoneInformTicketAssignmentRepository;
        private readonly IUserManager _userManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IRoutesRepository _routesRepository;

        public SingleWindowOpRoutineProcessor(
            IOpRoutineManager opRoutineManager,
            IDeviceManager deviceManager,
            IDeviceRepository deviceRepository,
            INodeRepository nodeRepository,
            IOpDataRepository opDataRepository,
            ITicketRepository ticketRepository,
            ICardRepository cardRepository,
            IConnectManager smsManager,
            IExternalDataRepository externalDataRepository, 
            IQueueManager queueManager, 
            IOpDataManager opDataManager,
            IPhonesRepository phonesRepository,
            IUserManager userManager, 
            IRoutesInfrastructure routesInfrastructure,
            IRoutesRepository routesRepository,
            IPhoneInformTicketAssignmentRepository phoneInformTicketAssignmentRepository) :
            base(opRoutineManager,
                deviceManager,
                deviceRepository,
                nodeRepository,
                opDataRepository)
        {
            _connectManager = smsManager;
            _externalDataRepository = externalDataRepository;
            _queueManager = queueManager;
            _opDataManager = opDataManager;
            _phonesRepository = phonesRepository;
            _userManager = userManager;
            _routesInfrastructure = routesInfrastructure;
            _routesRepository = routesRepository;
            _phoneInformTicketAssignmentRepository = phoneInformTicketAssignmentRepository;
            _ticketRepository = ticketRepository;
            _cardRepository = cardRepository;
        }
        
        public override void Process()
        {
            ReadDbData();

            switch (NodeDetails.Context.OpRoutineStateId) {
                case Model.DomainValue.OpRoutine.SingleWindow.State.Idle:
                    Idle(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.SingleWindow.State.GetTicket:
                    break;
                case Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu:
                    break;
                case Model.DomainValue.OpRoutine.SingleWindow.State.ContainerCloseAddOpVisa:
                    ContainerCloseAddOpVisa(NodeDetails);
                    break;  
                case Model.DomainValue.OpRoutine.SingleWindow.State.SupplyChangeAddOpVisa:
                    SupplyChangeAddOpVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.SingleWindow.State.DivideTicketAddOpVisa:
                    DivideTicketAddOpVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.SingleWindow.State.DeleteTicketAddOpVisa:
                    DeleteTicketAddOpVisa(NodeDetails);
                    break;

                case Model.DomainValue.OpRoutine.SingleWindow.State.EditTicketForm:
                    break;
                case Model.DomainValue.OpRoutine.SingleWindow.State.EditGetApiData:
                    EditGetApiData(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.SingleWindow.State.EditAddOpVisa:
                    EditAddOpVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.SingleWindow.State.EditPostApiData:
                    EditPostApiData(NodeDetails);
                    break;

                case Model.DomainValue.OpRoutine.SingleWindow.State.CloseAddOpVisa:
                    CloseAddOpVisa(NodeDetails);
                    break;
                case Model.DomainValue.OpRoutine.SingleWindow.State.ClosePostApiData:
                    ClosePostApiData(NodeDetails);
                    break;

                case Model.DomainValue.OpRoutine.SingleWindow.State.RouteEditData:
                    break;
                case Model.DomainValue.OpRoutine.SingleWindow.State.RouteAddOpVisa:
                    RouteAddOpVisa(NodeDetails);
                    break;
            }
        }

        private void Idle(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Config == null || !nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader)) return;

            var card = GetCardFromTableReader(nodeDetailsDto);
            if (card == null) return;

            TicketContainer ticketContainer = null;
            if (card.TicketContainerId != null)
                ticketContainer = _context.TicketContainers.FirstOrDefault(x => x.Id == card.TicketContainerId.Value);

            if (ticketContainer == null) {
                ticketContainer = _ticketRepository.NewTicketContainer();

                card.TicketContainerId = ticketContainer.Id;
                _cardRepository.Update<Card, string>(card);
            }

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.GetTicket;
            nodeDetailsDto.Context.TicketContainerId = ticketContainer.Id;
            _nodeRepository.ClearNodeProcessingMessage(_nodeId);

            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
        
        private void ContainerCloseAddOpVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketContainerId == null || !nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader)) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;
            
            var container = _context.TicketContainers.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.TicketContainerId.Value);
            if (container == null) return;

            _queueManager.RemoveFromQueue(container.Id);

            var cardList = _context.Cards.AsNoTracking().Where(e => e.TicketContainerId == nodeDetailsDto.Context.TicketContainerId).ToList();
            foreach (var c in cardList) {

                c.TicketContainerId = null;
                _cardRepository.Update<Card, string>(c);
            }
            
            var tickets = _context.Tickets.AsNoTracking().Where(x => x.TicketContainerId == nodeDetailsDto.Context.TicketContainerId.Value && x.RouteTemplateId.HasValue);
            foreach (var t in tickets)
            {
                var routeTemplateId = t.RouteTemplateId;
                t.RouteTemplateId = null;
                t.StatusId = TicketStatus.Closed;
                _ticketRepository.Update<Ticket, int>(t);
                if (routeTemplateId.HasValue)
                {
                    var routeTemplate = _routesRepository.GetRoute(routeTemplateId.Value);
                    if (!string.IsNullOrEmpty(routeTemplate.OwnerId))
                    {
                        _routesRepository.DeleteRoute(routeTemplate.Id);
                    }
                }
            }

            container.StatusId = TicketContainerStatus.Inactive;
            _nodeRepository.Update<TicketContainer, int>(container);
            
            _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id,
                new NodeProcessingMsgItem(ProcessingMsgType.Success, "Мітки та картки роз'єднано"));

            var ticket = _ticketRepository.GetTicketInContainer(nodeDetailsDto.Context.TicketContainerId.Value, TicketStatus.ToBeProcessed);
            if (ticket == null) _connectManager.SendSms(SmsTemplate.RemovedFromQueue, nodeDetailsDto.Context.TicketId);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.Idle;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void SupplyChangeAddOpVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketContainerId == null || !nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader)) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Зміна коду поставки",
                SingleWindowOpDataId = nodeDetailsDto.Context.OpDataId,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.SupplyChangeAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
        
        private void DeleteTicketAddOpVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketId == null || !nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader)) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.TicketId.Value);
            if (ticket == null) return;

            var opData = _opDataRepository.GetFirstOrDefault<SingleWindowOpData, Guid>(x => x.TicketId == ticket.Id);
            if (opData == null) return;
            
            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Видалення ТТН",
                SingleWindowOpDataId = opData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.DeleteTicketAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);
            
            ticket.StatusId = TicketStatus.Canceled;
            _ticketRepository.Update<Ticket, int>(ticket);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.GetTicket;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }
        
        private void DivideTicketAddOpVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketContainerId == null || nodeDetailsDto.Context?.TicketId == null || !nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader)) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var ticket = _context.Tickets.First(x => x.Id == nodeDetailsDto.Context.TicketId.Value);
            if (ticket.StatusId != TicketStatus.Completed)
            {
                return;
            }
            
            SignalRInvoke.StartSpinner(nodeDetailsDto.Id);
            
            var newTicket = _ticketRepository.NewTicket(nodeDetailsDto.Context.TicketContainerId.Value);
            newTicket.StatusId = TicketStatus.Completed;
            _ticketRepository.Update<Ticket, int>(newTicket);
            
            var opVisas = new List<OpVisa>();
            var pictures = new List<OpCameraImage>();
            var netto = nodeDetailsDto.Context.OpProcessData.Value;
            var currentBrutto = _context.ScaleOpDatas.AsNoTracking().Where(x => x.TicketId == nodeDetailsDto.Context.TicketId.Value
                                                                                   && x.TypeId == ScaleOpDataType.Gross
                                                                                   && x.StateId == OpDataState.Processed)
                .OrderByDescending(x => x.Id)
                .First();
            var currentTare = _context.ScaleOpDatas.AsNoTracking().Where(x => x.TicketId == nodeDetailsDto.Context.TicketId.Value
                                                                              && x.TypeId == ScaleOpDataType.Tare
                                                                              && x.StateId == OpDataState.Processed)
                .OrderByDescending(x => x.Id)
                .First();
            
            var newTicketBrutto = currentBrutto.TruckWeightValue;
            currentBrutto.TruckWeightValue = currentTare.TruckWeightValue + netto;
            var newTicketTare = currentBrutto.TruckWeightValue;

            var windowOpData = _context.SingleWindowOpDatas.First(x => x.TicketId == nodeDetailsDto.Context.TicketId.Value);
            var oneCApiClient = new OneCApiClient(GlobalConfigurationManager.OneCApiHost);
            OneCApiClient.GetDeliveryBill.Response deliveryBill = null;
            try {
                deliveryBill = !string.IsNullOrWhiteSpace(windowOpData.SupplyCode) 
                    ? oneCApiClient.GetDeliveryBillViaSupplyCode(windowOpData.SupplyCode)
                    : oneCApiClient.GetDeliveryBillViaBarCode(windowOpData.BarCode);
            }
            catch (Exception e) {
                Logger.Error($"SingleWindow. EditGetApi: Error while processing OneC api request: {e}");
            }
    
            if (deliveryBill == null || deliveryBill.ErrorCode != 0 || deliveryBill.Id == null || deliveryBill.Id == string.Empty) 
            {
                _opRoutineManager.UpdateProcessingMessage(windowOpData.NodeId.Value, new NodeProcessingMsgItem(
                    ProcessingMsgType.Warning, $"Помилка WebAPI. {deliveryBill?.ErrorMsg}"));
    
                return;
            }
            
            windowOpData.GrossValue = newTicketTare;
            windowOpData.NetValue =  windowOpData.GrossValue - windowOpData.TareValue;
            _ticketRepository.Update<SingleWindowOpData, Guid>(windowOpData);
            
            windowOpData.Id = new Guid();
            windowOpData.TicketId = newTicket.Id;
            windowOpData.DeliveryBillId = deliveryBill.Id;
            windowOpData.DeliveryBillCode = deliveryBill.Code;
            windowOpData.TareValue = newTicketTare;
            windowOpData.GrossValue = newTicketBrutto;
            windowOpData.NetValue = newTicketBrutto - newTicketTare;
            _ticketRepository.Add<SingleWindowOpData, Guid>(windowOpData);
            
            var opDatas = _opDataRepository.GetOpDataList(nodeDetailsDto.Context.TicketId.Value);
            foreach (var opData in opDatas)
            {
                dynamic opDataType = Assembly.GetAssembly(typeof(BaseOpData))
                    .GetTypes()
                    .First(type => type.IsSubclassOf(typeof(BaseOpData)) && type.FullName == opData.GetType().BaseType.FullName);
                
                dynamic data = GetOpDataList(Activator.CreateInstance(opDataType), opData.Id);

                switch (opData.GetType().BaseType.Name)
                {
                    case nameof(SingleWindowOpData):
                        var sOpData = (SingleWindowOpData)data;
                        opVisas = _context.OpVisas.AsNoTracking().Where(x => x.SingleWindowOpDataId == sOpData.Id).ToList();
                        pictures = _context.OpCameraImages.AsNoTracking().Where(x => x.SingleWindowOpDataId == sOpData.Id).ToList();

                        foreach (var opVisa in opVisas)
                        {
                            opVisa.SingleWindowOpDataId = sOpData.Id;
                            _opDataRepository.Add<OpVisa, int>(opVisa);
                        }
                        
                        foreach (var picture in pictures)
                        {
                            picture.SingleWindowOpDataId = sOpData.Id;
                            _opDataRepository.Add<OpCameraImage, int>(picture);
                        }
                        
                        break;
                    case nameof(SecurityCheckInOpData):
                        var sqOpData = (SecurityCheckInOpData)data;
                        opVisas = _context.OpVisas.AsNoTracking().Where(x => x.SecurityCheckInOpDataId == sqOpData.Id).ToList();
                        pictures = _context.OpCameraImages.AsNoTracking().Where(x => x.SecurityCheckInOpDataId == sqOpData.Id).ToList();
                        
                        sqOpData.Id = new Guid();
                        sqOpData.TicketId = newTicket.Id;
                        _opDataRepository.Add<SecurityCheckInOpData, Guid>(sqOpData);
                        
                        foreach (var opVisa in opVisas)
                        {
                            opVisa.SecurityCheckInOpDataId = sqOpData.Id;
                            _opDataRepository.Add<OpVisa, int>(opVisa);
                        }
                        
                        foreach (var picture in pictures)
                        {
                            picture.SecurityCheckInOpDataId = sqOpData.Id;
                            _opDataRepository.Add<OpCameraImage, int>(picture);
                        }
                        
                        break;
                    case nameof(SecurityCheckOutOpData):
                        var sqoOpData = (SecurityCheckOutOpData)data;
                        opVisas = _context.OpVisas.AsNoTracking().Where(x => x.SecurityCheckOutOpDataId == sqoOpData.Id).ToList();
                        pictures = _context.OpCameraImages.AsNoTracking().Where(x => x.SecurityCheckOutOpDataId == sqoOpData.Id).ToList();
                        
                        sqoOpData.Id = new Guid();
                        sqoOpData.TicketId = newTicket.Id;
                        _opDataRepository.Add<SecurityCheckOutOpData, Guid>(sqoOpData);
                        
                        foreach (var opVisa in opVisas)
                        {
                            opVisa.SecurityCheckOutOpDataId = sqoOpData.Id;
                            _opDataRepository.Add<OpVisa, int>(opVisa);
                        }
                        
                        foreach (var picture in pictures)
                        {
                            picture.SecurityCheckOutOpDataId = sqoOpData.Id;
                            _opDataRepository.Add<OpCameraImage, int>(picture);
                        }
                        
                        break;
                    case nameof(ScaleOpData):
                        var scOpData = (ScaleOpData)data;
                        opVisas = _context.OpVisas.AsNoTracking().Where(x => x.ScaleTareOpDataId == scOpData.Id).ToList();
                        pictures = _context.OpCameraImages.AsNoTracking().Where(x => x.ScaleOpDataId == scOpData.Id).ToList();
                        
                        scOpData.Id = new Guid();
                        scOpData.TicketId = newTicket.Id;
                        scOpData.TruckWeightValue = scOpData.TypeId == ScaleOpDataType.Gross ? newTicketBrutto : newTicketTare;
                        _opDataRepository.Add<ScaleOpData, Guid>(scOpData);
                        
                        foreach (var opVisa in opVisas)
                        {
                            opVisa.ScaleTareOpDataId = scOpData.Id;
                            _opDataRepository.Add<OpVisa, int>(opVisa);
                        }
                        
                        foreach (var picture in pictures)
                        {
                            picture.ScaleOpDataId = scOpData.Id;
                            _opDataRepository.Add<OpCameraImage, int>(picture);
                        }
                        
                        break;
                    case nameof(NonStandartOpData):
                        var nOpData = (NonStandartOpData)data;

                        nOpData.Id = new Guid();
                        nOpData.TicketId = newTicket.Id;
                        _opDataRepository.Add<NonStandartOpData, Guid>(nOpData);
                        break;
                }
            }

            var events = _context.OpDataEvents.AsNoTracking().Where(x => x.TicketId == nodeDetailsDto.Context.TicketId.Value).ToList();
            foreach (var e in events)
            {
                var newEvent = new OpDataEvent
                {
                    Cause = e.Cause,
                    Created = e.Created,
                    EmployeeId = e.EmployeeId,
                    NodeId = e.NodeId,
                    OpDataEventType = e.OpDataEventType,
                    TicketId = newTicket.Id,
                    Weight = e.Weight,
                    TypeOfTransaction = e.TypeOfTransaction
                };
                _opDataRepository.Add<OpDataEvent, int>(newEvent);
            }

            _opDataRepository.Update<ScaleOpData, Guid>(currentTare);
            _opDataRepository.Update<ScaleOpData, Guid>(currentBrutto);

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Розділена ТТН",
                SingleWindowOpDataId = nodeDetailsDto.Context.OpDataId,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.DivideTicketAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            nodeDetailsDto.Context.OpProcessData = null;
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private TEntity GetOpDataList<TEntity>(TEntity ignored, Guid id) where TEntity : BaseOpData
        {
            return _opDataRepository.GetFirstOrDefault<TEntity, Guid>(x => x.Id == id);
        }

        private void EditGetApiData(NodeDetails nodeDetailsDto)
        {
            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);
            if (nodeDetailsDto.Context?.OpDataId == null) return;

            var windowOpData = _context.SingleWindowOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (windowOpData == null) return;
            var result = windowOpData.SupplyCode == TechRoute.SupplyCode || GetOneCDataForSingleWindowOpData(windowOpData);
            if (!result)
            {
                nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
                UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
                return;
            }

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.EditTicketForm;
            nodeDetailsDto.Context.OpDataId = windowOpData.Id;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private bool GetOneCDataForSingleWindowOpData(SingleWindowOpData windowOpData)
        {
            var oneCApiClient = new OneCApiClient(GlobalConfigurationManager.OneCApiHost);
            OneCApiClient.GetDeliveryBill.Response deliveryBill = null;
                try {
                    deliveryBill = !string.IsNullOrWhiteSpace(windowOpData.SupplyCode) 
                        ? oneCApiClient.GetDeliveryBillViaSupplyCode(windowOpData.SupplyCode)
                        : oneCApiClient.GetDeliveryBillViaBarCode(windowOpData.BarCode);
                }
                catch (Exception e) {
                    Logger.Error($"SingleWindow. EditGetApi: Error while processing OneC api request: {e}");
                }
    
                if (deliveryBill == null || deliveryBill.ErrorCode != 0 || deliveryBill.Id == null || deliveryBill.Id == string.Empty) 
                {
                    _opRoutineManager.UpdateProcessingMessage(windowOpData.NodeId.Value, new NodeProcessingMsgItem(
                        ProcessingMsgType.Warning, $"Помилка WebAPI. {deliveryBill?.ErrorMsg}"));
    
                    return false;
                    
                }
                
                windowOpData.CreateDate = DateTime.Now;
                windowOpData.DeliveryBillId = deliveryBill.Id;
                windowOpData.DeliveryBillCode = deliveryBill.Code;
                windowOpData.SupplyTypeId = deliveryBill.SupplyTypeId;
                windowOpData.OrderCode = deliveryBill.OrderCode;
                windowOpData.DocumentTypeId = deliveryBill.DocumentTypeId;
                windowOpData.OrganizationId = deliveryBill.OrganizationId;
                windowOpData.KeeperOrganizationId = deliveryBill.KeeperOrganizationId;
                windowOpData.StockId = deliveryBill.StockId;
                windowOpData.ReceiverTypeId = deliveryBill.ReceiverTypeId;
                windowOpData.ReceiverId = deliveryBill.ReceiverId;
                windowOpData.ReceiverAnaliticsId = deliveryBill.ReceiverAnaliticsId;
                windowOpData.ProductId = deliveryBill.ProductId;
                windowOpData.HarvestId = deliveryBill.HarvestId;
                windowOpData.BuyBudgetId = deliveryBill.BuyBudgetId;
                windowOpData.SellBudgetId = deliveryBill.SellBudgetId;
                windowOpData.ProductContents = JsonConvert.SerializeObject(deliveryBill.ProductContents); 
                windowOpData.IsThirdPartyCarrier = deliveryBill.IsThirdPartyCarrier == "1";
                windowOpData.HiredTrailerNumber = deliveryBill.HiredTrailerNumberId;
                windowOpData.ContractCarrierId = deliveryBill.ContractCarrierId;
                windowOpData.SenderWeight = deliveryBill.SenderWeight;
                windowOpData.OriginalТТN = deliveryBill.OriginalТТN;
                windowOpData.HiredDriverCode = deliveryBill.HiredDriverCode;
                windowOpData.IncomeInvoiceSeries = deliveryBill.IncomeInvoiceSeries;
                windowOpData.IncomeInvoiceNumber = deliveryBill.IncomeInvoiceNumber;
                windowOpData.IncomeDocGrossValue = deliveryBill.IncomeDocGrossValue;
                windowOpData.IncomeDocTareValue = deliveryBill.IncomeDocTareValue;
                windowOpData.CarrierId = deliveryBill.CarrierCodeId;
                windowOpData.CarrierCode = deliveryBill.CarrierCodeId.HasValue
                    ? _externalDataRepository.GetPartnerDetail(deliveryBill.CarrierCodeId.Value)?.Code ?? "Хибний ключ"
                    : string.Empty;
                windowOpData.TransportId = deliveryBill.TransportId;
                windowOpData.TrailerId = deliveryBill.TrailerId;
                windowOpData.DriverOneId = deliveryBill.DriverOneId;
                windowOpData.DriverTwoId = deliveryBill.DriverTwoId;
                windowOpData.HiredTransportNumber = deliveryBill.HiredTransportNumberId;
                windowOpData.IncomeDocDateTime = deliveryBill.IncomeDocDate;
                windowOpData.SupplyTypeId = deliveryBill.SupplyTypeId;
                windowOpData.StatusType = deliveryBill.StatusType;
                windowOpData.CarrierRouteId = deliveryBill.CarrierRouteId;
                windowOpData.ReceiverDepotId = deliveryBill.ReceiverDepotId;
                windowOpData.SupplyCode = deliveryBill.SupplyCode;
                windowOpData.ReceiverTitle = deliveryBill.ReceiverId.HasValue
                    ? _externalDataRepository.GetStockDetail(deliveryBill.ReceiverId.Value)?.ShortName
                    ?? _externalDataRepository.GetSubdivisionDetail(deliveryBill.ReceiverId.Value)?.ShortName
                    ?? _externalDataRepository.GetPartnerDetail(deliveryBill.ReceiverId.Value)?.ShortName
                    : null;
                windowOpData.OrganizationTitle = deliveryBill.OrganizationId.HasValue
                    ? _externalDataRepository.GetOrganisationDetail(deliveryBill.OrganizationId.Value)?.ShortName
                    : null;
                windowOpData.ProductTitle = deliveryBill.ProductId.HasValue
                    ? _externalDataRepository.GetProductDetail(deliveryBill.ProductId.Value)?.ShortName 
                    : null;
            _opDataRepository.AddOrUpdate<SingleWindowOpData, Guid>(windowOpData);
    
                UpdateDeliveryBillDictionaryItems(windowOpData, oneCApiClient);

                _opRoutineManager.UpdateProcessingMessage(windowOpData.NodeId.Value, new NodeProcessingMsgItem(
                    ProcessingMsgType.Success, "WebAPI. Дані отримано успішно"));
                return true;
        }

        private void UpdateDeliveryBillDictionaryItems(SingleWindowOpData windowOpData, OneCApiClient oneCApiClient)
        {
            try
            {
                if (windowOpData.ReceiverId.HasValue)
                {
                    Logger.Info($"SingleWindow. EditGetApi: Update Partner: Id = {windowOpData.ReceiverId}");
                    var partner = oneCApiClient.GetPartner(windowOpData.ReceiverId.Value);
                    if (partner != null) _externalDataRepository.AddOrUpdate<Partner, Guid>(partner);
                }
            }
            catch (Exception e)
            {
                Logger.Warn($"SingleWindow. EditGetApi: Error while updating Partner dictionaries. Error: {e.Message}");
            }
            try
            {
                if (windowOpData.CarrierId.HasValue)
                {
                    Logger.Info($"SingleWindow. EditGetApi: Update Partner: Id = {windowOpData.CarrierId}");
                    var partner = oneCApiClient.GetPartner(windowOpData.CarrierId.Value);
                    if (partner != null) _externalDataRepository.AddOrUpdate<Partner, Guid>(partner); 
                }
            }
            catch (Exception e)
            {
                Logger.Warn($"SingleWindow. EditGetApi: Error while updating Partner dictionaries. Error: {e.Message}");
            }
            try
            {
                if (windowOpData.ReceiverId.HasValue)
                {
                    Logger.Info($"SingleWindow. EditGetApi: Update Stock: Id = {windowOpData.ReceiverId}");
                    var stock = oneCApiClient.GetStock(windowOpData.ReceiverId.Value);
                    if (stock != null) _externalDataRepository.AddOrUpdate<Stock, Guid>(stock);
                }
            }
            catch (Exception e)
            {
                Logger.Warn($"SingleWindow. EditGetApi: Error while updating Stock dictionaries. Error: {e.Message}");
            }
            try
            {
                if (windowOpData.OrganizationId.HasValue)
                {
                    Logger.Info($"SingleWindow. EditGetApi: Update Organisation: Id = {windowOpData.OrganizationId}");
                    var organisation = oneCApiClient.GetOrganisation(windowOpData.OrganizationId.Value);
                    if (organisation != null) _externalDataRepository.AddOrUpdate<Organisation, Guid>(organisation);  
                }
            }
            catch (Exception e)
            {
                Logger.Warn($"SingleWindow. EditGetApi: Error while updating Organisation dictionaries. Error: {e.Message}");
            }
            try
            {
                if (windowOpData.ProductId.HasValue)
                {
                    Logger.Info($"SingleWindow. EditGetApi: Update Product: Id = {windowOpData.ProductId}");
                    var product = oneCApiClient.GetProduct(windowOpData.ProductId.Value);
                    if (product != null) _externalDataRepository.AddOrUpdate<Product, Guid>(product);  
                }
            }
            catch (Exception e)
            {
                Logger.Warn($"SingleWindow. EditGetApi: Error while updating Product dictionaries. Error: {e.Message}");
            }
            try
            {
                if (windowOpData.StockId.HasValue)
                {
                    Logger.Info($"SingleWindow. EditGetApi: Update Stock: Id = {windowOpData.StockId}");
                    var stock = oneCApiClient.GetStock(windowOpData.StockId.Value);
                    if (stock != null) _externalDataRepository.AddOrUpdate<Stock, Guid>(stock); 
                }
            }
            catch (Exception e)
            {
                Logger.Warn($"SingleWindow. EditGetApi: Error while updating Stock dictionaries. Error: {e.Message}");
            }
            try
            {
                if (windowOpData.ContractCarrierId.HasValue)
                {
                    Logger.Info($"SingleWindow. EditGetApi: Update Contract: Id = {windowOpData.ContractCarrierId}");
                    var contract = oneCApiClient.GetContract(windowOpData.ContractCarrierId.Value);
                    if (contract != null) _externalDataRepository.AddOrUpdate<Contract, Guid>(contract);
                }
            }
            catch (Exception e)
            {
                Logger.Warn($"SingleWindow. EditGetApi: Error while updating Contract dictionaries. Error: {e.Message}");
            }
        }

        private void EditAddOpVisa(NodeDetails nodeDetailsDto)
        {
            // Validate node context
            if (nodeDetailsDto?.Context?.TicketContainerId == null
                || nodeDetailsDto.Context?.TicketId == null
                || nodeDetailsDto.Context?.OpDataId == null
                || !nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader))
                return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var windowOpData = _context.SingleWindowOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (windowOpData == null) return;

                var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Правка документу",
                SingleWindowOpDataId = windowOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.EditAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            windowOpData.EditOperatorId = card.EmployeeId;
            windowOpData.CheckOutDateTime = DateTime.Now;
            windowOpData.StateId = OpDataState.Processed;
            _opDataRepository.Update<SingleWindowOpData, Guid>(windowOpData);
            
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.EditPostApiData;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
            _nodeRepository.ClearNodeProcessingMessage(_nodeId);
        }

        private Infrastructure.Platform.ApiClient.OneC.OneCApiClient.UpdateDeliveryBillDto.Request GetUpdateDeliveryBillDtoRequest(SingleWindowOpData singleWindowOpData)
        {
            var createData = _opDataRepository.GetLastProcessed<ScaleOpData>(x => x.TicketId == singleWindowOpData.TicketId && x.TypeId == ScaleOpDataType.Gross)?
                .CheckOutDateTime;
            return new Infrastructure.Platform.ApiClient.OneC.OneCApiClient.UpdateDeliveryBillDto.Request
            {
                Activity = 0,

                Id = singleWindowOpData.DeliveryBillId,
                CreateOperatorId = singleWindowOpData.CreateOperatorId,
                CreateDate = createData ?? singleWindowOpData.CreateDate,
                EditDate = singleWindowOpData.EditDate,
                RegistrationTime = singleWindowOpData.RegistrationDateTime,
                InTime = singleWindowOpData.InTime,
                OutTime = singleWindowOpData.OutTime,
                FirstGrossTime = singleWindowOpData.FirstGrossTime,
                FirstTareTime = singleWindowOpData.FirstTareTime,
                LastGrossTime = singleWindowOpData.LastGrossTime,
                LastTareTime = singleWindowOpData.LastTareTime,
                EditOperatorId = singleWindowOpData.EditOperatorId,
                GrossValue = singleWindowOpData.GrossValue ?? 0,
                TareValue = singleWindowOpData.TareValue ?? 0,
                NetValue = (singleWindowOpData.NetValue ?? 0) - (singleWindowOpData.PackingWeightValue ?? 0),
                DriverOneId = singleWindowOpData.DriverOneId,
                DriverTwoId = singleWindowOpData.DriverTwoId,
                TransportId = singleWindowOpData.TransportId,
                HiredDriverCode = singleWindowOpData.HiredDriverCode,
                HiredTansportNumber = singleWindowOpData.HiredTransportNumber,
                IncomeInvoiceSeries = singleWindowOpData.IncomeInvoiceSeries,
                IncomeInvoiceNumber = singleWindowOpData.IncomeInvoiceNumber,
                ReceiverStockId = singleWindowOpData.ReceiverDepotId,
                IsThirdPartyCarrier = singleWindowOpData.IsThirdPartyCarrier ? "1" : "0",
                CarrierCode = string.IsNullOrWhiteSpace(singleWindowOpData.CarrierCode) 
                    ? singleWindowOpData.CustomPartnerName 
                    : singleWindowOpData.CarrierId.ToString(),
                BuyBudgetsId = singleWindowOpData.BuyBudgetId,
                SellBudgetsId = singleWindowOpData.SellBudgetId,
                PackingWeightValue = singleWindowOpData.PackingWeightValue ?? 0,
                SupplyCode = singleWindowOpData.SupplyCode,
                CollectionPointId = singleWindowOpData.CollectionPointId,
                LabHumidityType = singleWindowOpData.LabHumidityName,
                LabImpurityType = singleWindowOpData.LabImpurityName,
                LabIsInfectioned = singleWindowOpData.LabIsInfectioned ? "1" : "0",
                LabHumidityValue = singleWindowOpData.LabHumidityValue ?? 0,
                LabImpurityValue = singleWindowOpData.LabImpurityValue ?? 0,
                DocHumidityValue = singleWindowOpData.DocHumidityValue ?? 0,
                DocImpurityValue = singleWindowOpData.DocImpurityValue ?? 0,
                DocNetValue = singleWindowOpData.DocumentTypeId == ExternalData.DeliveryBill.Type.Outgoing 
                    ? (singleWindowOpData.NetValue ?? 0) - (singleWindowOpData.PackingWeightValue ?? 0) 
                    : singleWindowOpData.DocNetValue ?? 0,
                DocNetDateTime = singleWindowOpData.DocNetDateTime,
                ReturnCauseId = singleWindowOpData.ReturnCauseId,
                TrailerId = singleWindowOpData.TrailerId,
                TrailerNumber = singleWindowOpData.HiredTrailerNumber,
                TripTicketNumber = singleWindowOpData.TripTicketNumber,
                TripTicketDateTime = singleWindowOpData.TripTicketDateTime,
                WarrantSeries = singleWindowOpData.WarrantSeries,
                WarrantNumber = singleWindowOpData.WarrantNumber,
                WarrantDateTime = singleWindowOpData.WarrantDateTime,
                WarrantManagerName = singleWindowOpData.WarrantManagerName,
                StampList = singleWindowOpData.StampList,
                RuleNumber = singleWindowOpData.RuleNumber,
                TrailerGrossValue = singleWindowOpData.TrailerGrossValue ?? 0,
                TrailerTareValue = singleWindowOpData.TrailerTareValue ?? 0,
                IncomeDocGrossValue = singleWindowOpData.IncomeDocGrossValue ?? 0,
                IncomeDocTareValue = singleWindowOpData.IncomeDocTareValue ?? 0,
                IncomeDocDateTime = singleWindowOpData.IncomeDocDateTime,
                Comments = $"{singleWindowOpData.Comments} {_opDataRepository.GetLastProcessed<LabFacelessOpData>(singleWindowOpData.TicketId)?.Comment}",
                WeightDeltaValue = singleWindowOpData.WeightDeltaValue ?? 0,
                SupplyType = singleWindowOpData.SupplyTransportTypeId,
                LabolatoryOperatorId = singleWindowOpData.LabolatoryOperatorId,
                GrossOperatorId = string.Empty,
                ScaleInNumber = singleWindowOpData.ScaleInNumber,
                ScaleOutNumber = singleWindowOpData.ScaleOutNumber,
                BatchNumber = singleWindowOpData.BatchNumber,
                TareOperatorId = string.Empty,
                LoadingOperatorId = singleWindowOpData.LoadingOperatorId,
                LoadOutDate = singleWindowOpData.LoadOutDateTime,
                CarrierRouteId = singleWindowOpData.CarrierRouteId,
                LabOilContentValue = singleWindowOpData.LabOilContentValue ?? 0,
                InformationCarrier = singleWindowOpData.InformationCarrier,
                WeightingStatistics = _opDataManager.GetEvents(singleWindowOpData.TicketId.Value, (int?) OpDataEventType.Weight)
            };
        }

        private void EditPostApiData(NodeDetails nodeDetailsDto)
        {
            _nodeRepository.ClearNodeProcessingMessage(nodeDetailsDto.Id);

            if (nodeDetailsDto.Context?.OpDataId == null) return;

            var singleWindowOpData = _context.SingleWindowOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (singleWindowOpData == null) return;

            var useOneC = singleWindowOpData.SupplyCode != TechRoute.SupplyCode;

            Infrastructure.Platform.ApiClient.OneC.OneCApiClient.UpdateDeliveryBillDto.Response response = null;
            if (useOneC)
            {
                var request = GetUpdateDeliveryBillDtoRequest(singleWindowOpData);
                request.Activity = 0; // Update data 

                Logger.Info(JsonConvert.SerializeObject(request));
            
                try 
                {
                    var oneCApiClient = new OneCApiClient(GlobalConfigurationManager.OneCApiHost);
                    response = oneCApiClient.PostUpdateDeliveryBill(request, 200);
                }
                catch (Exception e) 
                {
                    Logger.Error($"SingleWindow. EditPostApiData: Error while processing OneC api request: {e}");
                }
            }
           

            if (useOneC && (response == null || response.ErrorCode != 0)) {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    ProcessingMsgType.Error, $"Помилка WebAPI. {response?.ErrorMsg}"));

                nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.EditTicketForm;
            }
            else
            {
                if (useOneC)
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                        ProcessingMsgType.Success,
                        "WebAPI. Дані передано успішно")); 
                }
                else
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                        ProcessingMsgType.Warning,
                        "Дані в 1С не були відправлені."));
                }
                

                nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
            }
           
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private void CloseAddOpVisa(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.Context?.TicketId == null || !nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader)) return;

            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var windowOpData = _context.SingleWindowOpDatas.FirstOrDefault(x => x.TicketId == nodeDetailsDto.Context.TicketId.Value);
            if (windowOpData == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Проводка документу",
                SingleWindowOpDataId = windowOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.CloseAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);

            windowOpData.StateId = OpDataState.Processed;
            windowOpData.CheckInDateTime = DateTime.Now;
            _ticketRepository.Update<SingleWindowOpData, Guid>(windowOpData);

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ClosePostApiData;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
            _nodeRepository.ClearNodeProcessingMessage(_nodeId);
        }

        private void ClosePostApiData(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto?.Context?.TicketId == null) return;
            
            var singleWindowOpData = _context.SingleWindowOpDatas.FirstOrDefault(x => x.TicketId == nodeDetailsDto.Context.TicketId.Value);
            if (singleWindowOpData == null) return;

            var cameraPostRequests = GetCameraPostRequest(singleWindowOpData);
            foreach (var cameraPostRequest in cameraPostRequests)
            {
                try 
                {
                    var oneCApiClient = new OneCApiClient(GlobalConfigurationManager.OneCApiHost);
                    oneCApiClient.PostImageFileJsonResponse(cameraPostRequest, 200);
                }
                catch (Exception e) 
                {
                    Logger.Error($"SingleWindow. CameraImagesPost: Error while processing OneC api request: {e}");
                }
            }

            if (cameraPostRequests.Count == 0)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    ProcessingMsgType.Error, $"Немає фотографій. Проводка документу відмінена."));

                nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
                UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
                return;
            }
            
            Logger.Info($"SingleWindow. ClosePostApiData: Posted {cameraPostRequests.Count} on Id: {singleWindowOpData.DeliveryBillId}");
            
            var request = GetUpdateDeliveryBillDtoRequest(singleWindowOpData);

            request.Activity = 1; // Update data and CloseTicket
            Logger.Info(JsonConvert.SerializeObject(request));

            OneCApiClient.UpdateDeliveryBillDto.Response updateResponse = null;
            try 
            {
                var oneCApiClient = new OneCApiClient(GlobalConfigurationManager.OneCApiHost);
                updateResponse = oneCApiClient.PostUpdateDeliveryBill(request, 200);
            }
            catch (Exception e) 
            {
                Logger.Error($"SingleWindow. CreatePostApiData: Error while processing OneC api request: {e}");
            }

            if (updateResponse == null || updateResponse.ErrorCode != 0) 
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                    ProcessingMsgType.Error,$"Помилка WebAPI. {updateResponse?.ErrorMsg}"));

                nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
                UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
                return;
            }

            var ticket = _context.Tickets.First(x => x.Id == nodeDetailsDto.Context.TicketId.Value);
            ticket.StatusId = TicketStatus.Closed;
            _context.SaveChanges();
            
            singleWindowOpData.CheckOutDateTime = DateTime.Now;
            _opDataRepository.Update<SingleWindowOpData, Guid>(singleWindowOpData);

            _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, new NodeProcessingMsgItem(
                ProcessingMsgType.Success, "WebAPI. Дані передано успішно"));

            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context);
        }

        private List<OneCApiClient.PostImageFileDto.Request> GetCameraPostRequest(SingleWindowOpData singleWindowOpData)
        {
            var result = new List<OneCApiClient.PostImageFileDto.Request>();
            if (!singleWindowOpData.TicketId.HasValue) return result;

            var opDataList = _opDataRepository.GetOpDataList(singleWindowOpData.TicketId.Value).OrderByDescending(x => x.CheckInDateTime).ToList();
            foreach (var opData in opDataList)
            {
                var imageList = _context.OpCameraImages.AsNoTracking().Where(e =>
                    e.SecurityCheckInOpDataId == opData.Id
                    || e.LabFacelessOpDataId == opData.Id
                    || e.LabRegularOpDataId == opData.Id
                    || e.LoadPointOpDataId == opData.Id
                    || e.SingleWindowOpDataId == opData.Id
                    || e.SecurityCheckInOpDataId == opData.Id
                    || e.SecurityCheckOutOpDataId == opData.Id
                    || e.ScaleOpDataId == opData.Id
                    || e.UnloadPointOpDataId == opData.Id
                    || e.NonStandartOpDataId == opData.Id
                );

                if (!imageList.Any()) continue;
                
                foreach (var image in imageList)
                {
                    var request = new OneCApiClient.PostImageFileDto.Request
                    {
                        Id = singleWindowOpData.DeliveryBillId,
                        CreateDate = image.DateTime.HasValue
                            ? image.DateTime.Value.ToString("yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture)
                            : string.Empty,
                        ImageName = image.ImagePath.Split('\\').Last(),
                    };


                    var device = _context.Devices.FirstOrDefault(x => x.Id == image.DeviceId);
                    if (device != null)
                    {
                        var deviceParam = device.DeviceParam.ParamJson;
                        request.IpAddressCamera = Regex.Match(deviceParam, @"\d+\.\d+\.\d+\.\d+").Value;
                    }

                    try
                    {
                        byte[] byteData = System.IO.File.ReadAllBytes(image.ImagePath);
                        request.Base64Content = Convert.ToBase64String(byteData);

                        result.Add(request);
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"SingleWindow. CameraImagesPost: Error while getting image file: {e}");
                    }
                }
            }

            return result;
        }

        private void RouteAddOpVisa(NodeDetails nodeDetailsDto)
        {
            // Validate node context
            if (nodeDetailsDto?.Context?.TicketContainerId == null
                || nodeDetailsDto.Context?.TicketId == null
                || nodeDetailsDto.Context?.OpDataId == null
                || !nodeDetailsDto.Config.Rfid.ContainsKey(NodeData.Config.Rfid.TableReader))
                return;

            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.TicketId.Value);
            if (ticket == null) return;
            
            var card = _userManager.GetValidatedUsersCardByTableReader(nodeDetailsDto);
            if (card == null) return;

            var windowOpData = _context.SingleWindowOpDatas.FirstOrDefault(x => x.Id == nodeDetailsDto.Context.OpDataId.Value);
            if (windowOpData == null) return;

            var visa = new OpVisa
            {
                DateTime = DateTime.Now,
                Message = "Правка маршруту",
                SingleWindowOpDataId = windowOpData.Id,
                EmployeeId = card.EmployeeId,
                OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.RouteAddOpVisa
            };
            _nodeRepository.Add<OpVisa, int>(visa);
            windowOpData.StateId = OpDataState.Processed;

            var isMixedFeedRoute = _routesInfrastructure.IsNodeAvailable((int) NodeIdValue.MixedFeedGuide, ticket.RouteTemplateId.Value);
            if (isMixedFeedRoute)
            {
                ticket.RouteType = RouteType.MixedFeedLoad;
                _ticketRepository.Update<Ticket, int>(ticket);
            }
            
            var isSecondaryTicket = _context.Tickets.AsNoTracking().Any(x => 
                    x.TicketContainerId == ticket.TicketContainerId 
                    && (x.StatusId == TicketStatus.ToBeProcessed
                        || x.StatusId == TicketStatus.Processing)
                    && x.OrderNo < ticket.OrderNo);
            
            if (ticket.RouteItemIndex == 0)
            {
                if (ticket.RouteTemplateId != null)
                {
                    if (isMixedFeedRoute && !isSecondaryTicket)
                    {
                        var freeMixedFeedSilo = _queueManager.GetFreeSiloDrive(windowOpData.ProductId.Value, ticket.Id);
                        if (freeMixedFeedSilo == null)
                        {
                            _connectManager.SendSms(SmsTemplate.NoMixedFeedProduct, nodeDetailsDto.Context.TicketId, _phonesRepository.GetPhone(Phone.MixedFeedManager));
                        }
                    }

                    var time = "---";

                    if (!isSecondaryTicket) 
                    {
                        _connectManager.SendSms(SmsTemplate.QueueRegistrationSms, nodeDetailsDto.Context.TicketId, null,
                        new Dictionary<string, object> {
                            { "EntranceTime", time }
                        });
                    }

                    foreach (var item in _phoneInformTicketAssignmentRepository.GetAll().Where(e => e.TicketId == ticket.Id))
                    {
                        _connectManager.SendSms(SmsTemplate.OnRegisterEmployeeInformation, nodeDetailsDto.Context.TicketId, item.PhoneDictionary.PhoneNumber);
                    }
                }

                if (ticket.StatusId != TicketStatus.ToBeProcessed)
                {
                    ticket.StatusId = TicketStatus.ToBeProcessed;
                    _ticketRepository.Update<Ticket, int>(ticket);
                }
            }
            else
            {
                if (!_connectManager.SendSms(SmsTemplate.RouteChangeSms, nodeDetailsDto.Context.TicketId))
                {
                    Logger.Error("SingleWindow. RouteAddOpVisa: Sms hasn`t been sent");
                }
            }
            
            _ticketRepository.Update<SingleWindowOpData, Guid>(windowOpData);

            if (_routesRepository.GetRoute(ticket.RouteTemplateId.Value).IsInQueue && !isMixedFeedRoute && !isSecondaryTicket)
            {
                _queueManager.OnRouteAssigned(ticket);
            }
            
            _nodeRepository.ClearNodeProcessingMessage(_nodeId);
            nodeDetailsDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
            UpdateNodeContext(nodeDetailsDto.Id, nodeDetailsDto.Context); 
        }

        private Card GetCardFromTableReader(NodeDetails nodeDetailsDto)
        {
            if (nodeDetailsDto.IsEmergency) return null;
            
            var rfidConfig = nodeDetailsDto.Config.Rfid[NodeData.Config.Rfid.TableReader];
            var rfidObidRwState = (RfidObidRwState) Program.GetDeviceState(rfidConfig.DeviceId);

            if (!_deviceRepository.IsDeviceStateValid(out var errMsg, rfidObidRwState, TimeSpan.FromSeconds(rfidConfig.Timeout)))
            {
                if (!string.IsNullOrEmpty(errMsg?.Text)) _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsg);
                return null;
            }

            var card = _context.Cards.AsNoTracking().FirstOrDefault(e =>
                e.Id.Equals(rfidObidRwState.InData.Rifd, StringComparison.CurrentCultureIgnoreCase));

            if (!_opRoutineManager.IsRfidCardValid(out var errMsgItem, card, CardType.TicketCard))
            {
                if (!string.IsNullOrEmpty(errMsg?.Text)) _opRoutineManager.UpdateProcessingMessage(nodeDetailsDto.Id, errMsgItem);
                return null;
            }

            return card;
        }
    }
}
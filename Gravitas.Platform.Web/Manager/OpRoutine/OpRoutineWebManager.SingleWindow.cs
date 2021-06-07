using AutoMapper;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.OneC;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpDataEvent.DAO;
using Gravitas.Model.DomainModel.PhoneInformTicketAssignment.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.Employee;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gravitas.Infrastructure.Platform.SignalRClient;
using Gravitas.Model.DomainModel.Card.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpData.TDO.Detail;
using Gravitas.Model.DomainModel.PredefinedRoute.DAO;
using Gravitas.Model.DomainModel.Queue.DAO;
using CardType = Gravitas.Model.DomainValue.CardType;
using TicketStatus = Gravitas.Model.DomainValue.TicketStatus;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool SingleWindow_Idle_SelectTicketContainer(int nodeId, int ticketContainerId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            var ticket = _ticketRepository.GetTicketInContainer(ticketContainerId, TicketStatus.Blank) ??
                         _ticketRepository.GetTicketInContainer(ticketContainerId, TicketStatus.New) ??
                         _ticketRepository.GetTicketInContainer(ticketContainerId, TicketStatus.ToBeProcessed) ??
                         _ticketRepository.GetTicketInContainer(ticketContainerId, TicketStatus.Processing) ??
                         _ticketRepository.GetTicketInContainer(ticketContainerId, TicketStatus.Completed) ??
                         _ticketRepository.GetTicketInContainer(ticketContainerId, TicketStatus.Closed);
            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(
                        ProcessingMsgType.Error,
                        $@"Маршрут не знадено. Маршрутний лист Id:{ticketContainerId}"));
                return false;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.GetTicket;
            nodeDto.Context.TicketContainerId = ticketContainerId;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
            return true;
        }

        public void SingleWindow_GetTicket_New(int nodeId, string supplyBarCode)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null) return;

            var ticket = _ticketRepository.NewTicket(nodeDto.Context.TicketContainerId.Value);

            var windowOpData = _opDataRepository.GetLastOpData<SingleWindowOpData>(ticket.Id, null) ?? new SingleWindowOpData
            {
                NodeId = nodeDto.Id,
                TicketId = ticket.Id,
                StateId = OpDataState.Init,
                CheckInDateTime = DateTime.Now,
                CheckOutDateTime = DateTime.Now,
                RegistrationDateTime = DateTime.Now,
                TicketContainerId = ticket.TicketContainerId
            };

            // check for bar code length
            if (supplyBarCode.Length == 18) windowOpData.BarCode = supplyBarCode;
            else windowOpData.SupplyCode = supplyBarCode;

            if (supplyBarCode == TechRoute.SupplyCode)
            {
                windowOpData.DeliveryBillId = supplyBarCode;
            }

            _opDataRepository.AddOrUpdate<SingleWindowOpData, Guid>(windowOpData);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.EditGetApiData;
            nodeDto.Context.TicketId = ticket.Id;
            nodeDto.Context.OpDataId = windowOpData.Id;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void SingleWindow_GetTicket_Change(int nodeId, string supplyCode)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            if (nodeDto.Context.TicketId == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error,
                        $@"Маршрут не знадено. Маршрутний лист Id:{nodeDto.Context.TicketContainerId.Value}"));
                return;
            }

            var opData = _opDataRepository.GetFirstOrDefault<SingleWindowOpData, Guid>(item => item.TicketId == nodeDto.Context.TicketId);

            var cause = $"Зміна коду поставки з {opData.SupplyCode} на {supplyCode}";

            try
            {
                var oneCApiClient = new OneCApiClient(GlobalConfigurationManager.OneCApiHost);
                var data = new OneCApiClient.ChangeSupplyCodeDto.Request
                {
                    Activity = 0,
                    Id = opData.DeliveryBillId,
                    NewSupplyCode = supplyCode,
                    WeightingStatistics = _opDataManager.GetEvents(nodeDto.Context.TicketId.Value, (int?)OpDataEventType.Weight)
                };
                Logger.Info(JsonConvert.SerializeObject(data));
                var response = oneCApiClient.ChangeSupplyCode(data);

                if (response != null && response.ErrorCode == 0 && string.IsNullOrEmpty(response.ErrorMsg))
                {
                    Logger.Info($"SingleWindow. ChangeSypplyCode: Change was successed: {JsonConvert.SerializeObject(response)}");

                    opData.DeliveryBillId = response.Id;
                    opData.DeliveryBillCode = response.Code;
                    opData.SupplyCode = supplyCode;
                    opData.SupplyTypeId = response.SupplyTypeId;
                    opData.OrderCode = response.OrderCode;
                    opData.DocumentTypeId = response.DocumentTypeId;
                    opData.OrganizationId = response.OrganizationId;
                    opData.KeeperOrganizationId = response.KeeperOrganizationId;
                    opData.StockId = response.StockId;
                    opData.ReceiverTypeId = response.ReceiverTypeId;
                    opData.ReceiverId = response.ReceiverId;
                    opData.ReceiverAnaliticsId = response.ReceiverAnaliticsId;
                    opData.ProductId = response.ProductId;
                    opData.HarvestId = response.HarvestId;
                    opData.BuyBudgetId = response.BuyBudgetId;
                    opData.SellBudgetId = response.SellBudgetsId;
                    opData.ProductContents = JsonConvert.SerializeObject(response.ProductContents);
                    opData.ReceiverTitle = response.ReceiverId.HasValue
                        ? _externalDataRepository.GetStockDetail(response.ReceiverId.Value)?.ShortName
                          ?? _externalDataRepository.GetSubdivisionDetail(response.ReceiverId.Value)?.ShortName
                          ?? _externalDataRepository.GetPartnerDetail(response.ReceiverId.Value)?.ShortName
                        : null;
                    opData.OrganizationTitle = response.OrganizationId.HasValue
                        ? _externalDataRepository.GetOrganisationDetail(response.OrganizationId.Value)?.ShortName
                        : null;
                    opData.ProductTitle = response.ProductId.HasValue
                        ? _externalDataRepository.GetProductDetail(response.ProductId.Value)?.ShortName 
                        : null;

                    _opDataRepository.Update<SingleWindowOpData, Guid>(opData);

                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                        ProcessingMsgType.Success, "WebAPI. Дані змінено успішно"));

                    _opDataManager.AddEvent(new OpDataEvent
                    {
                        Created = DateTime.Now,
                        NodeId = nodeId,
                        TypeOfTransaction = TypeOfTransaction.SupplyCodeChanged,
                        Cause = cause,
                        TicketId = nodeDto.Context.TicketId.Value
                    });

                    nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.SupplyChangeAddOpVisa;
                    UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                }
                else
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                        ProcessingMsgType.Error, $"Помилка WebAPI. {response?.ErrorMsg}"));

                    Logger.Error($"SingleWindow. ChangeSypplyCode: SupplyCode wasn't changed: {JsonConvert.SerializeObject(response)}");

                    nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
                    UpdateNodeContext(nodeDto.Id, nodeDto.Context);
                }
            }
            catch (Exception e)
            {
                Logger.Error($"SingleWindow. ChangeSypplyCode: Error while processing OneC api request: {e}");
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                    ProcessingMsgType.Error, "WebAPI. Помилка при зміні коду поставки."));
            }
        }

        public void SingleWindow_DivideTicket(int nodeId, int newWeightValue)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            var currentBrutto = _context.ScaleOpDatas.Where(x => x.TicketId == nodeDto.Context.TicketId.Value
                                                                                   && x.TypeId == ScaleOpDataType.Gross
                                                                                   && x.StateId == OpDataState.Processed)
                .OrderByDescending(x => x.Id)
                .First();

            if (currentBrutto.TruckWeightValue - 500 < newWeightValue)
            {
                return;
            }

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.DivideTicketAddOpVisa;
            nodeDto.Context.OpProcessData = newWeightValue;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool SingleWindow_GetTicket_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void SingleWindow_AddOperationVisa_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void SingleWindow_GetTicket_Detail(int nodeId, int ticketId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null) return;

            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == ticketId);
            if (ticket == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
            nodeDto.Context.TicketId = ticketId;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void SingleWindow_GetTicket_Delete(int nodeId, int ticketId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            nodeDto.Context.TicketId = ticketId;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.DeleteTicketAddOpVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void SingleWindow_ShowTicketMenu_Exit(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            nodeDto.Context.OpDataComponentId = null;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool SingleWindow_GetTicket_Close(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ContainerCloseAddOpVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool SingleWindow_ShowTicketMenu_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null) return false;

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.GetTicket;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            nodeDto.Context.OpDataComponentId = null;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool SingleWindow_ShowTicketMenu_Route(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.RouteEditData;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool SingleWindow_ShowTicketMenu_Edit(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);

            if (nodeDto?.Context?.TicketId == null)
                return false;

            var windowInOpData = _context.SingleWindowOpDatas.Where(e =>
                                     e.TicketId == nodeDto.Context.TicketId.Value
                                     && (e.StateId == OpDataState.Init || e.StateId == OpDataState.Processed))
                                 .OrderByDescending(e => e.CheckInDateTime)
                                 .FirstOrDefault();

            if (windowInOpData == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.EditTicketForm;
            nodeDto.Context.OpDataId = windowInOpData.Id;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool SingleWindow_ShowTicketMenu_Commit(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.CloseAddOpVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public byte[] SingleWindow_ShowTicketMenu_PrintDoc(int nodeId, string printoutTypeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketId == null) return null;

            var windowOutOpData = _context.SingleWindowOpDatas.FirstOrDefault(t => t.TicketId == nodeDto.Context.TicketId);
            if (string.IsNullOrWhiteSpace(windowOutOpData?.DeliveryBillId)) return null;

            OneCApiClient.GetBillFileDto.Response billFile = null;
            try
            {
                var oneCApiClient = new OneCApiClient(GlobalConfigurationManager.OneCApiHost);
                var request = new OneCApiClient.GetBillFileDto.Request
                {
                    DeliveryBillId = windowOutOpData.DeliveryBillId,
                    PrintoutTypeId = printoutTypeId
                };

                billFile = oneCApiClient.GetBillFile(request);
            }
            catch (Exception e)
            {
                Logger.Error($"SingleWindow_PrintDoc: Error while processing: {e}");
            }

            if (billFile != null && billFile.ErrorCode == 0 && string.IsNullOrEmpty(billFile.ErrorMsg)) return Convert.FromBase64String(billFile.Base64Content);

            _opRoutineManager.UpdateProcessingMessage(nodeDto.Id, new NodeProcessingMsgItem(
                ProcessingMsgType.Warning, $@"Помилка. {billFile?.ErrorMsg}"));

            return null;
        }

        public SingleWindowVms.SingleWindowOpDataDetailVm SingleWindow_EditTicketForm_GetData(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketId == null) return null;

            var daoSingleWindowOpData = _opDataManager.FetchRouteResults(nodeDto.Context.TicketId.Value);
            _opDataRepository.Update<SingleWindowOpData, Guid>(daoSingleWindowOpData);

            var dtoSingleWindowOpData = _opDataManager.GetSingleWindowOpDataDetail(daoSingleWindowOpData.Id);

            var vm = Mapper.Map<SingleWindowVms.SingleWindowOpDataDetailVm>(dtoSingleWindowOpData);
            SetEmployeePhones(vm);

            if (vm.CollectionPointId.HasValue)
            {
                vm.CollectionPointName = _externalDataRepository.GetAcceptancePointDetail(vm.CollectionPointId.Value)?.Name;
            }

            if (vm.ReturnCauseId.HasValue)
            {
                vm.ReturnCauseName = _externalDataRepository.GetReasonForRefundDetail(vm.ReturnCauseId.Value)?.Name;
            }

            if (vm.CarrierRouteId.HasValue)
            {
                vm.CarrierRouteName = _externalDataRepository.GetRouteDetail(vm.CarrierRouteId.Value)?.Name;
            }

            if (string.IsNullOrWhiteSpace(vm.InformationCarrier))
            {
                vm.InformationCarrier =
                    "Автомобільний перевізник підтверджує, що автомобіль з напівпричепом здійснював перевезення: - .08.2019р.; - .08.2019р.; - .08.2019р. відповідно.";
            }

            if (daoSingleWindowOpData.NetValue.HasValue && dtoSingleWindowOpData.PackingWeightValue > 0)
            {
                vm.NetValue = Math.Round((double)(daoSingleWindowOpData.NetValue.Value - dtoSingleWindowOpData.PackingWeightValue));
            }

            return vm;
        }

        public bool SingleWindow_EditTicketForm_Save(SingleWindowVms.SingleWindowOpDataDetailVm data)
        {
            var nodeDto = _nodeRepository.GetNodeDto(data.NodeId);
            if (nodeDto == null || !data.NodeId.HasValue || !nodeDto.Context.TicketId.HasValue) return false;

            if (!string.IsNullOrEmpty(data.CarrierCode))
            {
                if (!_context.Partners.Any(t => t.Code == data.CarrierCode))
                {
                    data.CustomPartnerName = data.CarrierCode;
                    data.CarrierCode = string.Empty;
                    data.CarrierId = null;
                }
                else
                {
                    data.CustomPartnerName = null;
                }
            }

            if (!ValidateSingleWindowFormData(data)) return false;

            
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDto.Context.TicketId.Value);
            if (ticket == null) return false;

            var isSecondaryTicket = _context.Tickets.Any(x =>
                    x.TicketContainerId == ticket.TicketContainerId
                    && (x.StatusId == TicketStatus.ToBeProcessed
                        || x.StatusId == TicketStatus.Processing)
                    && x.OrderNo < ticket.OrderNo);

            var queueData = _opDataRepository.GetSingleOrDefault<QueueRegister, int>(x => x.PhoneNumber == data.ContactPhoneNo);
            if (!isSecondaryTicket && queueData != null && queueData.TicketContainerId != ticket.TicketContainerId)
            {
                _opRoutineManager.UpdateProcessingMessage(data.NodeId.Value,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error,
                        @"Номер телефону вже зареєстровний в системі."));
                return false;
            }

            if (data.IsThirdPartyCarrier)
            {
                var blackListRegistry = _blackListRepository.GetBlackListDto();

                if (blackListRegistry.Partners.Select(t => t.Partner.Code).Contains(data.CarrierCode))
                {
                    _opRoutineManager.UpdateProcessingMessage(data.NodeId.Value,
                        new NodeProcessingMsgItem(ProcessingMsgType.Error,
                            @"Даний партнер перебуває у чорному списку та не може в'їхати"));
                    return false;
                }

                if (blackListRegistry.Transport.Select(t => t.TransportNo).Contains(data.HiredTransportNumber))
                {
                    _opRoutineManager.UpdateProcessingMessage(data.NodeId.Value,
                        new NodeProcessingMsgItem(ProcessingMsgType.Error,
                            @"Даний транспорт перебуває у чорному списку та не може в'їхати"));
                    return false;
                }

                if (blackListRegistry.Trailers.Select(t => t.TrailerNo).Contains(data.TrailerNumber))
                {
                    _opRoutineManager.UpdateProcessingMessage(data.NodeId.Value,
                        new NodeProcessingMsgItem(ProcessingMsgType.Error,
                            @"Даний причеп перебуває у чорному списку та не може в'їхати"));
                    return false;
                }

                if (data.HiredDriverCode != null)
                {
                    if (blackListRegistry.Drivers.Select(x => x.Name).Any(x => data.HiredDriverCode.Contains(x)))
                    {
                        _opRoutineManager.UpdateProcessingMessage(data.NodeId.Value,
                            new NodeProcessingMsgItem(ProcessingMsgType.Error,
                                @"Даний водій перебуває у чорному списку та не може в'їхати"));
                        return false;
                    }
                }

                if (data.CarrierId != null)
                {
                    var partner = _context.Partners.First(x => x.Id == data.CarrierId);
                    if (partner?.CarrierDriverId != null) data.DriverOneId = partner.CarrierDriverId.Value;
                }
            }

            data.EditDate = DateTime.Now;

            var driverCheckIn = _context.DriverCheckInOpDatas.FirstOrDefault(x => x.PhoneNumber == data.ContactPhoneNo);
            if (driverCheckIn != null)
            {
                _context.DriverCheckInOpDatas.Remove(driverCheckIn);
                _context.SaveChanges();
                SignalRInvoke.UpdateDriverCheckIn(null);
            }

            _opDataRepository.SetSingleWindowDetail(Mapper.Map<SingleWindowOpDataDetail>(data));
            SaveOnTicketRegisterInformEmployee(data.OnRegisterInformEmployees, data.TicketId.Value);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.EditAddOpVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private bool ValidateSingleWindowFormData(SingleWindowVms.SingleWindowOpDataDetailVm data)
        {
            if (!double.TryParse(data.ContactPhoneNo, out _) || data.ContactPhoneNo.Contains('+'))
            {
                _opRoutineManager.UpdateProcessingMessage(data.NodeId.Value,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error,
                        @"Не вірний формат номеру телефону."));
                return false;
            }

            if (string.IsNullOrEmpty(data.TransportId) && string.IsNullOrEmpty(data.HiredTransportNumber))
            {
                _opRoutineManager.UpdateProcessingMessage(data.NodeId.Value,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error,
                        @"Введіть номер автомобіля"));
                return false;
            }

            if (string.IsNullOrEmpty(data.DriverOneName) && string.IsNullOrEmpty(data.HiredDriverCode))
            {
                _opRoutineManager.UpdateProcessingMessage(data.NodeId.Value,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error,
                        @"Введіть ім'я водія"));
                return false;
            }

            return true;
        }

        public bool SingleWindow_EditTicketForm_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void SingleWindow_DeleteTicketAddOpVisa_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.GetTicket;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool SingleWindow_EditAddOpVisa_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.GetTicket;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool SingleWindow_RouteEditData_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.ShowTicketMenu;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool SingleWindow_RouteEditData_Save(SingleWindowVms.Route data)
        {
            Logger.Trace("RouteEditData: Start changing route.");
            var node = _nodeRepository.GetNodeDto(data.NodeId);
            if (node?.Context?.TicketId == null) return false;

            var windowOpData = _opDataRepository.GetLastOpData<SingleWindowOpData>(node.Context?.TicketId, null);
            var queueRecord = _context.QueueRegisters.Where(x => x.PhoneNumber == windowOpData.ContactPhoneNo).ToList();
            
            var ticket = _context.Tickets.FirstOrDefault(x => x.Id == node.Context.TicketId.Value);
            if (queueRecord.Any(x => x.TicketContainerId != ticket.TicketContainerId))
            {
                _opRoutineManager.UpdateProcessingMessage(data.NodeId,
                    new NodeProcessingMsgItem(ProcessingMsgType.Error,
                        $"Номер телефону {windowOpData.ContactPhoneNo} вже зареєстрований в черзі."));
                return false;
            }

            var newRouteJson = _routesInfrastructure.NormalizeRoute(data.RouteJson);

            var route = _routesRepository.GetFirstOrDefault<RouteTemplate, int>(item =>
                _routesInfrastructure.NormalizeRoute(item.RouteConfig) == newRouteJson);

            if (route is null)
            {
                Logger.Trace("RouteEditData: Route modified.");
                var routeTemplate = new RouteTemplate
                {
                    RouteConfig = newRouteJson,
                    Name = $"{_routesRepository.GetRoute(data.SelectedId)?.Name} - Редагований - {DateTime.Now}",
                    OwnerId = ticket.RouteTemplateId.ToString()
                };

                _routesRepository.Add<RouteTemplate, int>(routeTemplate);

                Logger.Trace($"RouteEditData: New routeTemplateId = {routeTemplate.Id}.");
                ticket.RouteTemplateId = routeTemplate.Id;
            }
            else
            {
                ticket.RouteTemplateId = route.Id;
            }

            _ticketRepository.Update<Model.DomainModel.Ticket.DAO.Ticket, int>(ticket);

            Logger.Trace($"RouteEditData: Route = {ticket.RouteTemplateId.Value} assigned to ticket = {ticket.Id}.");
            node.Context.OpDataId = _opDataRepository.GetLastOpData<SingleWindowOpData>(ticket.Id, null)?.Id;
            node.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.RouteAddOpVisa;
            return UpdateNodeContext(node.Id, node.Context);
        }

        public SingleWindowVms.Route SingleWindow_RouteEditDataVm(int nodeId)
        {
            var ticketId = _nodeRepository.GetNodeDto(nodeId).Context.TicketId;

            var supplyCode = _context.SingleWindowOpDatas.Where(x => x.TicketId == ticketId)
                                                         .Select(c => c.SupplyCode)
                                                         .First();

            var routeItems = _routesInfrastructure.GetRouteTemplates(RouteType.SingleWindow)
                                                .Where(x => supplyCode == TechRoute.SupplyCode ? x.IsTechnological : x.IsMain && !x.IsTechnological)
                                                .Select(item => new SelectListItem
                                                {
                                                    Value = item.Id.ToString(),
                                                    Text = item.Name
                                                })
                                                .OrderBy(x => x.Text)
                                                .ToList();

            var selectedId = -1;

            RouteTemplate routeTemplate = null;
            var ticket = _context.Tickets.First(x => x.Id == ticketId.Value);
            if (ticket.RouteTemplateId != null)
            {
                selectedId = (int) ticket.RouteTemplateId;
                routeTemplate = _context.RouteTemplates.First(x => x.Id == ticket.RouteTemplateId.Value);
            }

            return new SingleWindowVms.Route
            {
                NodeId = nodeId,
                Items = string.IsNullOrEmpty(routeTemplate?.OwnerId) ? routeItems : new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = routeTemplate.Id.ToString(),
                        Text = routeTemplate.Name
                    }
                },
                SelectedId = selectedId,
                TicketId = ticketId
            };
        }

        public SingleWindowVms.ProtocolPrintoutVm SingleWindow_ProtocolPrintout_GetVm(int nodeId, int? ticketIdExt = null)
        {
            int ticketContainerId;
            int? ticketId;
            var vm = new SingleWindowVms.ProtocolPrintoutVm();

            if (ticketIdExt is null)
            {
                var nodeDto = _nodeRepository.GetNodeDto(nodeId);
                if (nodeDto?.Context?.TicketContainerId == null || nodeDto?.Context?.TicketId == null)
                {
                    return vm;
                }

                ticketContainerId = nodeDto.Context.TicketContainerId.Value;
                ticketId = nodeDto.Context.TicketId.Value;
            }
            else
            {
                ticketContainerId = _context.Tickets.First(x => x.Id == ticketIdExt.Value).TicketContainerId;
                ticketId = ticketIdExt;
            }

            vm.CardNumber = _cardRepository.GetFirstOrDefault<Card, string>(item =>
                item.TicketContainerId == ticketContainerId && item.TypeId == CardType.TicketCard)?.No.ToString().Remove(0, 2);

            vm.TicketId = ticketId;

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId)
                                     ?? _opDataRepository.GetLastOpData<SingleWindowOpData>(ticketId, null);

            if (singleWindowOpData == null) return vm;

            vm.IsThirdPartyCarrier = singleWindowOpData.IsThirdPartyCarrier ? "Сторонній" : "Власний";

            vm.DeliveryBillCode = singleWindowOpData.DeliveryBillCode;
            vm.ReceiverName = singleWindowOpData.ReceiverId.HasValue
                ? _externalDataRepository.GetStockDetail(singleWindowOpData.ReceiverId.Value)?.ShortName
                  ?? _externalDataRepository.GetSubdivisionDetail(singleWindowOpData.ReceiverId.Value)?.ShortName
                  ?? _externalDataRepository.GetPartnerDetail(singleWindowOpData.ReceiverId.Value)?.ShortName
                  ?? "- Хибний ключ -"
                : string.Empty;

            vm.PartnerName = singleWindowOpData.CarrierId.HasValue
                ? _externalDataRepository.GetPartnerDetail(singleWindowOpData.CarrierId.Value)?.ShortName
                : singleWindowOpData.CustomPartnerName ?? string.Empty;

            vm.Comments = singleWindowOpData.Comments;

            if (singleWindowOpData.IsThirdPartyCarrier)
            {
                vm.TransportNo = singleWindowOpData.HiredTransportNumber;
                vm.TrailerNo = singleWindowOpData.HiredTrailerNumber;
            }
            else
            {
                vm.TransportNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId.Value)?.RegistrationNo ?? string.Empty;
                vm.TrailerNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId.Value)?.RegistrationNo ?? string.Empty;
            }

            vm.Nomenclature = _externalDataRepository.GetProductDetail(_opDataRepository
                    .GetLastOpData<SingleWindowOpData>(ticketId, null).ProductId.Value)?.ShortName ?? string.Empty;

            var tickets = _context.Tickets.Where(x => x.TicketContainerId == ticketContainerId
                                                      && x.StatusId != TicketStatus.Canceled).ToList();

            vm.OpDataItems = new OpDataItemsVm();
            foreach (var t in tickets)
            {
                var items = _opDataWebManager.GetOpDataItems(t.Id);
                vm.OpDataItems.Items.AddRange(items.Items);
            }

            vm.EntranceTime = singleWindowOpData.CheckInDateTime;
            vm.ExitTime = singleWindowOpData.CheckOutDateTime;

            return vm;
        }

        public SingleWindowVms.TechnologicalRoutePrintoutVm SingleWindow_TechnologicalRoutePrintout_GetVm(int nodeId)
        {
            var vm = new SingleWindowVms.TechnologicalRoutePrintoutVm
            {
                Date = DateTime.Now
            };

            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null || nodeDto.Context?.TicketId == null)
            {
                return vm;
            }

            var ticketId = nodeDto.Context?.TicketId.Value;

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId)
                                                 ?? _opDataRepository.GetLastOpData<SingleWindowOpData>(ticketId, null);

            if (singleWindowOpData == null) return vm;

            if (singleWindowOpData.IsThirdPartyCarrier)
            {
                vm.TruckNo = singleWindowOpData.HiredTransportNumber;
                vm.TrailerNo = singleWindowOpData.HiredTrailerNumber;
            }
            else
            {
                vm.TruckNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId.Value)?.RegistrationNo ?? string.Empty;
                vm.TrailerNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId.Value)?.RegistrationNo ?? string.Empty;
            }

            vm.SenderName = singleWindowOpData.OrganizationTitle;
            vm.ReceiverName = singleWindowOpData.ReceiverTitle;
            vm.Driver = singleWindowOpData.HiredDriverCode;
            vm.GrossDate = singleWindowOpData.LastGrossTime;
            vm.GrossValue = singleWindowOpData.GrossValue;
            vm.NetDate = singleWindowOpData.DocNetDateTime;
            vm.NetValue = singleWindowOpData.NetValue;
            vm.TareDate = singleWindowOpData.LastTareTime;
            vm.TareValue = singleWindowOpData.TareValue;
            vm.ProductName = singleWindowOpData.ProductTitle;
            return vm;
        }

        public SingleWindowVms.RoutePrintoutVm SingleWindow_RoutePrintout_GetVm(int nodeId, int ticketId)
        {
            var vm = new SingleWindowVms.RoutePrintoutVm
            {
                Date = DateTime.Now
            };

            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null || nodeDto.Context?.TicketId == null)
            {
                return vm;
            }

            vm.CardNumber = _cardRepository.GetContainerCardNo(nodeDto.Context.TicketContainerId.Value);
            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId)
                                     ?? _opDataRepository.GetLastOpData<SingleWindowOpData>(ticketId, null);

            if (singleWindowOpData == null) return vm;

            if (singleWindowOpData.IsThirdPartyCarrier)
            {
                vm.TransportNo = singleWindowOpData.HiredTransportNumber;
                vm.TrailerNo = singleWindowOpData.HiredTrailerNumber;
            }
            else
            {
                vm.TransportNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId.Value)?.RegistrationNo ?? string.Empty;
                vm.TrailerNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId.Value)?.RegistrationNo ?? string.Empty;
            }

            vm.Route = _routesInfrastructure.GetRouteForPrintout(nodeDto.Context.TicketId.Value);

            return vm;
        }

        public bool SingleWindow_RouteAddOpVisa_Back(int nodeId)
        {
            var node = _nodeRepository.GetNodeDto(nodeId);
            if (node?.Context?.TicketId == null) return false;

            var ticket = _context.Tickets.First(x => x.Id == node.Context.TicketId.Value);

            if (ticket.RouteTemplateId != null)
            {
                var ownerId = _routesRepository.GetRoute(ticket.RouteTemplateId.Value)?.OwnerId;

                int.TryParse(ownerId, out var routeId);

                var previousRoute =
                    _routesRepository.GetRoute(routeId);
                if (previousRoute != null)
                {
                    ticket.RouteTemplateId = previousRoute.Id;
                    _ticketRepository.Update<Model.DomainModel.Ticket.DAO.Ticket, int>(ticket);
                }
            }

            node.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SingleWindow.State.RouteEditData;
            return UpdateNodeContext(node.Id, node.Context);
        }

        private void SetEmployeePhones(SingleWindowVms.SingleWindowOpDataDetailVm vm)
        {
            var oldAssignment = _phoneInformTicketAssignmentRepository.GetAll().Where(e => e.TicketId == vm.TicketId);

            vm.OnRegisterInformEmployees = new List<InformEmployeeVm>();
            foreach (var phone in _phonesRepository.GetAll())
            {
                vm.OnRegisterInformEmployees.Add(new InformEmployeeVm()
                {
                    InformOnRegister = oldAssignment.Any(e => e.PhoneDictionaryId == phone.Id),
                    EmployeePhone = phone
                });
            }
        }

        private void SaveOnTicketRegisterInformEmployee(List<InformEmployeeVm> informInfo, int ticketId)
        {
            if (!IsInformPhonesChangedOrEmpty(informInfo, ticketId)) return;
            DeleteOldInformPhones(ticketId);
            AddNewInformPhones(informInfo, ticketId);
        }

        private bool IsInformPhonesChangedOrEmpty(List<InformEmployeeVm> informInfo, int ticketId)
        {
            var oldPhones = _phoneInformTicketAssignmentRepository.GetAll().Where(e => e.TicketId == ticketId).ToList();

            if (oldPhones.Count == 0)
            {
                return true;
            }
            foreach (var info in informInfo)
            {
                if (info.InformOnRegister && oldPhones.All(e => e.PhoneDictionaryId != info.EmployeePhone.Id))
                {
                    return true;
                }
            }

            return false;
        }

        private void DeleteOldInformPhones(int ticketId)
        {
            var oldPhones = _phoneInformTicketAssignmentRepository.GetAll().Where(e => e.TicketId == ticketId).ToList();
            foreach (var phone in oldPhones)
            {
                _phoneInformTicketAssignmentRepository.Delete(phone.Id);
            }
        }

        private void AddNewInformPhones(List<InformEmployeeVm> informInfo, int ticketId)
        {
            var phones = _phonesRepository.GetAll().ToList();
            for (var i = 0; i < informInfo.Count; ++i)
            {
                if (informInfo[i].InformOnRegister)
                {
                    _phoneInformTicketAssignmentRepository.Add(new PhoneInformTicketAssignment()
                    {
                        PhoneDictionaryId = phones[i].Id,
                        TicketId = ticketId
                    });
                }
            }
        }
    }
}
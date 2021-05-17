using System;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using Gravitas.Platform.Web.ViewModel;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool LoadPointGuide_Idle_SelectTicketContainer(long nodeId, long ticketContainerId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            var ticket = GetActiveTicket(ticketContainerId, nodeDto.Id);
            if (ticket == null) return false;

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.BindLoadPoint;
            nodeDto.Context.TicketContainerId = ticketContainerId;
            nodeDto.Context.TicketId = ticket.Id;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public LoadPointGuideVms.BindDestPointVm LoadPointGuide_BindLoadPoint_GetVm(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null)
            {
                SendWrongContextMessage(nodeId);
                return null;
            }

            var vm = new LoadPointGuideVms.BindDestPointVm
            {
                NodeId = nodeId, 
                Card = _cardRepository.GetContainerCardNo(nodeDto.Context.TicketContainerId.Value)
            };

            var ticketId = (_ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value, Dom.Ticket.Status.Processing)
                ?? _ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value, Dom.Ticket.Status.ToBeProcessed))?.Id;

            if (ticketId == null) return vm;

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId);
            if (singleWindowOpData != null)
            {
                vm.WeightValue = singleWindowOpData.LoadTarget;
                vm.ProductName = _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId)?.ShortName ?? string.Empty;

                vm.ReceiverName = !string.IsNullOrWhiteSpace(singleWindowOpData.ReceiverId)
                    ? _externalDataRepository.GetStockDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? _externalDataRepository.GetSubdivisionDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? _externalDataRepository.GetPartnerDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? singleWindowOpData.CustomPartnerName
                      ?? "- Хибний ключ -"
                    : string.Empty;

                vm.IsThirdPartyCarrier = singleWindowOpData.IsThirdPartyCarrier;
                vm.PackingWeightValue = singleWindowOpData.PackingWeightValue;
                vm.LoadTargetDeviationPlus = singleWindowOpData.LoadTargetDeviationPlus;
                vm.LoadTargetDeviationMinus = singleWindowOpData.LoadTargetDeviationMinus;
                if (singleWindowOpData.IsThirdPartyCarrier)
                {
                    vm.TransportNo = singleWindowOpData.HiredTransportNumber;
                    vm.TrailerNo = singleWindowOpData.HiredTrailerNumber;
                }
                else
                {
                    vm.TransportNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId)?.RegistrationNo ?? string.Empty;
                    vm.TrailerNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId)?.RegistrationNo ?? string.Empty;
                }
            }

            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(ticketId.Value);
            if (partLoadValue.HasValue)
            {
                vm.WeightValue = partLoadValue.Value;
            }

            var loadGuideData = _opDataRepository.GetLastOpData<LoadGuideOpData>(ticketId, null);
            var loadPointData = _opDataRepository.GetLastOpData<LoadPointOpData>(ticketId, null);
            if (loadPointData == null || loadGuideData.CheckInDateTime > loadPointData.CheckInDateTime)
                if (loadGuideData != null)
                {
                    vm.DestNodeId = loadGuideData.LoadPointNodeId;
                    vm.DestNodeName = loadGuideData.Node?.Name;
                }

            return vm;
        }

        public bool LoadPointGuide_BindLoadPoint_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketContainerId == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeId);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.OpProcessData = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LoadPointGuide_BindLoadPoint_Next(LoadPointGuideVms.BindDestPointVm vm)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto.Context.TicketId == null)
            {
                SendWrongContextMessage(vm.NodeId);
                return false;
            }

            Guid opDataId;
            if (nodeDto.Context.OpProcessData.HasValue && nodeDto.Context.OpProcessData == (long)NodeIdValue.UnloadPointGuideEl23)
            {
                var unloadGuideOpData =
                    _opDataRepository.GetLastProcessed<UnloadGuideOpData>(x => x.TicketId == nodeDto.Context.TicketId);
                var unloadPointOpData =
                    _opDataRepository.GetLastProcessed<UnloadPointOpData>(x => x.TicketId == nodeDto.Context.TicketId);

                if (unloadGuideOpData == null || unloadGuideOpData.CheckOutDateTime < unloadPointOpData?.CheckOutDateTime)
                {
                    unloadGuideOpData = new UnloadGuideOpData
                    {
                        StateId = Dom.OpDataState.Init,
                        NodeId = (long?) NodeIdValue.UnloadPointGuideEl23,
                        TicketId = nodeDto.Context.TicketId,
                        TicketContainerId = nodeDto.Context.TicketContainerId,
                        CheckInDateTime = DateTime.Now,
                        CheckOutDateTime = DateTime.Now
                    };
                }

                unloadGuideOpData.UnloadPointNodeId = vm.DestNodeId;
                _opDataRepository.AddOrUpdate<UnloadGuideOpData, Guid>(unloadGuideOpData);
                opDataId = unloadGuideOpData.Id;
            }
            else
            {
                var loadGuideOpData =
                    _opDataRepository.GetLastProcessed<LoadGuideOpData>(x => x.TicketId == nodeDto.Context.TicketId);
                var loadPointOpData =
                    _opDataRepository.GetLastProcessed<LoadPointOpData>(x => x.TicketId == nodeDto.Context.TicketId);

                if (loadGuideOpData == null || loadGuideOpData.CheckOutDateTime < loadPointOpData?.CheckOutDateTime)
                {
                    loadGuideOpData = new LoadGuideOpData
                    {
                        StateId = Dom.OpDataState.Init,
                        NodeId = nodeDto.Id,
                        TicketId = nodeDto.Context.TicketId,
                        CheckInDateTime = DateTime.Now,
                        CheckOutDateTime = DateTime.Now,
                        TicketContainerId = nodeDto.Context.TicketContainerId
                    };
                }

                loadGuideOpData.LoadPointNodeId = vm.DestNodeId;
                _opDataRepository.AddOrUpdate<LoadGuideOpData, Guid>(loadGuideOpData);
                opDataId = loadGuideOpData.Id;
            }

            nodeDto.Context.OpDataId = opDataId;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.AddOpVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void AddOpVisa_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            var l = _context.LoadGuideOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            var u = _context.UnloadGuideOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (l != null) _context.LoadGuideOpDatas.Remove(l);
            if (u != null) _context.UnloadGuideOpDatas.Remove(u);
            _context.SaveChanges();

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.BindLoadPoint;
            nodeDto.Context.OpDataId = null;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public bool LoadPointGuide_Idle_SelectRejectedForUnload(long nodeId, long ticketContainerId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            var ticket = GetActiveTicket(ticketContainerId, nodeDto.Id);
            if (ticket == null) return false;

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.BindLoadPoint;
            nodeDto.Context.TicketContainerId = ticketContainerId;
            nodeDto.Context.TicketId = ticket.Id;
            nodeDto.Context.OpProcessData = (long) NodeIdValue.UnloadPointGuideEl23;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LoadPointGuide_Idle_SelectRejectedForLoad(long nodeId, long ticketContainerId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            var ticket = GetActiveTicket(ticketContainerId, nodeDto.Id);
            if (ticket == null) return false;
   
            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.BindLoadPoint;
            nodeDto.Context.TicketContainerId = ticketContainerId;
            nodeDto.Context.TicketId = ticket.Id;
            nodeDto.Context.OpProcessData = (long) NodeIdValue.LoadPointGuideEl23;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        private Ticket GetActiveTicket(long ticketContainerId, long nodeId)
        {
            var ticket = _ticketRepository.GetTicketInContainer(ticketContainerId, Dom.Ticket.Status.Processing)
                ?? _ticketRepository.GetTicketInContainer(ticketContainerId, Dom.Ticket.Status.ToBeProcessed);
            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(
                        Dom.Node.ProcessingMsg.Type.Error,
                        $@"Маршрут не знадено. Маршрутний лист Id:{ticketContainerId}"));
                return null;
            }

            return ticket;
        }
    }
}
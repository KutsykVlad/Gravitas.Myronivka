using System;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using Gravitas.Platform.Web.ViewModel.OpRoutine.LoadPointGuide2;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool LoadPointGuide2_Idle_SelectTicketContainer(long nodeId, long ticketContainerId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            var ticket = _ticketRepository.GetTicketInContainer(ticketContainerId, Dom.Ticket.Status.Processing)
                         ?? _ticketRepository.GetTicketInContainer(ticketContainerId, Dom.Ticket.Status.ToBeProcessed);
            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeId,
                    new NodeProcessingMsgItem(
                        Dom.Node.ProcessingMsg.Type.Error,
                        $@"Маршрут не знадено. Маршрутний лист Id:{ticketContainerId}"));
                return false;
            }
            
            var loadGuideOpData =
                _opDataRepository.GetFirstOrDefault<LoadGuideOpData, Guid>(item => item.TicketId == ticket.Id);

            if (loadGuideOpData is null)
            {
                loadGuideOpData = new LoadGuideOpData
                {
                    StateId = Dom.OpDataState.Init,
                    NodeId = nodeId,
                    TicketId = ticket.Id,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now
                };
                _ticketRepository.Add<LoadGuideOpData, Guid>(loadGuideOpData);
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide2.State.BindLoadPoint;
            nodeDto.Context.TicketContainerId = ticketContainerId;
            nodeDto.Context.TicketId = ticket.Id;
            nodeDto.Context.OpDataId = loadGuideOpData.Id;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public LoadPointGuide2Vms.BindDestPointVm LoadPointGuide2_BindLoadPoint_GetVm(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null)
            {
                SendWrongContextMessage(nodeId);
                return null;
            }

            var vm = new LoadPointGuide2Vms.BindDestPointVm
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

        public bool LoadPointGuide2_BindLoadPoint_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketContainerId == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeId);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide2.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.OpProcessData = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LoadPointGuide2_BindLoadPoint_Next(LoadPointGuide2Vms.BindDestPointVm vm)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto.Context.TicketId == null)
            {
                SendWrongContextMessage(vm.NodeId);
                return false;
            }

            if (vm.DestNodeId == 0) return false;
            
            var loadGuideOpData = _context.LoadGuideOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);

            if (loadGuideOpData.LoadPointNodeId == 0)
            {
                var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDto.Context.TicketId.Value);
                if (ticket == null)
                {
                    return false;
                }

                if (!_routesInfrastructure.GetNextNodes(ticket.Id).Contains(nodeDto.Id))
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                        new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, "Автомобіль не закінчив обробку на попередніх вузлах"));
                
                    return false;
                }

                _routesInfrastructure.MoveForward(ticket.Id, vm.NodeId);
            }
            
            loadGuideOpData.LoadPointNodeId = (long) NodeIdValue.LoadPoint72;
            _opDataRepository.AddOrUpdate<LoadGuideOpData, Guid>(loadGuideOpData);

            nodeDto.Context.OpProcessData = vm.DestNodeId;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide2.State.AddOpVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        // public void UnloadPointGuide2_AddOpVisa_Back(long nodeId)
        // {
        //     var nodeDto = _nodeRepository.GetNodeDto(nodeId);
        //     if (nodeDto?.Context?.OpDataId == null)
        //     {
        //         SendWrongContextMessage(nodeId);
        //         return;
        //     }
        //
        //     nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointGuide.State.BindLoadPoint;
        //     nodeDto.Context.OpDataId = null;
        //     UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        // }
    }
}
using System;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.Dto;
using Gravitas.Platform.Web.ViewModel;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool MixedFeedLoad_ConfirmOperation_Next(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.AddOperationVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool MixedFeedLoad_AddOperationVisa_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool MixedFeedLoad_IdleWorkstation_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool MixedFeedLoad_Workstation_Process(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public MixedFeedLoadVms.IdleVm MixedFeedLoad_IdleVm(long nodeId)
        {
            var vm = new MixedFeedLoadVms.IdleVm {NodeId = nodeId};

            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketContainerId == null || nodeDto.Context.TicketId == null) return vm;

            vm.BindedTruck = new MixedFeedLoadTicketContainerItemVm();

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId)
                                     ?? _opDataRepository.GetLastOpData<SingleWindowOpData>(nodeDto.Context.TicketId, null);
            if (singleWindowOpData != null)
            {
                vm.BindedTruck.ProductName = _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId)?.ShortName ?? string.Empty;
                vm.BindedTruck.ReceiverDepotName = singleWindowOpData.ReceiverDepotId != null
                    ? _externalDataManager.GetStockItemVm(singleWindowOpData.ReceiverDepotId)?.ShortName ?? string.Empty
                    : string.Empty;
                vm.BindedTruck.LoadTarget = singleWindowOpData.LoadTarget;
                vm.BindedTruck.LoadTargetDeviationMinus = singleWindowOpData.LoadTargetDeviationMinus;
                vm.BindedTruck.LoadTargetDeviationPlus = singleWindowOpData.LoadTargetDeviationPlus;
                vm.BindedTruck.ReceiverName = !string.IsNullOrWhiteSpace(singleWindowOpData.ReceiverId)
                    ? _externalDataRepository.GetStockDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? _externalDataRepository.GetSubdivisionDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? _externalDataRepository.GetPartnerDetail(singleWindowOpData.ReceiverId)?.ShortName
                      ?? "- Хибний ключ -"
                    : string.Empty;

                if (singleWindowOpData.IsThirdPartyCarrier)
                {
                    vm.BindedTruck.TransportNo = singleWindowOpData.HiredTransportNumber;
                }
                else
                {
                    vm.BindedTruck.TransportNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId)?.RegistrationNo ?? string.Empty;
                }

                var partLoadValue = _scaleManager.GetPartLoadUnloadValue(nodeDto.Context.TicketId.Value);
                if (partLoadValue.HasValue)
                {
                    vm.PartLoadUnload = true;
                    vm.BindedTruck.LoadTarget = partLoadValue.Value;
                }
            }

            vm.IsActive = nodeDto.IsActive;

            return vm;
        }

        public void MixedFeedLoad_Workstation_SetNodeActive(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return;

            if (!nodeDto.IsActive && nodeDto.Context.TicketId == null) _nodeManager.ChangeNodeState(nodeId, true);
        }

        public void MixedFeedLoad_Cleanup_Start(MixedFeedLoadVms.CleanupVm vm)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(vm.NodeId);
                return;
            }

            if (nodeDto.Context.TicketId.HasValue || nodeDto.Context.OpDataId.HasValue || nodeDto.Context.TicketContainerId.HasValue)
            {
                _opRoutineManager.UpdateProcessingMessage(vm.NodeId,
                    new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Error, @"На вузлі знаходяться автомобілі. Очистка неможлива."));
            }

            nodeDto.Context.OpProcessData = vm.CleanupTime;
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.AddCleanupVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void MixedFeedLoad_Cleanup_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.Workstation;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void MixedFeedLoad_Workstation_Cleanup(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.Cleanup;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void MixedFeedLoad_ConfirmOperation_Cancel(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            var opData = _context.MixedFeedLoadOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (opData == null) return;

            opData.StateId = Dom.OpDataState.Canceled;
            _context.SaveChanges();
            
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.AddOperationVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void MixedFeedLoad_ConfirmOperation_Reject(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }
            
            var opData = _context.MixedFeedLoadOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (opData == null) return;

            opData.StateId = Dom.OpDataState.Rejected;
            _context.SaveChanges();
            
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.AddOperationVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void MixedFeedLoad_AddChangeStateVisa_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void MixedFeedLoad_Idle_ChangeState(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.MixedFeedLoad.State.AddChangeStateVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
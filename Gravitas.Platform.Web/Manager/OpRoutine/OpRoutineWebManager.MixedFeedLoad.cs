using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool MixedFeedLoad_ConfirmOperation_Next(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddOperationVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool MixedFeedLoad_AddOperationVisa_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool MixedFeedLoad_IdleWorkstation_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Workstation;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool MixedFeedLoad_Workstation_Process(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public MixedFeedLoadVms.IdleVm MixedFeedLoad_IdleVm(int nodeId)
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

        public void MixedFeedLoad_Workstation_SetNodeActive(int nodeId)
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
                    new NodeProcessingMsgItem(NodeData.ProcessingMsg.Type.Error, @"На вузлі знаходяться автомобілі. Очистка неможлива."));
            }

            nodeDto.Context.OpProcessData = vm.CleanupTime;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddCleanupVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void MixedFeedLoad_Cleanup_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Workstation;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void MixedFeedLoad_Workstation_Cleanup(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Cleanup;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void MixedFeedLoad_ConfirmOperation_Cancel(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            var opData = _context.MixedFeedLoadOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (opData == null) return;

            opData.StateId = OpDataState.Canceled;
            _context.SaveChanges();
            
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddOperationVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void MixedFeedLoad_ConfirmOperation_Reject(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }
            
            var opData = _context.MixedFeedLoadOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (opData == null) return;

            opData.StateId = OpDataState.Rejected;
            _context.SaveChanges();
            
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddOperationVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void MixedFeedLoad_AddChangeStateVisa_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void MixedFeedLoad_Idle_ChangeState(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedLoad.State.AddChangeStateVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
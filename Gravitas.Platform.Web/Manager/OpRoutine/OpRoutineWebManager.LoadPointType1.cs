using System;
using System.Collections.Generic;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.OpData.DAO.Json;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using Gravitas.Platform.Web.ViewModel;
using Newtonsoft.Json;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool LoadPointType1_ConfirmOperation_Next(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.AddOperationVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LoadPointType1_AddOperationVisa_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LoadPointType1_IdleWorkstation_Back(long nodeId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Workstation;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool LoadPointType1_Workstation_Process(long nodeId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            nodeDto.Context.OpRoutineStateId = nodeDto.Group == NodeGroup.Load ? Dom.OpRoutine.LoadPointType1.State.Idle : Dom.OpRoutine.UnloadPointType1.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public LoadPointType1Vms.IdleVm LoadPointType1_IdleVm(long nodeId)
        {
            var vm = new LoadPointType1Vms.IdleVm {NodeId = nodeId};

           var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketContainerId == null || nodeDto.Context.TicketId == null) return vm;

            vm.BindedTruck = new LoadPointTicketContainerItemVm();

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
                
                var daoProductContentList =
                    JsonConvert.DeserializeObject<List<ProductContent>>(singleWindowOpData.ProductContents);

                var productContents = new SingleWindowVms.ProductContentListVm
                {
                    Items = daoProductContentList?.Select(e => new SingleWindowVms.ProductContentItemVm
                    {
                        No = e.No,
                        OrderNumber = e.OrderNumber,
                        ProductId = e.ProductId,
                        ProductName = _externalDataRepository.GetProductDetail(e.ProductId)?.ShortName ?? "- Хибний ключ -",
                        UnitId = e.UnitId,
                        UnitName = _externalDataRepository.GetMeasureUnitDetail(e.UnitId)?.ShortName ?? "- Хибний ключ -",
                        Quantity = e.Quantity
                    }).ToList()
                };

                vm.ProductContentList = productContents;

                vm.IsActive = nodeDto.IsActive;
            }

            return vm;
        }

        public void LoadPointType1_Workstation_SetNodeActive(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return;

            if (!nodeDto.IsActive) _nodeManager.ChangeNodeState(nodeId, true);
        }

        public void LoadPointType1_ConfirmOperation_Cancel(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            var opData = _context.LoadPointOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (opData == null) return;

            opData.StateId = Dom.OpDataState.Canceled;
            _context.SaveChanges();
            
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.AddOperationVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void LoadPointType1_ConfirmOperation_Reject(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }
            
            var opData = _context.LoadPointOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (opData == null) return;
            opData.StateId = Dom.OpDataState.Rejected;
            _context.SaveChanges();
            
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.AddOperationVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void LoadPointType1_AddChangeStateVisa_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void LoadPointType1_Idle_ChangeState(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.AddChangeStateVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void LoadPointType1_Idle_GetTareValue(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.LoadPointType1.State.GetTareValue;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
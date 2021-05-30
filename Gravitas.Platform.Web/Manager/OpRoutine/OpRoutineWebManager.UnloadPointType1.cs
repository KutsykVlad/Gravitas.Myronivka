using System.Linq;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool UnloadPointType1_ConfirmOperation_Next(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.AddOperationVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool UnloadPointType1_AddOperationVisa_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool UnloadPointType1_IdleWorkstation_Back(int nodeId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.Workstation;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool UnloadPointType1_Workstation_Process(int nodeId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            nodeDto.Context.OpRoutineStateId = nodeDto.Group == NodeGroup.Load ? Model.DomainValue.OpRoutine.LoadPointType1.State.Idle : Model.DomainValue.OpRoutine.UnloadPointType1.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public UnloadPointType1Vms.IdleVm UnloadPointType1_IdleVm(int nodeId)
        {
            var vm = new UnloadPointType1Vms.IdleVm();
            vm.NodeId = nodeId;

            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketContainerId == null) return vm;

            vm.BindedTruck = new UnloadPointTicketContainerItemVm();

            var card = _context.Cards.FirstOrDefault(e =>
                    e.TicketContainerId == nodeDto.Context.TicketContainerId.Value &&
                    e.TypeId == CardType.TicketCard);
            if (card != null) vm.BindedTruck.CardNumber = card.No.ToString();

            var ticketId = (_ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value,
                                TicketStatus.Processing)
                            ?? _ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value,
                                TicketStatus.ToBeProcessed)
                            ?? _ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value,
                                TicketStatus.New)
                )?.Id;

            if (ticketId == null) return vm;

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId)
                                     ?? _opDataRepository.GetLastOpData<SingleWindowOpData>(ticketId, null);
            if (singleWindowOpData != null)
            {
                vm.BindedTruck.DelliveryBillCode = singleWindowOpData.DeliveryBillCode;
                vm.BindedTruck.IsThirdPartyCarrier = singleWindowOpData.IsThirdPartyCarrier;
                vm.BindedTruck.SenderName = _externalDataRepository
                                            .GetOrganisationDetail(singleWindowOpData.OrganizationId.Value)
                                            ?.ShortName ?? singleWindowOpData.CustomPartnerName;

                if (singleWindowOpData.IsThirdPartyCarrier)
                {
                    vm.BindedTruck.TransportNo = singleWindowOpData.HiredTransportNumber;
                    vm.BindedTruck.TrailerNo = singleWindowOpData.HiredTrailerNumber;
                }
                else
                {
                    vm.BindedTruck.TransportNo =
                        _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId.Value)?.RegistrationNo ??
                        string.Empty;
                    vm.BindedTruck.TrailerNo =
                        _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId.Value)?.RegistrationNo ??
                        string.Empty;
                }
                
                var currentBrutto = _context.ScaleOpDatas.Where(x => x.TicketId == nodeDto.Context.TicketId.Value
                                                                                       && x.TypeId == ScaleOpDataType.Gross
                                                                                       && x.StateId == OpDataState.Processed)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();
                var currentTare = _context.ScaleOpDatas.Where(x => x.TicketId == nodeDto.Context.TicketId.Value
                                                                                     && x.TypeId == ScaleOpDataType.Tare
                                                                                     && x.StateId == OpDataState.Processed)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();

                if (currentBrutto != null && currentTare != null)
                {
                    vm.BindedTruck.WeightValue = (currentBrutto.TruckWeightValue + currentBrutto.TrailerWeightValue) -
                                                 (currentTare.TruckWeightValue + currentTare.TrailerWeightValue) ?? 0;
                }
                vm.BindedTruck.Product = _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId.Value)?.ShortName ?? string.Empty;

                vm.BindedTruck.Comment = singleWindowOpData.Comments;
            }

            return vm;
        }

        public void UnloadPointType1_Workstation_SetNodeActive(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return;

            if (!nodeDto.IsActive) _nodeManager.ChangeNodeState(nodeId, true);
        }

        public void UnloadPointType1_AddChangeStateVisa_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void UnloadPointType1_Idle_ChangeState(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.AddChangeStateVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public void UnloadPointType1_Idle_GetTareValue(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointType1.State.GetTareValue;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
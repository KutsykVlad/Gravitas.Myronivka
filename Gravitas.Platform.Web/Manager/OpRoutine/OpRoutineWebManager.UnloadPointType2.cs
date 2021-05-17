using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Gravitas.Model;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool UnloadPointType2_ConfirmOperation_Next(long nodeId, string acceptancePointCode)
        {
            if (acceptancePointCode == null) return false;

            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return false;

            var singleWindowOpData = _opDataRepository.GetLastOpData<SingleWindowOpData>(nodeDto.Context.TicketId, null);

            if (singleWindowOpData == null) return false;

            singleWindowOpData.CollectionPointId = _context.AcceptancePoints.First(x=> x.Code == acceptancePointCode).Id;
            _opDataRepository.Update<SingleWindowOpData, Guid>(singleWindowOpData);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointType2.State.AddOperationVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool UnloadPointType2_AddOperationVisa_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return false;

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointType2.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool UnloadPointType2_IdleWorkstation_Back(long nodeId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointType2.State.Workstation;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool UnloadPointType2_Workstation_Process(long nodeId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            nodeDto.Context.OpRoutineStateId = nodeDto.Group == NodeGroup.Load ? Dom.OpRoutine.LoadPointType1.State.Idle : Dom.OpRoutine.UnloadPointType2.State.Idle;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public UnloadPointType2Vms.IdleVm UnloadPointType2_IdleVm(long nodeId)
        {
            var vm = new UnloadPointType2Vms.IdleVm();
            vm.NodeId = nodeId;

            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketContainerId == null) return vm;

            vm.BindedTruck = new UnloadPointTicketContainerItemVm();

            var card = _ticketRepository
                .GetSingleOrDefault<Card, string>(e =>
                    e.TicketContainerId == nodeDto.Context.TicketContainerId.Value &&
                    e.TypeId == Dom.Card.Type.TicketCard);
            if (card != null) vm.BindedTruck.CardNumber = card.No.ToString();

            var ticketId = (_ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value,
                                Dom.Ticket.Status.Processing)
                            ?? _ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value,
                                Dom.Ticket.Status.ToBeProcessed)
                            ?? _ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value,
                                Dom.Ticket.Status.New)
                )?.Id;

            if (ticketId == null) return vm;

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId)
                                     ?? _opDataRepository.GetLastOpData<SingleWindowOpData>(ticketId, null);
            if (singleWindowOpData != null)
            {
                vm.BindedTruck.DelliveryBillCode = singleWindowOpData.DeliveryBillCode;
                vm.BindedTruck.IsThirdPartyCarrier = singleWindowOpData.IsThirdPartyCarrier;
                vm.BindedTruck.SenderName = _externalDataRepository
                                            .GetOrganisationDetail(singleWindowOpData.OrganizationId)
                                            ?.ShortName ?? singleWindowOpData.CustomPartnerName;

                if (singleWindowOpData.IsThirdPartyCarrier)
                {
                    vm.BindedTruck.TransportNo = singleWindowOpData.HiredTransportNumber;
                    vm.BindedTruck.TrailerNo = singleWindowOpData.HiredTrailerNumber;
                }
                else
                {
                    vm.BindedTruck.TransportNo =
                        _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId)?.RegistrationNo ??
                        string.Empty;
                    vm.BindedTruck.TrailerNo =
                        _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId)?.RegistrationNo ??
                        string.Empty;
                }

                var currentBrutto = _context.ScaleOpDatas.Where(x => x.TicketId == nodeDto.Context.TicketId.Value
                                                                                       && x.TypeId == Dom.ScaleOpData.Type.Gross
                                                                                       && x.StateId == Dom.OpDataState.Processed)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();
                var currentTare = _context.ScaleOpDatas.Where(x => x.TicketId == nodeDto.Context.TicketId.Value
                                                                                     && x.TypeId == Dom.ScaleOpData.Type.Tare
                                                                                     && x.StateId == Dom.OpDataState.Processed)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault();

                if (currentBrutto != null)
                {
                    vm.BindedTruck.WeightValue = (currentBrutto.TruckWeightValue + currentBrutto.TrailerWeightValue) ?? 0;
                }
                vm.BindedTruck.Product = _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId)?.ShortName ?? string.Empty;

                vm.BindedTruck.Comment = singleWindowOpData.Comments;
            }

            return vm;
        }

        public void UnloadPointType2_Workstation_SetNodeActive(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto == null) return;

            if (!nodeDto.IsActive) _nodeManager.ChangeNodeState(nodeId, true);
        }

        public void UnloadPointType2_AddChangeStateVisa_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointType2.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void UnloadPointType2_Idle_ChangeState(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return;
            }

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointType2.State.AddChangeStateVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

    }
}
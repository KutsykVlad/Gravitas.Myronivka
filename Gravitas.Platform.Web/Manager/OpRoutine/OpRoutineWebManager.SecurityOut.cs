using System.Collections.Generic;
using System.Linq;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public SecurityOutVms.ShowOperationsListVm SecurityOut_ShowOperationsList_GetData(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketId == null)
                return null;

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(nodeDto.Context.TicketId.Value);

            string truckNo;
            string trailerNo;
            if (singleWindowOpData.IsThirdPartyCarrier)
            {
                truckNo = singleWindowOpData.HiredTransportNumber;
                trailerNo = singleWindowOpData.HiredTrailerNumber;
            }
            else
            {
                truckNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId).RegistrationNo;
                trailerNo = string.IsNullOrEmpty(singleWindowOpData.TrailerId) ? " " : _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId).RegistrationNo;
            }

            var ticket = _context.Tickets.First(x => x.Id == nodeDto.Context.TicketId.Value);
            var tickets = _context.Tickets.Where(x => x.TicketContainerId == ticket.TicketContainerId && x.StatusId == TicketStatus.Completed)
                .OrderBy(x => x.OrderNo)
                .Select(x => x.Id)
                .ToList();
            var allTickets = new List<int>();
            allTickets.AddRange(tickets);
            allTickets.Add(ticket.Id);
            
            var routineData = new SecurityOutVms.ShowOperationsListVm
            {
                NodeId = nodeId,
                Tickets = allTickets,
                TruckNo = truckNo,
                TrailerNo = trailerNo,
                IsTechRoute = singleWindowOpData.SupplyCode == TechRoute.SupplyCode
            };

            return routineData;
        }

        public bool SecurityOut_ShowOperationsList_Confirm(int nodeId, bool isConfirmed)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.OpDataId == null) return false;

            var securityCheckOutOpData = _context.SecurityCheckOutOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (securityCheckOutOpData == null) return false;

            securityCheckOutOpData.IsCameraImagesConfirmed = isConfirmed;
            _context.SaveChanges();

            if(isConfirmed)
            {
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.EditStampList;
            }
            else
            {
                nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.Idle;
                _nodeRepository.ClearNodeProcessingMessage(nodeId);
                nodeDto.Context.TicketContainerId = null;
                nodeDto.Context.TicketId = null;
                nodeDto.Context.OpDataId = null;
            }

            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool SecurityOut_EditStampList_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return false;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.ShowOperationsList;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool SecurityOut_EditStampList(SecurityOutVms.EditStampListVm vm)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto?.Context?.OpDataId == null) return false;

            var securityCheckOutOpData = _context.SecurityCheckOutOpDatas.FirstOrDefault(x => x.Id == nodeDto.Context.OpDataId.Value);
            if (securityCheckOutOpData == null) return false;

            securityCheckOutOpData.SealList = vm.StampList;
            _context.SaveChanges();

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.AddRouteControlVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void SecurityOut_CheckOwnTransport_Next(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.AddRouteControlVisa;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public void SecurityOut_CheckOwnTransport_Reject(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.OpRoutineStateId == null) return;

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.SecurityOut.State.Idle;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
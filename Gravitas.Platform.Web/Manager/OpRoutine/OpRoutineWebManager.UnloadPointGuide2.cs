using System;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel.OpRoutine.UnloadPointGuide2;
using LabFacelessOpData = Gravitas.Model.DomainModel.OpData.DAO.LabFacelessOpData;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool UnloadPointGuide2_Idle_SelectTicketContainer(int nodeId, int ticketContainerId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            var ticket = _ticketRepository.GetTicketInContainer(ticketContainerId, TicketStatus.Processing);
            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(
                        ProcessingMsgType.Error,
                        $@"Маршрут не знадено. Маршрутний лист Id:{ticketContainerId}"));
                return false;
            }

            var unloadGuideOpData =
				_opDataRepository.GetFirstOrDefault<UnloadGuideOpData, Guid>(item => item.TicketId == ticket.Id);

            if (unloadGuideOpData is null)
            {
                unloadGuideOpData = new UnloadGuideOpData
                {
                    StateId = OpDataState.Init,
                    NodeId = nodeId,
                    TicketId = ticket.Id,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now
                };
                _ticketRepository.Add<UnloadGuideOpData, Guid>(unloadGuideOpData);
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointGuide2.State.BindUnloadPoint;
            nodeDto.Context.TicketContainerId = ticketContainerId;
            nodeDto.Context.TicketId = ticket.Id;
            nodeDto.Context.OpDataId = unloadGuideOpData.Id;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public UnloadPointGuide2Vms.BindUnloadPointVm UnloadPointGuide2_BindUnloadPoint_GetVm(int nodeId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto.Context?.TicketContainerId == null || nodeDto.Context.TicketId == null)
            {
                SendWrongContextMessage(nodeDto.Id);
                return null;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            var vm = new UnloadPointGuide2Vms.BindUnloadPointVm {NodeId = nodeId};

            var routeId = _context.Tickets.First(x => x.Id == nodeDto.Context.TicketId.Value)?.RouteTemplateId;
            if (!routeId.HasValue) return vm;
            
            var ticketId = _ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value, TicketStatus.Processing)?.Id;

            if (ticketId == null) return vm;

            var data = _opDataManager.GetBasicTicketData(ticketId.Value);
            vm.ProductName = data.ProductName;
            vm.IsThirdPartyCarrier = data.IsThirdPartyCarrier;
            vm.SenderName = data.SenderName;
            vm.Comment = data.SingleWindowComment;
            vm.TransportNo = data.TransportNo;
            vm.TrailerNo = data.TrailerNo;

            var labFacelessOpData = _opDataRepository.GetLastProcessed<LabFacelessOpData>(ticketId);
            if (labFacelessOpData != null)
            {
                vm.HumidityValue = labFacelessOpData.HumidityValue;
                vm.ImpurityValue = labFacelessOpData.ImpurityValue;
                vm.EffectiveValue = labFacelessOpData.EffectiveValue;
                vm.LabComment = labFacelessOpData.Comment;
            }

            var unloadGuideData = _opDataRepository.GetLastOpData<UnloadGuideOpData>(ticketId, null);
            if (unloadGuideData != null)
            {
                vm.UnloadNodeId = unloadGuideData.UnloadPointNodeId;
                vm.UnloadNodeName = unloadGuideData.Node?.Name;
            }

            return vm;
        }

        public bool UnloadPointGuide2_BindUnloadPoint_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto.Context.TicketContainerId == null)
            {
                SendWrongContextMessage(nodeDto.Id);
                return false;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeId);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointGuide2.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        } 
        
        public bool UnloadPointGuide2_BindUnloadPoint_Next(UnloadPointGuide2Vms.BindUnloadPointVm vm)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto?.Context?.TicketId == null || nodeDto.Context.OpDataId == null)
            {
                SendWrongContextMessage(vm.NodeId);
                return false;
            }

            if (vm.UnloadNodeId == 0) return false;
            
            var unloadGuideOpData = _context.UnloadGuideOpDatas.First(x => x.Id == nodeDto.Context.OpDataId.Value);

            if (unloadGuideOpData.UnloadPointNodeId == 0)
            {
                var ticket = _context.Tickets.FirstOrDefault(x => x.Id == nodeDto.Context.TicketId.Value);
                if (ticket == null)
                {
                    return false;
                }

                if (!_routesInfrastructure.GetNextNodes(ticket.Id).Contains(nodeDto.Id))
                {
                    _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                        new NodeProcessingMsgItem(ProcessingMsgType.Warning, "Автомобіль не закінчив обробку на попередніх вузлах"));
                
                    return false;
                }

                _routesInfrastructure.MoveForward(ticket.Id, vm.NodeId);
            }
            
            unloadGuideOpData.UnloadPointNodeId = (int) NodeIdValue.UnloadPoint50;
            _opDataRepository.AddOrUpdate<UnloadGuideOpData, Guid>(unloadGuideOpData);

            nodeDto.Context.OpProcessData = vm.UnloadNodeId;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.UnloadPointGuide2.State.AddOpVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
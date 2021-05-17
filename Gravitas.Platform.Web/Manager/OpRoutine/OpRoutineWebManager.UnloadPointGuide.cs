using System;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Model.Dto;
using Gravitas.Platform.Web.ViewModel.OpRoutine.UnloadPointGuide;
using Dom = Gravitas.Model.DomainValue.Dom;
using LabFacelessOpData = Gravitas.Model.DomainModel.OpData.DAO.LabFacelessOpData;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool UnloadPointGuide_Idle_SelectTicketContainer(long nodeId, long ticketContainerId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            var ticket = _ticketRepository.GetTicketInContainer(ticketContainerId, Dom.Ticket.Status.Processing);
            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(
                        Dom.Node.ProcessingMsg.Type.Error,
                        $@"Маршрут не знадено. Маршрутний лист Id:{ticketContainerId}"));
                return false;
            }

            var unloadGuideOpData =
				_opDataRepository.GetFirstOrDefault<UnloadGuideOpData, Guid>(item => item.TicketId == ticket.Id);

            if (unloadGuideOpData is null)
            {
                unloadGuideOpData = new UnloadGuideOpData
                {
                    StateId = Dom.OpDataState.Init,
                    NodeId = nodeId,
                    TicketId = ticket.Id,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now
                };
                _ticketRepository.Add<UnloadGuideOpData, Guid>(unloadGuideOpData);
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide.State.BindUnloadPoint;
            nodeDto.Context.TicketContainerId = ticketContainerId;
            nodeDto.Context.TicketId = ticket.Id;
            nodeDto.Context.OpDataId = unloadGuideOpData.Id;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public UnloadPointGuideVms.BindUnloadPointVm UnloadPointGuide_BindUnloadPoint_GetVm(long nodeId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto.Context?.TicketContainerId == null || nodeDto.Context.TicketId == null)
            {
                SendWrongContextMessage(nodeDto.Id);
                return null;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            var vm = new UnloadPointGuideVms.BindUnloadPointVm {NodeId = nodeId};

            var routeId = _context.Tickets.First(x => x.Id == nodeDto.Context.TicketId.Value)?.RouteTemplateId;
            if (!routeId.HasValue) return vm;
            
            var destinationPointList = _routesInfrastructure.GetNodesInGroup(routeId, NodeGroup.Unload);

            vm.NodeItems = _nodeRepository.GetNodeItems().Items
                .Where(e => destinationPointList.Contains(e.Id))
                .ToList();

            var ticketId = _ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value, Dom.Ticket.Status.Processing)?.Id;

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

        public bool UnloadPointGuide_BindUnloadPoint_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto.Context.TicketContainerId == null)
            {
                SendWrongContextMessage(nodeDto.Id);
                return false;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeId);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        } 
        
        public void UnloadPointGuide_Idle_AskFromQueue(long nodeId, long ticketContainerId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto.Context.TicketContainerId == null)
            {
                SendWrongContextMessage(nodeDto.Id);
                return;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeId);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide.State.EntryAddOpVisa;
            nodeDto.Context.TicketContainerId = ticketContainerId;
            UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
        
        public bool UnloadPointGuide_Idle_AskFromQueue_Back(long nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto.Context.TicketContainerId == null)
            {
                SendWrongContextMessage(nodeDto.Id);
                return false;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeId);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        } 

        public bool UnloadPointGuide_BindUnloadPoint_Next(UnloadPointGuideVms.BindUnloadPointVm vm)
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
                        new NodeProcessingMsgItem(Dom.Node.ProcessingMsg.Type.Warning, "Автомобіль не закінчив обробку на попередніх вузлах"));
                
                    return false;
                }

                _routesInfrastructure.MoveForward(ticket.Id, vm.NodeId);
            }
            
            unloadGuideOpData.UnloadPointNodeId = vm.UnloadNodeId;
            unloadGuideOpData.StateId = Dom.OpDataState.Processed;
            _opDataRepository.AddOrUpdate<UnloadGuideOpData, Guid>(unloadGuideOpData);

            nodeDto.Context.OpRoutineStateId = Dom.OpRoutine.UnloadPointGuide.State.AddOpVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }
    }
}
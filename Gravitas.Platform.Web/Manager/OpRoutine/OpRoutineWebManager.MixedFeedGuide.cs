using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainModel.MixedFeed.DAO;
using Gravitas.Model.DomainModel.Node.TDO.Json;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel.MixedFeedManage;
using Gravitas.Platform.Web.ViewModel.OpRoutine.MixedFeedGuide;

namespace Gravitas.Platform.Web.Manager.OpRoutine
{
    public partial class OpRoutineWebManager
    {
        public bool MixedFeedGuide_Idle_SelectTicketContainer(int nodeId, int ticketContainerId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            var ticket = _ticketRepository.GetTicketInContainer(ticketContainerId, TicketStatus.Processing) ??
                            _ticketRepository.GetTicketInContainer(ticketContainerId, TicketStatus.ToBeProcessed);
            if (ticket == null)
            {
                _opRoutineManager.UpdateProcessingMessage(nodeDto.Id,
                    new NodeProcessingMsgItem(
                        ProcessingMsgType.Error,
                        $@"Маршрут не знадено. Маршрутний лист Id:{ticketContainerId}"));
                return false;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedGuide.State.BindLoadPoint;
            nodeDto.Context.TicketContainerId = ticketContainerId;
            nodeDto.Context.TicketId = ticket.Id;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public MixedFeedGuideVms.BindDestPointVm MixedFeedGuide_BindLoadPoint_GetVm(int nodeId)
        {
            // Validate node context
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context?.TicketContainerId == null
                || nodeDto.Context.TicketId == null)
            {
                SendWrongContextMessage(nodeId);
                return null;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeDto.Id);
            var vm = new MixedFeedGuideVms.BindDestPointVm {NodeId = nodeId};
            
            var routeId = _context.Tickets.First(x => x.Id == nodeDto.Context.TicketId.Value)?.RouteTemplateId;
            if (!routeId.HasValue)
            {
                return vm;
            }
            
            var destinationList = new List<int>
            {
                (int) NodeIdValue.MixedFeedLoad1,
                (int) NodeIdValue.MixedFeedLoad2,
                (int) NodeIdValue.MixedFeedLoad3,
                (int) NodeIdValue.MixedFeedLoad4
            };
            
            var singleWindowOpData = _context.SingleWindowOpDatas.FirstOrDefault(z => z.TicketId == nodeDto.Context.TicketId);
            if (singleWindowOpData == null) return vm;

            var driveList = _context.MixedFeedSilos.Where(z => z.ProductId == singleWindowOpData.ProductId)
                .Select(z => z.Drive)
                .ToList();

            var loadPointList = destinationList
                .Where(x => _routesInfrastructure.IsNodeAvailable(x, routeId.Value))
                .Where(x => driveList.Contains(x))
                .ToList();
            
            vm.NodeItems = _nodeRepository.GetNodeItems()
                                          .Items
                                          .Where(e => loadPointList.Contains(e.Id))
                                          .ToList();

            var ticketId = (_ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value, TicketStatus.Processing)
                ?? _ticketRepository.GetTicketInContainer(nodeDto.Context.TicketContainerId.Value, TicketStatus.ToBeProcessed))?.Id;
            if (ticketId == null) return vm;
            var data = _opDataManager.GetBasicTicketData(ticketId.Value);
            vm.ProductName = data.ProductName;
            vm.ReceiverDepotName = data.ReceiverDepotName;
            vm.LoadTarget = data.LoadTarget;
            vm.ReceiverName = data.SenderName;
            vm.TransportNo = data.TransportNo;
            
            vm.Card = _cardRepository.GetContainerCardNo(nodeDto.Context.TicketContainerId.Value);

            var mixedFeedGuideOpData = _opDataRepository.GetLastOpData<LoadGuideOpData>(ticketId, null);
            if (mixedFeedGuideOpData != null)
            {
                vm.DestNodeId = mixedFeedGuideOpData.LoadPointNodeId;
                vm.DestNodeName = mixedFeedGuideOpData.Node?.Name;
            }
            
            var opData = _opDataRepository.GetLastOpData(ticketId, OpDataState.Processed);
            if (opData?.Node.Name is string processedNodeName)
            {
                vm.LastNodeName = processedNodeName;
            }
            
            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(ticketId.Value);
            if (partLoadValue.HasValue)
            {
                vm.LoadTarget = partLoadValue.Value;
            }

            return vm;
        }

        public bool MixedFeedGuide_BindLoadPoint_Back(int nodeId)
        {
            var nodeDto = _nodeRepository.GetNodeDto(nodeId);
            if (nodeDto?.Context.TicketContainerId == null)
            {
                SendWrongContextMessage(nodeId);
                return false;
            }

            _nodeRepository.ClearNodeProcessingMessage(nodeId);

            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedGuide.State.Idle;
            nodeDto.Context.TicketContainerId = null;
            nodeDto.Context.TicketId = null;
            nodeDto.Context.OpDataId = null;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public bool MixedFeedGuide_BindLoadPoint_Next(MixedFeedGuideVms.BindDestPointVm vm)
        {
            var nodeDto = _nodeRepository.GetNodeDto(vm.NodeId);
            if (nodeDto?.Context?.TicketContainerId == null
                || nodeDto.Context?.TicketId == null)
            {
                SendWrongContextMessage(vm.NodeId);
                return false;
            }

            var mixedFeedGuideOpData =
                _opDataRepository.GetLastProcessed<LoadGuideOpData>(x => x.TicketId == nodeDto.Context.TicketId);
            var mixedFeedLoadOpData =
                _opDataRepository.GetLastProcessed<LoadPointOpData>(x => x.TicketId == nodeDto.Context.TicketId);

            if (mixedFeedGuideOpData == null || mixedFeedGuideOpData.CheckOutDateTime < mixedFeedLoadOpData?.CheckOutDateTime)
            {
                mixedFeedGuideOpData = new LoadGuideOpData
                {
                    StateId = OpDataState.Init,
                    NodeId = nodeDto.Id,
                    TicketId = nodeDto.Context.TicketId,
                    TicketContainerId = nodeDto.Context.TicketContainerId,
                    CheckInDateTime = DateTime.Now,
                    CheckOutDateTime = DateTime.Now
                };
            }

            mixedFeedGuideOpData.LoadPointNodeId = vm.DestNodeId;
            mixedFeedGuideOpData.CheckInDateTime = DateTime.Now;
            mixedFeedGuideOpData.CheckOutDateTime = DateTime.Now;
            mixedFeedGuideOpData.StateId = OpDataState.Init;
            _opDataRepository.AddOrUpdate<LoadGuideOpData, Guid>(mixedFeedGuideOpData);


            nodeDto.Context.OpDataId = mixedFeedGuideOpData.Id;
            nodeDto.Context.OpRoutineStateId = Model.DomainValue.OpRoutine.MixedFeedGuide.State.AddOpVisa;
            return UpdateNodeContext(nodeDto.Id, nodeDto.Context);
        }

        public MixedFeedProtocolVm MixedFeed_ProtocolPrintout_GetVm(int nodeId)
        {
            var opVisa = _context.OpVisas.Where(x => x.OpRoutineStateId == Model.DomainValue.OpRoutine.MixedFeedManage.State.AddOperationVisa)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();
            var vm = new MixedFeedProtocolVm
            {
                Date = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                SiloOperator = opVisa != null
                    ? _externalDataRepository.GetEmployeeDetail(opVisa.EmployeeId.Value)?.ShortName
                    : ""
            };

            var siloItems = _opDataRepository.GetQuery<MixedFeedSilo, int>();
            foreach (var item in siloItems)
            {
                vm.GetType().GetProperty($"Product{item.Id}")?.SetValue(vm, _externalDataRepository.GetProductDetail(item.ProductId.Value)?.ShortName);
                vm.GetType().GetProperty($"Spec{item.Id}")?.SetValue(vm, item.Specification);
                vm.GetType().GetProperty($"Queue{item.Id}")?.SetValue(vm, item.LoadQueue.ToString());
                vm.GetType().GetProperty($"Scale{item.Id}")?.SetValue(vm, item.SiloWeight.ToString());
                vm.GetType().GetProperty($"EmptyHeight{item.Id}")?.SetValue(vm, item.SiloEmpty.ToString(CultureInfo.InvariantCulture));
                vm.GetType().GetProperty($"FullHeight{item.Id}")?.SetValue(vm, item.SiloFull.ToString(CultureInfo.InvariantCulture));
            }

            return vm;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Gravitas.DAL;
using Gravitas.Infrastructure.Platform.Manager.CentralLaboratory;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.Scale;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Model.DomainModel.Ticket.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.TicketContainer.List;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Platform.Web.Manager
{
    public class TicketContainerWebManager : ITicketContainerWebManager
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICentralLaboratoryManager _centralLaboratoryManager;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly IOpDataRepository _opDataRepository;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly IScaleManager _scaleManager;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISettings _settings;
        private readonly GravitasDbContext _context;

        public TicketContainerWebManager(
            ITicketRepository ticketRepository,
            IOpDataRepository opDataRepository,
            IExternalDataRepository externalDataRepository,
            IRoutesInfrastructure routesInfrastructure,
            ICentralLaboratoryManager centralLaboratoryManager,
            ICardRepository cardRepository,
            IScaleManager scaleManager,
            ISettings settings,
            GravitasDbContext context)
        {
            _ticketRepository = ticketRepository;
            _opDataRepository = opDataRepository;
            _externalDataRepository = externalDataRepository;
            _routesInfrastructure = routesInfrastructure;
            _centralLaboratoryManager = centralLaboratoryManager;
            _cardRepository = cardRepository;
            _scaleManager = scaleManager;
            _settings = settings;
            _context = context;
        }

        public IEnumerable<long> GetActiveTicketContainers(List<long> inputNodeIds)
        {
            var tickets = 
                (from card in _context.Cards
                    join ticket in _context.Tickets on card.TicketContainerId equals ticket.ContainerId 
                    where card.TicketContainerId.HasValue
                          && card.TypeId == Dom.Card.Type.TicketCard
                          && (ticket.StatusId == Dom.Ticket.Status.Processing
                              || ticket.StatusId == Dom.Ticket.Status.ToBeProcessed)
                    select ticket)
                .GroupBy(x => x.ContainerId)
                .Select(x => x.OrderByDescending(z => z.StatusId)
                    .FirstOrDefault())
                .ToList();

            if (inputNodeIds == null
                || !inputNodeIds.Any())
                return tickets.Select(x => x.ContainerId);

            var resultContainers = new List<long>();

            foreach (var ticket in tickets)
            {
                if (ticket?.RouteTemplateId == null) continue;

                var isNodeInTicketRoute = inputNodeIds.Any(x =>
                    _routesInfrastructure.IsNodeAvailable(x, ticket.SecondaryRouteTemplateId ?? ticket.RouteTemplateId.Value));
                if (!isNodeInTicketRoute) continue;
                resultContainers.Add(ticket.ContainerId);
            }

            return resultContainers;
        }

        public ICollection<UnloadGuideTicketContainerItemVm> GetUnloadGuideTicketContainerItemsVm(IEnumerable<long> containerIds)
        {
            return containerIds.Select(GetUnloadGuideTicketContainerItemVm)
                .OrderByDescending(item => item.IsActive)
                .ThenByDescending(item => item.UnloadNodeId != 0)
                .ToList();
        }

        public ICollection<SingleWindowTicketContainerItemVm> GetSingleWindowTicketContainerItemsVm(IEnumerable<long> containerIds)
        {
            return containerIds.Select(GetSingleWindowTicketContainerItemVm)
                .OrderBy(x => x.BaseData.CheckInDateTime)
                .ToList();
        }

        public ICollection<UnloadQueueTicketContainerItemVm> GetUnloadQueueTicketContainerItemsVm()
        {
            var vm =
                (from singleOpData in _context.SingleWindowOpDatas
                    join p in _context.Products on singleOpData.ProductId ?? string.Empty equals p.Id into productJoin
                    join o in _context.QueueRegisters on singleOpData.TicketContainerId equals o.TicketContainerId
                    from product in productJoin.DefaultIfEmpty()
                    where o != null && (singleOpData.DocHumidityValue != null || singleOpData.DocImpurityValue != null)
                    select new UnloadQueueTicketContainerItemVm
                    {
                        TicketContainerId = singleOpData.TicketContainerId.Value,
                        Nomenclature = product.ShortName,
                        DocImpurityValue = singleOpData.DocImpurityValue == null
                            ? singleOpData.DocImpurityValue
                            : Math.Round(singleOpData.DocImpurityValue.Value, 2),
                        DocHumidityValue = singleOpData.DocHumidityValue == null
                            ? singleOpData.DocHumidityValue
                            : Math.Round(singleOpData.DocHumidityValue.Value, 2),
                        IsAllowedToEnterTerritory = o.IsAllowedToEnterTerritory
                    })
                .ToList();

            return vm;
        }

        public ICollection<UnloadPointTicketContainerItemVm> GetUnloadPointTicketContainerItemsVm(IEnumerable<long> containerIds, long nodeId)
        {
            var result = new List<UnloadPointTicketContainerItemVm>();
            foreach (var containerId in containerIds)
            {
                var ticket = _ticketRepository.GetTicketInContainer(containerId, Dom.Ticket.Status.Processing)
                             ?? _ticketRepository.GetTicketInContainer(containerId, Dom.Ticket.Status.ToBeProcessed);
                if (ticket == null) continue;
                var unloadGuideOpData = GetUnloadGuideOpDataLastOpData(ticket.Id, null);
                var unloadPointOpData = GetUnloadPointOpDataLastOpData(ticket.Id, null);
                if (unloadGuideOpData?.UnloadPointNodeId == nodeId &&
                    (unloadPointOpData == null
                     || unloadPointOpData.StateId != Dom.OpDataState.Processed
                     || unloadGuideOpData.CheckOutDateTime > unloadPointOpData.CheckOutDateTime))
                    result.Add(GetUnloadPointTicketContainerItemVm(containerId));
            }

            return result;
        }

        public List<LoadGuideTicketContainerItemVm> GetLoadGuideTicketContainerItemsVm(IEnumerable<long> containerIds)
        {
            return containerIds.Select(GetLoadGuideTicketContainerItemVm).ToList();
        }

        public ICollection<LoadPointTicketContainerItemVm> GetLoadPointTicketContainerItemsVm(IEnumerable<long> containerIds, long nodeId)
        {
            var result = new List<LoadPointTicketContainerItemVm>();
            foreach (var containerId in containerIds)
            {
                var ticket = _ticketRepository.GetTicketInContainer(containerId, Dom.Ticket.Status.Processing)
                             ?? _ticketRepository.GetTicketInContainer(containerId, Dom.Ticket.Status.ToBeProcessed);
                if (ticket == null) continue;
                var loadGuideOpData = GetLoadGuideOpDataLastOpData(ticket.Id, null);
                var loadPointOpData = GetLoadPointOpDataLastOpData(ticket.Id, null);
                if (loadGuideOpData?.LoadPointNodeId == nodeId
                    && (loadPointOpData == null
                        || loadPointOpData.StateId != Dom.OpDataState.Processed
                        || loadGuideOpData.CheckOutDateTime > loadPointOpData.CheckOutDateTime))
                    result.Add(GetLoadPointTicketContainerItemVm(containerId));
            }

            return result;
        }

        public ICollection<LoadGuideTicketContainerItemVm> GetRejectedLoadGuideTicketContainerItemsVm(IEnumerable<long> containerIds)
        {
            return containerIds.Select(GetRejectedLoadGuideTicketContainerItemVm).ToList();
        }

        public ICollection<UnloadGuideTicketContainerItemVm> GetRejectedUnloadGuideTicketContainerItemsVm(IEnumerable<long> containerIds)
        {
            return containerIds.Select(GetRejectedUnloadGuideTicketContainerItemVm)
                .OrderByDescending(item => item.IsActive)
                .ThenByDescending(item => item.UnloadNodeId != 0)
                .ToList();
        }

        public ICollection<MixedFeedGuideTicketContainerItemVm> GetMixedFeedGuideTicketContainerItemsVm(IEnumerable<long> containerIds)
        {
            var items = containerIds.Where(item =>
                {
                    var ticket = _ticketRepository.GetTicketInContainer(item, Dom.Ticket.Status.Processing) ??
                                 _ticketRepository.GetTicketInContainer(item, Dom.Ticket.Status.ToBeProcessed);
                    if (ticket == null) return false;

                    var opData = GetMixedFeedLoadOpDataLastOpData(ticket.Id,null);

                    return opData == null;
                })
                .Select(GetMixedFeedGuideTicketContainerItemVm)
                .ToList();
            return items;
        }

        public ICollection<MixedFeedLoadTicketContainerItemVm> GetMixedFeedLoadTicketContainerItemsVm(IEnumerable<long> containerIds, long nodeId)
        {
            var result = new List<MixedFeedLoadTicketContainerItemVm>();
            foreach (var containerId in containerIds)
            {
                var ticket = _ticketRepository.GetTicketInContainer(containerId, Dom.Ticket.Status.Processing)
                             ?? _ticketRepository.GetTicketInContainer(containerId, Dom.Ticket.Status.ToBeProcessed);
                if (ticket == null) continue;
                var loadGuideOpData = GetMixedFeedGuideOpDataLastOpData(ticket.Id, null);
                var loadPointOpData = GetMixedFeedLoadOpDataLastOpData(ticket.Id, null);
                if (loadPointOpData?.CheckOutDateTime < loadGuideOpData?.CheckOutDateTime) loadPointOpData = null;
                if (loadGuideOpData?.LoadPointNodeId == nodeId && (loadPointOpData == null || loadPointOpData.StateId != Dom.OpDataState.Processed))
                    result.Add(GetMixedFeedLoadTicketContainerItemVm(containerId));
            }

            return result;
        }

        public ICollection<MixedFeedGuideTicketContainerItemVm> GetRejectedMixedFeedLoadTicketContainerItemsVm(IEnumerable<long> containerIds)
        {
            return containerIds.Select(GetRejectedMixedFeedGuideTicketContainerItemVm).ToList();
        }

        public ICollection<MixedFeedUnloadTicketContainerItemVm> GetRejectedMixedFeedUnloadTicketContainerItemsVm(IEnumerable<long> containerIds)
        {
            return containerIds.Select(GetRejectedMixedFeedUnloadTicketContainerItemVm).ToList();
        }

        public ICollection<CentralLabTicketContainerItemVm> GetCentralLabTicketContainerListVm(IEnumerable<long> containerIds)
        {
            var items = containerIds.Where(item =>
                {
                    var ticket = _ticketRepository.GetTicketInContainer(item, Dom.Ticket.Status.Processing)
                                 ?? _ticketRepository.GetTicketInContainer(item, Dom.Ticket.Status.ToBeProcessed);
                    if (ticket == null) return false;

                    var opData = _context.CentralLabOpDatas.Where(x => x.TicketId == ticket.Id)
                        .OrderByDescending(x => x.SampleCheckInDateTime)
                        .FirstOrDefault();
                    return opData == null
                           || opData.StateId != Dom.OpDataState.Processed && opData.StateId != Dom.OpDataState.Rejected;
                }
            );
            return items.Select(GetCentralLabTicketContainerItemVm).OrderBy(x => x.Order).ToList();
        }

        public ICollection<LabFacelessTicketContainerItemVm> GetLabFacelessTicketContainerItemsVm()
        {
            var containerIds = GetActiveTicketContainers()
                .Where(containerId =>
                {
                    var ticketId =
                        (_ticketRepository.GetTicketInContainer(containerId,
                             Dom.Ticket.Status.Processing)
                         ?? _ticketRepository.GetTicketInContainer(containerId,
                             Dom.Ticket.Status.ToBeProcessed)
                         ?? _ticketRepository.GetTicketInContainer(containerId, Dom.Ticket.Status.New)
                        )?.Id;

                    var labOpData =
                        _context.LabFacelessOpDatas.Where(x => x.TicketId == ticketId)
                            .OrderByDescending(x => x.CheckInDateTime)
                            .FirstOrDefault();

                    return labOpData != null && (labOpData.StateId == Dom.OpDataState.Collision
                                                 || labOpData.StateId ==
                                                 Dom.OpDataState.CollisionApproved
                                                 || labOpData.StateId ==
                                                 Dom.OpDataState.CollisionDisapproved);
                })
                .ToList();

            return containerIds.Select(GetLabFacelessTicketContainerItemVm).ToList();
        }

        public ICollection<LabFacelessTicketContainerItemVm> GetSelfServiceLabTicketContainerItemsVm()
        {
            var containerIds = GetActiveTicketContainers()
                .Where(containerId =>
                {
                    var ticketId = _ticketRepository.GetTicketInContainer(containerId, Dom.Ticket.Status.Processing)?.Id;
                    var labOpData = GetLabFacelessLastOpData(ticketId, null);

                    return labOpData != null && (labOpData.StateId == Dom.OpDataState.Processed
                                                 || labOpData.StateId == Dom.OpDataState.Canceled
                                                 || labOpData.StateId == Dom.OpDataState.Rejected);
                })
                .Select(item => item)
                .ToList();

            return containerIds.Select(GetLabFacelessTicketContainerItemVm).ToList();
        }

        private CentralLabTicketContainerItemVm GetCentralLabTicketContainerItemVm(long containerId)
        {
            var baseData = GetBaseRegistryDataByContainer(containerId);

            var state = _centralLaboratoryManager.GetTicketStateInCentralLab(baseData.TicketId);
            var vm = new CentralLabTicketContainerItemVm
            {
                State = _centralLaboratoryManager.LabStateName[state],
                ClassStyle = _centralLaboratoryManager.LabStateClassStyle[state],
                Order = _centralLaboratoryManager.LabStateOrder[state],
                IsActive = false,
                TransportNo = baseData.TransportNo,
                TrailerNo = baseData.TrailerNo,
                ProductName = baseData.ProductName,
                Card = _cardRepository.GetContainerCardNo(containerId)
            };


            var opData = _context.CentralLabOpDatas.Where(x => x.TicketId == baseData.TicketId)
                .OrderByDescending(x => x.SampleCheckInDateTime)
                .FirstOrDefault();

            if (opData == null) return vm;

            vm.Id = opData.Id;
            vm.SampleCheckInDateTime = opData.SampleCheckInDateTime;
            vm.SampleCheckOutDateTime = opData.SampleCheckOutTime;
            vm.IsActive = state == CentralLabState.SamplesCollected
                          || state == CentralLabState.WaitForOperator
                          || state == CentralLabState.OnCollision
                          || state == CentralLabState.CollisionApproved
                          || state == CentralLabState.CollisionDisapproved;

            return vm;
        }

        private MixedFeedGuideTicketContainerItemVm GetMixedFeedGuideTicketContainerItemVm(long containerId)
        {
            var vm = new MixedFeedGuideTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;

            if (!baseData.TicketId.HasValue) return vm;
            var mixedFeedGuideOpData = _opDataRepository.GetLastProcessed<MixedFeedGuideOpData>(baseData.TicketId);
            if (mixedFeedGuideOpData != null)
            {
                var node = _context.Nodes.FirstOrDefault(x => x.Id == mixedFeedGuideOpData.LoadPointNodeId);

                vm.LoadGateId = mixedFeedGuideOpData.LoadPointNodeId;
                vm.LoadNodeName = node != null ? node.Name : string.Empty;
            }

            var ticket = _context.Tickets.First(x => x.Id == baseData.TicketId.Value);

            var opData = _opDataRepository.GetLastOpData(baseData.TicketId, Dom.OpDataState.Processed);
            if (opData?.Node.Name is string processedNodeName)
            {
                vm.LastNodeName = processedNodeName;

                var queueRegisterData = _opDataRepository
                    .GetSingleOrDefault<QueueRegister, long>(t => t.TicketContainerId == containerId);
                vm.CanInvite = (queueRegisterData == null || !queueRegisterData.IsAllowedToEnterTerritory) && ticket.RouteItemIndex == 1;
            }

            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(ticket.Id);
            if (partLoadValue.HasValue) vm.BaseData.LoadTarget = partLoadValue.Value;

            vm.IsActive = ticket.RouteTemplateId.HasValue;
            return vm;
        }

        private MixedFeedGuideTicketContainerItemVm GetRejectedMixedFeedGuideTicketContainerItemVm(long containerId)
        {
            var vm = new MixedFeedGuideTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;

            if (!baseData.TicketId.HasValue) return vm;
            var mixedFeedGuideOpData = GetMixedFeedGuideOpDataLastOpData(baseData.TicketId, null);
            var scaleOpData = GetScaleOpDataLastOpData(baseData.TicketId, null);
            if (mixedFeedGuideOpData != null && mixedFeedGuideOpData.CheckOutDateTime > scaleOpData?.CheckOutDateTime)
            {
                var node = _context.Nodes.FirstOrDefault(x => x.Id == mixedFeedGuideOpData.LoadPointNodeId);

                vm.LoadGateId = mixedFeedGuideOpData.LoadPointNodeId;
                vm.LoadNodeName = node != null ? node.Name : string.Empty;
            }

            var ticket = _context.Tickets.First(x => x.Id == baseData.TicketId.Value);

            var opData = _opDataRepository.GetLastOpData(baseData.TicketId, Dom.OpDataState.Processed);
            if (opData?.Node.Name is string processedNodeName)
            {
                vm.LastNodeName = processedNodeName;

                var queueRegisterData = _opDataRepository
                    .GetSingleOrDefault<QueueRegister, long>(t => t.TicketContainerId == containerId);
                vm.CanInvite = (queueRegisterData == null || !queueRegisterData.IsAllowedToEnterTerritory) && ticket.RouteItemIndex == 1;
            }

            vm.IsActive = ticket.RouteTemplateId.HasValue;

            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(baseData.TicketId.Value);
            if (partLoadValue.HasValue) vm.BaseData.LoadTarget = partLoadValue.Value;

            return vm;
        }

        private MixedFeedUnloadTicketContainerItemVm GetRejectedMixedFeedUnloadTicketContainerItemVm(long containerId)
        {
            var vm = new MixedFeedUnloadTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;
            vm.LoadTarget = baseData.LoadTarget;

            if (!baseData.TicketId.HasValue) return vm;

            var opData = _opDataRepository.GetLastOpData(baseData.TicketId, Dom.OpDataState.Processed);
            if (opData?.Node.Name is string processedNodeName) vm.LastNodeName = processedNodeName;

            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(baseData.TicketId.Value);
            if (partLoadValue.HasValue) vm.LoadTarget = partLoadValue.Value;

            return vm;
        }

        private LoadPointTicketContainerItemVm GetLoadPointTicketContainerItemVm(long containerId)
        {
            var baseData = GetBaseRegistryDataByContainer(containerId);
            var vm = new LoadPointTicketContainerItemVm
            {
                ProductName = baseData.ProductName,
                ReceiverDepotName = baseData.ReceiverDepotName, 
                LoadTarget = baseData.LoadTarget,
                TransportNo = baseData.TransportNo,
            };
            
            if (!baseData.TicketId.HasValue) return vm;
            
            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(baseData.TicketId.Value);
            if (partLoadValue.HasValue) vm.LoadTarget = partLoadValue.Value;

            var singleWindowOpData =
                (from singleOpData in _context.SingleWindowOpDatas
                    join s in _context.Stocks on singleOpData.ReceiverId ?? string.Empty equals s.Id into stockJoin
                    join p in _context.Partners on singleOpData.ReceiverId ?? string.Empty equals p.Id into partnerJoin
                    join sub in _context.Subdivisions on singleOpData.ReceiverId ?? string.Empty equals sub.Id into subDivisionJoin
                    from subDivision in subDivisionJoin.DefaultIfEmpty()
                    from stock in stockJoin.DefaultIfEmpty()
                    from partner in partnerJoin.DefaultIfEmpty()
                    where singleOpData.TicketId == baseData.TicketId
                    select new
                    {
                        singleOpData.LoadTargetDeviationMinus,
                        singleOpData.LoadTargetDeviationPlus,
                        Receiver = singleOpData.ReceiverId != null && singleOpData.ReceiverId != ""
                            ? stock.ShortName
                              ?? subDivision.ShortName
                              ?? partner.ShortName
                              ?? singleOpData.CustomPartnerName
                              ?? "- Хибний ключ -"
                            : string.Empty
                    })
                .FirstOrDefault();
            if (singleWindowOpData == null) return vm;
            vm.LoadTargetDeviationMinus = singleWindowOpData.LoadTargetDeviationMinus;
            vm.LoadTargetDeviationPlus = singleWindowOpData.LoadTargetDeviationPlus;
            vm.ReceiverName = singleWindowOpData.Receiver;

            return vm;
        }

        private MixedFeedLoadTicketContainerItemVm GetMixedFeedLoadTicketContainerItemVm(long containerId)
        {
            var baseData = GetBaseRegistryDataByContainer(containerId);
            var vm = new MixedFeedLoadTicketContainerItemVm
            {
                ProductName = baseData.ProductName,
                ReceiverDepotName = baseData.ReceiverDepotName,
                LoadTarget = baseData.LoadTarget,
                TransportNo = baseData.TransportNo
            };
            if (!baseData.TicketId.HasValue) return vm;
            
            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(baseData.TicketId.Value);
            if (partLoadValue.HasValue) vm.LoadTarget = partLoadValue.Value;
            
            var singleWindowOpData =
                (from singleOpData in _context.SingleWindowOpDatas
                    join s in _context.Stocks on singleOpData.ReceiverId ?? string.Empty equals s.Id into stockJoin
                    join p in _context.Partners on singleOpData.ReceiverId ?? string.Empty equals p.Id into partnerJoin
                    join sub in _context.Subdivisions on singleOpData.ReceiverId ?? string.Empty equals sub.Id into subDivisionJoin
                    from subDivision in subDivisionJoin.DefaultIfEmpty()
                    from stock in stockJoin.DefaultIfEmpty()
                    from partner in partnerJoin.DefaultIfEmpty()
                    where singleOpData.TicketId == baseData.TicketId
                    select new
                    {
                        singleOpData.LoadTargetDeviationMinus,
                        singleOpData.LoadTargetDeviationPlus,
                        Receiver = singleOpData.ReceiverId != null && singleOpData.ReceiverId != ""
                            ? stock.ShortName
                              ?? subDivision.ShortName
                              ?? partner.ShortName
                              ?? singleOpData.CustomPartnerName
                              ?? "- Хибний ключ -"
                            : string.Empty
                    })
                .FirstOrDefault();
            if (singleWindowOpData == null) return vm;
            vm.LoadTargetDeviationMinus = singleWindowOpData.LoadTargetDeviationMinus;
            vm.LoadTargetDeviationPlus = singleWindowOpData.LoadTargetDeviationPlus;
            vm.ReceiverName = singleWindowOpData.Receiver;

            return vm;
        }

        private UnloadPointTicketContainerItemVm GetUnloadPointTicketContainerItemVm(long containerId)
        {
            var baseData = GetBaseRegistryDataByContainer(containerId);
            var vm = new UnloadPointTicketContainerItemVm
            {
                CardNumber = _cardRepository.GetContainerCardNo(containerId)
            };

            var scaleOpData = _context.ScaleOpDatas.Where(x => x.TicketId == baseData.TicketId && x.TypeId == Dom.ScaleOpData.Type.Gross)
                .OrderByDescending(x => x.CheckOutDateTime)
                .FirstOrDefault();
            vm.WeightValue = (scaleOpData?.TrailerWeightValue ?? 0) + scaleOpData?.TruckWeightValue ?? 0;

            vm.DelliveryBillCode = baseData.DeliveryBillCode;
            vm.Comment = baseData.SingleWindowComment;
            vm.IsThirdPartyCarrier = baseData.IsThirdPartyCarrier;
            vm.SenderName = baseData.SenderName;
            vm.TransportNo = baseData.TransportNo;
            vm.TrailerNo = baseData.TrailerNo;

            return vm;
        }

        private LoadGuideTicketContainerItemVm GetLoadGuideTicketContainerItemVm(long containerId)
        {
            var vm = new LoadGuideTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;

            if (!baseData.TicketId.HasValue) return vm;
            var ticket = _context.Tickets.First(x => x.Id == baseData.TicketId);

            var singleWindowOpData =
                (from singleOpData in _context.SingleWindowOpDatas
                    join s in _context.Stocks on singleOpData.ReceiverId ?? string.Empty equals s.Id into stockJoin
                    join p in _context.Partners on singleOpData.ReceiverId ?? string.Empty equals p.Id into partnerJoin
                    join sub in _context.Subdivisions on singleOpData.ReceiverId ?? string.Empty equals sub.Id into subDivisionJoin
                    from subDivision in subDivisionJoin.DefaultIfEmpty()
                    from stock in stockJoin.DefaultIfEmpty()
                    from partner in partnerJoin.DefaultIfEmpty()
                    where singleOpData.TicketId == ticket.Id
                    select new
                    {
                        singleOpData.LoadTargetDeviationMinus,
                        singleOpData.LoadTargetDeviationPlus,
                        Sender = singleOpData.ReceiverId != null && singleOpData.ReceiverId != ""
                            ? stock.ShortName
                              ?? subDivision.ShortName
                              ?? partner.ShortName
                              ?? singleOpData.CustomPartnerName
                              ?? "- Хибний ключ -"
                            : string.Empty
                    })
                .FirstOrDefault();
            if (singleWindowOpData != null)
            {
                vm.BaseData.SenderName = singleWindowOpData.Sender;
                vm.LoadTarget = baseData.LoadTarget;
                vm.LoadTargetDeviationMinus = singleWindowOpData.LoadTargetDeviationMinus;
                vm.LoadTargetDeviationPlus = singleWindowOpData.LoadTargetDeviationPlus;
            }

            var loadGuideData = _opDataRepository.GetLastProcessed<LoadGuideOpData>(baseData.TicketId);
            if (loadGuideData != null)
            {
                var node = _context.Nodes.FirstOrDefault(x => x.Id == loadGuideData.LoadPointNodeId);

                vm.LoadNodeId = loadGuideData.LoadPointNodeId;
                vm.LoadNodeName = node != null ? node.Name : string.Empty;
            }

            var queueRegisterData = _opDataRepository.GetSingleOrDefault<QueueRegister, long>(t => t.TicketContainerId == containerId);
            vm.CanInvite = (queueRegisterData == null || !queueRegisterData.IsAllowedToEnterTerritory) && ticket.RouteItemIndex == 1;

            vm.IsActive = ticket.RouteTemplateId.HasValue;
            return vm;
        }

        private IEnumerable<long> GetActiveTicketContainers()
        {
            var r = (from card in _context.Cards
                    where card.TicketContainerId.HasValue && card.TypeId == Dom.Card.Type.TicketCard
                    select card.TicketContainerId.Value)
                .ToList();
            return r;
        }

        private LabFacelessTicketContainerItemVm GetLabFacelessTicketContainerItemVm(long containerId)
        {
            var vm = new LabFacelessTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;

            if (!baseData.TicketId.HasValue)
            {
                return vm;
            }
            var labFacelessOpData =
                GetLabFacelessLastOpData(baseData.TicketId.Value, Dom.OpDataState.Collision)
                ?? GetLabFacelessLastOpData(baseData.TicketId.Value, Dom.OpDataState.CollisionApproved)
                ?? GetLabFacelessLastOpData(baseData.TicketId.Value, Dom.OpDataState.CollisionDisapproved);
            if (labFacelessOpData != null)
            {
                vm.Comment = labFacelessOpData.Comment?.Length > 60
                    ? $"{labFacelessOpData.Comment.Substring(0, 50)}..."
                    : labFacelessOpData.Comment;

                vm.State = _context.OpDataStates.First(x=> x.Id == labFacelessOpData.StateId).Name;
                vm.IsReadyToManage = labFacelessOpData.StateId == Dom.OpDataState.CollisionApproved
                                     || labFacelessOpData.StateId == Dom.OpDataState.CollisionDisapproved;
            }

            return vm;
        }

        private SingleWindowTicketContainerItemVm GetSingleWindowTicketContainerItemVm(long containerId)
        {
            var vm = new SingleWindowTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId, true);
            vm.BaseData = baseData;

            var opData = _opDataRepository.GetLastOpData(baseData.TicketId, Dom.OpDataState.Processed);
            if (opData?.Node.Name is string processedNodeName) vm.NodeName = processedNodeName;

            vm.TruckState = Dom.TruckState.NotRegistered;
            var queueRegister =
                _opDataRepository.GetSingleOrDefault<QueueRegister, long>(q => q.TicketContainerId == containerId);
            if (queueRegister != null)
            {
                vm.TruckState = Dom.TruckState.Registered;
                if (queueRegister.IsAllowedToEnterTerritory) vm.TruckState = Dom.TruckState.AllowToEnter;
                if (queueRegister.SMSTimeAllowed.HasValue
                    && queueRegister.SMSTimeAllowed.Value.AddMinutes(_settings.QueueEntranceTimeout) < DateTime.Now)
                    vm.TruckState = Dom.TruckState.MissedEntrance;
            }

            return vm;
        }

        private UnloadGuideTicketContainerItemVm GetUnloadGuideTicketContainerItemVm(long containerId)
        {
            var vm = new UnloadGuideTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;

            var labFacelessOpData = _opDataRepository.GetLastProcessed<LabFacelessOpData>(baseData.TicketId);
            if (labFacelessOpData != null)
            {
                vm.HumidityValue = labFacelessOpData.HumidityValue;
                vm.ImpurityValue = labFacelessOpData.ImpurityValue;
                vm.EffectiveValue = labFacelessOpData.EffectiveValue;
                vm.IsActive = labFacelessOpData.StateId == Dom.OpDataState.Processed;
            }

            var unloadGuideData = _opDataRepository.GetLastOpData<UnloadGuideOpData>(baseData.TicketId, null);
            if (unloadGuideData != null)
            {
                var node = _context.Nodes.FirstOrDefault(x => x.Id == unloadGuideData.UnloadPointNodeId);

                vm.UnloadNodeId = unloadGuideData.UnloadPointNodeId;
                vm.UnloadNodeName = node != null ? node.Name : string.Empty;
            }

            return vm;
        }

        private UnloadGuideTicketContainerItemVm GetRejectedUnloadGuideTicketContainerItemVm(long containerId)
        {
            var vm = new UnloadGuideTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;

            var unloadGuideData = GetUnloadGuideOpDataLastOpData(baseData.TicketId, null);
            var unloadPointData = GetUnloadPointOpDataLastOpData(baseData.TicketId, null);
            if (unloadGuideData != null && (unloadPointData == null || unloadGuideData?.CheckOutDateTime > unloadPointData.CheckOutDateTime))
            {
                var node = _context.Nodes.FirstOrDefault(x => x.Id == unloadGuideData.UnloadPointNodeId);

                vm.UnloadNodeId = unloadGuideData.UnloadPointNodeId;
                vm.UnloadNodeName = node != null ? node.Name : string.Empty;
            }

            var singleWindowOpData = GetSingleWindowOpDataLastOpData(baseData.TicketId, null);

            vm.LoadTarget = singleWindowOpData.LoadTarget;
            vm.LoadTargetDeviationMinus = singleWindowOpData.LoadTargetDeviationMinus;
            vm.LoadTargetDeviationPlus = singleWindowOpData.LoadTargetDeviationPlus;

            var ticket = _ticketRepository.GetTicketInContainer(containerId, Dom.Ticket.Status.Processing);
            if (ticket == null) return vm;

            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(ticket.Id);
            if (partLoadValue.HasValue) vm.LoadTarget = partLoadValue.Value;

            return vm;
        }

        private LoadGuideTicketContainerItemVm GetRejectedLoadGuideTicketContainerItemVm(long containerId)
        {
            var vm = new LoadGuideTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;

            var loadGuideData = GetLoadGuideOpDataLastOpData(baseData.TicketId, null);
            var loadPointData = GetLoadPointOpDataLastOpData(baseData.TicketId, null);

            if (loadPointData == null || loadGuideData?.CheckOutDateTime > loadPointData.CheckOutDateTime)
            {
                var node = _context.Nodes.FirstOrDefault(x => x.Id == loadGuideData.LoadPointNodeId);

                vm.LoadNodeId = loadGuideData.LoadPointNodeId;
                vm.LoadNodeName = node != null ? node.Name : string.Empty;
            }

            var singleWindowOpData = GetSingleWindowOpDataLastOpData(baseData.TicketId, null);

            vm.LoadTarget = singleWindowOpData.LoadTarget;
            vm.LoadTargetDeviationMinus = singleWindowOpData.LoadTargetDeviationMinus;
            vm.LoadTargetDeviationPlus = singleWindowOpData.LoadTargetDeviationPlus;

            var ticket = _ticketRepository.GetTicketInContainer(containerId, Dom.Ticket.Status.Processing);
            if (ticket == null) return vm;

            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(ticket.Id);
            if (partLoadValue.HasValue) vm.LoadTarget = partLoadValue.Value;

            return vm;
        }

        private readonly long[] _statuses = {
            Dom.Ticket.Status.Processing, 
            Dom.Ticket.Status.ToBeProcessed, 
            Dom.Ticket.Status.Completed, 
            Dom.Ticket.Status.Closed, 
            Dom.Ticket.Status.New
        };

        private BaseRegistryData GetBaseRegistryDataByContainer(long containerId, bool showAdditionalProductName = false)
        {
            var vm = new BaseRegistryData
            {
                CardNumber = _cardRepository.GetContainerCardNo(containerId)
            };

            var tickets = _context.Tickets.Where(x => x.ContainerId == containerId).ToList();
            Ticket ticket = null;
            foreach (var status in _statuses)
            {
                ticket = tickets.FirstOrDefault(x => x.StatusId == status);
                if (ticket != null)
                {
                    break;
                }
            }
            
            if (ticket == null) return vm;
            vm.TicketStatus = ticket.StatusId;
            vm.TicketId = ticket.Id;
            vm.TicketContainerId = containerId;

            var singleWindowOpData =
                (from singleOpData in _context.SingleWindowOpDatas
                    join p in _context.Products on singleOpData.ProductId ?? string.Empty equals p.Id into productJoin
                    join o in _context.Organisations on singleOpData.OrganizationId ?? string.Empty equals o.Id into organizationJoin
                    join s in _context.Stocks on singleOpData.ReceiverDepotId ?? string.Empty equals s.Id into stockJoin
                    join p in _context.Partners on singleOpData.CarrierId ?? string.Empty equals p.Id into partnerJoin
                    from product in productJoin.DefaultIfEmpty()
                    from organization in organizationJoin.DefaultIfEmpty()
                    from stock in stockJoin.DefaultIfEmpty()
                    from partner in partnerJoin.DefaultIfEmpty()
                    where singleOpData.TicketId == ticket.Id
                    select new
                    {
                        singleOpData.CheckInDateTime,
                        Stock = stock != null ? stock.ShortName : string.Empty,
                        singleOpData.LoadTarget,
                        Product = product != null ? product.ShortName : (showAdditionalProductName ? singleOpData.ProductTitle : string.Empty),
                        singleOpData.IsThirdPartyCarrier,
                        Organization = organization != null ? organization.ShortName : singleOpData.CustomPartnerName,
                        singleOpData.CustomPartnerName,
                        singleOpData.Comments,
                        singleOpData.PredictionEntranceTime,
                        Partner = partner != null ? partner.ShortName : singleOpData.CustomPartnerName,
                        singleOpData.HiredTransportNumber,
                        singleOpData.HiredTrailerNumber,
                        singleOpData.TransportId,
                        singleOpData.TrailerId,
                        singleOpData.DeliveryBillCode
                    })
                .FirstOrDefault();

            if (singleWindowOpData != null)
            {
                vm.CheckInDateTime = singleWindowOpData.CheckInDateTime ?? DateTime.Now;
                vm.ReceiverDepotName = singleWindowOpData.Stock;
                vm.LoadTarget = singleWindowOpData.LoadTarget;
                vm.ProductName = singleWindowOpData.Product;
                vm.IsThirdPartyCarrier = singleWindowOpData.IsThirdPartyCarrier;
                vm.SenderName = singleWindowOpData.Organization;
                vm.SingleWindowComment = singleWindowOpData.Comments;
                vm.PredictionEntranceTime = singleWindowOpData.PredictionEntranceTime;
                vm.PartnerName = singleWindowOpData.Partner;
                vm.DeliveryBillCode = singleWindowOpData.DeliveryBillCode;

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

            return vm;
        }

        private LabFacelessOpData GetLabFacelessLastOpData(long? ticketId, int? stateId)
        {
            if (ticketId == null) return null;
            return _context.LabFacelessOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private SingleWindowOpData GetSingleWindowOpDataLastOpData(long? ticketId, int? stateId)
        {
            if (ticketId == null) return null;
            return _context.SingleWindowOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private ScaleOpData GetScaleOpDataLastOpData(long? ticketId, int? stateId)
        {
            if (ticketId == null) return null;
            return _context.ScaleOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private MixedFeedGuideOpData GetMixedFeedGuideOpDataLastOpData(long? ticketId, int? stateId)
        {
            if (ticketId == null) return null;
            return _context.MixedFeedGuideOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private LoadGuideOpData GetLoadGuideOpDataLastOpData(long? ticketId, int? stateId)
        {
            if (ticketId == null) return null;
            return _context.LoadGuideOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private LoadPointOpData GetLoadPointOpDataLastOpData(long? ticketId, int? stateId)
        {
            if (ticketId == null) return null;
            return _context.LoadPointOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private UnloadGuideOpData GetUnloadGuideOpDataLastOpData(long? ticketId, int? stateId)
        {
            if (ticketId == null) return null;
            return _context.UnloadGuideOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private MixedFeedLoadOpData GetMixedFeedLoadOpDataLastOpData(long? ticketId, int? stateId)
        {
            if (ticketId == null) return null;
            return _context.MixedFeedLoadOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private UnloadPointOpData GetUnloadPointOpDataLastOpData(long? ticketId, int? stateId)
        {
            if (ticketId == null) return null;
            return _context.UnloadPointOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
    }

    public class BaseRegistryData
    {
        public long? TicketId { get; set; }
        public long? TicketStatus { get; set; }

        [DisplayName("Id")]
        public long TicketContainerId { get; set; }

        [DisplayName("Картка")]
        public string CardNumber { get; set; }

        [DisplayName("Продукт")]
        public string ProductName { get; set; }

        [DisplayName("Транспорт")]
        public string TransportNo { get; set; }

        [DisplayName("Причеп")]
        public string TrailerNo { get; set; }

        [DisplayName("Отримувач")]
        public string SenderName { get; set; }

        [DisplayName("Перевізник")]
        public bool IsThirdPartyCarrier { get; set; }

        [DisplayName("Коментар Єдиного вікна")]
        public string SingleWindowComment { get; set; }

        [DisplayName("Погрузити, кг.")]
        public double LoadTarget { get; set; }

        [DisplayName("Склад контрагента")]
        public string ReceiverDepotName { get; set; }

        [DisplayName("Орієнтовний час в'їзду")]
        public DateTime? PredictionEntranceTime { get; set; }

        [DisplayName("Перевізник")]
        public string PartnerName { get; set; }

        public DateTime CheckInDateTime { get; set; }
        public string DeliveryBillCode { get; set; }
    }
}
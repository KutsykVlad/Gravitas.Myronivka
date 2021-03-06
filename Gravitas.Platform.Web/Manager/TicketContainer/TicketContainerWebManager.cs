using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.Card;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Ticket;
using Gravitas.Infrastructure.Common.Helper;
using Gravitas.Infrastructure.Platform.Manager.CentralLaboratory;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Infrastructure.Platform.Manager.Scale;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.TicketContainer.List;
using TicketStatus = Gravitas.Model.DomainValue.TicketStatus;

namespace Gravitas.Platform.Web.Manager.TicketContainer
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

        public IEnumerable<int> GetActiveTicketContainers(List<int> inputNodeIds)
        {
            var tickets = 
                (from card in _context.Cards
                    join ticket in _context.Tickets on card.TicketContainerId equals ticket.TicketContainerId 
                    where card.TicketContainerId.HasValue
                          && card.TypeId == CardType.TicketCard
                          && (ticket.StatusId == TicketStatus.Processing
                              || ticket.StatusId == TicketStatus.ToBeProcessed)
                    select ticket)
                .GroupBy(x => x.TicketContainerId)
                .Select(x => x.OrderByDescending(z => z.StatusId)
                    .FirstOrDefault())
                .ToList();

            if (inputNodeIds == null
                || !inputNodeIds.Any())
                return tickets.Select(x => x.TicketContainerId);

            var resultContainers = new List<int>();

            foreach (var ticket in tickets)
            {
                if (ticket?.RouteTemplateId == null) continue;

                var isNodeInTicketRoute = inputNodeIds.Any(x =>
                    _routesInfrastructure.IsNodeAvailable(x, ticket.SecondaryRouteTemplateId ?? ticket.RouteTemplateId.Value));
                if (!isNodeInTicketRoute) continue;
                resultContainers.Add(ticket.TicketContainerId);
            }

            return resultContainers;
        }

        public ICollection<UnloadGuideTicketContainerItemVm> GetUnloadGuideTicketContainerItemsVm(IEnumerable<int> containerIds)
        {
            return containerIds.Select(GetUnloadGuideTicketContainerItemVm)
                .OrderByDescending(item => item.IsActive)
                .ThenByDescending(item => item.UnloadNodeId != 0)
                .ToList();
        }

        public ICollection<SingleWindowTicketContainerItemVm> GetSingleWindowQueueTicketContainerItemsVm(IEnumerable<int> containerIds)
        {
            return containerIds.Select(GetSingleWindowTicketContainerItemVm)
                .OrderBy(x => x.BaseData.CheckInDateTime)
                .ToList();
        }
        
        public ICollection<SingleWindowTicketContainerItemVm> GetSingleWindowInProgressTicketContainerItemsVm(IEnumerable<int> containerIds)
        {
            return containerIds.Select(GetSingleWindowTicketContainerItemVm)
                .OrderBy(x => x.BaseData.CheckInDateTime)
                .ToList();
        }
        
        public ICollection<SingleWindowTicketContainerItemVm> GetSingleWindowProcessedTicketContainerItemsVm(IEnumerable<int> containerIds)
        {
            return containerIds.Select(GetSingleWindowTicketContainerItemVm)
                .OrderBy(x => x.BaseData.CheckInDateTime)
                .ToList();
        }

        public ICollection<UnloadQueueTicketContainerItemVm> GetUnloadQueueTicketContainerItemsVm()
        {
            var vm =
                (from singleOpData in _context.SingleWindowOpDatas
                    join p in _context.Products on singleOpData.ProductId equals p.Id into productJoin
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

        public ICollection<UnloadPointTicketContainerItemVm> GetUnloadPointTicketContainerItemsVm(IEnumerable<int> containerIds, int nodeId)
        {
            var result = new List<UnloadPointTicketContainerItemVm>();
            foreach (var containerId in containerIds)
            {
                var ticket = _ticketRepository.GetTicketInContainer(containerId, TicketStatus.Processing)
                             ?? _ticketRepository.GetTicketInContainer(containerId, TicketStatus.ToBeProcessed);
                if (ticket == null) continue;
                var unloadGuideOpData = GetUnloadGuideOpDataLastOpData(ticket.Id, null);
                var unloadPointOpData = GetUnloadPointOpDataLastOpData(ticket.Id, null);
                if (unloadGuideOpData?.UnloadPointNodeId == nodeId &&
                    (unloadPointOpData == null
                     || unloadPointOpData.StateId != OpDataState.Processed
                     || unloadGuideOpData.CheckOutDateTime > unloadPointOpData.CheckOutDateTime))
                    result.Add(GetUnloadPointTicketContainerItemVm(containerId));
            }

            return result;
        }

        public List<LoadGuideTicketContainerItemVm> GetLoadGuideTicketContainerItemsVm(IEnumerable<int> containerIds)
        {
            return containerIds.Select(GetLoadGuideTicketContainerItemVm).ToList();
        }

        public ICollection<LoadPointTicketContainerItemVm> GetLoadPointTicketContainerItemsVm(IEnumerable<int> containerIds, int nodeId)
        {
            var result = new List<LoadPointTicketContainerItemVm>();
            foreach (var containerId in containerIds)
            {
                var ticket = _ticketRepository.GetTicketInContainer(containerId, TicketStatus.Processing)
                             ?? _ticketRepository.GetTicketInContainer(containerId, TicketStatus.ToBeProcessed);
                if (ticket == null) continue;
                var loadGuideOpData = GetLoadGuideOpDataLastOpData(ticket.Id, null);
                var loadPointOpData = GetLoadPointOpDataLastOpData(ticket.Id, null);
                if (loadGuideOpData?.LoadPointNodeId == nodeId
                    && (loadPointOpData == null
                        || loadPointOpData.StateId != OpDataState.Processed
                        || loadGuideOpData.CheckOutDateTime > loadPointOpData.CheckOutDateTime))
                    result.Add(GetLoadPointTicketContainerItemVm(containerId));
            }

            return result;
        }

        public ICollection<LoadGuideTicketContainerItemVm> GetRejectedLoadGuideTicketContainerItemsVm(IEnumerable<int> containerIds)
        {
            return containerIds.Select(GetRejectedLoadGuideTicketContainerItemVm).ToList();
        }

        public ICollection<UnloadGuideTicketContainerItemVm> GetRejectedUnloadGuideTicketContainerItemsVm(IEnumerable<int> containerIds)
        {
            return containerIds.Select(GetRejectedUnloadGuideTicketContainerItemVm)
                .OrderByDescending(item => item.IsActive)
                .ThenByDescending(item => item.UnloadNodeId != 0)
                .ToList();
        }

        public ICollection<MixedFeedGuideTicketContainerItemVm> GetMixedFeedGuideTicketContainerItemsVm(IEnumerable<int> containerIds)
        {
            var items = containerIds.Where(item =>
                {
                    var ticket = _ticketRepository.GetTicketInContainer(item, TicketStatus.Processing) ??
                                 _ticketRepository.GetTicketInContainer(item, TicketStatus.ToBeProcessed);
                    if (ticket == null) return false;

                    var opData = GetLoadPointOpDataLastOpData(ticket.Id,null);

                    return opData == null;
                })
                .Select(GetMixedFeedGuideTicketContainerItemVm)
                .ToList();
            return items;
        }

        public ICollection<MixedFeedLoadTicketContainerItemVm> GetMixedFeedLoadTicketContainerItemsVm(IEnumerable<int> containerIds, int nodeId)
        {
            var result = new List<MixedFeedLoadTicketContainerItemVm>();
            foreach (var containerId in containerIds)
            {
                var ticket = _ticketRepository.GetTicketInContainer(containerId, TicketStatus.Processing)
                             ?? _ticketRepository.GetTicketInContainer(containerId, TicketStatus.ToBeProcessed);
                if (ticket == null) continue;
                var loadGuideOpData = GetLoadGuideOpDataLastOpData(ticket.Id, null);
                var loadPointOpData = GetLoadPointOpDataLastOpData(ticket.Id, null);
                if (loadPointOpData?.CheckOutDateTime < loadGuideOpData?.CheckOutDateTime) loadPointOpData = null;
                if (loadGuideOpData?.LoadPointNodeId == nodeId && (loadPointOpData == null || loadPointOpData.StateId != OpDataState.Processed))
                    result.Add(GetMixedFeedLoadTicketContainerItemVm(containerId));
            }

            return result;
        }

        public ICollection<MixedFeedGuideTicketContainerItemVm> GetRejectedMixedFeedLoadTicketContainerItemsVm(IEnumerable<int> containerIds)
        {
            return containerIds.Select(GetRejectedMixedFeedGuideTicketContainerItemVm).ToList();
        }

        public ICollection<MixedFeedUnloadTicketContainerItemVm> GetRejectedMixedFeedUnloadTicketContainerItemsVm(IEnumerable<int> containerIds)
        {
            return containerIds.Select(GetRejectedMixedFeedUnloadTicketContainerItemVm).ToList();
        }

        public ICollection<CentralLabTicketContainerItemVm> GetCentralLabTicketContainerListVm(IEnumerable<int> containerIds)
        {
            var items = containerIds.Where(item =>
                {
                    var ticket = _ticketRepository.GetTicketInContainer(item, TicketStatus.Processing)
                                 ?? _ticketRepository.GetTicketInContainer(item, TicketStatus.ToBeProcessed);
                    if (ticket == null) return false;

                    var opData = _context.CentralLabOpDatas.Where(x => x.TicketId == ticket.Id)
                        .OrderByDescending(x => x.SampleCheckInDateTime)
                        .FirstOrDefault();
                    return opData == null
                           || opData.StateId != OpDataState.Processed && opData.StateId != OpDataState.Rejected;
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
                             TicketStatus.Processing)
                         ?? _ticketRepository.GetTicketInContainer(containerId,
                             TicketStatus.ToBeProcessed)
                         ?? _ticketRepository.GetTicketInContainer(containerId, TicketStatus.New)
                        )?.Id;

                    var labOpData =
                        _context.LabFacelessOpDatas.Where(x => x.TicketId == ticketId)
                            .OrderByDescending(x => x.CheckInDateTime)
                            .FirstOrDefault();

                    return labOpData != null && (labOpData.StateId == OpDataState.Collision
                                                 || labOpData.StateId ==
                                                 OpDataState.CollisionApproved
                                                 || labOpData.StateId ==
                                                 OpDataState.CollisionDisapproved);
                })
                .ToList();

            return containerIds.Select(GetLabFacelessTicketContainerItemVm).ToList();
        }

        public ICollection<LabFacelessTicketContainerItemVm> GetSelfServiceLabTicketContainerItemsVm()
        {
            var containerIds = GetActiveTicketContainers()
                .Where(containerId =>
                {
                    var ticketId = _ticketRepository.GetTicketInContainer(containerId, TicketStatus.Processing)?.Id;
                    var labOpData = GetLabFacelessLastOpData(ticketId, null);

                    return labOpData != null && (labOpData.StateId == OpDataState.Processed
                                                 || labOpData.StateId == OpDataState.Canceled
                                                 || labOpData.StateId == OpDataState.Rejected);
                })
                .Select(item => item)
                .ToList();

            return containerIds.Select(GetLabFacelessTicketContainerItemVm).ToList();
        }

        private CentralLabTicketContainerItemVm GetCentralLabTicketContainerItemVm(int containerId)
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

        private MixedFeedGuideTicketContainerItemVm GetMixedFeedGuideTicketContainerItemVm(int containerId)
        {
            var vm = new MixedFeedGuideTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;

            if (!baseData.TicketId.HasValue) return vm;
            var mixedFeedGuideOpData = _opDataRepository.GetLastProcessed<LoadGuideOpData>(baseData.TicketId);
            if (mixedFeedGuideOpData != null)
            {
                var node = _context.Nodes.FirstOrDefault(x => x.Id == mixedFeedGuideOpData.LoadPointNodeId);

                vm.LoadGateId = mixedFeedGuideOpData.LoadPointNodeId;
                vm.LoadNodeName = node != null ? node.Name : string.Empty;
            }

            var ticket = _context.Tickets.First(x => x.Id == baseData.TicketId.Value);

            var opData = _opDataRepository.GetLastOpData(baseData.TicketId, OpDataState.Processed);
            if (opData?.Node.Name is string processedNodeName)
            {
                vm.LastNodeName = processedNodeName;

                var queueRegisterData = _opDataRepository
                    .GetSingleOrDefault<QueueRegister, int>(t => t.TicketContainerId == containerId);
                vm.CanInvite = (queueRegisterData == null || !queueRegisterData.IsAllowedToEnterTerritory) && ticket.RouteItemIndex == 1;
            }

            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(ticket.Id);
            if (partLoadValue.HasValue) vm.BaseData.LoadTarget = partLoadValue.Value;

            vm.IsActive = ticket.RouteTemplateId.HasValue;
            return vm;
        }

        private MixedFeedGuideTicketContainerItemVm GetRejectedMixedFeedGuideTicketContainerItemVm(int containerId)
        {
            var vm = new MixedFeedGuideTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;

            if (!baseData.TicketId.HasValue) return vm;
            var mixedFeedGuideOpData = GetLoadGuideOpDataLastOpData(baseData.TicketId, null);
            var scaleOpData = GetScaleOpDataLastOpData(baseData.TicketId, null);
            if (mixedFeedGuideOpData != null && mixedFeedGuideOpData.CheckOutDateTime > scaleOpData?.CheckOutDateTime)
            {
                var node = _context.Nodes.FirstOrDefault(x => x.Id == mixedFeedGuideOpData.LoadPointNodeId);

                vm.LoadGateId = mixedFeedGuideOpData.LoadPointNodeId;
                vm.LoadNodeName = node != null ? node.Name : string.Empty;
            }

            var ticket = _context.Tickets.First(x => x.Id == baseData.TicketId.Value);

            var opData = _opDataRepository.GetLastOpData(baseData.TicketId, OpDataState.Processed);
            if (opData?.Node.Name is string processedNodeName)
            {
                vm.LastNodeName = processedNodeName;

                var queueRegisterData = _opDataRepository
                    .GetSingleOrDefault<QueueRegister, int>(t => t.TicketContainerId == containerId);
                vm.CanInvite = (queueRegisterData == null || !queueRegisterData.IsAllowedToEnterTerritory) && ticket.RouteItemIndex == 1;
            }

            vm.IsActive = ticket.RouteTemplateId.HasValue;

            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(baseData.TicketId.Value);
            if (partLoadValue.HasValue) vm.BaseData.LoadTarget = partLoadValue.Value;

            return vm;
        }

        private MixedFeedUnloadTicketContainerItemVm GetRejectedMixedFeedUnloadTicketContainerItemVm(int containerId)
        {
            var vm = new MixedFeedUnloadTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;
            vm.LoadTarget = baseData.LoadTarget;

            if (!baseData.TicketId.HasValue) return vm;

            var opData = _opDataRepository.GetLastOpData(baseData.TicketId, OpDataState.Processed);
            if (opData?.Node.Name is string processedNodeName) vm.LastNodeName = processedNodeName;

            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(baseData.TicketId.Value);
            if (partLoadValue.HasValue) vm.LoadTarget = partLoadValue.Value;

            return vm;
        }

        private LoadPointTicketContainerItemVm GetLoadPointTicketContainerItemVm(int containerId)
        {
            var baseData = GetBaseRegistryDataByContainer(containerId);
            var vm = new LoadPointTicketContainerItemVm
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
                    join s in _context.Stocks on singleOpData.ReceiverId equals s.Id into stockJoin
                    join p in _context.Partners on singleOpData.ReceiverId equals p.Id into partnerJoin
                    join sub in _context.Subdivisions on singleOpData.ReceiverId equals sub.Id into subDivisionJoin
                    from subDivision in subDivisionJoin.DefaultIfEmpty()
                    from stock in stockJoin.DefaultIfEmpty()
                    from partner in partnerJoin.DefaultIfEmpty()
                    where singleOpData.TicketId == baseData.TicketId
                    select new
                    {
                        singleOpData.LoadTargetDeviationMinus,
                        singleOpData.LoadTargetDeviationPlus,
                        Receiver = singleOpData.ReceiverId != null && singleOpData.ReceiverId.HasValue
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

        private MixedFeedLoadTicketContainerItemVm GetMixedFeedLoadTicketContainerItemVm(int containerId)
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
                    join s in _context.Stocks on singleOpData.ReceiverId equals s.Id into stockJoin
                    join p in _context.Partners on singleOpData.ReceiverId equals p.Id into partnerJoin
                    join sub in _context.Subdivisions on singleOpData.ReceiverId equals sub.Id into subDivisionJoin
                    from subDivision in subDivisionJoin.DefaultIfEmpty()
                    from stock in stockJoin.DefaultIfEmpty()
                    from partner in partnerJoin.DefaultIfEmpty()
                    where singleOpData.TicketId == baseData.TicketId
                    select new
                    {
                        singleOpData.LoadTargetDeviationMinus,
                        singleOpData.LoadTargetDeviationPlus,
                        Receiver = singleOpData.ReceiverId != null && singleOpData.ReceiverId.HasValue
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

        private UnloadPointTicketContainerItemVm GetUnloadPointTicketContainerItemVm(int containerId)
        {
            var baseData = GetBaseRegistryDataByContainer(containerId);
            var vm = new UnloadPointTicketContainerItemVm
            {
                CardNumber = _cardRepository.GetContainerCardNo(containerId)
            };

            var scaleOpData = _context.ScaleOpDatas.Where(x => x.TicketId == baseData.TicketId && x.TypeId == ScaleOpDataType.Gross)
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

        private LoadGuideTicketContainerItemVm GetLoadGuideTicketContainerItemVm(int containerId)
        {
            var vm = new LoadGuideTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;

            if (!baseData.TicketId.HasValue) return vm;
            var ticket = _context.Tickets.First(x => x.Id == baseData.TicketId);

            var singleWindowOpData =
                (from singleOpData in _context.SingleWindowOpDatas
                    join s in _context.Stocks on singleOpData.ReceiverId equals s.Id into stockJoin
                    join p in _context.Partners on singleOpData.ReceiverId equals p.Id into partnerJoin
                    join sub in _context.Subdivisions on singleOpData.ReceiverId equals sub.Id into subDivisionJoin
                    from subDivision in subDivisionJoin.DefaultIfEmpty()
                    from stock in stockJoin.DefaultIfEmpty()
                    from partner in partnerJoin.DefaultIfEmpty()
                    where singleOpData.TicketId == ticket.Id
                    select new
                    {
                        singleOpData.LoadTargetDeviationMinus,
                        singleOpData.LoadTargetDeviationPlus,
                        Sender = singleOpData.ReceiverId != null && singleOpData.ReceiverId.HasValue
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

            var queueRegisterData = _opDataRepository.GetSingleOrDefault<QueueRegister, int>(t => t.TicketContainerId == containerId);
            vm.CanInvite = (queueRegisterData == null || !queueRegisterData.IsAllowedToEnterTerritory) && ticket.RouteItemIndex == 1;

            vm.IsActive = ticket.RouteTemplateId.HasValue;
            return vm;
        }

        private IEnumerable<int> GetActiveTicketContainers()
        {
            var r = (from card in _context.Cards
                    where card.TicketContainerId.HasValue && card.TypeId == CardType.TicketCard
                    select card.TicketContainerId.Value)
                .ToList();
            return r;
        }

        private LabFacelessTicketContainerItemVm GetLabFacelessTicketContainerItemVm(int containerId)
        {
            var vm = new LabFacelessTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId);
            vm.BaseData = baseData;

            if (!baseData.TicketId.HasValue)
            {
                return vm;
            }
            var labFacelessOpData =
                GetLabFacelessLastOpData(baseData.TicketId.Value, OpDataState.Collision)
                ?? GetLabFacelessLastOpData(baseData.TicketId.Value, OpDataState.CollisionApproved)
                ?? GetLabFacelessLastOpData(baseData.TicketId.Value, OpDataState.CollisionDisapproved);
            if (labFacelessOpData != null)
            {
                vm.Comment = labFacelessOpData.Comment?.Length > 60
                    ? $"{labFacelessOpData.Comment.Substring(0, 50)}..."
                    : labFacelessOpData.Comment;

                vm.State = labFacelessOpData.StateId.GetDescription();
                vm.IsReadyToManage = labFacelessOpData.StateId == OpDataState.CollisionApproved
                                     || labFacelessOpData.StateId == OpDataState.CollisionDisapproved;
            }

            return vm;
        }

        private SingleWindowTicketContainerItemVm GetSingleWindowTicketContainerItemVm(int containerId)
        {
            var vm = new SingleWindowTicketContainerItemVm();
            var baseData = GetBaseRegistryDataByContainer(containerId, true);
            vm.BaseData = baseData;

            var opData = _opDataRepository.GetLastOpData(baseData.TicketId, OpDataState.Processed);
            if (opData?.Node.Name is string processedNodeName) vm.NodeName = processedNodeName;

            vm.TruckState = TruckState.NotRegistered;
            var queueRegister =
                _opDataRepository.GetSingleOrDefault<QueueRegister, int>(q => q.TicketContainerId == containerId);
            if (queueRegister != null)
            {
                vm.TruckState = TruckState.Registered;
                if (queueRegister.IsAllowedToEnterTerritory) vm.TruckState = TruckState.AllowToEnter;
                if (queueRegister.SMSTimeAllowed.HasValue
                    && queueRegister.SMSTimeAllowed.Value.AddMinutes(_settings.QueueEntranceTimeout) < DateTime.Now)
                    vm.TruckState = TruckState.MissedEntrance;
            }

            return vm;
        }

        private UnloadGuideTicketContainerItemVm GetUnloadGuideTicketContainerItemVm(int containerId)
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
                vm.IsActive = labFacelessOpData.StateId == OpDataState.Processed;
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

        private UnloadGuideTicketContainerItemVm GetRejectedUnloadGuideTicketContainerItemVm(int containerId)
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

            var ticket = _ticketRepository.GetTicketInContainer(containerId, TicketStatus.Processing);
            if (ticket == null) return vm;

            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(ticket.Id);
            if (partLoadValue.HasValue) vm.LoadTarget = partLoadValue.Value;

            return vm;
        }

        private LoadGuideTicketContainerItemVm GetRejectedLoadGuideTicketContainerItemVm(int containerId)
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

            var ticket = _ticketRepository.GetTicketInContainer(containerId, TicketStatus.Processing);
            if (ticket == null) return vm;

            var partLoadValue = _scaleManager.GetPartLoadUnloadValue(ticket.Id);
            if (partLoadValue.HasValue) vm.LoadTarget = partLoadValue.Value;

            return vm;
        }

        private readonly TicketStatus[] _statuses = {
            TicketStatus.Processing, 
            TicketStatus.ToBeProcessed, 
            TicketStatus.Completed, 
            TicketStatus.Closed, 
            TicketStatus.New
        };

        private BaseRegistryData GetBaseRegistryDataByContainer(int containerId, bool showAdditionalProductName = false)
        {
            var vm = new BaseRegistryData
            {
                CardNumber = _cardRepository.GetContainerCardNo(containerId)
            };

            var tickets = _context.Tickets.Where(x => x.TicketContainerId == containerId).ToList();
            Model.DomainModel.Ticket.DAO.Ticket ticket = null;
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
            vm.RouteTemplateId = ticket.RouteTemplateId ?? 0;
            vm.RouteName = ticket.RouteTemplate?.Name;

            var singleWindowOpData =
                (from singleOpData in _context.SingleWindowOpDatas
                    join p in _context.Products on singleOpData.ProductId equals p.Id into productJoin
                    join o in _context.Organisations on singleOpData.OrganizationId equals o.Id into organizationJoin
                    join s in _context.Stocks on singleOpData.ReceiverDepotId equals s.Id into stockJoin
                    join p in _context.Partners on singleOpData.CarrierId equals p.Id into partnerJoin
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
                        Partner = partner != null ? partner.ShortName : singleOpData.CustomPartnerName,
                        singleOpData.HiredTransportNumber,
                        singleOpData.HiredTrailerNumber,
                        singleOpData.TransportId,
                        singleOpData.TrailerId,
                        singleOpData.DeliveryBillCode,
                        singleOpData.DocumentTypeId
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
                vm.PartnerName = singleWindowOpData.Partner;
                vm.DeliveryBillCode = singleWindowOpData.DeliveryBillCode;
                vm.DocumentType = singleWindowOpData.DocumentTypeId;

                if (singleWindowOpData.IsThirdPartyCarrier)
                {
                    vm.TransportNo = singleWindowOpData.HiredTransportNumber;
                    vm.TrailerNo = singleWindowOpData.HiredTrailerNumber;
                }
                else
                {
                    vm.TransportNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId.Value)?.RegistrationNo ?? string.Empty;
                    vm.TrailerNo = _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId.Value)?.RegistrationNo ?? string.Empty;
                }
            }

            return vm;
        }

        private LabFacelessOpData GetLabFacelessLastOpData(int? ticketId, OpDataState? stateId)
        {
            if (ticketId == null) return null;
            return _context.LabFacelessOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private SingleWindowOpData GetSingleWindowOpDataLastOpData(int? ticketId, OpDataState? stateId)
        {
            if (ticketId == null) return null;
            return _context.SingleWindowOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private ScaleOpData GetScaleOpDataLastOpData(int? ticketId, OpDataState? stateId)
        {
            if (ticketId == null) return null;
            return _context.ScaleOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }

        private LoadGuideOpData GetLoadGuideOpDataLastOpData(int? ticketId, OpDataState? stateId)
        {
            if (ticketId == null) return null;
            return _context.LoadGuideOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private LoadPointOpData GetLoadPointOpDataLastOpData(int? ticketId, OpDataState? stateId)
        {
            if (ticketId == null) return null;
            return _context.LoadPointOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }
        
        private UnloadGuideOpData GetUnloadGuideOpDataLastOpData(int? ticketId, OpDataState? stateId)
        {
            if (ticketId == null) return null;
            return _context.UnloadGuideOpDatas
                .Where(x => x.TicketId == ticketId && (!stateId.HasValue || x.StateId == stateId))
                .OrderByDescending(x => x.CheckInDateTime)
                .FirstOrDefault();
        }

        private UnloadPointOpData GetUnloadPointOpDataLastOpData(int? ticketId, OpDataState? stateId)
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
        public int? TicketId { get; set; }
        public TicketStatus? TicketStatus { get; set; }

        [DisplayName("Id")]
        public int TicketContainerId { get; set; }

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

        [DisplayName("Перевізник")]
        public string PartnerName { get; set; }
        
        public string RouteName { get; set; }
        
        public int RouteTemplateId { get; set; }

        public DateTime CheckInDateTime { get; set; }
        public string DeliveryBillCode { get; set; }
        
        [DisplayName("Операція")]
        public string DocumentType { get; set; }
    }
}
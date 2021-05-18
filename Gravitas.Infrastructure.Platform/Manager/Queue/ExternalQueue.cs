using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.Queue;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Model.DomainValue;
using NLog;
using NLog.Fluent;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public class CatItem
    {
        public List<RouteInfo> Items = new List<RouteInfo>();
        public int SpaceLeft { get; set; }
        public long IdPattern { get; set; }
    }

    public class ExternalQueue
    {
        private const int OwnerCategory = (int) QueueCategory.Company;
        private const int PartnersCategory = (int) QueueCategory.Partners;
        private const int OtherCategory = (int) QueueCategory.Others;
        private const int MixedFeedCategory = (int) QueueCategory.MixedFeedLoad;
        private const int PreRegisterCategory = (int) QueueCategory.PreRegisterCategory;
        private readonly IExternalDataRepository _externalRepo;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IOpDataRepository _opDataRepository;
        private readonly IQueryable<QueuePatternItem> _patterns;
        private readonly GravitasDbContext _context;
        private readonly Dictionary<long, List<CatItem>> _routesQueue = new Dictionary<long, List<CatItem>>();

        public ExternalQueue(IQueueSettingsRepository queueSettingsRepository,
            IOpDataRepository opDataRepository,
            IExternalDataRepository externalRepo, 
            GravitasDbContext context)
        {
            _opDataRepository = opDataRepository;
            _externalRepo = externalRepo;
            _context = context;
            _patterns = queueSettingsRepository.GetQueuePatternItems().OrderBy(p => p.PriorityId);
        }

        private int GetQueuePatternId(RouteInfo route)
        {
            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(route.ActiveTicketId);

            var category = OwnerCategory;

            if (singleWindowOpData.IsPreRegistered)
            {
                category = PreRegisterCategory;
            } else if (IsNodeAvailable((int) NodeIdValue.MixedFeedGuide, route.PathNodes))
            {
                category = MixedFeedCategory;
            } else if (singleWindowOpData != null && singleWindowOpData.IsThirdPartyCarrier)
            {
                if (!string.IsNullOrEmpty(singleWindowOpData.CarrierId))
                {
                    var partner = _externalRepo.GetPartnerItems().Items.FirstOrDefault(x => x.Id == singleWindowOpData.CarrierId);

                    if (partner != null)
                    {
                        var resPatternCategory = _patterns.FirstOrDefault(p =>
                            p.PartnerId == partner.Id && p.CategoryId == PartnersCategory);

                        if (resPatternCategory != null) return (int) resPatternCategory.Id;
                    }

                    category = OtherCategory;
                }
                else
                {
                    category = OtherCategory;
                }
            }

            var item = _patterns.FirstOrDefault(p => p.CategoryId == category);
            if (item == null) throw new Exception($"Не знайдено в БД категорії {category}");

            return (int) item.Id;
        }

        private void InsertNewPattern(long routeId)
        {
            foreach (var pattern in _patterns)
                _routesQueue[routeId].Add(new CatItem
                {
                    IdPattern = pattern.Id, SpaceLeft = pattern.Count
                });
        }

        public void Add(RouteInfo route)
        {
            _logger.Info($"Adding route to ticket container = {route.TicketContainerId}.");

            var routeId =
                _context.Tickets.Where(t =>
                        t.TicketContainerId == route.TicketContainerId &&
                        (t.StatusId == TicketStatus.Processing || t.StatusId == TicketStatus.ToBeProcessed))
                    .OrderBy(x => x.OrderNo)
                    .FirstOrDefault()?.RouteTemplateId;
            if (!routeId.HasValue)
            {
                Log.Error($"Can't add RouteInfo without RouteTemplateId to queue. TicketContainerID {route.TicketContainerId}");
                return;
            }

            if (!_routesQueue.TryGetValue(routeId.Value, out var routeQueue))
            {
                routeQueue = new List<CatItem>();
                _routesQueue.Add(routeId.Value, routeQueue);
            }

            var idPatternItem = GetQueuePatternId(route);

            var set = routeQueue.FirstOrDefault(item => item.IdPattern == idPatternItem && item.SpaceLeft > 0);
            if (set == null)
            {
                InsertNewPattern(routeId.Value);
                set = routeQueue.FirstOrDefault(item => item.IdPattern == idPatternItem && item.SpaceLeft > 0);
            }

            set?.Items.Add(route);
            if (set != null) set.SpaceLeft--;

            _logger.Info($"TicketContainer {route.TicketContainerId} added to route {routeId} queue.");
            PrintQueue(routeId.Value);
        }

        private void PrintQueue(long routeId)
        {
            var data = new StringBuilder($"ExternalQueue for Route = {routeId} (TicketContainers): ");
            var queue = GetQueue(routeId);
            if (queue.Any())
            {
                queue.ForEach(i => data.Append($"{i.TicketContainerId} "));
                _logger.Info($"{data}");
            }
        }
        
        public void PrintAll()
        {
            foreach (var routes in _routesQueue)
            {
                PrintQueue(routes.Key);
            }
        }

        public void Remove(RouteInfo route)
        {
            Remove(route.TicketContainerId);
        }

        public void Remove(int ticketContainerId)
        {
            var queue = GetQueueCatItems(ticketContainerId);
            var group = queue.FirstOrDefault(g => g.Items.Any(r => r.TicketContainerId == ticketContainerId));
            if (group == null) return;
            var route = group.Items.SingleOrDefault(r => r.TicketContainerId == ticketContainerId);
            group.Items.Remove(route);

            if (group.Items.Count == 0 && group.SpaceLeft == 0) queue.Remove(group);
        }

        public RouteInfo Get(int ticketContainerId)
        {
            var s = GetQueueCatItems(ticketContainerId).SingleOrDefault(q => q.Items.Any(r => r.TicketContainerId == ticketContainerId));
            return s?.Items.SingleOrDefault(r => r.TicketContainerId == ticketContainerId);
        }

        public List<RouteInfo> GetQueue(long routeId)
        {
            _routesQueue.TryGetValue(routeId, out var routeQueue);
            if (_routesQueue.Count == 0)
            {
                _routesQueue.Remove(routeId);
                routeQueue = null;
            }

            return routeQueue == null ? new List<RouteInfo>() : routeQueue.SelectMany(q => q.Items).ToList();
        }

        private List<CatItem> GetQueueCatItems(int ticketContainerId)
        {
            foreach (var queues in _routesQueue)
            {
                var s = queues.Value.SingleOrDefault(q => q.Items.Any(r => r.TicketContainerId == ticketContainerId));
                if (s == null) continue;
                return queues.Value;
            }

            return new List<CatItem>();
        }
        
        private bool IsNodeAvailable(int nodeId, List<List<int>> route)
        {
            foreach (var r in route)
                if (r.Contains(nodeId))
                    return true;

            return false;
        }
    }
}
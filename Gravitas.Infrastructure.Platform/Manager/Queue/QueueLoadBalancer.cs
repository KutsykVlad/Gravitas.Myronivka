using System;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public class QueueLoadBalancer
    {
        private readonly List<int> _endPointNodeLoad;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public Dictionary<long, NodeLoad> NodesLoad { get; } = new Dictionary<long, NodeLoad>();

        public QueueLoadBalancer(IEnumerable<Model.DomainModel.Node.DAO.Node> nodes, List<int> endPointNodeLoad)
        {
            _endPointNodeLoad = endPointNodeLoad;
            foreach (var node in nodes)
                NodesLoad.Add(node.Id,
                    new NodeLoad
                    {
                        MaxQuata = node.Quota, RoutesLoad = new List<NodeRouteLoad>()
                    });
        }

        public void UpdateQuatas(IEnumerable<Model.DomainModel.Node.DAO.Node> nodes)
        {
            foreach (var node in nodes) NodesLoad[node.Id].MaxQuata = node.Quota;
        }

        public bool IsPlace(RouteInfo route)
        {
            var routeNodeCount = 0;

            foreach (var alternativeNodes in route.GroupAlternativeNodes)
            {
                if (!alternativeNodes.QuotaEnabled) continue;

                var isFreeSpace = alternativeNodes.Nodes.All(nodeId =>
                {
                    var load = NodesLoad[nodeId].RoutesLoad.ToList();
                    var current = load.Sum(x => x.ArrivalProbability);
                    _logger.Trace($"Future load on Node {nodeId}: {current}");
                    return current < NodesLoad[nodeId].MaxQuata;
                });
                
                if (!isFreeSpace)
                {
                    _logger.Info(
                        $"No quata for TicketContainerId: {route.TicketContainerId}. " +
                        $"Current load for nodes {string.Join(",", alternativeNodes.Nodes)} is maximum" +
                        $" Current routes ticketContainersID {NodesLoad[alternativeNodes.Nodes[0]].NodeLoadToString(routeNodeCount)}");
                    return false;
                }

                routeNodeCount++;
            }

            _logger.Info($"There is quata for route with TicketContainerId: {route.TicketContainerId}. ");
            return true;
        }

        public void AddRouteLoad(RouteInfo route, int startRouteIndex = 0)
        {
            for (var index = startRouteIndex; index < route.GroupAlternativeNodes.Count; index++)
            {
                if (!route.GroupAlternativeNodes[index].QuotaEnabled) continue;
                
                var alternativeNodes = route.PathNodes[index];

                var probability = 1.0 / alternativeNodes.Count;
                foreach (var node in alternativeNodes)
                {
                    if (!NodesLoad.ContainsKey(node)) NodesLoad.Add(node, new NodeLoad());

                    NodesLoad[node].RoutesLoad.Add(new NodeRouteLoad
                    {
                        ArrivalProbability = probability, 
                        TicketContainerId = route.TicketContainerId,
                        NodesBeforeArrival = index
                    });
                }
                    
                if (alternativeNodes.Any(a => _endPointNodeLoad.Contains(a))) break;
            }
        }

        public void UpdateLoadOnNodeArrival(RouteInfo route, long currentRouteIndex)
        {
            _logger.Info($"UpdateLoadOnNodeHandle. TicketContainerId: {route.TicketContainerId}");
            RemoveRoute(route);
            AddRouteLoad(route, (int) currentRouteIndex + 1);
        }

        public void RemoveRoute(long ticketContainerId)
        {
            _logger.Info($"RemoveRoute. TicketContainerId: {ticketContainerId}");
            foreach (var nodeLoad in NodesLoad.Values) nodeLoad.RoutesLoad.RemoveAll(r => r.TicketContainerId == ticketContainerId);
        }

        public void RemoveRoute(RouteInfo route)
        {
            RemoveRoute(route.TicketContainerId);
        }
        
    }
}
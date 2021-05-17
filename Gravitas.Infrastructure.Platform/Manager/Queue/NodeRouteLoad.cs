namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public class NodeRouteLoad
    {
        public long TicketContainerId { get; set; }
        public double ArrivalProbability { get; set; }
        public int NodesBeforeArrival { get; set; }
    }
}
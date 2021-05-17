namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public class EndPointNodeLoad
    {
        public long NodeId { get; set; }
        public float Trucks { get; set; }
        public float LoadInPercent { get; set; }
    }
}
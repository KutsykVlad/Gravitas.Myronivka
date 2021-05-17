namespace Gravitas.Model
{
    public class TransportBlackListRecord : BaseEntity<long>
    {
        public string TransportNo { get; set; }
        public string Comment { get; set; }
    }
}

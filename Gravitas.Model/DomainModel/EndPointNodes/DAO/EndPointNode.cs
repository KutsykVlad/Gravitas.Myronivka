namespace Gravitas.Model
{
    public class EndPointNode : BaseEntity<int>
    {
        public long NodeId { get; set; }
        public virtual Node Node { get; set; }
    }
}

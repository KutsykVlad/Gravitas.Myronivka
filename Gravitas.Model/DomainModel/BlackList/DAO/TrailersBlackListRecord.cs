namespace Gravitas.Model
{
    public class TrailersBlackListRecord : BaseEntity<long>
    {
        public string TrailerNo { get; set; }
        public string Comment { get; set; }
    }
}

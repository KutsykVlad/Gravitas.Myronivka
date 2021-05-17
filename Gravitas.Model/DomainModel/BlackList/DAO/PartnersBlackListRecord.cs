namespace Gravitas.Model
{
    public class PartnersBlackListRecord : BaseEntity<long>
    {
        public string PartnerId { get; set; }
        public string Comment { get; set; }
        public virtual ExternalData.Partner Partner { get; set; }
    }
}

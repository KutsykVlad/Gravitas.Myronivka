using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.BlackList.DAO
{
    public class TrailersBlackListRecord : BaseEntity<int>
    {
        public string TrailerNo { get; set; }
        public string Comment { get; set; }
    }
}

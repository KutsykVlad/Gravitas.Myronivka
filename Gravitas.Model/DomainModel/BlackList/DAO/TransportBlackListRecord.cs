using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.BlackList.DAO
{
    public class TransportBlackListRecord : BaseEntity<int>
    {
        public string TransportNo { get; set; }
        public string Comment { get; set; }
    }
}

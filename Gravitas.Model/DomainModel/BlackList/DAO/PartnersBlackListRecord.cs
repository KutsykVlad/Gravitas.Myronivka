using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.BlackList.DAO
{
    public class PartnersBlackListRecord : BaseEntity<int>
    {
        public string PartnerId { get; set; }
        public string Comment { get; set; }
        public virtual ExternalData.AcceptancePoint.DAO.ExternalData.Partner Partner { get; set; }
    }
}

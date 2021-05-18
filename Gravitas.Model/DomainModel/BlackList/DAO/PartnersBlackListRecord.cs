using Gravitas.Model.DomainModel.Base;
using Gravitas.Model.DomainModel.ExternalData.Partner.DAO;

namespace Gravitas.Model.DomainModel.BlackList.DAO
{
    public class PartnersBlackListRecord : BaseEntity<int>
    {
        public string PartnerId { get; set; }
        public string Comment { get; set; }
        public virtual Partner Partner { get; set; }
    }
}

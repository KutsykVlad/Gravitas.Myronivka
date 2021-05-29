using Gravitas.Model.DomainModel.MixedFeed.DAO;
using Gravitas.Model.DomainModel.OpData.DAO.Base;

namespace Gravitas.Model.DomainModel.OpData.DAO
{
    public class LoadPointOpData : BaseOpData
    {
        public int? MixedFeedSiloId { get; set; }
        public virtual MixedFeedSilo MixedFeedSilo { get; set; }
    }
}
using Gravitas.Model.DomainModel.MixedFeed.DAO;

namespace Gravitas.Model
{
    public class MixedFeedLoadOpData : BaseOpData
    {
        public long? MixedFeedSiloId { get; set; }
        public virtual MixedFeedSilo MixedFeedSilo { get; set; }
    }
}
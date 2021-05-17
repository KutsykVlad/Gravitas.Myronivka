using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.MixedFeed.DAO
{
    public class MixedFeedSiloDevice: BaseEntity<int>
    {
        public int MixedFeedSiloId { get; set; }
        public virtual MixedFeedSilo MixedFeedSilo { get; set; }

        public int DeviceId { get; set; }
        public virtual Device.DAO.Device Device { get; set; }
    }
}
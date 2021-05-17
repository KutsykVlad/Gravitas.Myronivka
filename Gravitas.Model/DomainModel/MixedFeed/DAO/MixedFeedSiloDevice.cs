using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.MixedFeed.DAO
{
    public class MixedFeedSiloDevice: BaseEntity<int>
    {
        public long MixedFeedSiloId { get; set; }
        public virtual MixedFeedSilo MixedFeedSilo { get; set; }

        public long DeviceId { get; set; }
        public virtual Device.DAO.Device Device { get; set; }
    }
}
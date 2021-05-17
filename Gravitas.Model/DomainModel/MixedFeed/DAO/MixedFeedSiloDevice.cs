namespace Gravitas.Model.DomainModel.MixedFeed.DAO
{
    public class MixedFeedSiloDevice: BaseEntity<long>
    {
        public long MixedFeedSiloId { get; set; }
        public virtual MixedFeedSilo MixedFeedSilo { get; set; }

        public long DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }
}
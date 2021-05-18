using System.Data.Entity.Migrations;
using Gravitas.DAL.DbContext;
using Gravitas.Model.DomainModel.MixedFeed.DAO;

namespace Gravitas.DAL.TestData
{
    public static partial class TestData
    {
        public static void MixedFeedDevices(GravitasDbContext context)
        {
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 1, MixedFeedSiloId = 1, DeviceId = 17100113});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 2, MixedFeedSiloId = 2, DeviceId = 17100121});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 3, MixedFeedSiloId = 3, DeviceId = 17100111});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 4, MixedFeedSiloId = 3, DeviceId = 17100112});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 5, MixedFeedSiloId = 4, DeviceId = 17100123});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 6, MixedFeedSiloId = 4, DeviceId = 17100124});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 7, MixedFeedSiloId = 5, DeviceId = 17100114});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 8, MixedFeedSiloId = 6, DeviceId = 17100122});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 10, MixedFeedSiloId = 8, DeviceId = 17100141});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 11, MixedFeedSiloId = 9, DeviceId = 17100131});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 12, MixedFeedSiloId = 9, DeviceId = 17100132});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 13, MixedFeedSiloId = 10, DeviceId = 17100143});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 14, MixedFeedSiloId = 10, DeviceId = 17100144});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 15, MixedFeedSiloId = 11, DeviceId = 17100134});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 16, MixedFeedSiloId = 12, DeviceId = 17100142});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 17, MixedFeedSiloId = 13, DeviceId = 17100153});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 18, MixedFeedSiloId = 14, DeviceId = 17100161});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 19, MixedFeedSiloId = 15, DeviceId = 17100151});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 20, MixedFeedSiloId = 15, DeviceId = 17100152});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 21, MixedFeedSiloId = 16, DeviceId = 17100163});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 22, MixedFeedSiloId = 16, DeviceId = 17100164});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 23, MixedFeedSiloId = 17, DeviceId = 17100154});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 24, MixedFeedSiloId = 18, DeviceId = 17100162});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 25, MixedFeedSiloId = 19, DeviceId = 17100173});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 26, MixedFeedSiloId = 20, DeviceId = 17100181});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 27, MixedFeedSiloId = 21, DeviceId = 17100171});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 28, MixedFeedSiloId = 21, DeviceId = 17100172});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 29, MixedFeedSiloId = 22, DeviceId = 17100183});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 30, MixedFeedSiloId = 22, DeviceId = 17100184});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 31, MixedFeedSiloId = 23, DeviceId = 17100174});
            context.Set<MixedFeedSiloDevice>().AddOrUpdate(new MixedFeedSiloDevice {Id = 32, MixedFeedSiloId = 24, DeviceId = 17100182});

            context.SaveChanges();
        }
    }
}
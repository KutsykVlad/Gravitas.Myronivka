using System.Data.Entity.Migrations;
using Gravitas.Model.DomainModel.MixedFeed.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.PostDeployment
{
    public static partial class PostDeployment
    {
        public static class MixedFeed
        {
            public static void Silo(GravitasDbContext context)
            {
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 1, Drive = (int) NodeIdValue.MixedFeedLoad1, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 2, Drive = (int) NodeIdValue.MixedFeedLoad1, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 3, Drive = (int) NodeIdValue.MixedFeedLoad1, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 4, Drive = (int) NodeIdValue.MixedFeedLoad1, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 5, Drive = (int) NodeIdValue.MixedFeedLoad1, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 6, Drive = (int) NodeIdValue.MixedFeedLoad1, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 7, Drive = (int) NodeIdValue.MixedFeedLoad2, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 8, Drive = (int) NodeIdValue.MixedFeedLoad2, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 9, Drive = (int) NodeIdValue.MixedFeedLoad2, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 10, Drive = (int) NodeIdValue.MixedFeedLoad2, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 11, Drive = (int) NodeIdValue.MixedFeedLoad2, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 12, Drive = (int) NodeIdValue.MixedFeedLoad2, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 13, Drive = (int) NodeIdValue.MixedFeedLoad3, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 14, Drive = (int) NodeIdValue.MixedFeedLoad3, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 15, Drive = (int) NodeIdValue.MixedFeedLoad3, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 16, Drive = (int) NodeIdValue.MixedFeedLoad3, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 17, Drive = (int) NodeIdValue.MixedFeedLoad3, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 18, Drive = (int) NodeIdValue.MixedFeedLoad3, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 19, Drive = (int) NodeIdValue.MixedFeedLoad4, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 20, Drive = (int) NodeIdValue.MixedFeedLoad4, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 21, Drive = (int) NodeIdValue.MixedFeedLoad4, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 22, Drive = (int) NodeIdValue.MixedFeedLoad4, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 23, Drive = (int) NodeIdValue.MixedFeedLoad4, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
                context.Set<MixedFeedSilo>().AddOrUpdate(new MixedFeedSilo { Id = 24, Drive = (int) NodeIdValue.MixedFeedLoad4, LoadQueue = 0, SiloEmpty = 0, SiloFull = 0, SiloWeight = 0, Specification = string.Empty, IsActive = true});
    
                context.SaveChanges();
            }
        }
    }
}

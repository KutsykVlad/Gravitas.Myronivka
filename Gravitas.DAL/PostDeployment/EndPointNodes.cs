using System.Data.Entity.Migrations;
using Gravitas.Model;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.PostDeployment
{

    public static partial class PostDeployment
    {
        public static void EndPointNodes(GravitasDbContext context)
        {
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint10 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint11 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint12 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint13 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint14 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint20 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint21 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint22 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint50 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.MixedFeedLoad1 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.MixedFeedLoad2 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.MixedFeedLoad3 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.MixedFeedLoad4 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint31 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint32 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.UnloadPoint61 });
            context.Set<EndPointNode>().AddOrUpdate(new EndPointNode { NodeId = (long) NodeIdValue.LoadPoint73 });

            context.SaveChanges();
        }
    }
}

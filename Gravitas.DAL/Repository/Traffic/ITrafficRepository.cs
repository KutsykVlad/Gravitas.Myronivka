using Gravitas.DAL.DbContext;
using Gravitas.Model;

namespace Gravitas.DAL
{
    public interface ITrafficRepository : IBaseRepository<GravitasDbContext>
    {
        void OnNodeArrival(TrafficRecord record);
        void OnNodeHandle(long ticketContainerId, long nodeId);

        NodeTrafficHistory GetNodeTrafficHistory(long nodeId);
    }
}

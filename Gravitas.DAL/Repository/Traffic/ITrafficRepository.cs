using Gravitas.DAL.Repository._Base;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Traffic.TDO;

namespace Gravitas.DAL.Repository.Traffic
{
    public interface ITrafficRepository : IBaseRepository
    {
        void OnNodeArrival(TrafficRecord record);
        void OnNodeHandle(int ticketContainerId, int nodeId);
        NodeTrafficHistory GetNodeTrafficHistory(int nodeId);
    }
}

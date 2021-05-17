using Gravitas.Model;
using Gravitas.Model.DomainModel.Ticket.DAO;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public interface IQueueManager
    {
        bool IsAllowedEnterTerritory(long ticketId);
        void OnNodeArrival(long ticketId, long nodeId);
        void OnRouteAssigned(Ticket ticket);
        void OnRouteUpdated(Ticket ticket);
        void OnImmediateEntranceAccept(long ticketContainerId);
        void RemoveFromQueue(long ticketContainerId);
        long? GetFreeSiloDrive(string productId, long ticketId);
        void RestoreState();
    }
}
using System;
using Gravitas.Model.DomainModel.Ticket.DAO;

namespace Gravitas.Infrastructure.Platform.Manager.Queue
{
    public interface IQueueManager
    {
        bool IsAllowedEnterTerritory(int ticketId);
        void OnNodeArrival(int ticketId, int nodeId);
        void OnRouteAssigned(Ticket ticket);
        void OnRouteUpdated(Ticket ticket);
        void OnImmediateEntranceAccept(int ticketContainerId);
        void RemoveFromQueue(int ticketContainerId);
        int? GetFreeSiloDrive(Guid productId, int ticketId);
        void RestoreState();
        int TrucksBefore(int routeId, int ticketContainerId);
    }
}
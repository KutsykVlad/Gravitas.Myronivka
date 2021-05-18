using System;

namespace Gravitas.Infrastructure.Platform.Manager.Queue.Infrastructure
{
    public interface IQueueInfrastructure
    {
        DateTime GetPredictionEntranceTime(int routeId);
        void ImmediateEntrance(int ticketContainer);
        int GetTruckCountInQueue(int routeId);
    }
}
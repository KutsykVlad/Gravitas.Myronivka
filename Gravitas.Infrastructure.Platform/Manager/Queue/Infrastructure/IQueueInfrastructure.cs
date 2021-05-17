using System;

namespace Gravitas.Infrastructure.Platform.Manager.Queue.Infrastructure
{
    public interface IQueueInfrastructure
    {
        DateTime GetPredictionEntranceTime(long routeId);
        void ImmediateEntrance(long ticketContainer);
        int GetTruckCountInQueue(long routeId);
    }
}
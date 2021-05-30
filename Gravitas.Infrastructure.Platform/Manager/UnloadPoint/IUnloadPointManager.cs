using System;

namespace Gravitas.Infrastructure.Platform.Manager.UnloadPoint
{
    public interface IUnloadPointManager
    {
        bool ConfirmUnloadGuide(int ticketId, Guid employeeId);
        bool ConfirmUnloadPoint(int ticketId, Guid employeeId);
    }
}
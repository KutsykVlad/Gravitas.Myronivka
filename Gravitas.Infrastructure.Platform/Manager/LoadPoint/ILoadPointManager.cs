using System;

namespace Gravitas.Infrastructure.Platform.Manager.LoadPoint
{
    public interface ILoadPointManager
    {
        bool ConfirmLoadGuide(int ticketId, Guid employeeId);
        bool ConfirmLoadPoint(int ticketId, Guid employeeId);
    }
}
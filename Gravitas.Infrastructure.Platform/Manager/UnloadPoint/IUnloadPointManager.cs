namespace Gravitas.Infrastructure.Platform.Manager.UnloadPoint
{
    public interface IUnloadPointManager
    {
        bool ConfirmUnloadGuide(int ticketId, string employeeId);
        bool ConfirmUnloadPoint(int ticketId, string employeeId);
    }
}
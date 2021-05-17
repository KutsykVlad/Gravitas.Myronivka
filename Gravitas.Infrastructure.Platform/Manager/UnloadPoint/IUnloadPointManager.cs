namespace Gravitas.Infrastructure.Platform.Manager.UnloadPoint
{
    public interface IUnloadPointManager
    {
        bool ConfirmUnloadGuide(long ticketId, string employeeId);
        bool ConfirmUnloadPoint(long ticketId, string employeeId);
    }
}
namespace Gravitas.CollisionCoordination.Manager.LaboratoryData
{
    public interface IOpDataRepository
    {
        object GetLastProcessed<T>(int? ticketId);
    }
}
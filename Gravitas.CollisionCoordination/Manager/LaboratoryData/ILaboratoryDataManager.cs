using Gravitas.CollisionCoordination.Models;

namespace Gravitas.CollisionCoordination.Manager.LaboratoryData
{
    public interface ILaboratoryDataManager
    {
        LaboratoryDataVm CollectData(long ticketId, string approvedBy);
        LaboratoryDataVm CollectCentralLaboratoryData(long ticketId, string approvedBy);
    }
}
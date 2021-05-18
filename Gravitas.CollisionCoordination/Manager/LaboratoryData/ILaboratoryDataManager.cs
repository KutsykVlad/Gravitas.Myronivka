using Gravitas.CollisionCoordination.Models;

namespace Gravitas.CollisionCoordination.Manager.LaboratoryData
{
    public interface ILaboratoryDataManager
    {
        LaboratoryDataVm CollectData(int ticketId, string approvedBy);
        LaboratoryDataVm CollectCentralLaboratoryData(int ticketId, string approvedBy);
    }
}
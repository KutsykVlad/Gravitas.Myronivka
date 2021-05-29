using Gravitas.Model.DomainModel.EmployeeRoles.DTO;
using Gravitas.Model.DomainModel.Node.TDO.Detail;

namespace Gravitas.Core.DeviceManager.Card
{
    public interface ICardManager
    {
        CardReadResult GetTruckCardByOnGateReader(NodeDetails nodeDetailsDto);
        CardReadResult GetTruckCardByZebraReader(NodeDetails nodeDetailsDto);
        Model.DomainModel.Card.DAO.Card GetLaboratoryTrayOnTableReader(NodeDetails nodeDetailsDto);
        bool IsMasterEmployeeCard(Model.DomainModel.Card.DAO.Card card, int nodeId);
        bool IsLaboratoryEmployeeCard(Model.DomainModel.Card.DAO.Card card, int nodeId);
        bool IsAdminCard(Model.DomainModel.Card.DAO.Card card, RolesDto employeeRoles = null);
        void SetRfidValidationDO(bool isValid, NodeDetails nodeDetailsDto);
        (CardReadResult card, bool input) GetTruckCardByZebraReaderDirection(NodeDetails nodeDetailsDto);
    }
}
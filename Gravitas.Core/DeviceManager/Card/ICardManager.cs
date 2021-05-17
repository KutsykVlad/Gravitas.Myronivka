using Gravitas.Model.Dto;
using Node = Gravitas.Model.Dto.Node;

namespace Gravitas.Core.DeviceManager.Card
{
    public interface ICardManager
    {
        CardReadResult GetTruckCardByOnGateReader(Node nodeDto);
        CardReadResult GetTruckCardByZebraReader(Node nodeDto);
        Model.DomainModel.Card.DAO.Card GetLaboratoryTrayOnTableReader(Node nodeDto);
        bool IsMasterEmployeeCard(Model.DomainModel.Card.DAO.Card card, long nodeId);
        bool IsLaboratoryEmployeeCard(Model.DomainModel.Card.DAO.Card card, long nodeId);
        bool IsAdminCard(Model.DomainModel.Card.DAO.Card card, RolesDto employeeRoles = null);
        void SetRfidValidationDO(bool isValid, Node nodeDto);
        (CardReadResult card, bool input) GetTruckCardByZebraReaderDirection(Node nodeDto);
    }
}
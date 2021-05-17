using Node = Gravitas.Model.Dto.Node;

namespace Gravitas.Core.DeviceManager.User
{
    public interface IUserManager
    {
        Model.DomainModel.Card.DAO.Card GetValidatedUsersCardByTableReader(Node nodeDto);
        Model.DomainModel.Card.DAO.Card GetValidatedUsersCardByOnGateReader(Node nodeDto);
    }
}
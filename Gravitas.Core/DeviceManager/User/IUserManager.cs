using Node = Gravitas.Model.DomainModel.Node.TDO.Detail.Node;

namespace Gravitas.Core.DeviceManager.User
{
    public interface IUserManager
    {
        Model.DomainModel.Card.DAO.Card GetValidatedUsersCardByTableReader(Node nodeDto);
        Model.DomainModel.Card.DAO.Card GetValidatedUsersCardByOnGateReader(Node nodeDto);
    }
}
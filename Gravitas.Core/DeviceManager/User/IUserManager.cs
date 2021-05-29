using Gravitas.Model.DomainModel.Node.TDO.Detail;

namespace Gravitas.Core.DeviceManager.User
{
    public interface IUserManager
    {
        Model.DomainModel.Card.DAO.Card GetValidatedUsersCardByTableReader(NodeDetails nodeDetailsDto);
        Model.DomainModel.Card.DAO.Card GetValidatedUsersCardByOnGateReader(NodeDetails nodeDetailsDto);
    }
}
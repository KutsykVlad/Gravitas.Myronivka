using Node = Gravitas.Model.Dto.Node;

namespace Gravitas.Core.DeviceManager.User
{
    public interface IUserManager
    {
        Model.Card GetValidatedUsersCardByTableReader(Node nodeDto);
        Model.Card GetValidatedUsersCardByOnGateReader(Node nodeDto);
    }
}
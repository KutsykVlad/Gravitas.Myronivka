using System;
using Gravitas.Platform.Web.ViewModel.User;

namespace Gravitas.Platform.Web.Manager.User
{
    public interface IUserWebManager
    {
        UserListVm GetUserList(string name = "", int pageNumber = 1, int pageSize = 25);
        UserDetailsVm GetUserDetails(Guid id);

        (bool, string) AssignCardToUser(Guid userId);
    }
}
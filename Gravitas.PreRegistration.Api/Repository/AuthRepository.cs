using System;
using System.Linq;
using System.Threading.Tasks;
using Gravitas.PreRegistration.Api.DbContext;
using Gravitas.PreRegistration.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gravitas.PreRegistration.Api.Repository
{
    public class AuthRepository : IDisposable
    {
        private readonly AuthContext _ctx;
 
        private readonly UserManager<IdentityUser> _userManager;
 
        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }
 
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new IdentityUser
            {
                Email = userModel.Email,
                UserName = userModel.Email
            };
 
            var result = await _userManager.CreateAsync(user, userModel.Password);
 
            return result;
        }
 
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);
 
            return user;
        }
        
        public async Task<IdentityResult> ChangePassword(string userId, ChangePasswordBindingModel model)
        {
            var result = await _userManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);
            return result;
        }
 
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
 
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var logins = user.Logins;
            var rolesForUser = await _userManager.GetRolesAsync(userId);

            using (var transaction = _ctx.Database.BeginTransaction())
            {
                foreach (var login in logins.ToList())
                {
                    var result = await _userManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    if (!result.Succeeded) return false;
                }

                if (rolesForUser.Any())
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await _userManager.RemoveFromRoleAsync(user.Id, item);
                        if (!result.Succeeded) return false;
                    }
                }

                var r = await _userManager.DeleteAsync(user);
                if (!r.Succeeded) return false;
                transaction.Commit();
            }

            return true;
        }
    }
}
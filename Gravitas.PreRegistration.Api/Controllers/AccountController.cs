using System.Threading.Tasks;
using System.Web.Http;
using Gravitas.DAL.Repository.PreRegistration;
using Gravitas.Model.DomainModel.PreRegistration.DAO;
using Gravitas.PreRegistration.Api.Models;
using Gravitas.PreRegistration.Api.Repository;
using Microsoft.AspNet.Identity;

namespace Gravitas.PreRegistration.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly AuthRepository _authRepository;
        private readonly IPreRegistrationRepository _preRegistrationRepository;
 
        public AccountController(IPreRegistrationRepository preRegistrationRepository)
        {
            _preRegistrationRepository = preRegistrationRepository;
            _authRepository = new AuthRepository();
        }
 
        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
 
            var result = await _authRepository.RegisterUser(userModel);
 
            var errorResult = GetErrorResult(result);
 
            if (errorResult != null)
            {
                return errorResult;
            }

            _preRegistrationRepository.AddOrUpdateCompany(new PreRegisterCompany
            {
                AllowToAdd = false, Email = userModel.Email, TrucksMax = 0, TrucksInProgress = 0
            });
 
            return Ok();
        }
        
        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _authRepository.ChangePassword(User.Identity.GetUserId(), model);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        
        // DELETE: /Account/Delete
        [HttpDelete, ActionName("Delete")]
        public async Task<IHttpActionResult> DeleteConfirmed()
        {
            var result = await _authRepository.DeleteUser(User.Identity.GetUserId());
            
            if (!result)
            {
                return BadRequest();
            }

            _preRegistrationRepository.RemoveCompany(User.Identity.GetUserName());
            
            return Ok();
        }
 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _authRepository.Dispose();
            }
 
            base.Dispose(disposing);
        }
 
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }
 
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("errorMessage", error);
                    }
                }
 
                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }
 
                return BadRequest(ModelState);
            }
 
            return null;
        }
    }
}
using System.Security.Claims;
using System.Threading.Tasks;
using Gravitas.PreRegistration.Api.Repository;
using Microsoft.Owin.Security.OAuth;

namespace Gravitas.PreRegistration.Api.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
 
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
 
            using (var repo = new AuthRepository())
            {
                var user = await repo.FindUser(context.UserName, context.Password);
 
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
 
                context.Validated(identity);
            }
        }
    }
}
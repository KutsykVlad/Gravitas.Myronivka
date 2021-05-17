using Microsoft.AspNet.Identity.EntityFramework;

namespace Gravitas.PreRegistration.Api.DbContext
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
 
        }
    }
}
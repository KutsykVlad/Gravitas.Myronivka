using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gravitas.PreRegistration.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PartnerId { get; set; }
        public string PartnerName { get; set; }
        public bool IsRegistrationAllowed { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class RegisteredTruck
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string TruckNo { get; set; }
        public string TrailerNo { get; set; }
        public string PhoneNo { get; set; }
        public long RouteId { get; set; }
        public DateTime EntranceTime { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<RegisteredTruck> RegisteredTrucks { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
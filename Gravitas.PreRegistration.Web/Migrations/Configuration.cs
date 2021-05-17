using Gravitas.PreRegistration.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Gravitas.PreRegistration.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Gravitas.PreRegistration.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Gravitas.Gravitas.PreRegistration.Api.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(Gravitas.PreRegistration.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser { Email = "romyac83@gmail.com", UserName = "romyac83@gmail.com" };
            string password = "ad46D_ewr3";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
            }

            context.SaveChanges();
        }
    }
}

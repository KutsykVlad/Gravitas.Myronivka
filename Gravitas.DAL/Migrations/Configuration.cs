using System.Data.Entity.Migrations;
using Gravitas.DAL.DbContext;

namespace Gravitas.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GravitasDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GravitasDbContext context)
        {
            // PostDeployment.PostDeployment.Deploy(context);
            // TestData.TestData.Deploy(context);
        }
    }
}
using System.Data.Entity.Migrations;
using System.Linq;
using Gravitas.Model;

namespace Gravitas.DAL.PostDeployment
{
    public static partial class PostDeployment
    {

        public static class EmployeeRoles
        {

            public static void Roles(GravitasDbContext context)
            {
//                if (!context.Set<Role>().Select(t => t.Name).Contains("Адміністратор"))
//                {
//                    context.Set<Role>().AddOrUpdate(new Role() { Name = "Адміністратор" });
//
//                    context.SaveChanges();
//
//                    var id = context.Set<Role>().First(t => t.Name == "Адміністратор").Id;
//                    var nodes = context.Set<Node>().Select(t => t.Id).ToList();
//                    foreach (var node in nodes)
//                    {
//                        context.Set<Model.RoleAssignment>().AddOrUpdate(new Model.RoleAssignment(){RoleId = id, NodeId = node});
//                    }
//                    context.SaveChanges();
//                }
            }
        }
    }
}

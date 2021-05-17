using System.Data.Entity.Migrations;
using System.Linq;
using Gravitas.Model;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.PostDeployment
{
    public static partial class PostDeployment
    {
        public static class Roles
        {
            public static void Type(GravitasDbContext context)
            {
                context.Set<Role>().AddOrUpdate(new Role { Id = (long)UserRole.Admin, Name = "Адміністратор" });
                context.Set<Role>().AddOrUpdate(new Role { Id = (long)UserRole.Security, Name = "Охоронець" });
                context.Set<Role>().AddOrUpdate(new Role { Id = (long)UserRole.Laboratory, Name = "Лаборант" });
                context.Set<Role>().AddOrUpdate(new Role { Id = (long)UserRole.LoadWorker, Name = "Машиніст погрузки" });
                context.Set<Role>().AddOrUpdate(new Role { Id = (long)UserRole.Master, Name = "Майстер" });
                context.Set<Role>().AddOrUpdate(new Role { Id = (long)UserRole.Dispatcher, Name = "Диспетчер" });
                context.Set<Role>().AddOrUpdate(new Role { Id = (long)UserRole.SingleWindow, Name = "Оператор Єдиного вікна" });
                context.Set<Role>().AddOrUpdate(new Role { Id = (long)UserRole.LaboratoryManager, Name = "Менеджер погодження" });

                context.SaveChanges();
            }

            public static void Assigment(GravitasDbContext context)
            {
                var nodeList = context.Nodes.Select(x => x.Id).ToList();
                foreach (var nodeId in nodeList)
                {
                    context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Admin, NodeId = nodeId });
                }

                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowFirst });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowSecond });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowReadonly1 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowReadonly2 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowReadonly3 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowReadonly4 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowReadonly5 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowReadonly6 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowReadonly7 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowReadonly8 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowReadonly9 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.SingleWindowReadonly10 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.Weighbridge1 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.Weighbridge2 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.Weighbridge3 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.Weighbridge4 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.SingleWindow, NodeId = (long)NodeIdValue.Weighbridge5 });

                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Laboratory, NodeId = (long)NodeIdValue.Laboratory0 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Laboratory, NodeId = (long)NodeIdValue.Laboratory3 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Laboratory, NodeId = (long)NodeIdValue.Laboratory4 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Laboratory, NodeId = (long)NodeIdValue.Laboratory5 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Laboratory, NodeId = (long)NodeIdValue.CentralLaboratoryProcess1 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Laboratory, NodeId = (long)NodeIdValue.CentralLaboratoryProcess2 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Laboratory, NodeId = (long)NodeIdValue.CentralLaboratoryProcess3 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Laboratory, NodeId = (long)NodeIdValue.CentralLaboratoryGetOil1 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Laboratory, NodeId = (long)NodeIdValue.CentralLaboratoryGetOil2 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Laboratory, NodeId = (long)NodeIdValue.CentralLaboratoryGetShrot });

                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Master, NodeId = (long)NodeIdValue.LoadPointGuideTareWarehouse });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Master, NodeId = (long)NodeIdValue.LoadPointGuideEl23 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Master, NodeId = (long)NodeIdValue.LoadPointGuideEl45 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Master, NodeId = (long)NodeIdValue.UnloadPointGuideEl23 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Master, NodeId = (long)NodeIdValue.LoadPointGuideMPZ });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Master, NodeId = (long)NodeIdValue.LoadPointGuideShrotHuskOil });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Master, NodeId = (long)NodeIdValue.UnloadPointGuideEl45 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Master, NodeId = (long)NodeIdValue.CentralLaboratoryProcess1 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Master, NodeId = (long)NodeIdValue.CentralLaboratoryProcess2 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Master, NodeId = (long)NodeIdValue.CentralLaboratoryProcess3 });

                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Security, NodeId = (long)NodeIdValue.SecurityIn1 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Security, NodeId = (long)NodeIdValue.SecurityIn2 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Security, NodeId = (long)NodeIdValue.SecurityOut1 });
                context.Set<RoleAssignment>().AddOrUpdate(new RoleAssignment { RoleId = (long)UserRole.Security, NodeId = (long)NodeIdValue.SecurityOut2 });

                context.SaveChanges();
            }
        }
    }
}
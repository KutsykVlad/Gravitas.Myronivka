using System.Data.Entity.Migrations;
using Gravitas.Model;

namespace Gravitas.DAL.PostDeployment
{

    public static partial class PostDeployment
    {
        public static void PhoneNumbers(GravitasDbContext context)
        {
            context.Set<PhoneDictionary>().AddOrUpdate(new PhoneDictionary { Id = Dom.Phone.Security, PhoneNumber = "380503537199" , EmployeePosition = "Охоронець"});
            context.Set<PhoneDictionary>().AddOrUpdate(new PhoneDictionary { Id = Dom.Phone.Dispatcher, PhoneNumber = "380472590407", EmployeePosition = "Диспетчер" });
            context.Set<PhoneDictionary>().AddOrUpdate(new PhoneDictionary { Id = Dom.Phone.CentralLaboratoryWorker, PhoneNumber = "380952833261", EmployeePosition = "Правцівник центральної лабораторії" });
            context.Set<PhoneDictionary>().AddOrUpdate(new PhoneDictionary { Id = Dom.Phone.MixedFeedManager, PhoneNumber = "380502124293", EmployeePosition = "Оператор комбікормового заводу" });

            context.SaveChanges();
        }
    }
}

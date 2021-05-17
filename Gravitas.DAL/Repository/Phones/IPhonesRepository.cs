using System.Collections.Generic;
using Gravitas.Model;

namespace Gravitas.DAL 
{
    public interface IPhonesRepository : IBaseRepository<GravitasDbContext>
    {
        string GetPhone(int phoneId);
        List<PhoneDictionary> GetAll();
    }
}

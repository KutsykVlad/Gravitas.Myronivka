using System.Collections.Generic;
using Gravitas.DAL.DbContext;
using Gravitas.Model;
using Gravitas.Model.DomainModel.PhoneDictionary.DAO;

namespace Gravitas.DAL 
{
    public interface IPhonesRepository : IBaseRepository<GravitasDbContext>
    {
        string GetPhone(int phoneId);
        List<PhoneDictionary> GetAll();
    }
}

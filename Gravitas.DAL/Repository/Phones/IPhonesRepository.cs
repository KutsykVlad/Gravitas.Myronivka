using System.Collections.Generic;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.PhoneDictionary.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Repository.Phones 
{
    public interface IPhonesRepository : IBaseRepository
    {
        string GetPhone(Phone phoneId);
        List<PhoneDictionary> GetAll();
    }
}

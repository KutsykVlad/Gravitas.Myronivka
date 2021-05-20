using System.Collections.Generic;
using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.PhoneDictionary.DAO;
using Gravitas.Model.DomainValue;

namespace Gravitas.DAL.Repository.Phones
{
    public class PhonesRepository : BaseRepository, IPhonesRepository
    {
        private readonly GravitasDbContext _context;

        public PhonesRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public string GetPhone(Phone phoneId) => _context.PhoneDictionaries.FirstOrDefault(x => x.Id == phoneId)?.PhoneNumber ?? string.Empty;
        
        public List<PhoneDictionary> GetAll()
        {
            return GetQuery<PhoneDictionary, Phone>().ToList();
        }
    }
}

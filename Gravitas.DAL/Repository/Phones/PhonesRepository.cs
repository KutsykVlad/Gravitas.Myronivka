using System.Collections.Generic;
using System.Linq;
using Gravitas.Model;

namespace Gravitas.DAL
{
    public class PhonesRepository : BaseRepository<GravitasDbContext>, IPhonesRepository
    {
        private readonly GravitasDbContext _context;

        public PhonesRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public string GetPhone(int phoneId) => GetEntity<PhoneDictionary, long>(phoneId)?.PhoneNumber ?? string.Empty;
        
        public List<PhoneDictionary> GetAll()
        {
            return GetQuery<PhoneDictionary, long>().ToList();
        }
    }
}

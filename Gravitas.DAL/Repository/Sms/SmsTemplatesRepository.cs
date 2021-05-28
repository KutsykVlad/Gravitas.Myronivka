using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;

namespace Gravitas.DAL.Repository.Sms
{
    public class SmsTemplatesRepository : BaseRepository, ISmsTemplatesRepository
    {
        private readonly GravitasDbContext _context;

        public SmsTemplatesRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public string GetSmsTemplate(Model.DomainValue.SmsTemplate templateId)
        {
            return string.Empty;
        }
    }
}

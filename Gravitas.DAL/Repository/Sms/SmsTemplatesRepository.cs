using System.Linq;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.Sms.DAO;
using Gravitas.Model.DomainModel.Sms.DTO;

namespace Gravitas.DAL.Repository.Sms
{
    public class SmsTemplatesRepository : BaseRepository, ISmsTemplatesRepository
    {
        private readonly GravitasDbContext _context;

        public SmsTemplatesRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public string GetSmsTemplate(int templateId)
        {
            return _context.SmsTemplates.AsNoTracking().FirstOrDefault(x => x.Id == templateId)?.Text ?? string.Empty;
        }

        public SmsTemplates GetSmsTemplates()
        {
            var result = new SmsTemplates
            {
                Items = GetQuery<SmsTemplate, int>().ToList()
            };
            result.Count = result.Items.Count;
            return result;
        }
    }
}

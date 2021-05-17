using System.Linq;
using Gravitas.Model;

namespace Gravitas.DAL
{
    public class SmsTemplatesRepository : BaseRepository<GravitasDbContext>, ISmsTemplatesRepository
    {
        private readonly GravitasDbContext _context;

        public SmsTemplatesRepository(GravitasDbContext context) : base(context)
        {
            _context = context;
        }

        public string GetSmsTemplate(long templateId)
        {
            return _context.SmsTemplates.AsNoTracking().FirstOrDefault(x => x.Id == templateId)?.Text ?? string.Empty;
        }

        public SmsTemplates GetSmsTemplates()
        {
            var result = new SmsTemplates();
            result.Items = this.GetQuery<SmsTemplate, long>().ToList();
            result.Count = result.Items.Count;
            return result;
        }
    }
}

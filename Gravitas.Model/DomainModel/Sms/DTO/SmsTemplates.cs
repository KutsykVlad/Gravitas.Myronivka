using System.Collections.Generic;

namespace Gravitas.Model
{
    public class SmsTemplates
    {
        public SmsTemplates()
        {
            Items = new List<SmsTemplate>();
        }

        public ICollection<SmsTemplate> Items { get; set; }
        public int Count { get; set; }
    }
}

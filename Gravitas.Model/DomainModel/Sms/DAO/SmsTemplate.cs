using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model
{
    public class SmsTemplate : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}

using Gravitas.Model.DomainModel.Base;

namespace Gravitas.Model.DomainModel.Sms.DAO
{
    public class SmsTemplate : BaseEntity<DomainValue.SmsTemplate>
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}

namespace Gravitas.Model
{
    public class SmsTemplate : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}

using System.ComponentModel;

namespace Gravitas.Infrastructure.Platform.Manager.Connect
{
    public class SmsMessage
    {
        public string PhoneNumber { get; set; }

        [Description("Message that will be sent to recipient")]
        public string Message { get; set; }
    }
}
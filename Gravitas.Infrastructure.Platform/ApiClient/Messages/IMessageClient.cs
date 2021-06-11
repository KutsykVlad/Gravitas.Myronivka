using System.Net.Mail;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Model.DomainValue;

namespace Gravitas.Infrastructure.Platform.ApiClient.Messages
{
    public interface IMessageClient
    {
        (long Id, MessageStatus Value) SendSms(SmsMessage message);
        bool SendEmail(MailMessage message);
    }
}
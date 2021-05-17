using System.Net.Mail;
using Gravitas.Infrastructure.Platform.Manager.Connect;

namespace Gravitas.Infrastructure.Platform.ApiClient.Messages
{
    public interface IMessageClient
    {
        bool SendSms(SmsMessage message);
        bool SendEmail(MailMessage message);
    }
}
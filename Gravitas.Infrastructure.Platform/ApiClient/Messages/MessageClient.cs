using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using NLog;

namespace Gravitas.Infrastructure.Platform.ApiClient.Messages
{
    public class MessageClient : IMessageClient
    {
        private static readonly object LockObject = new object();
        private readonly SmtpClient _client;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly string _rootEmail;
        private readonly string _rootEmailDestination;

        public MessageClient(string host, string rootEmail, string rootEmailDestination, string login, string password)
        {
            _client = new SmtpClient
            {
                Port = 587,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(login.Trim(), password.Trim()),
                Host = host
            };
            _rootEmail = rootEmail;
            _rootEmailDestination = rootEmailDestination;
        }

        public bool SendSms(SmsMessage sms)
        {
            if (string.IsNullOrEmpty(sms?.PhoneNumber))
            {
                return false;
            }
            lock (LockObject)
            {
                try
                {
                    _logger.Trace($"MessageClient: Host = {_client.Host}");
                    _logger.Trace($"MessageClient: Port = {_client.Port}");
                    _logger.Trace($"MessageClient: RootEmail =  {_rootEmail}");
                    _logger.Trace($"MessageClient: DestinationEmail =  {_rootEmailDestination}");
                    _logger.Trace($"MessageClient: MailTo =  {sms?.PhoneNumber?.Trim()}@{_rootEmailDestination.Trim()}");

                    _logger.Info($"MessageClient: Sms on number {sms?.PhoneNumber}, with text {sms?.Message}");
                    var text = Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(sms?.Message ?? string.Empty));
                    using (var mail = new MailMessage
                    {
                        From = new MailAddress(_rootEmail.Trim()), Subject = "Message", Body = text
                    })
                    {
                        mail.To.Add($"{sms?.PhoneNumber.Trim()}@{_rootEmailDestination.Trim()}");
                        _client.Send(mail);
                    }
                }
                catch (Exception e)
                {
                    _logger.Error($"MessageClient: Error on sending sms: {e}");
                    return false;
                }

                _logger.Info($"MessageClient: Sms on number {sms?.PhoneNumber} has been sent");
                return true;
            }
        }

        public bool SendEmail(MailMessage message)
        {
            try
            {
                _logger.Debug($"MessageClient: Host = {_client.Host}");
                _logger.Debug($"MessageClient: Port = {_client.Port}");
                _logger.Debug($"MessageClient: RootEmail =  {_rootEmail}");
                _logger.Debug($"MessageClient: MailTo =  {message.To[0]}");
                _logger.Debug($"MessageClient: MailFrom =  {message.From}");
                _client.Send(message);
            }
            catch (Exception e)
            {
                _logger.Error($"MessageClient: Error on sending email: {e}");
                return false;
            }

            _logger.Info($"MessageClient: Email on {message.To[0]} has been sent");
            return true;
        }
    }
}
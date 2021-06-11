using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using Gravitas.Infrastructure.Platform.ApiClient.Messages.Models;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Model.DomainValue;
using Newtonsoft.Json;
using NLog;

namespace Gravitas.Infrastructure.Platform.ApiClient.Messages
{
    public class MessageClient : IMessageClient
    {
        private readonly string _omniHost;
        private static readonly object LockObject = new object();
        private readonly SmtpClient _smtpClient;
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public MessageClient(string host, string login, string password, string omniHost, string omniLogin, string omniPassword)
        {
            _omniHost = omniHost;
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{omniLogin}:{omniPassword}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            _smtpClient = new SmtpClient
            {
                Port = 587,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(login.Trim(), password.Trim()),
                Host = host
            };
        }

        public (long Id, MessageStatus Value) SendSms(SmsMessage sms)
        {
            lock (LockObject)
            {
                try
                {
                    var message = new OmniMessage
                    {
                        To = new Receiver[]
                        {
                            new Receiver
                            {
                                Msisdn = sms.PhoneNumber
                            }
                        },
                        Body = new Body
                        {
                            Value = sms.Message
                        }
                    };

                    var request = new HttpRequestMessage(HttpMethod.Post, _omniHost)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json")
                    };

                    var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();

                    var result = JsonConvert.DeserializeObject<OmniSmsSendResult>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    _logger.Info($"MessageClient: Sms on number {sms.PhoneNumber} has been sent");
                    return (result.Id, result.Request.Value);
                }
                catch (Exception e)
                {
                    _logger.Error($"MessageClient: Error on sending sms: {e}");
                    return (default, MessageStatus.Undeliverable);
                }
            }
        }

        public bool SendEmail(MailMessage message)
        {
            try
            {
                _smtpClient.Send(message);
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
using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.Messages;
using Gravitas.Infrastructure.Platform.Manager.Settings;
using Gravitas.PreRegistration.Api.Helpers.Abstractions;
using Gravitas.PreRegistration.Api.Models;

namespace Gravitas.PreRegistration.Api.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private const string Subject = "Запит дозволу на попередню реєстрацію";

        private readonly IMessageClient _messageClient;
        private readonly Settings _settings;

        public EmailHelper(IMessageClient messageClient,
            Settings settings)
        {
            _messageClient = messageClient;
            _settings = settings;
        }

        public void SendEmail(CompanyInfo info)
        {
            var mailMessage = CreateMessage(GlobalConfigurationManager.RootEmail,
                _settings.PreRegistrationAdminEmail, Subject, CreateHtmlBody(info));

            var fileName = info.CompanyName.Trim() + ".jpg";
            AddJpegAttachment(mailMessage, fileName, GenerateStreamFromString(info.Attachment));

            _messageClient.SendEmail(mailMessage);
        }

        private string CreateHtmlBody(CompanyInfo info)
        {
            return
                $"<p><strong> Назва компанії:</strong> {info.CompanyName} </p><p><strong> Код ЄДРПОУ або ФОП: </strong> {info.EnterpriseCode} </p><p><strong> Контактний номер: </strong> {info.PhoneNo} </p><p><strong> Кількість автомобілів: </strong> {info.NumberOfRegistrations}</p>";
        }

        private MailMessage CreateMessage(string from, string to, string subject, string htmlBody)
        {
            var mailMessage = new MailMessage()
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true,
                Priority = MailPriority.High,
                To = { to }
            };

            return mailMessage;
        }

        private void AddJpegAttachment(MailMessage mailMessage, string fileName, Stream attachmentFile)
        {
            if (attachmentFile == null) return;
            var attachment = new Attachment(attachmentFile, new ContentType("image/jpeg"));
            var disposition = attachment.ContentDisposition;
            disposition.DispositionType = DispositionTypeNames.Attachment;
            disposition.FileName = fileName;

            mailMessage.Attachments.Add(attachment);
        }

        private static Stream GenerateStreamFromString(string attachment)
        {
            var base64FileFromAttachment = attachment.Substring(attachment.IndexOf(',') + 1);
            return new MemoryStream(Convert.FromBase64String(base64FileFromAttachment));
        }
    }
}
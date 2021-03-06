using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.DAL.Repository.OpWorkflow.OpData;
using Gravitas.DAL.Repository.OpWorkflow.Routes;
using Gravitas.DAL.Repository.Phones;
using Gravitas.DAL.Repository.Sms;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.Messages;
using Gravitas.Infrastructure.Platform.Manager.ReportTool;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model.DomainModel.Message.DAO;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using NLog;

namespace Gravitas.Infrastructure.Platform.Manager.Connect
{
    public class ConnectManager : IConnectManager
    {
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMessageClient _messageClient;
        private readonly IOpDataRepository _opDataRepository;
        private readonly IPhonesRepository _phonesRepository;
        private readonly IReportTool _reportTool;
        private readonly IRoutesRepository _routesRepository;
        private readonly ISmsTemplatesRepository _smsTemplatesRepository;
        private readonly GravitasDbContext _context;
        private readonly INodeRepository _nodeRepository;

        public ConnectManager(IMessageClient messageClient,
            ISmsTemplatesRepository smsTemplatesRepository,
            IExternalDataRepository externalDataRepository,
            IOpDataRepository opDataRepository,
            IReportTool reportTool,
            IRoutesRepository routesRepository,
            IPhonesRepository phonesRepository, 
            GravitasDbContext context, 
            INodeRepository nodeRepository)
        {
            _messageClient = messageClient;
            _smsTemplatesRepository = smsTemplatesRepository;
            _externalDataRepository = externalDataRepository;
            _opDataRepository = opDataRepository;
            _reportTool = reportTool;
            _routesRepository = routesRepository;
            _phonesRepository = phonesRepository;
            _context = context;
            _nodeRepository = nodeRepository;
        }

        public void SendSms(SmsTemplate smsId, int? ticketId, string phoneNumber = null, Dictionary<string, object> parameters = null, string cardId = null)
        {
            var message = GenerateSmsMessage(smsId, ticketId, phoneNumber, parameters);
            var (id, status) = _messageClient.SendSms(message);
            _context.Messages.Add(new Message
            {
                CardId = cardId,
                TypeId = MessageType.Sms,
                Text = message.Message,
                Created = DateTime.Now,
                Receiver = phoneNumber,
                DeliveryId = id,
                Status = status,
                RetryCount = 1
            });
            _context.SaveChanges();
        }

        public void SendEmail(EmailTemplate emailId, string emailAddress = null, object data = null, string attachmentPath = null, string cardId = null)
        {
            var message = GenerateEmailMessage(emailId, emailAddress, data, attachmentPath);
            _messageClient.SendEmail(message);
            _context.Messages.Add(new Message
            {
                CardId = cardId,
                TypeId = MessageType.Email,
                Text = message.Body,
                Created = DateTime.Now,
                Receiver = emailAddress,
                AttachmentPath = attachmentPath,
                DeliveryId = 0,
                Status = MessageStatus.Accepted,
                RetryCount = 1
            });
        }
        
        public void SendTelegramPost(TelegramGroupType groupType, string message)
        {
            var telegramBot = _context.TelegramBots.First(x => x.Type == groupType);
            _messageClient.SendTelegramPost(new TelegramMessage
            {
                BotToken = telegramBot.Token,
                ChatId = telegramBot.ChatId,
                Message = message
            });
            _context.Messages.Add(new Message
            {
                CardId = null,
                TypeId = MessageType.Telegram,
                Text = message,
                Created = DateTime.Now,
                Receiver = telegramBot.Name,
                AttachmentPath = null,
                DeliveryId = 0,
                Status = MessageStatus.Accepted,
                RetryCount = 1
            });
            _context.SaveChanges();
        }

        private MailMessage GenerateEmailMessage(EmailTemplate emailId, string emailAddress, object data, string attachmentPath)
        {
            var message = new MailMessage(
                new MailAddress(GlobalConfigurationManager.RootEmail, "Булат. Автоматичні повідомлення"),
                new MailAddress(emailAddress))
            {
                IsBodyHtml = true
            };

            string templatePath;
            var template = "";
            switch (emailId)
            {
                case EmailTemplate.CollisionApproval:
                    templatePath = "~/Content/EmailTemplate/CollisionEmailTemplate.html";
                    message.Subject = "Погодження лабораторних показників.";
                    template = File.ReadAllText(HttpContext.Current.Server.MapPath(templatePath), Encoding.GetEncoding(1251));
                    break;
                case EmailTemplate.CentralLaboratoryCollisionApproval:
                    templatePath = "~/Content/EmailTemplate/CentralLabolatoryEmailTemplate.html";
                    message.Subject = "Погодження лабораторних показників.";
                    template = File.ReadAllText(HttpContext.Current.Server.MapPath(templatePath), Encoding.GetEncoding(1251));
                    break;
                case EmailTemplate.DeviceInvalidInformation:
                    templatePath = "Content/EmailTemplate/DeviceInvalidInformation.html";
                    message.Subject = "IP адреси пристроїв до яких зник доступ.";
                    template = File.ReadAllText(templatePath, Encoding.GetEncoding(1251));
                    break;
            }

            message.Body = _reportTool.ReplaceTokens(template, data);

            if (!string.IsNullOrEmpty(attachmentPath)) message.Attachments.Add(new Attachment(attachmentPath));
            return message;
        }

        private void PutAdditionalProperties(ref SmsMessageData data, Dictionary<string, object> parameters)
        {
            if (parameters == null) return;

            var t = data.GetType();
            foreach (var propertyName in parameters.Keys)
            {
                var propertyInfo = t.GetProperty(propertyName);
                if (propertyInfo == null) throw new Exception($"Property not found {propertyName}");

                propertyInfo.SetValue(data, parameters[propertyName], null);
            }
        }

        private SmsMessage GenerateSmsMessage(SmsTemplate smsId, int? ticketId, string phoneNumber, Dictionary<string, object> parameters)
        {
            var data = new SmsMessageData();
            var sms = new SmsMessage();
            if (!ticketId.HasValue) return sms;

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId);
            if (singleWindowOpData == null) return null;

            data.NodeNumber = _opDataRepository.GetLastOpData(ticketId).Node.Name?.Last().ToString();
            data.DispatcherPhoneNumber = _phonesRepository.GetPhone(Phone.Dispatcher);
            data.TransportNo = singleWindowOpData.HiredTransportNumber;
            data.TrailerNo = singleWindowOpData.HiredTrailerNumber;
            data.ReceiverName = _externalDataRepository
                                    .GetOrganisationDetail(singleWindowOpData.OrganizationId.Value)
                                    ?.ShortName ?? singleWindowOpData.CustomPartnerName;
            PutAdditionalProperties(ref data, parameters);

            switch (smsId)
            {
                case SmsTemplate.DestinationPointApprovalSms:
                    var lastOpData = _opDataRepository.GetOpDataList(ticketId.Value)
                        .OrderBy(x => x.CheckOutDateTime)
                        .LastOrDefault();
                    if (!lastOpData.NodeId.HasValue) break;
                    long? destPoint = null;
                    _logger.Info($"Connect manager: DestinationPointApprovalSms, last node: {lastOpData.NodeId.Value}");
                    switch (lastOpData.NodeId.Value)
                    {
                        case (int) NodeIdValue.LoadPointGuideEl23:
                        case (int) NodeIdValue.LoadPointGuideEl45:
                        case (int) NodeIdValue.LoadPointGuideTareWarehouse:
                        case (int) NodeIdValue.LoadPointGuideShrotHuskOil:
                        case (int) NodeIdValue.MixedFeedGuide:
                            destPoint = _opDataRepository.GetLastProcessed<LoadGuideOpData>(ticketId)?.LoadPointNodeId;
                            break;
                        case (int) NodeIdValue.UnloadPointGuideEl23:
                        case (int) NodeIdValue.UnloadPointGuideEl45:
                            destPoint = _opDataRepository.GetLastProcessed<UnloadGuideOpData>(ticketId)?.UnloadPointNodeId;
                            break;
                        case (int) NodeIdValue.UnloadPointGuideLowerArea:
                            data.DestinationPoint = $"Авторозвантажувач #{_nodeRepository.GetNodeContext((int) NodeIdValue.UnloadPointGuideLowerArea)?.OpProcessData}";
                            break;
                        case (int) NodeIdValue.LoadPointGuideLowerArea:
                            data.DestinationPoint = $"Склад #{_nodeRepository.GetNodeContext((int) NodeIdValue.LoadPointGuideLowerArea)?.OpProcessData}";
                            break;
                    }

                    if (string.IsNullOrEmpty(data.DestinationPoint))
                    {
                        data.DestinationPoint = destPoint != null
                            ? _context.Nodes.FirstOrDefault(x => x.Id == destPoint.Value)?.Name
                            : string.Empty;
                    }
                    break;
                case SmsTemplate.RouteChangeSms:
                    var ticket = _context.Tickets.AsNoTracking().First(x => x.Id == ticketId.Value);
                    if (!ticket.RouteTemplateId.HasValue) break;

                    var route = _routesRepository
                        .GetRoute((int) (ticket.SecondaryRouteTemplateId ?? ticket.RouteTemplateId))
                        .RouteConfig
                        .DeserializeRoute()
                        .GroupDictionary
                        .Select(groupItem => groupItem
                            .Value
                            .NodeList
                            .Select(item => item.Id)
                            .ToList())
                        .ToList();
                    var nodes = route[ticket.SecondaryRouteTemplateId.HasValue ? ticket.SecondaryRouteItemIndex : ticket.RouteItemIndex];

                    foreach (var nodeId in nodes) data.DestinationPoint += $"{_context.Nodes.First(x => x.Id == nodeId).Name}, ";
                    break;
            }

            if (!singleWindowOpData.IsThirdPartyCarrier)
            {
                data.TransportNo =
                    singleWindowOpData.TransportId.HasValue 
                        ? _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId.Value)?.RegistrationNo
                        : string.Empty;
                data.TrailerNo =
                    singleWindowOpData.TrailerId.HasValue
                        ? _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId.Value)?.RegistrationNo
                        : string.Empty;
            }

            data.ProductName = _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId.Value)?.ShortName ??
                               string.Empty;

            sms = new SmsMessage
            {
                PhoneNumber = phoneNumber ?? (smsId == SmsTemplate.InvalidPerimeterGuardianSms
                                  ? _phonesRepository.GetPhone(Phone.Security)
                                  : singleWindowOpData.ContactPhoneNo),
                Message = _reportTool.ReplaceTokens(_smsTemplatesRepository.GetSmsTemplate(smsId), data)
            };

            return sms;
        }
    }
}
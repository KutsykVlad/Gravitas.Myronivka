using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient.Messages;
using Gravitas.Infrastructure.Platform.Manager.Connect;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Model.DomainModel.OpData.DAO;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager.Report;
using NLog;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Infrastructure.Platform.Manager
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


        public bool SendSms(long smsId, long? ticketId, string phoneNumber = null, Dictionary<string, object> parameters = null)
        {
            return _messageClient.SendSms(GenerateSmsMessage(smsId, ticketId, phoneNumber, parameters));
        }

        public bool SendEmail(long emailId, string emailAddress = null, object data = null, string attachmentPath = null)
        {
            return _messageClient.SendEmail(GenerateEmailMessage(emailId, emailAddress, data, attachmentPath));
        }

        private MailMessage GenerateEmailMessage(long emailId, string emailAddress, object data, string attachmentPath)
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
                case Dom.Email.Template.CollisionApproval:
                    templatePath = "~/Content/EmailTemplate/CollisionEmailTemplate.html";
                    message.Subject = "Погодження лабораторних показників.";
                    template = File.ReadAllText(HttpContext.Current.Server.MapPath(templatePath), Encoding.GetEncoding(1251));
                    break;
                case Dom.Email.Template.CentralLaboratoryCollisionApproval:
                    templatePath = "~/Content/EmailTemplate/CentralLabolatoryEmailTemplate.html";
                    message.Subject = "Погодження лабораторних показників.";
                    template = File.ReadAllText(HttpContext.Current.Server.MapPath(templatePath), Encoding.GetEncoding(1251));
                    break;
                case Dom.Email.Template.DeviceInvalidInformation:
                    templatePath = "Content/EmailTemplate/DeviceInvalidInformation.html";
                    message.Subject = "IP адреси пристроїв до яких зник доступ.";
                    template = File.ReadAllText(templatePath, Encoding.GetEncoding(1251));
                    break;
            }

            message.Body = _reportTool.ReplaceTokens(template, data);

            if (!string.IsNullOrEmpty(attachmentPath)) message.Attachments.Add(new Attachment(attachmentPath));
            return message;
        }


        //Set additional properties
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


        private SmsMessage GenerateSmsMessage(long smsId, long? ticketId, string phoneNumber, Dictionary<string, object> parameters)
        {
            var data = new SmsMessageData();
            var sms = new SmsMessage();
            if (!ticketId.HasValue) return sms;

            var singleWindowOpData = _opDataRepository.GetLastProcessed<SingleWindowOpData>(ticketId);
            if (singleWindowOpData == null) return null;

            data.NodeNumber = _opDataRepository.GetLastOpData(ticketId).Node.Name?.Last().ToString();
            data.DispatcherPhoneNumber = _phonesRepository.GetPhone(Dom.Phone.Dispatcher);
            data.TransportNo = singleWindowOpData.HiredTransportNumber;
            data.TrailerNo = singleWindowOpData.HiredTrailerNumber;
            data.ReceiverName = _externalDataRepository
                                    .GetOrganisationDetail(singleWindowOpData.OrganizationId)
                                    ?.ShortName ?? singleWindowOpData.CustomPartnerName;
            PutAdditionalProperties(ref data, parameters);

            switch (smsId)
            {
                case Dom.Sms.Template.DestinationPointApprovalSms:
                    var lastOpData = _opDataRepository.GetOpDataList(ticketId.Value)
                        .OrderBy(x => x.CheckOutDateTime)
                        .LastOrDefault();
                    if (!lastOpData.NodeId.HasValue) break;
                    long? destPoint = null;
                    _logger.Info($"Connect manager: DestinationPointApprovalSms, last node: {lastOpData.NodeId.Value}");
                    switch (lastOpData.NodeId.Value)
                    {
                        case (long) NodeIdValue.LoadPointGuideEl23:
                        case (long) NodeIdValue.LoadPointGuideEl45:
                        case (long) NodeIdValue.LoadPointGuideTareWarehouse:
                        case (long) NodeIdValue.LoadPointGuideShrotHuskOil:
                            destPoint = _opDataRepository.GetLastProcessed<LoadGuideOpData>(ticketId)?.LoadPointNodeId;
                            break;
                        case (long) NodeIdValue.UnloadPointGuideEl23:
                        case (long) NodeIdValue.UnloadPointGuideEl45:
                            destPoint = _opDataRepository.GetLastProcessed<UnloadGuideOpData>(ticketId)?.UnloadPointNodeId;
                            break;
                        case (long) NodeIdValue.MixedFeedGuide:
                            destPoint = _opDataRepository.GetLastProcessed<MixedFeedGuideOpData>(ticketId)?.LoadPointNodeId;
                            break;
                        case (long) NodeIdValue.UnloadPointGuideLowerArea:
                            data.DestinationPoint = $"Авторозвантажувач #{_nodeRepository.GetNodeContext((long) NodeIdValue.UnloadPointGuideLowerArea)?.OpProcessData}";
                            break;
                        case (long) NodeIdValue.LoadPointGuideLowerArea:
                            data.DestinationPoint = $"Склад #{_nodeRepository.GetNodeContext((long) NodeIdValue.LoadPointGuideLowerArea)?.OpProcessData}";
                            break;
                    }

                    if (string.IsNullOrEmpty(data.DestinationPoint))
                    {
                        data.DestinationPoint = destPoint != null
                            ? _context.Nodes.FirstOrDefault(x => x.Id == destPoint.Value)?.Name
                            : string.Empty;
                    }
                    break;
                case Dom.Sms.Template.RouteChangeSms:
                    var ticket = _context.Tickets.AsNoTracking().First(x => x.Id == ticketId.Value);
                    if (!ticket.RouteTemplateId.HasValue) break;

                    var route = _routesRepository
                        .GetRoute((long) (ticket.SecondaryRouteTemplateId ?? ticket.RouteTemplateId))
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
                case Dom.Sms.Template.OnPreRegister:
                    if (singleWindowOpData.PredictionEntranceTime.HasValue)
                    {
                        data.EntranceTime = singleWindowOpData.PredictionEntranceTime.Value.ToString(CultureInfo.InvariantCulture);

                    }
                    break;
            }

            if (!singleWindowOpData.IsThirdPartyCarrier)
            {
                data.TransportNo =
                    _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TransportId)?.RegistrationNo ??
                    string.Empty;
                data.TrailerNo =
                    _externalDataRepository.GetFixedAssetDetail(singleWindowOpData.TrailerId)?.RegistrationNo ??
                    string.Empty;
            }

            data.ProductName = _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId)?.ShortName ??
                               string.Empty;

            sms = new SmsMessage
            {
                PhoneNumber = phoneNumber ?? (smsId == Dom.Sms.Template.InvalidPerimeterGuardianSms
                                  ? _phonesRepository.GetPhone(Dom.Phone.Security)
                                  : singleWindowOpData.ContactPhoneNo),
                Message = _reportTool.ReplaceTokens(_smsTemplatesRepository.GetSmsTemplate(smsId), data)
            };

            return sms;
        }
    }
}
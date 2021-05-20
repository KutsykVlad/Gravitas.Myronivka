using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Gravitas.Infrastructure.Common.Configuration;
using Gravitas.Infrastructure.Platform.ApiClient;
using Gravitas.Model.DomainValue;
using Newtonsoft.Json;
using NLog;

namespace Gravitas.Platform.Web.Manager.CollisionManager
{
    public class CollisionWebManager : ICollisionWebManager
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public bool SendToConfirmation(int ticketId, IEnumerable<string> phoneList, IEnumerable<string> emailList, EmailTemplate? templateId = null)
        {
            _logger.Info("Entry to SendToConfirmation");
            using (var requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;
                requestMessage.Content = new StringContent(
                    JsonConvert.SerializeObject(new
                    {
                        TicketId = ticketId,
                        EmailList = emailList.Where(item => item != null).ToList(),
                        PhoneList = phoneList.Where(item => item != null).ToList(),
                        TemplateId = templateId
                    }), Encoding.UTF8,
                    "application/json");

                _logger.Info("Send request in SendToConfirmation");
                using (var response = new BaseApiClient($"{GlobalConfigurationManager.CollisionsReceiveHost}/api/collision/receive/")
                        .Send(requestMessage))
                {
                    if (response == null) return false;
                    _logger.Info("Response " + response.StatusCode);
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK: return true;
                        case HttpStatusCode.BadRequest: return false;
                        default: return false;
                    }
                }
            }
        }
    }
}
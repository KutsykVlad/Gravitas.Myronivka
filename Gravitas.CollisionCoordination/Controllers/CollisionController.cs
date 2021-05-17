using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Gravitas.CollisionCoordination.Attributes;
using Gravitas.CollisionCoordination.Helpers;
using Gravitas.CollisionCoordination.Manager.CollisionManager;
using Gravitas.CollisionCoordination.Models;
using Newtonsoft.Json.Linq;
using NLog;

namespace Gravitas.CollisionCoordination.Controllers
{
    [RoutePrefix("api/collision")]
    public class CollisionController : ApiController
    {
        private readonly ICollisionManager _collisionManager;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CollisionController(ICollisionManager collisionManager) => _collisionManager = collisionManager;

        [RequireHttps, HttpGet, Route("confirm")]
        public HttpResponseMessage Confirm(Guid opDataId, bool approved, string approvedBy)
        {
            _logger.Info($"Collision coordination: confirm received. OpData={opDataId}, Approved={approved}");
            if (!_collisionManager.IsOpDataValid(opDataId)) return SendConfirmResponse(ResponseType.Invalid);

            if (approved)
            {
                _collisionManager.Approve(opDataId, approvedBy);
            }
            else
            {
                _collisionManager.Disapprove(opDataId, approvedBy);
            }

            _collisionManager.SendCentralLabNotification(opDataId, approved);
            
            return SendConfirmResponse(approved ? ResponseType.Approved : ResponseType.Disapproved);
        }

        [HttpPost, Route("receive")]
        public IHttpActionResult Receive(JObject data)
        {
            CollisionData confirmationData;
            try
            {
                confirmationData = data.ToObject<CollisionData>();
            }
            catch (Exception e)
            {
                _logger.Error($"Collision coordination: coordination data receive error. Data={data}, Exception={e}");
                return BadRequest();
            }

            if (confirmationData.TicketId == null)
            {
                _logger.Error($"Collision coordination: bad request, no TicketId. Data={data}");
                return BadRequest();
            }
            _logger.Info($"Collision coordination: coordination data received. TicketId={confirmationData.TicketId}");

            var result = confirmationData.SendNotifications(_collisionManager);
            
            return result ? (IHttpActionResult) Ok() : InternalServerError();
        }

        private HttpResponseMessage SendConfirmResponse(ResponseType type)
        {
            var fileAddress = HttpContext.Current.Server.MapPath("~/Content/Actions/Invalid.html");
            var response = new HttpResponseMessage();
            switch (type)
            {
                case ResponseType.Approved:
                    fileAddress = HttpContext.Current.Server.MapPath("~/Content/Actions/Approved.html");
                    break;
                case ResponseType.Disapproved:
                    fileAddress = HttpContext.Current.Server.MapPath("~/Content/Actions/Disapproved.html");
                    break;
                case ResponseType.Invalid:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            response.Content = new StringContent(File.ReadAllText(fileAddress));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            _logger.Info($"Collision coordination: response send. Type={type}");
            return response;
        }
    }
}
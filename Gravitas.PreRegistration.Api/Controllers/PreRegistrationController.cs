using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Gravitas.PreRegistration.Api.Helpers;
using Gravitas.PreRegistration.Api.Helpers.Abstractions;
using Gravitas.PreRegistration.Api.Managers.Queue;
using Gravitas.PreRegistration.Api.Models;
using Microsoft.AspNet.Identity;
using NLog;

namespace Gravitas.PreRegistration.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/preregistration")]
    public class PreRegistrationApiController : ApiController
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IQueueManager _queueManager;
        private readonly IEmailHelper _emailHelper;

        public PreRegistrationApiController(IQueueManager queueManager,
            IEmailHelper emailHelper)
        {
            _queueManager = queueManager;
            _emailHelper = emailHelper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get-products")]
        public async Task<IHttpActionResult> GetProductList()
        {
            try
            {
                var data = await Task.Run(() => _queueManager.GetAvailableProducts());
                var result = ProductHelper.ConvertToDto(data);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<IHttpActionResult> AddToQueue(AddToQueueItem item)
        {
            try
            {
                var (result, message) = await Task.Run(() => _queueManager.AddToQueue(item, User.Identity.GetUserName()));

                return result ? (IHttpActionResult)Ok(message) : BadRequest(message);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("sendEmail")]
        public IHttpActionResult SendEmail(CompanyInfo info)
        {
            try
            {
                _emailHelper.SendEmail(info);
                _queueManager.UpdateDetails(User.Identity.GetUserName(), info);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IHttpActionResult> DeleteFromQueue(string phoneNo)
        {
            try
            {
                var (result, message) = await Task.Run(() => _queueManager.DeleteFromQueue(phoneNo, User.Identity.GetUserName()));
                return result ? (IHttpActionResult)Ok(message) : BadRequest(message);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("get-details")]
        public async Task<IHttpActionResult> GetDetails()
        {
            try
            {
                var data = await Task.Run(() => _queueManager.GetDetails(User.Identity.GetUserName()));
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("get-trucks")]
        public async Task<IHttpActionResult> GetTrucks(int skip, int pageSize = 20)
        {
            try
            {
                var result = await Task.Run(() => _queueManager.GetTrucks(User.Identity.GetUserName())
                    .OrderBy(e => e.RegisterDateTime)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList());
                var totalCount = await Task.Run(() => _queueManager.GetTrucks(User.Identity.GetUserName()).Count());

                return Ok(new { result, totalCount });
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return InternalServerError();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get-bar-chart-info")]
        public async Task<IHttpActionResult> GetBarChartInfo()
        {
            try
            {
                var data = await Task.Run(() => _queueManager.GetAvailableProducts());
                var result = ProductHelper.GetBarChartInfos(data);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return InternalServerError();
            }
        }
    }
}
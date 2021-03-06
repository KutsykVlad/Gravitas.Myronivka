using System;
using System.Threading.Tasks;
using System.Web.Http;
using Gravitas.DAL.Repository.Queue;
using Gravitas.Model.DomainValue;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.Manager.Admin;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.Admin.QueuePriority;

namespace Gravitas.Platform.Web.Controllers.Api
{
    public class AdminApiController : ApiController
    {
        private readonly IAdminWebManager _adminWebManager;
        private readonly IQueueSettingsRepository _queueSettingsRepository;

        public AdminApiController(IAdminWebManager adminWebManager,
            IQueueSettingsRepository queueSettingsRepository)
        {
            _adminWebManager = adminWebManager;
            _queueSettingsRepository = queueSettingsRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetQueueCategories()
        {
            try
            {
                var data = await Task.Run(() => _queueSettingsRepository.GetCategories());

                return Ok(data);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetQueuePriorities()
        {
            try
            {
                var data = await Task.Run(() => _queueSettingsRepository.GetPriorities());

                return Ok(data);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetQueuePatternItems()
        {
            try
            {
                var data = await Task.Run(() => _adminWebManager.GetQueueTable());

                return Ok(data);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> PostQueuePatternItems([FromBody]QueuePatternVm result)
        {
            try
            {
                var data = await Task.Run(() =>
                {
                    foreach (var item in result.Items)
                    {
                        _adminWebManager.UpdateQueuePatternItem(item);
                    }
                    return _adminWebManager.GetQueueTable();
                });

                return Ok(data);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> DeleteQueuePatternItem(int id)
        {
            try
            {
                var data = await Task.Run(() =>
                {
                    _adminWebManager.DeleteQueuePatternItem(new QueuePatternItemVm() {QueuePatternItemId = id});
                    return _adminWebManager.GetQueueTable();
                });

                return Ok(data);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> AddQueuePatternItem()
        {
            try
            {
                var data = await Task.Run(() =>
                {
                    _adminWebManager.UpdateQueuePatternItem(new QueuePatternItemVm
                    {
                        Count = 0,
                        ReceiverId = null,
                        Priority = QueuePriority.Medium,
                        Category = QueueCategory.Partners
                    });
                    return _adminWebManager.GetQueueTable();
                });

                return Ok(data);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

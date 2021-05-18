using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Gravitas.DAL;
using Gravitas.DAL.DbContext;
using Gravitas.Infrastructure.Platform.Manager.Routes;
using Gravitas.Model;
using Gravitas.Platform.Web.Manager;
using Gravitas.Infrastructure.Platform.Manager.Queue.Infrastructure;
using Gravitas.Model.DomainModel.Queue.DAO;
using Gravitas.Platform.Web.ViewModel.Queue;
using Dom = Gravitas.Model.DomainValue.Dom;

namespace Gravitas.Platform.Web.Controllers.Api
{
    public class QueueApiController : ApiController
    {
        private readonly IExternalDataWebManager _externalDataManager;
        private readonly IRoutesInfrastructure _routesInfrastructure;
        private readonly INodeRepository _nodeRepository;
        private readonly IQueueInfrastructure _queueInfrastructure;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly IOpDataRepository _opDataRepository;
        private readonly GravitasDbContext _context;

        public QueueApiController(IExternalDataWebManager externalDataManager, 
            INodeRepository nodeRepository,
            IQueueInfrastructure queueInfrastructure,
            IRoutesInfrastructure routesInfrastructure, 
            IExternalDataRepository externalDataRepository, 
            IOpDataRepository opDataRepository, 
            GravitasDbContext context)
        {
            _externalDataManager = externalDataManager;
            _nodeRepository = nodeRepository;
            _queueInfrastructure = queueInfrastructure;
            _routesInfrastructure = routesInfrastructure;
            _externalDataRepository = externalDataRepository;
            _opDataRepository = opDataRepository;
            _context = context;
        }

        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetExternalQueueList(string productId)
        {
            try
            {
                var data = await Task.Run(() =>
                {
                    var list = _opDataRepository.GetQuery<QueueRegister, long>().ToList();
                    var vm = new FilteredExternalQueueVm {Items = new List<FilteredExternalQueueItemVm>()};

                    foreach (var item in list)
                    {
                        var singleWindowOpData =_context.SingleWindowOpDatas.FirstOrDefault(e => e.TicketContainerId == item.TicketContainerId && (productId == null || e.ProductId == productId) &&
                                                                                                 (e.DocHumidityValue != null || e.DocImpurityValue != null));
                        if (singleWindowOpData != null)
                        {
                            vm.Items.Add(new FilteredExternalQueueItemVm
                            {
                                TicketContainerId = item.TicketContainerId,
                                Nomenclature =
                                    _externalDataRepository.GetProductDetail(singleWindowOpData.ProductId)?.ShortName,
                                DocImpurityValue = singleWindowOpData.DocImpurityValue == null ? singleWindowOpData.DocImpurityValue : Math.Round(singleWindowOpData.DocImpurityValue.Value, 2),
                                DocHumidityValue = singleWindowOpData.DocHumidityValue == null ? singleWindowOpData.DocHumidityValue : Math.Round(singleWindowOpData.DocHumidityValue.Value, 2),
                                IsAllowedToEnterTerritory = item.IsAllowedToEnterTerritory
                            });
                        }
                    }

                    vm.ProductId = productId;
                    return vm;
                });
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetPartnerList(int page, string filter)
        {
            try
            {
                const int numberOfObjectsPerPage = 5;
                var items = await Task.Run(() => _externalDataManager.GetPartnerItemsVm(page, numberOfObjectsPerPage, filter));

                return Ok(items);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetFilteredProductList()
        {
            try
            {
                var items = await Task.Run(() =>
                {
                    return _routesInfrastructure.GetRouteTemplates(Dom.Route.Type.SingleWindow)
                                                   .Select(item => new KeyValuePair<string, long>(item.Name, item.Id)).ToList();
                });
                return Ok(items);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> DeleteQueueRecord(string phoneNo)
        {
            try
            {
                await Task.Run(() =>
                {
                    var register = _context.QueueRegisters.FirstOrDefault(item => item.PhoneNumber!= null && item.PhoneNumber.Contains(phoneNo));
                    _nodeRepository.Delete<QueueRegister, long>(register);
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAvailableDateTime(long routeId)
        {
            try
            {
                var date = await Task.Run(() => _queueInfrastructure.GetPredictionEntranceTime(routeId));
                return Ok(date);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        public class PreRegisterItem
        {
            public string TruckNo { get; set; }
            public string TrailerNo { get; set; }
            public string PhoneNo { get; set; }
            public long RouteId { get; set; }
        }

        [HttpPost]
        [HttpOptions]
        public async Task<IHttpActionResult> OrderQueue([FromBody]PreRegisterItem item)
        {
            try
            {
                if (Request.Method == HttpMethod.Options)
                {
                    return Ok();
                }
                await Task.Run(() =>
                {
                    var queueRegister = new QueueRegister
                    {
                        PhoneNumber = item.PhoneNo,
                        SMSTimeAllowed = _queueInfrastructure.GetPredictionEntranceTime(item.RouteId),
                        TruckPlate = item.TruckNo,
                        TrailerPlate = item.TrailerNo,
                        RouteTemplateId = item.RouteId
                    };
                    
                    _nodeRepository.Add<QueueRegister, long>(queueRegister);
                });

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}

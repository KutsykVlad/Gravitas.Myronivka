using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Gravitas.DAL;
using Gravitas.Model;
using Gravitas.Model.DomainModel.Node.DAO;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DAO.ExternalData;

namespace Gravitas.Platform.Web.Controllers.Api
{
    public class SingleWindowApiController : ApiController
    {
        private readonly IExternalDataWebManager _externalDataManager;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly GravitasDbContext _context;

        public SingleWindowApiController(IOpRoutineWebManager opRoutineWebManager,
            IExternalDataWebManager externalDataManager,
            INodeRepository nodeRepository,
            IExternalDataRepository externalDataRepository, 
            GravitasDbContext context)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _externalDataManager = externalDataManager;
            _nodeRepository = nodeRepository;
            _externalDataRepository = externalDataRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetNodeName(long? nodeId)
        {
            if (nodeId == null || nodeId == 0) return BadRequest("There is no nodeId provided");

            var routineData = await
                Task.Run(() => _nodeRepository.GetEntity<Node, long>(nodeId.Value).Name);

            return Ok(routineData);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetBudgetName(string budgetId)
        {
            if (string.IsNullOrEmpty(budgetId)) return BadRequest("There is no budget provided");

            var routineData = await
                Task.Run(() => _nodeRepository.GetEntity<ExternalData.Budget, string>(budgetId).Name);

            return Ok(routineData);
        }

        #region Products

        [HttpGet]
        public async Task<IHttpActionResult> FilteredProductItems(int page, string filter)
        {
            try
            {
                const int numberOfObjectsPerPage = 5;
                var productItems = await Task.Run(() => _externalDataManager.GetProductItemsVm(page, numberOfObjectsPerPage, filter));

                return Ok(productItems);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region getData

        [HttpGet]
        public async Task<IHttpActionResult> GetRoutineData(long? nodeId)
        {
            if (nodeId == null) return BadRequest("There is no nodeId provided");

            var routineData = await
                Task.Run(() => _opRoutineWebManager.SingleWindow_EditTicketForm_GetData(nodeId.Value));

            return Ok(routineData);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetBudgetData(string parentId = null)
        {
            try
            {
                BudgetItemsVm data;
                if (parentId == null)
                {
                    data = await Task.Run(() => _externalDataManager.GetBudgetChildren());
                    return Ok(data);
                }

                data = await Task.Run(() => _externalDataManager.GetBudgetChildren(parentId));
                return Ok(data);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> GetProductData(string parentId)
        {
            try
            {
                var data = await Task.Run(() => _externalDataManager.GetProductChildren(parentId));
                return Ok(data);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> GetProductName(string id)
        {
            try
            {
                var data = await Task.Run(() => _externalDataRepository.GetProductDetail(id)?.ShortName ?? string.Empty);
                return Ok(data);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetPartnerData(string parentId)
        {
            try
            {
                PartnerItemsVm data;
                if (parentId == null)
                {
                    data = await Task.Run(() => _externalDataManager.GetPartnerChildren());
                    return Ok(data);
                }

                data = await Task.Run(() => _externalDataManager.GetPartnerChildren(parentId));
                return Ok(data);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetEmployeeData(string parentId)
        {
            try
            {
                EmployeeItemsVm data;
                if (parentId == null)
                {
                    data = await Task.Run(() => _externalDataManager.GetEmployeeChildren());
                    return Ok(data);
                }

                data = await Task.Run(() => _externalDataManager.GetEmployeeChildren(parentId));
                return Ok(data);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        #endregion

        #region Stock

        [HttpGet]
        public async Task<IHttpActionResult> GetStockItems(string id)
        {
            try
            {
                var stockItems = _context.Stocks.Where(item => item.CustomerId == id).ToList();
                return Ok(stockItems);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetStockItem(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return BadRequest("There is no stock provided");
                var stockItem = await Task.Run(() => _externalDataManager.GetStockItemVm(id));

                return Ok(stockItem);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Partners 

        [HttpGet]
        public async Task<IHttpActionResult> PartnerItem(string carrierCode)
        {
            try
            {
                if (string.IsNullOrEmpty(carrierCode)) return BadRequest("There is no partner provided");
                var partner = await Task.Run(() => _externalDataManager.GetPartnerItem(carrierCode));
                return Ok(partner);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> PartnerItems()
        {
            try
            {
                var partnerItems = await Task.Run(() => _externalDataManager.GetPartnerItemsVm());
                return Ok(partnerItems);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        public async Task<IHttpActionResult> FilteredPartnerItemsPage(int page, string filter)
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

        #endregion

        #region Asset

        [HttpGet]
        public async Task<IHttpActionResult> FilteredAssetItems(int page, string filter)
        {
            try
            {
                const int numberOfObjectsPerPage = 5;
                var fixedAssetItems = await Task.Run(() => _externalDataManager.GetFixedAssetItemsVm(page, numberOfObjectsPerPage, filter));

                return Ok(fixedAssetItems);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> AssetItem(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) return BadRequest("There is no asset provided");
                var assetItem = await Task.Run(() => _externalDataManager.GetFixedAssetItem(id));
                return Ok(assetItem);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        #endregion

        #region Trailer

        [HttpGet]
        public async Task<IHttpActionResult> FilteredTrailerItems(int page, string filter)
        {
            try
            {
                const int numberOfObjectsPerPage = 5;
                var fixedTrailerItems = await Task.Run(() => _externalDataManager.GetFixedTrailerItemsVm(page, numberOfObjectsPerPage, filter));

                return Ok(fixedTrailerItems);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> TrailerItem(string id)
        {
            try
            {
                var trailerItem = await Task.Run(() => _externalDataManager.GetFixedTrailerItemVm(id));
                return Ok(trailerItem);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Employee

        [HttpGet]
        public async Task<IHttpActionResult> FilteredDriverItems(int page, string filter)
        {
            try
            {
                const int numberOfObjectsPerPage = 5;
                var driverItems = await Task.Run(() => _externalDataManager.GetEmployeeItemsVm(page, numberOfObjectsPerPage, filter));

                return Ok(driverItems);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        public async Task<IHttpActionResult> EmployeeItem(string id)
        {
            try
            {
                var driverItems = await Task.Run(() => _externalDataManager.GetEmployeeItemVm(id));
                return Ok(driverItems);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion

        #region Budget

        [HttpGet]
        public async Task<IHttpActionResult> Budget1Items(int page)
        {
            try
            {
                // no paging yet
                var budgetItems = await Task.Run(() => _externalDataManager.GetBudgetItemsVm());
                return Ok(budgetItems);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

	    /// <summary>
	    ///     Budget1 Items
	    /// </summary>
	    /// <returns></returns>
	    [HttpGet]
        public async Task<IHttpActionResult> Budget2Items(int page)
        {
            try
            {
                // no paging yet
                var budgetItems = await Task.Run(() => _externalDataManager.GetBudgetItemsVm());
                return Ok(budgetItems);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion
    }
}
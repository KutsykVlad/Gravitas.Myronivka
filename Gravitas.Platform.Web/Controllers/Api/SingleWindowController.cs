using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Gravitas.DAL.DbContext;
using Gravitas.DAL.Repository.ExternalData;
using Gravitas.DAL.Repository.Node;
using Gravitas.Platform.Web.Manager;
using Gravitas.Platform.Web.Manager.OpRoutine;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.ExternalData.Budget.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.Employee.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.Partner.List;

namespace Gravitas.Platform.Web.Controllers.Api
{
    public class SingleWindowApiController : ApiController
    {
        private readonly IExternalDataWebManager _externalDataManager;
        private readonly IExternalDataRepository _externalDataRepository;
        private readonly IOpRoutineWebManager _opRoutineWebManager;
        private readonly GravitasDbContext _context;

        public SingleWindowApiController(IOpRoutineWebManager opRoutineWebManager,
            IExternalDataWebManager externalDataManager,
            IExternalDataRepository externalDataRepository, 
            GravitasDbContext context)
        {
            _opRoutineWebManager = opRoutineWebManager;
            _externalDataManager = externalDataManager;
            _externalDataRepository = externalDataRepository;
            _context = context;
        }

        [HttpGet]
        public IHttpActionResult GetNodeName(int? nodeId)
        {
            if (nodeId == null || nodeId == 0) return BadRequest("There is no nodeId provided");

            var routineData = _context.Nodes.First(x => x.Id == nodeId.Value).Name;

            return Ok(routineData);
        }

        [HttpGet]
        public IHttpActionResult GetBudgetName(Guid budgetId)
        {
            var routineData = _context.Budgets.First(x => x.Id == budgetId).Name;

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
        public async Task<IHttpActionResult> GetRoutineData(int? nodeId)
        {
            if (nodeId == null) return BadRequest("There is no nodeId provided");

            var routineData = await
                Task.Run(() => _opRoutineWebManager.SingleWindow_EditTicketForm_GetData(nodeId.Value));

            return Ok(routineData);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetBudgetData(Guid? parentId = null)
        {
            try
            {
                var data = await Task.Run(() => _externalDataManager.GetBudgetChildren(parentId));
                return Ok(data);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> GetProductData(Guid? parentId)
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
        public async Task<IHttpActionResult> GetProductName(Guid id)
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
        public async Task<IHttpActionResult> GetPartnerData(Guid? parentId)
        {
            try
            {
                var data = await Task.Run(() => _externalDataManager.GetPartnerChildren(parentId));
                return Ok(data);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetEmployeeData(Guid? parentId)
        {
            try
            {
                var data = await Task.Run(() => _externalDataManager.GetEmployeeChildren(parentId));
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
        public IHttpActionResult GetStockItems(Guid id)
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
        public async Task<IHttpActionResult> GetStockItem(Guid id)
        {
            try
            {
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
        public async Task<IHttpActionResult> AssetItem(Guid id)
        {
            try
            {
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
        public async Task<IHttpActionResult> TrailerItem(Guid id)
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
        public async Task<IHttpActionResult> EmployeeItem(Guid id)
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
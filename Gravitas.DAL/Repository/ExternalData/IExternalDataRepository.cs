using Gravitas.DAL.DbContext;
using Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DTO.Detail;

namespace Gravitas.DAL {

	public interface IExternalDataRepository : IBaseRepository<GravitasDbContext> {

		ExternalData.AcceptancePointDetail GetAcceptancePointDetail(string id);
		ExternalData.BudgetDetail GetBudgetDetail(string id);
		ExternalData.ContractDetail GetContractDetail(string id);
		ExternalData.CropDetail GetCropDetail(string id);
		ExternalData.DeliveryBillStatusDetail GetDeliveryBillStatusDetail(string id);
		ExternalData.DeliveryBillTypeDetail GetDeliveryBillTypeDetail(string id);
		ExternalData.EmployeeDetail GetEmployeeDetail(string id);
		ExternalData.ExternalEmployeeDetail GetExternalEmployeeDetail(string id);
		ExternalData.FixedAssetDetail GetFixedAssetDetail(string id);
		ExternalData.LabImpurityСlassifierDetail GetLabImpurityСlassifierDetail(string id);
		ExternalData.LabHumidityСlassifierDetail GetLabHumidityСlassifierDetail(string id);
		ExternalData.LabInfectionedСlassifierDetail GetLabInfectionedСlassifierDetail(string id);
		ExternalData.MeasureUnitDetail GetMeasureUnitDetail(string id);
		ExternalData.OriginTypeDetail GetOriginTypeDetail(string id);
		ExternalData.OrganisationDetail GetOrganisationDetail(string id);
		ExternalData.PartnerDetail GetPartnerDetail(string id);
		ExternalData.ProductDetail GetProductDetail(string id);
		ExternalData.ReasonForRefundDetail GetReasonForRefundDetail(string id);
		ExternalData.RouteDetail GetRouteDetail(string id);
		ExternalData.StockDetail GetStockDetail(string id);
		ExternalData.SubdivisionDetail GetSubdivisionDetail(string id);
		ExternalData.SupplyTransportTypeDetail GetSupplyTransportTypeDetail(string id);
		ExternalData.SupplyTypeDetail GetSupplyTypeDetail(string id);
		ExternalData.YearOfHarvestDetail GetYearOfHarvestDetail(string id);


	    ExternalData.ProductItems GetProductChildItems(string id);
	    ExternalData.ProductItems GetProductItems();
		ExternalData.BudgetItems GetBudgetItems();
		ExternalData.EmployeeItems GetEmployeeItems();
		ExternalData.OrganisationItems GetOrganisationItems();
		ExternalData.FixedTrailerItems GetFixedTrailerItems(int pageNo, int perPageNo);

	    ExternalData.PartnerItems GetFilteredPagePartnerItems(int pageNo, int perPageNo, string filter);
	    ExternalData.EmployeeItems GetFilteredPageEmployeeItems(int pageNo, int perPageNo, string filter);
	    ExternalData.StockItems GetFilteredPageStockItems(int pageNo, int perPageNo, string filter);
	    ExternalData.FixedTrailerItems GetFilteredPageFixedTrailerItems(int pageNo, int perPageNo, string filter);
	    ExternalData.FixedAssetItems GetFilteredPageFixedAssetItems(int pageNo, int perPageNo, string filter);
	    ExternalData.ProductItems GetFilteredPageProductItems(int pageNo, int perPageNo, string filter);

        ExternalData.PartnerItems GetPartnerItems();
		ExternalData.StockItems GetStockItems();
		ExternalData.SupplyTransportTypeItems GetSupplyTransportTypeItems();
		ExternalData.LabHumidityСlassifierItems GetLabHumidityСlassifierItems();
		ExternalData.LabImpurityСlassifierItems GetLabImpurityСlassifierItems();
		ExternalData.LabInfectionedСlassifierItems GetLabInfectionedСlassifierItems();
		ExternalData.LabDeviceResultTypeItems GetLabDevResultTypeItems();

	    ExternalData.BudgetItems GetBudgetChildItem(string parentId);
	    ExternalData.PartnerItems GetPartnerChildItems(string parentId);
	    ExternalData.EmployeeItems GetEmployeeChildItems(string parentId);
	}
}

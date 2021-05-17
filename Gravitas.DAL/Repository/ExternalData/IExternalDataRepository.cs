namespace Gravitas.DAL {

	public interface IExternalDataRepository : IBaseRepository<GravitasDbContext> {

		Model.Dto.ExternalData.AcceptancePointDetail GetAcceptancePointDetail(string id);
		Model.Dto.ExternalData.BudgetDetail GetBudgetDetail(string id);
		Model.Dto.ExternalData.ContractDetail GetContractDetail(string id);
		Model.Dto.ExternalData.CropDetail GetCropDetail(string id);
		Model.Dto.ExternalData.DeliveryBillStatusDetail GetDeliveryBillStatusDetail(string id);
		Model.Dto.ExternalData.DeliveryBillTypeDetail GetDeliveryBillTypeDetail(string id);
		Model.Dto.ExternalData.EmployeeDetail GetEmployeeDetail(string id);
		Model.Dto.ExternalData.ExternalEmployeeDetail GetExternalEmployeeDetail(string id);
		Model.Dto.ExternalData.FixedAssetDetail GetFixedAssetDetail(string id);
		Model.Dto.ExternalData.LabImpurityСlassifierDetail GetLabImpurityСlassifierDetail(string id);
		Model.Dto.ExternalData.LabHumidityСlassifierDetail GetLabHumidityСlassifierDetail(string id);
		Model.Dto.ExternalData.LabInfectionedСlassifierDetail GetLabInfectionedСlassifierDetail(string id);
		Model.Dto.ExternalData.MeasureUnitDetail GetMeasureUnitDetail(string id);
		Model.Dto.ExternalData.OriginTypeDetail GetOriginTypeDetail(string id);
		Model.Dto.ExternalData.OrganisationDetail GetOrganisationDetail(string id);
		Model.Dto.ExternalData.PartnerDetail GetPartnerDetail(string id);
		Model.Dto.ExternalData.ProductDetail GetProductDetail(string id);
		Model.Dto.ExternalData.ReasonForRefundDetail GetReasonForRefundDetail(string id);
		Model.Dto.ExternalData.RouteDetail GetRouteDetail(string id);
		Model.Dto.ExternalData.StockDetail GetStockDetail(string id);
		Model.Dto.ExternalData.SubdivisionDetail GetSubdivisionDetail(string id);
		Model.Dto.ExternalData.SupplyTransportTypeDetail GetSupplyTransportTypeDetail(string id);
		Model.Dto.ExternalData.SupplyTypeDetail GetSupplyTypeDetail(string id);
		Model.Dto.ExternalData.YearOfHarvestDetail GetYearOfHarvestDetail(string id);


	    Model.Dto.ExternalData.ProductItems GetProductChildItems(string id);
	    Model.Dto.ExternalData.ProductItems GetProductItems();
		Model.Dto.ExternalData.BudgetItems GetBudgetItems();
		Model.Dto.ExternalData.EmployeeItems GetEmployeeItems();
		Model.Dto.ExternalData.OrganisationItems GetOrganisationItems();
		Model.Dto.ExternalData.FixedTrailerItems GetFixedTrailerItems(int pageNo, int perPageNo);

	    Model.Dto.ExternalData.PartnerItems GetFilteredPagePartnerItems(int pageNo, int perPageNo, string filter);
	    Model.Dto.ExternalData.EmployeeItems GetFilteredPageEmployeeItems(int pageNo, int perPageNo, string filter);
	    Model.Dto.ExternalData.StockItems GetFilteredPageStockItems(int pageNo, int perPageNo, string filter);
	    Model.Dto.ExternalData.FixedTrailerItems GetFilteredPageFixedTrailerItems(int pageNo, int perPageNo, string filter);
	    Model.Dto.ExternalData.FixedAssetItems GetFilteredPageFixedAssetItems(int pageNo, int perPageNo, string filter);
	    Model.Dto.ExternalData.ProductItems GetFilteredPageProductItems(int pageNo, int perPageNo, string filter);

        Model.Dto.ExternalData.PartnerItems GetPartnerItems();
		Model.Dto.ExternalData.StockItems GetStockItems();
		Model.Dto.ExternalData.SupplyTransportTypeItems GetSupplyTransportTypeItems();
		Model.Dto.ExternalData.LabHumidityСlassifierItems GetLabHumidityСlassifierItems();
		Model.Dto.ExternalData.LabImpurityСlassifierItems GetLabImpurityСlassifierItems();
		Model.Dto.ExternalData.LabInfectionedСlassifierItems GetLabInfectionedСlassifierItems();
		Model.Dto.ExternalData.LabDeviceResultTypeItems GetLabDevResultTypeItems();

	    Model.Dto.ExternalData.BudgetItems GetBudgetChildItem(string parentId);
	    Model.Dto.ExternalData.PartnerItems GetPartnerChildItems(string parentId);
	    Model.Dto.ExternalData.EmployeeItems GetEmployeeChildItems(string parentId);
	}
}

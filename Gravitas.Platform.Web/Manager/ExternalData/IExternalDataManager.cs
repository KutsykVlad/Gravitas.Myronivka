using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager
{
    public interface IExternalDataWebManager
    {
        BudgetItemsVm GetBudgetItemsVm();

        OrganisationItemsVm GetOrganisationItemsVm();

        ProductItemsVm GetProductItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);

        EmployeeItemsVm GetEmployeeItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);
        EmployeeItemVm GetEmployeeItemVm(string id);

        FixedTrailerItemsVm GetFixedTrailerItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);
        FixedTrailerItemVm GetFixedTrailerItemVm(string id);

        FixedAssetItemsVm GetFixedAssetItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);
        FixedAssetItemVm GetFixedAssetItem(string id);

        PartnerItemsVm GetPartnerItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);
        PartnerItemVm GetPartnerItem(string carrierCode);

        StockItemsVm GetStockItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);
        StockItemVm GetStockItemVm(string id);

        SupplyTransportTypeItemsVm GetSupplyTransportTypeItemsVm();
        LabHumidityСlassifierItemsVm GetLabHumidityСlassifierItemsVm();
        LabImpurityСlassifierItemsVm GetLabImpurityСlassifierItemsVm();
        LabInfectionedСlassifierItemsVm GetLabInfectionedСlassifierItemsVm();
        LabEffectiveClassifierItemsVm GetLabEffectiveСlassifierItemsVm();

        BudgetItemsVm GetBudgetChildren(string parentId = "00000000-0000-0000-0000-000000000000");
        ProductItemsVm GetProductChildren(string parentId = "00000000-0000-0000-0000-000000000000");
        EmployeeItemsVm GetEmployeeChildren(string parentId = "00000000-0000-0000-0000-000000000000");
        PartnerItemsVm GetPartnerChildren(string parentId = "00000000-0000-0000-0000-000000000000");
    }
}
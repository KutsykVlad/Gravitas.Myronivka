using System;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.ExternalData.Budget.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.Employee.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.FixedAsset.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.LabClassifier.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.Organisation;
using Gravitas.Platform.Web.ViewModel.ExternalData.Partner.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.Product.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.Stock.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.SupplyTransportType.List;

namespace Gravitas.Platform.Web.Manager
{
    public interface IExternalDataWebManager
    {
        BudgetItemsVm GetBudgetItemsVm();

        OrganisationItemsVm GetOrganisationItemsVm();

        ProductItemsVm GetProductItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);

        EmployeeItemsVm GetEmployeeItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);
        EmployeeItemVm GetEmployeeItemVm(Guid id);

        FixedTrailerItemsVm GetFixedTrailerItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);
        FixedTrailerItemVm GetFixedTrailerItemVm(Guid id);

        FixedAssetItemsVm GetFixedAssetItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);
        FixedAssetItemVm GetFixedAssetItem(Guid id);

        PartnerItemsVm GetPartnerItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);
        PartnerItemVm GetPartnerItem(string carrierCode);

        StockItemsVm GetStockItemsVm(int pageNo = 1, int perPageNo = 10, string filter = null);
        StockItemVm GetStockItemVm(Guid id);

        SupplyTransportTypeItemsVm GetSupplyTransportTypeItemsVm();
        LabHumidityСlassifierItemsVm GetLabHumidityСlassifierItemsVm();
        LabImpurityСlassifierItemsVm GetLabImpurityСlassifierItemsVm();
        LabInfectionedСlassifierItemsVm GetLabInfectionedСlassifierItemsVm();
        LabEffectiveClassifierItemsVm GetLabEffectiveСlassifierItemsVm();

        BudgetItemsVm GetBudgetChildren(Guid? parentId);
        ProductItemsVm GetProductChildren(Guid? parentId);
        EmployeeItemsVm GetEmployeeChildren(Guid? parentId);
        PartnerItemsVm GetPartnerChildren(Guid? parentId);
    }
}
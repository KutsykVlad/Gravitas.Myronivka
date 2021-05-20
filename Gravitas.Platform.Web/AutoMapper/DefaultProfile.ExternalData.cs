using Gravitas.Model.DomainModel.ExternalData.Budget.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.Employee.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.FixedAsset.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.Organization.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.Partner.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.Stock.DTO.List;
using Gravitas.Model.DomainModel.ExternalData.SupplyTransportType.DTO.List;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.ExternalData.Budget.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.Employee.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.FixedAsset.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.Organisation;
using Gravitas.Platform.Web.ViewModel.ExternalData.Partner.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.Stock.List;
using Gravitas.Platform.Web.ViewModel.ExternalData.SupplyTransportType.List;

namespace Gravitas.Platform.Web.AutoMapper
{
    public partial class DefaultProfile
    {
        public void ConfigureExternalData()
        {
            CreateMap<BudgetItem, BudgetItemVm>().ReverseMap();
            CreateMap<BudgetItems, BudgetItemsVm>().ReverseMap();

            CreateMap<EmployeeItem, EmployeeItemVm>().ReverseMap();
            CreateMap<EmployeeItems, EmployeeItemsVm>().ReverseMap();

            CreateMap<OrganisationItem, OrganisationItemVm>().ReverseMap();
            CreateMap<OrganisationItems, EmployeeItemsVm>().ReverseMap();

            CreateMap<FixedAssetItem, FixedAssetItemVm>().ReverseMap();
            CreateMap<FixedAssetItems, FixedAssetItemsVm>().ReverseMap();

            CreateMap<PartnerItem, PartnerItemVm>().ReverseMap();
            CreateMap<PartnerItems, PartnerItemsVm>().ReverseMap();

            CreateMap<StockItem, StockItemVm>().ReverseMap();
            CreateMap<StockItems, StockItemsVm>().ReverseMap();

            CreateMap<SupplyTransportTypeItem, SupplyTransportTypeItemVm>().ReverseMap();
            CreateMap<SupplyTransportTypeItems, SupplyTransportTypeItemsVm>().ReverseMap();
        }
    }
}
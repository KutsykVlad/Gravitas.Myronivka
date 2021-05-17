using Gravitas.Model.Dto;
using Gravitas.Platform.Web.ViewModel;
using ExternalData = Gravitas.Model.DomainModel.ExternalData.AcceptancePoint.DTO.Detail.ExternalData;

namespace Gravitas.Platform.Web.AutoMapper
{
    public partial class DefaultProfile
    {
        public void ConfigureExternalData()
        {
            CreateMap<ExternalData.BudgetItem, BudgetItemVm>().ReverseMap();
            CreateMap<ExternalData.BudgetItems, BudgetItemsVm>().ReverseMap();

            CreateMap<ExternalData.EmployeeItem, EmployeeItemVm>().ReverseMap();
            CreateMap<ExternalData.EmployeeItems, EmployeeItemsVm>().ReverseMap();

            CreateMap<ExternalData.OrganisationItem, OrganisationItemVm>().ReverseMap();
            CreateMap<ExternalData.OrganisationItems, EmployeeItemsVm>().ReverseMap();

            CreateMap<ExternalData.FixedAssetItem, FixedAssetItemVm>().ReverseMap();
            CreateMap<ExternalData.FixedAssetItems, FixedAssetItemsVm>().ReverseMap();

            CreateMap<ExternalData.PartnerItem, PartnerItemVm>().ReverseMap();
            CreateMap<ExternalData.PartnerItems, PartnerItemsVm>().ReverseMap();

            CreateMap<ExternalData.StockItem, StockItemVm>().ReverseMap();
            CreateMap<ExternalData.StockItems, StockItemsVm>().ReverseMap();

            CreateMap<ExternalData.SupplyTransportTypeItem, SupplyTransportTypeItemVm>().ReverseMap();
            CreateMap<ExternalData.SupplyTransportTypeItems, SupplyTransportTypeItemsVm>().ReverseMap();
        }
    }
}
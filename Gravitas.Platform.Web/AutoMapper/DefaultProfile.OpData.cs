using Gravitas.Model.DomainModel.OpData.TDO.Detail;
using Gravitas.Model.DomainModel.OpData.TDO.Json;
using Gravitas.Platform.Web.ViewModel;
using Gravitas.Platform.Web.ViewModel.OpData.Base;
using SingleWindowVms = Gravitas.Platform.Web.ViewModel.OpRoutine.SingleWindow.SingleWindowVms;

namespace Gravitas.Platform.Web.AutoMapper
{
    public partial class DefaultProfile
    {
        public void ConfigureOpData()
        {
            CreateMap<BaseOpDataDetail, BaseOpDataDetailVm>().ReverseMap();

            CreateMap<SingleWindowOpDataDetail, SingleWindowVms.SingleWindowOpDataDetailVm>()
                .ForMember(e => e.DriverOneName, e => e.Ignore())
                .ForMember(e => e.DriverTwoName, e => e.Ignore())
                .ForMember(e => e.IsTechnologicalRoute, e => e.Ignore())
                .ForMember(e => e.OnRegisterInformEmployees, e => e.Ignore())
                .ReverseMap();
            CreateMap<ProductContentList, SingleWindowVms.ProductContentListVm>().ReverseMap();
            CreateMap<ProductContentItem, SingleWindowVms.ProductContentItemVm>().ReverseMap();

            CreateMap<AnalysisValueDescriptor, LaboratoryInVms.AnalysisValueDescriptorVm>().ReverseMap();
        }
    }
}
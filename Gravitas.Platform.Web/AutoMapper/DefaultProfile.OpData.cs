using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.AutoMapper {

	public partial class DefaultProfile {

		public void ConfigureOpData() {

			CreateMap<Model.Dto.BaseOpDataDetail, BaseOpDataDetailVm>().ReverseMap();

			CreateMap<Model.Dto.SingleWindowOpDataDetail, SingleWindowVms.SingleWindowOpDataDetailVm>()
				.ForMember(e => e.DriverOneName, e => e.Ignore())
				.ForMember(e => e.DriverTwoName, e => e.Ignore())
				.ForMember(e => e.IsTechnologicalRoute, e => e.Ignore())
				.ForMember(e => e.OnRegisterInformEmployees, e => e.Ignore())
				.ReverseMap();
			CreateMap<Model.Dto.ProductContentList, SingleWindowVms.ProductContentListVm>().ReverseMap();
			CreateMap<Model.Dto.ProductContentItem, SingleWindowVms.ProductContentItemVm>().ReverseMap();

			CreateMap<Model.Dto.AnalysisValueDescriptor, LaboratoryInVms.AnalysisValueDescriptorVm>().ReverseMap();
			//CreateMap<Model.Dto.LabFacelessOpDataComponent, LabolatoryInVms.LabFacelessOpDataComponentItemVm> ().ReverseMap();
		}
	}
}
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.AutoMapper
{
	public partial class DefaultProfile {
		public void ConfigureOrganisationUnit() {

			CreateMap<Model.Dto.OrganizationUnitDetail, OrganizationUnitDetailVm>().ReverseMap();
			
			CreateMap<Model.Dto.OrganizationUnitItem, OrganizationUnitItemVm>();
			CreateMap<Model.Dto.OrganizationUnitItems, OrganizationUnitItemsVm>();
		}
	}
}
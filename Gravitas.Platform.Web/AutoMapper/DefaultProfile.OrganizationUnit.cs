using Gravitas.Model.DomainModel.OrganizationUnit.DTO.Detail;
using Gravitas.Model.DomainModel.OrganizationUnit.DTO.List;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.AutoMapper
{
	public partial class DefaultProfile {
		public void ConfigureOrganisationUnit() {

			CreateMap<OrganizationUnitDetail, OrganizationUnitDetailVm>().ReverseMap();
			
			CreateMap<OrganizationUnitItem, OrganizationUnitItemVm>();
			CreateMap<OrganizationUnitItems, OrganizationUnitItemsVm>();
		}
	}
}
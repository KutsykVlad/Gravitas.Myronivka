using AutoMapper;
using Gravitas.DAL.Repository.OrganizationUnit;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.OrganizationUnit
{
    public class OrganizationUnitWebManager : IOrganizationUnitWebManager
    {
        private readonly IOrganizationUnitRepository _organizationUnitRepository;

        public OrganizationUnitWebManager(IOrganizationUnitRepository organizationUnitRepository)
        {
            _organizationUnitRepository = organizationUnitRepository;
        }

        public OrganizationUnitDetailVm GetOrganizationUnitDetailVm(int id)
        {
            var dto = _organizationUnitRepository.GetOrganizationUnitDetail(id);
            var vm = Mapper.Map<OrganizationUnitDetailVm>(dto);
            return vm;
        }
    }
}
using AutoMapper;
using Gravitas.DAL;
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager
{
    public class OrganizationUnitWebManager : IOrganizationUnitWebManager
    {
        private readonly IOrganizationUnitRepository _organizationUnitRepository;

        public OrganizationUnitWebManager(IOrganizationUnitRepository organizationUnitRepository)
        {
            _organizationUnitRepository = organizationUnitRepository;
        }

        public OrganizationUnitDetailVm GetOrganizationUnitDetailVm(long id)
        {
            var dto = _organizationUnitRepository.GetOrganizationUnitDetail(id);
            var vm = Mapper.Map<OrganizationUnitDetailVm>(dto);
            return vm;
        }
    }
}
using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager
{
    public interface IOrganizationUnitWebManager
    {
        OrganizationUnitDetailVm GetOrganizationUnitDetailVm(long id);
    }
}
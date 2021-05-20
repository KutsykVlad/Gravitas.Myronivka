using Gravitas.Platform.Web.ViewModel;

namespace Gravitas.Platform.Web.Manager.OrganizationUnit
{
    public interface IOrganizationUnitWebManager
    {
        OrganizationUnitDetailVm GetOrganizationUnitDetailVm(int id);
    }
}
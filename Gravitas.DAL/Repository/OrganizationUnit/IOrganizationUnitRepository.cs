using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.OrganizationUnit.DTO.Detail;

namespace Gravitas.DAL.Repository.OrganizationUnit
{
    public interface IOrganizationUnitRepository : IBaseRepository
    {
        OrganizationUnitDetail GetOrganizationUnitDetail(int id);
    }
}
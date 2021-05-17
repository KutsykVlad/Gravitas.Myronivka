using Gravitas.Model.DomainModel.OrganizationUnit.DTO.Detail;
using Gravitas.Model.Dto;

namespace Gravitas.DAL
{
    public interface IOrganizationUnitRepository : IBaseRepository<GravitasDbContext>
    {
        OrganizationUnitDetail GetOrganizationUnitDetail(long id);
    }
}
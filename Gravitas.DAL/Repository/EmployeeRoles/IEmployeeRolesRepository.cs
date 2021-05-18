using Gravitas.DAL.DbContext;
using Gravitas.Model.DomainModel.EmployeeRoles.DTO;

namespace Gravitas.DAL
{
    public interface IEmployeeRolesRepository: IBaseRepository<GravitasDbContext>
    {
        RolesDto GetEmployeeRoles(string employeeId);

        RolesDto GetRoles();

        void UpdateRole(RoleDetail roleDetail);

        void AddRole(RoleDetail roleDetail);

        void DeleteRole(long roleId);

        void ApplyEmployeeRoles(RolesDto employeeRoles, string employeeId);

    }
}

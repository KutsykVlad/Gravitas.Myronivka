using System;
using Gravitas.DAL.Repository._Base;
using Gravitas.Model.DomainModel.EmployeeRoles.DTO;

namespace Gravitas.DAL.Repository.EmployeeRoles
{
    public interface IEmployeeRolesRepository: IBaseRepository
    {
        RolesDto GetEmployeeRoles(Guid employeeId);

        RolesDto GetRoles();

        void UpdateRole(RoleDetail roleDetail);

        void AddRole(RoleDetail roleDetail);

        void DeleteRole(int roleId);

        void ApplyEmployeeRoles(RolesDto employeeRoles, Guid employeeId);

    }
}

namespace Gravitas.DAL
{
    public interface IEmployeeRolesRepository: IBaseRepository<GravitasDbContext>
    {
        Model.Dto.RolesDto GetEmployeeRoles(string employeeId);

        Model.Dto.RolesDto GetRoles();

        void UpdateRole(Model.Dto.RoleDetail roleDetail);

        void AddRole(Model.Dto.RoleDetail roleDetail);

        void DeleteRole(long roleId);

        void ApplyEmployeeRoles(Model.Dto.RolesDto employeeRoles, string employeeId);

    }
}

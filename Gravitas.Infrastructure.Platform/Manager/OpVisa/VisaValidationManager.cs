using Gravitas.DAL;

namespace Gravitas.Infrastructure.Platform.Manager
{
    public class VisaValidationManager : IVisaValidationManager
    {
        private readonly IEmployeeRolesRepository _employeeRolesRepository;

        public VisaValidationManager(IEmployeeRolesRepository employeeRolesRepository)
        {
            _employeeRolesRepository = employeeRolesRepository;
        }


        public bool ValidateEmployeeAccess(long nodeId, string employeeId)
        {
            var employeeRoles = _employeeRolesRepository.GetEmployeeRoles(employeeId);

            foreach (var role in employeeRoles.Items)
                if (role.Nodes.Contains(nodeId))
                    return true;

            return false;
        }
    }
}
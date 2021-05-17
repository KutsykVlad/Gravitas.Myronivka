namespace Gravitas.Infrastructure.Platform.Manager
{
    public interface IVisaValidationManager
    {
        bool ValidateEmployeeAccess(long nodeId, string employeeId);
    }
}